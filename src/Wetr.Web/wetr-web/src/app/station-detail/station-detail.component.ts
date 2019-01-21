import { Component, OnInit, Input } from '@angular/core';
import { MeasurementDevice } from '../Core';

@Component({
  selector: 'app-station-detail',
  templateUrl: './station-detail.component.html',
  styleUrls: ['./station-detail.component.css']
})
export class StationDetailComponent implements OnInit {

  @Input() device: MeasurementDevice;

  constructor() { }

  ngOnInit() {
  }

  printcurrent(): void {
    console.log(this.device);
  }
}
