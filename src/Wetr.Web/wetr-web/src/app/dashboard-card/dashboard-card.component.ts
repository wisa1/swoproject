import { Component, OnInit, Input } from '@angular/core';
import { MeasurementDevice } from '../Core/model/measurementDevice';
import { CommunitiesService, Community } from '../Core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard-card',
  templateUrl: './dashboard-card.component.html',
  styleUrls: ['./dashboard-card.component.css']
})
export class DashboardCardComponent implements OnInit {
  @Input() device: MeasurementDevice;
  communities: Community[] = [];

  constructor(private communityService: CommunitiesService,
              private router: Router) { }

  ngOnInit() {
    this.communityService.communitiesGetAllCommunities().subscribe(x => this.communities = x);
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
