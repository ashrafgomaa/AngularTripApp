import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/Common/http';
import { ISalesUnits } from './ISalesUnits'
import { IBookings, IBookingsRespobse } from './IBookings'
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class DreamlineService {

  private serviceUrl: string = 'http://localhost:29949/api/Dreamline/';

  constructor(private http: HttpClient) { }

  getSalesUnits(): Observable<ISalesUnits[]> {
    return this.http.get<ISalesUnits[]>(this.serviceUrl + 'GetSalesUnits').pipe(
      catchError(this.handleError)
    );
  }

  getBookings(salesUnitID: number, pageIndex: number, pageSize: number): Observable<IBookingsRespobse> {
    return this.http.get<IBookingsRespobse>(this.serviceUrl + 'GetBookingBySalesUnitID/' + salesUnitID + "/" + pageIndex + "/" + pageSize).pipe(
      catchError(this.handleError)
    );
  }

  searchBookings(salesUnitID: number, searchText: string, pageSize: number): Observable<IBookings[]> {
    return this.http.get<IBookings[]>(this.serviceUrl + 'BookingSearch/' + salesUnitID + "/" + searchText + "/" + pageSize).pipe(
      catchError(this.handleError)
    );
  }

  handleError(error: HttpErrorResponse) {
    return throwError(error.message || "Server Error!");
  }
}
