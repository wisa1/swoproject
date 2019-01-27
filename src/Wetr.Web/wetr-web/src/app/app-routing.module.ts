import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes} from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { StationsComponent } from './stations/stations.component';
import { StationDetailComponent } from './station-detail/station-detail.component';
import { StationAddComponent } from './station-add/station-add.component';
import { v } from '@angular/core/src/render3';
import { MeasurementAddComponent } from './measurement-add/measurement-add.component';

const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'stations', component: StationsComponent },
  { path: 'stations/:id', component: StationDetailComponent },
  { path: 'stations/:id/addMeasurement', component: MeasurementAddComponent },
  { path: 'addStation', component: StationAddComponent }
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})

export class AppRoutingModule { }


