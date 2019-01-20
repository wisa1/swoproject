import { Component, OnInit } from '@angular/core';
import { DevicesService } from '../Core/api/devices.service';

@Component({
  selector: 'app-stations',
  templateUrl: './stations.component.html',
  styleUrls: ['./stations.component.css']
})
export class StationsComponent implements OnInit {

  constructor(private devicesService: DevicesService) { }

  ngOnInit() {
    this.devicesService.devicesGetAllDevices().subscribe(console.log);
  }

}
