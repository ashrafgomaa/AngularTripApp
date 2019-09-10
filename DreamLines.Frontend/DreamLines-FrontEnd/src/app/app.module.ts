import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { BookingsComponent } from './bookings/bookings.component';
import { SalesunitsComponent } from './salesunits/salesunits.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';

import { AppRoutingModule, routingComponents } from './app-routing.module';
import { DreamlineService } from './Dreamline.service';
import { HttpClientModule } from '@angular/Common/http';
import { NgxPaginationModule } from 'ngx-pagination';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';


@NgModule({
  declarations: [
    AppComponent,
    BookingsComponent,
    SalesunitsComponent,
    PageNotFoundComponent,
    routingComponents,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    NgxPaginationModule,
    NgbModule,
  ],
  providers: [DreamlineService],
  bootstrap: [AppComponent]
})
export class AppModule { }
