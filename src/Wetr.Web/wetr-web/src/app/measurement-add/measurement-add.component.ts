import { Component, OnInit } from '@angular/core';
import { Measurement } from '../Core/model/measurement';
import { Router, ActivatedRoute } from '@angular/router';
import { DevicesService } from '../Core/api/devices.service';
import { MeasurementType } from '../Core/model/measurementType';
import { MeasurementTypesService, MeasurementsService } from '../Core';

@Component({
  selector: 'app-measurement-add',
  templateUrl: './measurement-add.component.html',
  styleUrls: ['./measurement-add.component.css']
})
export class MeasurementAddComponent implements OnInit {

  measurement: Measurement = {
    deviceID: null,
    timestamp: null,
    typeID: null,
    unitOfMeasureID: null,
    value: null
  };

  measurementTypes: MeasurementType[];

  constructor( private deviceService: DevicesService,
               private router: Router,
               private measurementTypeService: MeasurementTypesService,
               private measurementService: MeasurementsService,
               private route: ActivatedRoute) {}

  ngOnInit() {
    this.measurementTypeService.measurementTypesGetAllTypes().subscribe(
      val => this.measurementTypes = val
    );
  }

  onSubmit() {
    this.route.params.subscribe(
      (params) => {
        this.deviceService.devicesGetDeviceById(params['id']).subscribe(
          (res) => {
            this.measurement.deviceID = res.ID;
            this.measurementService.measurementsInsertMeasurement(this.measurement).subscribe(
              () => {
                this.router.navigate(['/stations/' + this.measurement.deviceID]);
              }
            );
          }
        );
      }
    );
  }
}
