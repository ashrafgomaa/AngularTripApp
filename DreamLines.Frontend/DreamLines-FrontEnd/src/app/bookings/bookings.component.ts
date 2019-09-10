import { Component, OnInit, ViewChild } from '@angular/core';
import { DreamlineService } from '../dreamline.service';
import { IBookingsRespobse, IBookings } from '../IBookings';
import { Subject, Observable, of } from 'rxjs';
import { debounceTime, filter, distinctUntilChanged, tap, switchMap, catchError, retry } from 'rxjs/operators';
import { String, StringBuilder } from 'typescript-string-operations';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';

@Component({
  selector: '[app-bookings]',
  templateUrl: './bookings.component.html',
  styleUrls: ['./bookings.component.css']
})
export class BookingsComponent implements OnInit {

  pageSizes = [10, 50, 100, 200];

  public bookingsResponse: IBookingsRespobse;
  public errorMessage = "";
  public salesUnitName = '';

  public salesUnitID: number = 1;
  pageIndex: number = 1;
  total: number;
  loading: boolean;
  itemsPerPage: number = 10;

  constructor(private service: DreamlineService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit() {
    this.route.paramMap.subscribe((params: ParamMap) => {
      this.salesUnitID = parseInt(params.get('salesUnitId'));
      this.salesUnitName = params.get('salesName');
    });

    this.getPage(this.pageIndex);
  }

  getPage(page: number) {
    this.loading = true;
    if (page != undefined)
      this.pageIndex = page;
    this.service.getBookings(this.salesUnitID, this.pageIndex, this.itemsPerPage).subscribe(data => {
      this.bookingsResponse = data;
      this.total = data.totalCount;
      this.loading = false;
    }, error => this.errorMessage = error);
  }

  model: any;
  searching = false;
  searchFailed = false;

  search = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      tap(() => this.searching = true),
      switchMap(term =>
        this.service.searchBookings(this.salesUnitID, term, this.itemsPerPage).pipe(
          tap(() => this.searchFailed = false),
          catchError(() => {
            this.searchFailed = true;
            return of([]);
          }))
      ),
      tap(() => this.searching = false)
    );

  formatter(obj: IBookings) {
    return String.Format("{0}, {1}, {2} {3}", obj.id, obj.shipName, obj.currency, obj.price);
  }

  goToSalesUnits() {
    this.router.navigate(['../../'], { relativeTo: this.route });
  }

}
