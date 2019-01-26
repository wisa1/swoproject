import { Component, OnInit, ViewChild } from '@angular/core';
import { DevicesService } from '../Core/api/devices.service';
import { MeasurementDevice } from '../Core/model/MeasurementDevice';
import { MatSelectionList, MatRow } from '@angular/material';
import { MeasurementDeviceLogic } from '../BusinessLogic/MeasurementDeviceLogic';
import { Community } from '../Core/model/community';
import { CommunitiesService } from '../Core/api/communities.service';

@Component({
  selector: 'app-stations',
  templateUrl: './stations.component.html',
  styleUrls: ['./stations.component.css']
})
export class StationsComponent implements OnInit {

  public devices: MeasurementDevice[];
  public communities: Community[];

  displayedColumns: string[] = ['ShownInDashboard', 'Name', 'Address', 'City'];

  constructor(private devicesService: DevicesService,
              private communitiesService: CommunitiesService) {}

  ngOnInit() {
    this.devicesService.devicesGetAllDevices().subscribe(
      val => this.devices = val
    );

    this.communitiesService.communitiesGetAllCommunities().subscribe(
      val => this.communities = val
    );
  }

  deviceIsInDashboard(device: MeasurementDevice): boolean {
    return MeasurementDeviceLogic.deviceIsInDashboard(device);
  }

  /*
  invertIsInDashboard(device: MeasurementDevice) {
    MeasurementDeviceLogic.invertIsInDashboard(device);
  }
  */

  getCommunityName(device): string {
    if ( device !== undefined ) {
      return this.communities.find(comm => device.CommunityID == comm.ID).Name;
    }
    return '';
  }
}
