import { Component, ViewChild } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { MeasurementDevice } from '../Core';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent {
  constructor(private router: Router,
              private route: ActivatedRoute) {}

  @ViewChild('searchTerm') searchBox;

  stationSelected(device: MeasurementDevice) {
    console.log(device);

    this.router.navigate(['stations', device.ID], {relativeTo: this.route});
  }
}
