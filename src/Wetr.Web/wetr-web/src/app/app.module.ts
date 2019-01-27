import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { DashboardComponent } from './dashboard/dashboard.component';
import { NavigationComponent } from './navigation/navigation.component';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './material/material.module';
import { LayoutModule } from '@angular/cdk/layout';
import { MatToolbarModule, MatSidenavModule, MatIconModule, MatListModule, MatCheckbox, MatCheckboxModule, MatTableModule, MatGridListModule, MatSelect, MatSelectModule, MatFormFieldModule, MatInputModule, MatCardModule } from '@angular/material';
import { StationsComponent } from './stations/stations.component';
import { MatButtonModule } from '@angular/material/button';

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
import { StationAddComponent } from './station-add/station-add.component';
import { MeasurementAddComponent } from './measurement-add/measurement-add.component';

import { OwlDateTimeModule, OwlNativeDateTimeModule } from 'ng-pick-datetime';
import { DashboardCardComponent } from './dashboard-card/dashboard-card.component';
import { FlexLayoutModule } from '@angular/flex-layout';
import { AuthService } from './auth/auth.service';
import { CallbackComponent } from './callback/callback.component';

import { ChartsModule} from 'ng2-charts';

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    NavigationComponent,
    StationsComponent,
    StationDetailComponent,
    StationSearchComponent,
    StationAddComponent,
    MeasurementAddComponent,
    DashboardCardComponent,
    CallbackComponent
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
    MatSelectModule,
    MatFormFieldModule,
    MatInputModule,
    OwlDateTimeModule,
    OwlNativeDateTimeModule,
    FlexLayoutModule,
    MatButtonModule,
    MatCardModule,
    ChartsModule,
  ],
  providers: [GoogleMapsAPIWrapper, AuthService],
  bootstrap: [AppComponent]
})
export class AppModule { }
