import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { MeasurementDevice } from '../Core/model/measurementDevice';
import { debounceTime, distinctUntilChanged, tap, switchMap } from 'rxjs/operators';
import { DevicesService } from '../Core';

@Component({
  selector: 'app-station-search',
  templateUrl: './station-search.component.html',
  styleUrls: ['./station-search.component.css']
})
export class StationSearchComponent implements OnInit {
  isLoading = false;
  foundStations: MeasurementDevice[] = [];
  keyup = new EventEmitter<string>();
  searchTerm: string;

  @Output() stationSelected = new EventEmitter<MeasurementDevice>();

  constructor(private devicesService: DevicesService) { }

  ngOnInit() {
    this.keyup.pipe(
      debounceTime(500),
      distinctUntilChanged(),
      tap(() => this.isLoading = true),
      switchMap(searchTerm => this.devicesService.devicesGetAllDevices()),
      tap(() => this.isLoading = false),
      ).subscribe((stations) => {
        this.foundStations = stations.filter(x => x.DeviceName.includes(this.searchTerm));
      });
  }

  updateSearchTerm(event) {
    this.searchTerm = event.target.value;
  }

  resetStations() {
    this.foundStations = [];
  }
}
