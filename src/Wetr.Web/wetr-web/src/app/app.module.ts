import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { DashboardComponent } from './dashboard/dashboard.component';
import { NavigationComponent } from './navigation/navigation.component';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './material/material.module';
import { LayoutModule } from '@angular/cdk/layout';
import { MatToolbarModule, MatButtonModule, MatSidenavModule, MatIconModule, MatListModule, MatCheckbox, MatCheckboxModule, MatTableModule, MatGridListModule } from '@angular/material';
import { StationsComponent } from './stations/stations.component';

import { environment } from 'src/environments/environment';
import { ApiModule } from './Core/api.module';
import { HttpClientModule } from '@angular/common/http';
import { StationDetailComponent } from './station-detail/station-detail.component';

import { FormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AgmCoreModule, GoogleMapsAPIWrapper} from '@agm/core';
import { MeasurementTypesApi } from './Core/api/MeasurementTypesApi';
import { MeasurementsService } from './Core';
import { StationSearchComponent } from './station-search/station-search.component';

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    NavigationComponent,
    StationsComponent,
    StationDetailComponent,
    StationSearchComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MaterialModule,
    LayoutModule,
    MatToolbarModule,
    MatButtonModule,
    MatSidenavModule,
    MatCheckboxModule,
    MatIconModule,
    MatListModule,
    ApiModule,
    HttpClientModule,
    AppRoutingModule,
    MatTableModule,
    MatGridListModule,
    AgmCoreModule.forRoot({apiKey: 'AIzaSyD_ZP3Mj-PMi1mG_Bfq5K8DJxz3LntJZ2o'}), //Maps
    FormsModule,
    NgbModule.forRoot(),
  ],
  providers: [GoogleMapsAPIWrapper],
  bootstrap: [AppComponent]
})
export class AppModule { }
