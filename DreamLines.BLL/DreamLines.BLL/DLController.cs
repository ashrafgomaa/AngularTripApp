using DreamLines.BLL.EntityHelper;
using DreamLines.Entity;
using DreamLines.Models.UI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamLines.BLL
{
    public class DLController
    {
        AppDbContext _context;
        public DLController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UISalesUnit>> GetSalesUnitsAsync()
        {

            //Define the date duration for quering the data
            DateTime dtStart = new DateTime(2016, 1, 1);
            DateTime dtEnd = new DateTime(2016, 3, 31);
            IQueryable<UISalesUnit> dItems = GetSalesUnitWithinDuration(dtStart, dtEnd);

            //get list asynchrounosly 
            var counter = await dItems.CountAsync();

            //if there are not any result, then return not found
            if (counter > 0)
            {
                var sItems = await dItems.ToListAsync();
                //another query to join result sfrom the above query to sales units table to get full sales unit data
                var fullInfoList = from i in dItems
                                   join sItem in _context.SalesUnits on i.Id equals sItem.Id
                                   select new UISalesUnit()
                                   {
                                       Id = sItem.Id,
                                       Country = sItem.Country,
                                       Currency = sItem.Currency,
                                       Name = sItem.Name,
                                       TotalPrice = i.TotalPrice
                                   };
                var list = await fullInfoList.ToListAsync();
                return fullInfoList.ToList();
            }
            else
                return null;
        }

        private IQueryable<UISalesUnit> GetSalesUnitWithinDuration(DateTime dtStart, DateTime dtEnd)
        {

            //usin the following linq query to make join between Ships and Bookings table then group result by sales units
            return _context.Ships.Join(_context.Bookings.Where(b => b.BookingDate.Date >= dtStart.Date && b.BookingDate.Date <= dtEnd.Date), s => s.Id, b => b.ShipId, (s, b) => new { Id = s.SalesUnitId, TotalPrice = b.Price }).GroupBy(s => s.Id).Select(x => new UISalesUnit() { Id = x.Key, TotalPrice = x.Select(g => g.TotalPrice).Sum() });
        }

        public async Task<BookingPage> GetBookingBySalesUnitIDAsync(int? salesUnitId, int? PageIndex, int? PageSize)
        {
            //validate parameters
            if (salesUnitId == null)
            {
                throw new ArgumentNullException();
            }
            //Set default values if not specified
            if (PageIndex == null)
            {
                PageIndex = 1;
            }
            if (PageSize == null)
            {
                PageSize = 1;
            }

            //Define the date duration for quering the data
            DateTime dtStart = new DateTime(2016, 1, 1);
            DateTime dtEnd = new DateTime(2016, 3, 31);

            //Linq query to get list of bookings related to the sales unit
            var fullInfoList = from b in _context.Bookings
                               join sItem in _context.Ships on b.ShipId equals sItem.Id
                               join soUnit in _context.SalesUnits on sItem.SalesUnitId equals soUnit.Id
                               where sItem.SalesUnitId.Equals(salesUnitId) && b.BookingDate.Date >= dtStart.Date && b.BookingDate.Date <= dtEnd.Date
                               orderby b.BookingDate descending
                               select new UIBooking()
                               {
                                   Id = b.Id,
                                   Currency = soUnit.Currency,
                                   BookingDate = b.BookingDate,
                                   Price = b.Price,
                                   ShipName = sItem.Name
                               };
            //initialize an object of class helper to select only one page of the result
            var pageList = new PageList<UIBooking>();
            //Get a page of result asynchrounosly
            await pageList.CreateAsync(fullInfoList, PageIndex.Value, PageSize.Value);

            //if page index is greater than the total page count
            if (pageList.PageCount < PageIndex)
                throw new IndexOutOfRangeException();

            //return the specific page of result
            return new BookingPage()
            {
                TotalCount = pageList.TotalCount,
                PageCount = pageList.PageCount,
                BookingList = pageList.ToList()
            };
        }

        public async Task<List<UIBooking>> BookingSearchAsync(int? salesUnitId, string searchText, int? PageSize)
        {
            //validate parameters, or set default values if not specified
            if (PageSize == null)
            {
                PageSize = 1;
            }
            //Define the date duration for quering the data
            DateTime dtStart = new DateTime(2016, 1, 1);
            DateTime dtEnd = new DateTime(2016, 3, 31);

            //Linq query to get list of bookings related to the sales unit, by search keyword
            var fullInfoList = from b in _context.Bookings
                               join sItem in _context.Ships on b.ShipId equals sItem.Id
                               join soUnit in _context.SalesUnits on sItem.SalesUnitId equals soUnit.Id
                               where sItem.SalesUnitId.Equals(salesUnitId) && b.BookingDate.Date >= dtStart.Date && b.BookingDate.Date <= dtEnd.Date
                               && (b.Id.ToString().Contains(searchText) || sItem.Name.Contains(searchText))
                               orderby b.BookingDate descending

                               select new UIBooking()
                               {
                                   Id = b.Id,
                                   Currency = soUnit.Currency,
                                   BookingDate = b.BookingDate,
                                   Price = b.Price,
                                   ShipName = sItem.Name
                               };
            //initialize an object of class helper to select only one page of the result
            var pageList = new PageList<UIBooking>();
            //Get a page of result asynchrounosly
            await pageList.CreateAsync(fullInfoList, 1, PageSize.Value);

            //return the specific page of result
            return pageList.ToList();


        }
    }
}
