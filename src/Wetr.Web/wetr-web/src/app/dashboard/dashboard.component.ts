import { Component, OnInit } from '@angular/core';
import { MeasurementDevice, DevicesService } from '../Core';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  devices: MeasurementDevice[] = [];


  constructor(private devicesService: DevicesService) { }
  ngOnInit() {

    let i = 0;
    let key = localStorage.key(i);

    while (key != null) {
      if (key.includes('Device')) {
        let device = JSON.parse(localStorage.getItem(key));
        this.devicesService.devicesGetDeviceById(device.ID).subscribe( x => this.devices.push(x));
      }
      i++;
      key = localStorage.key(i);
    }
  }

}
