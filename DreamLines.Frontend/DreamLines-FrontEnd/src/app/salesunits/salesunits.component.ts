import { Component, OnInit } from '@angular/core';
import { DreamlineService } from '../dreamline.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-salesunits',
  templateUrl: './salesunits.component.html',
  styleUrls: ['./salesunits.component.css']
})
export class SalesunitsComponent implements OnInit {

  public salesUnitList = [];
  public errorMessage = "";

  constructor(private service: DreamlineService, private _route: Router, private route: ActivatedRoute) { }

  ngOnInit() {
    this.service.getSalesUnits().subscribe(data => this.salesUnitList = data, error => this.errorMessage = error);
  }

  GotoBookingDetails(salesUnit) {
    //this._route.navigate(['/bookings', salesUnit.id, salesUnit.name]);
    this._route.navigate([salesUnit.id, salesUnit.name], { relativeTo: this.route });
  }

}
