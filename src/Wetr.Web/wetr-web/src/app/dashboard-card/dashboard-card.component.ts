import { Component, OnInit, Input } from '@angular/core';
import { MeasurementDevice } from '../Core/model/measurementDevice';
import { CommunitiesService, Community, MeasurementsService } from '../Core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard-card',
  templateUrl: './dashboard-card.component.html',
  styleUrls: ['./dashboard-card.component.css']
})
export class DashboardCardComponent implements OnInit {
  @Input() device: MeasurementDevice;
  communities: Community[] = [];

  public lineChartType = 'line';
  public lineChartColors: Array<any> = [
    { // grey
      backgroundColor: 'rgba(148,159,177,0.2)',
      borderColor: 'rgba(148,159,177,1)',
      pointBackgroundColor: 'rgba(148,159,177,1)',
      pointBorderColor: '#fff',
      pointHoverBackgroundColor: '#fff',
      pointHoverBorderColor: 'rgba(148,159,177,0.8)'
    },
    { // dark grey
      backgroundColor: 'rgba(77,83,96,0.2)',
      borderColor: 'rgba(77,83,96,1)',
      pointBackgroundColor: 'rgba(77,83,96,1)',
      pointBorderColor: '#fff',
      pointHoverBackgroundColor: '#fff',
      pointHoverBorderColor: 'rgba(77,83,96,1)'
    },
    { // grey
      backgroundColor: 'rgba(148,159,177,0.2)',
      borderColor: 'rgba(148,159,177,1)',
      pointBackgroundColor: 'rgba(148,159,177,1)',
      pointBorderColor: '#fff',
      pointHoverBackgroundColor: '#fff',
      pointHoverBorderColor: 'rgba(148,159,177,0.8)'
    }
  ];

  public deviceData: number[] = [];
  public lineChartData: Array<any> = null;
  public lineChartLegend = true;
  public lineChartOptions: any = {
    responsive: false,
    scales : {
      yAxes: [{
         ticks: {
            steps : 10,
            stepValue : 10,
            max : 25,
          }
      }]
    }
  };

  constructor(private communityService: CommunitiesService,
              private router: Router,
              private measurementsService: MeasurementsService) { }
  ngOnInit() {
    this.communityService.communitiesGetAllCommunities().subscribe(x => this.communities = x);

    let dateNow = new Date();
    let dateFrom = new Date();
    dateFrom.setDate(dateNow.getDate() - 3);
    this.measurementsService.measurementsQueryMeasurements(
      0,
      0,
      this.device.ID,
      this.device.Latitude,
      this.device.Longitude,
      1,
      dateFrom,
      dateNow,
      0).subscribe(results => {
        for (let result of results) {
          this.deviceData.push(result.Value);
        }
        this.lineChartData = [{data: this.deviceData, label: 'Temperature history of the last day'}];
        console.log(this.lineChartData);
      });
  }

  getCommunity(): string {
    let comm;
    comm = this.communities.find(x => x.ID == this.device.CommunityID);
    if (comm !== undefined) {
      return comm.Name;
    }
    return '';
  }
  addMeasurement() {
    this.router.navigate(['stations/' + this.device.ID + '/addMeasurement']);
  }
  goToDetails() {
    this.router.navigate(['stations/' + this.device.ID]);
  }



}
