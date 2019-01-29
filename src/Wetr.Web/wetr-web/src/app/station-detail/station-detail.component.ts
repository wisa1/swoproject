import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { MeasurementDevice, Community, MeasurementsService, MeasurementTypesService, MeasurementType, DevicesService } from '../Core';
import { CommunitiesService } from '../Core/api/communities.service';
import { formatDate } from '@angular/common';
import { GroupedResultRecord } from '../Core/model/GroupedResultRecord';
import { ActivatedRoute } from '@angular/router';
import { MeasurementDeviceLogic } from '../BusinessLogic/MeasurementDeviceLogic';
import { MatDatepickerToggle } from '@angular/material';

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

  minTempValues: GroupedResultRecord[];
  avgTempValues: GroupedResultRecord[];
  maxTempValues: GroupedResultRecord[];

  measurementTypes: MeasurementType[];
  displayedColumns: string[] = ['Value', 'Type'];
  isInDashboard = false;

  //#region  chart stuff

  public lineChartData: Array<any> = undefined;
  //public lineChartLabels:Array<any> = ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12', '13', '14'];
  public lineChartLabels: Array<any> = [];
    public lineChartOptions: any = {
    responsive: true,
    maintainAspectRatio: false,
    scales : {
      yAxes: [{
         ticks: {
            steps : 3,
            stepValue : 20,
            max : 35,
          }
      }]
    }
  };
  public lineChartLegend: boolean = false;
  public lineChartType = 'line';
  public lineChartColors: Array<any> = [
    { // grey
      backgroundColor: 'rgba(148,159,177,0.2)',
      borderColor: 'rgba(0, 0, 255, 1)',
      pointBackgroundColor: 'rgba(148,159,177,1)',
      pointBorderColor: '#fff',
      pointHoverBackgroundColor: '#fff',
      pointHoverBorderColor: 'rgba(148,159,177,0.8)'
    },
    { // dark grey
      backgroundColor: 'rgba(77,83,96,0.2)',
      borderColor: 'rgba(0, 255, 0, 1)',
      pointBackgroundColor: 'rgba(77,83,96,1)',
      pointBorderColor: '#fff',
      pointHoverBackgroundColor: '#fff',
      pointHoverBorderColor: 'rgba(77,83,96,1)'
    },
    { // grey
      backgroundColor: 'rgba(148,159,177,0.2)',
      borderColor: 'rgba(255, 0, 0, 1)',
      pointBackgroundColor: 'rgba(148,159,177,1)',
      pointBorderColor: '#fff',
      pointHoverBackgroundColor: '#fff',
      pointHoverBorderColor: 'rgba(148,159,177,0.8)'
    }
  ];



  //#endregion
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
          this.getTempValues();
          this.isInDashboard = this.deviceIsInDashboard();
        }
      )
    );

    //Get Colleciton of all Measurement Types
    this.measurementTypesService.measurementTypesGetAllTypes().subscribe(
      val => this.measurementTypes = val
    );
  }

  getTempValues(): any {
    let fromDate = new Date(new Date().getFullYear(), 0, 1);
    let toDate = new Date(new Date().getFullYear(), 11, 31);

    if (this.device !== undefined) {
      this.measurementsService.measurementsQueryMeasurements(
        3,
        2,
        this.device.ID,
        this.device.latitude,
        this.device.longitude,
        1,
        fromDate,
        toDate, 0
      ).subscribe(minValues => {
        let results = [];
        for (let result of minValues) {
          results.push(Math.round(result.Value * 100) / 100;
        }

        if (this.lineChartData == undefined) {
          this.lineChartData = [{data: results, label: 'min'}];
        } else {
          this.lineChartData.push({data: results, label: 'min'});
        }
        console.log(this.lineChartData);
      });

      this.measurementsService.measurementsQueryMeasurements(
        2,
        2,
        this.device.ID,
        this.device.latitude,
        this.device.longitude,
        1,
        fromDate,
        toDate, 0
      ).subscribe(maxValues => {
        let results = [];
        for (let result of maxValues) {
          results.push(Math.round(result.Value * 100) / 100;
        }

        if (this.lineChartData == undefined) {
          this.lineChartData = [{data: results, label: 'max'}];
        } else {
          this.lineChartData.push({data: results, label: 'max'});
        }
        console.log(this.lineChartData);
      });

      this.measurementsService.measurementsQueryMeasurements(
        4,
        2,
        this.device.ID,
        this.device.latitude,
        this.device.longitude,
        1,
        fromDate,
        toDate, 0
      ).subscribe(avgValues => {
        let results = [];
        let i = 1;
        for (let result of avgValues) {
          results.push(Math.round(result.Value * 100) / 100;
          this.lineChartLabels.push('KW' + i);
          i++;
        }



        if (this.lineChartData == undefined) {
          this.lineChartData = [{data: results, label: 'avg'}];
        } else {
          this.lineChartData.push({data: results, label: 'avg'});
        }
        console.log(this.lineChartData);
      });
    }
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
    if (m1.dateTimeStart > m2.dateTimeStart) {
      return 1;
    }
    return -1;
  }

  deviceIsInDashboard(): boolean {
    if (this.device !== undefined) {
      let inDashboard = MeasurementDeviceLogic.deviceIsInDashboard(this.device);
      return inDashboard;
    }
    return false;
  }

  invertIsInDashboard() {
    MeasurementDeviceLogic.invertIsInDashboard(this.device);
  }



}
