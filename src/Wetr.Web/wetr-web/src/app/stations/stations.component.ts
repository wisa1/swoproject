import { Component, OnInit, ViewChild } from '@angular/core';
import { DevicesService } from '../Core/api/devices.service';
import { MeasurementDevice } from '../Core/model/MeasurementDevice';
import { MatSelectionList, MatRow } from '@angular/material';
import { Measurement } from '../Core';
import { MeasurementDeviceLogic } from '../BusinessLogic/MeasurementDeviceLogic';

@Component({
  selector: 'app-stations',
  templateUrl: './stations.component.html',
  styleUrls: ['./stations.component.css']
})
export class StationsComponent implements OnInit {

  public devices: MeasurementDevice[];
  public selectedDevice: MeasurementDevice;
  public selectedDeviceID = -1;
  displayedColumns: string[] = ['ShownInDashboard', 'Name', 'Address'];

  constructor(private devicesService: DevicesService) {}

  ngOnInit() {
    this.devicesService.devicesGetAllDevices().subscribe(
      val => this.devices = val
    );
  }

  setSelectedDevice(device: MeasurementDevice) {
    this.selectedDevice = device;
    console.log(this.selectedDevice);
  }

  deviceIsInDashboard(device: MeasurementDevice): boolean {
    return MeasurementDeviceLogic.deviceIsInDashboard(device);
  }

  invertIsInDashboard(device: MeasurementDevice) {
    MeasurementDeviceLogic.invertIsInDashboard(device);
  }

  rowSelected(row: MatRow) {
    console.log(row);
    this.selectedDevice = (row as MeasurementDevice);
    this.selectedDeviceID = (row as MeasurementDevice).ID;
  }
}
