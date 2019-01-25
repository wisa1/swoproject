import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { MeasurementDevice, Community, MeasurementsService, MeasurementTypesService } from '../Core';
import { CommunitiesService } from '../Core/api/communities.service';
import { formatDate } from '@angular/common';
import { GroupedResultRecord } from '../Core/model/GroupedResultRecord';

@Component({
  selector: 'app-station-detail',
  templateUrl: './station-detail.component.html',
  styleUrls: ['./station-detail.component.css']
})
export class StationDetailComponent implements OnInit {

  @Input() device: MeasurementDevice;
  communities: Community[];
  results: GroupedResultRecord[];
  measurementTypes: MeasurementType[];
  displayedColumns: string[] = ['Value'];

  constructor(private communitiesService: CommunitiesService,
              private measurementsService: MeasurementsService,
              private measurementTypesService: MeasurementTypesService) { }

  ngOnInit() {
    this.communitiesService.communitiesGetAllCommunities().subscribe(
      val => this.communities = val
    );

    this.measurementTypesService.measurementTypesGetAllTypes().subscribe(
      val => this.measurementTypes = val
    );
  }

  ngOnChanges(changes: SimpleChanges) {
    console.log('changing to device');
    console.log(this.device);
    this.getMeasurements(this.device);
  }

  printcurrent(): void {
    console.log(this.results);
  }

  getCommunityName(device: MeasurementDevice): string {
    return this.communities.find(val => device.CommunityID == val.ID).Name;
  }

  getMeasurements(device: MeasurementDevice) {
    let fromDate = new Date();
    fromDate.setDate(fromDate.getDate() - 7);

    let toDate = new Date();

    if ( this.device !== undefined ) {
      this.measurementsService.measurementsQueryMeasurements(
        0,
        0,
        this.device.ID,
        this.device.latitude,
        this.device.longitude,
        undefined,
        fromDate,
        toDate,
        0).subscribe(val => this.results = val);
    }
  }
}
