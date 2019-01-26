import { Component, OnInit, ViewChild } from '@angular/core';
import { DevicesService, Community, CommunitiesService } from '../Core';
import { NgForm } from '@angular/forms';
import { MeasurementDevice } from '../Core/model/measurementDevice';
import { Router } from '@angular/router';

@Component({
  selector: 'app-station-add',
  templateUrl: './station-add.component.html',
  styleUrls: ['./station-add.component.css']
})
export class StationAddComponent implements OnInit {

  constructor(private deviceService: DevicesService,
              private communitiesService: CommunitiesService,
              private router: Router) { }

  device: MeasurementDevice = {
    ID: null,
    address: '',
    communityID: 0,
    deviceName: '',
    latitude: 0,
    longitude: 0
  };

  submitted = false;
  communities: Community[];

  selectedCommunity: string;
  errors: { [key: string]: string } = {};

  ngOnInit() {

    this.communitiesService.communitiesGetAllCommunities().subscribe(
      val => this.communities = val
    );
  }

  onSubmit() {
    console.log(this.device);
    this.deviceService.devicesInsertDevice(this.device).subscribe(() => {
      this.router.navigate(['/stations']);
    });


  }
}
