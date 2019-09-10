import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SalesunitsComponent } from './salesunits/salesunits.component';
import { BookingsComponent } from './bookings/bookings.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';

const routes: Routes = [
  { path: "", redirectTo:'/DLTask', pathMatch:'full' },
  { path: "DLTask", component: SalesunitsComponent },
  { path: "DLTask/:salesUnitId/:salesName", component: BookingsComponent },
  { path: "**", component: PageNotFoundComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {

}

export const routingComponents = [SalesunitsComponent, BookingsComponent];
