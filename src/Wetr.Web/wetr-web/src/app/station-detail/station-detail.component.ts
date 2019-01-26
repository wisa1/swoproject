import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { MeasurementDevice, Community, MeasurementsService, MeasurementTypesService, MeasurementType, DevicesService } from '../Core';
import { CommunitiesService } from '../Core/api/communities.service';
import { formatDate } from '@angular/common';
import { GroupedResultRecord } from '../Core/model/GroupedResultRecord';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-station-detail',
  templateUrl: './station-detail.component.html',
  styleUrls: ['./station-detail.component.css']
})
export class StationDetailComponent implements OnInit {

  device: MeasurementDevice;
  community: Community;
  results: GroupedResultRecord[];
  lastValues: GroupedResultRecord[];
  measurementTypes: MeasurementType[];
  displayedColumns: string[] = ['Value', 'Type'];

  constructor(private deviceService: DevicesService,
              private communitiesService: CommunitiesService,
              private measurementsService: MeasurementsService,
              private measurementTypesService: MeasurementTypesService,
              private activeRoute: ActivatedRoute) { }

  ngOnInit() {
    //Device from url parameter
    this.activeRoute.params.subscribe(
      params => this.deviceService.devicesGetDeviceById(params['id']).subscribe(
        res => {
          this.device = res;

          //get Community
          this.communitiesService.communitiesGetCommunityById(this.device.CommunityID).subscribe(
            res => this.community = res
          );

          //Get Latest Measurmeents
          this.getMeasurements(this.device);
        }
      )
    );

    //Get Colleciton of all Measurement Types
    this.measurementTypesService.measurementTypesGetAllTypes().subscribe(
      val => this.measurementTypes = val
    );
  }

  getCommunityName(device: MeasurementDevice): string {
    let comm;
    comm = this.communities.find(val => device.CommunityID == val.ID);
    if ( comm != undefined ) {
      return comm.Name;
    }
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
        this.device.longitude, null,

        fromDate,
        toDate,
        0).subscribe((measurements: GroupedResultRecord[]) => {
          measurements.sort(this.sortResultsDescending);

          this.lastValues = [];
          for (let measurement of measurements) {

            let vals = this.lastValues.find(val => val.MeasurementTypeID == measurement.MeasurementTypeID);
            if (vals === undefined ) {
              this.lastValues.push(measurement);
            }
          }
        });
    }
  }

  getMeasurementType(result: GroupedResultRecord): MeasurementType {
    if (result != undefined && this.measurementTypes != undefined) {
      return this.measurementTypes.find(v => v.ID == result.MeasurementTypeID);
    }
  }

  sortResultsDescending(m1, m2: GroupedResultRecord): number {
    if (m1.dateTimeStart > m2.dateTimeStart){
      return 1;
    }
    return -1;
  }
}
