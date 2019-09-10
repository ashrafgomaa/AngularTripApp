export 
  interface IBookings {
  id: number,
  currency: string,
  bookingDate: Date,
  price: number,
  shipName: string
}


export
  interface IBookingsRespobse {
  totalCount: number,
  pageCount: string,
  bookingList: IBookings[]
}
