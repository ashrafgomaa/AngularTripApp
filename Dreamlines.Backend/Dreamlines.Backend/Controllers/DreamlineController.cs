using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DreamLines.BLL;
using DreamLines.BLL.EntityHelper;
using DreamLines.Entity;
using DreamLines.BLL.EntityHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dreamlines.Backend.Controllers
{
    /// <summary>
    /// API controller to provid the required information, it will be called by frontend application
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DreamlineController : ControllerBase
    {
        /// <summary>
        /// intialize the DBcontext to query database
        /// </summary>
        private readonly AppDbContext _context;
        private readonly DLController _dlController;

        /// <summary>
        /// class Constructor which appect the DBContext initialized while starting up the application using DI.
        /// </summary>
        /// <param name="context">Entity framework DB context</param>
        public DreamlineController(AppDbContext context)
        {
            _context = context;
            _dlController = new DLController(_context);
        }

        /// <summary>
        /// Get sales units data within the required range
        /// </summary>
        /// <returns>List of json models which provide information about the sales units, {Id:,Country:,Currency:,Name:,TotalPrice:}</returns>
        [HttpGet]
        [Route("GetSalesUnits")]
        public async Task<IActionResult> GetSalesUnits()
        {
            try
            {
              
                //get list asynchrounosly 
                var sItems = await _dlController.GetSalesUnitsAsync();

                //if there are not any result, then return not found
                if (sItems == null)
                    return NotFound();

                //return the result
                return Ok(sItems.ToList());
            }
            catch (Exception ex)
            {
                //log exception
                return NotFound();
            }
            
        }
        /// <summary>
        /// Get list of bookings related to specific sales unit id
        /// </summary>
        /// <param name="salesUnitId">sales unit id</param>
        /// <param name="PageIndex">page index</param>
        /// <param name="PageSize">page size</param>
        /// <returns>List of json models which provide information about the sales units, {TotalCount:,PageCount:,BookingList:{Id:,Currency:,BookingDate:,Price:,ShipName}}</returns>
        [HttpGet, ActionName("GetBookingBySalesUnitID")]
        [Route("GetBookingBySalesUnitID/{salesUnitId?}/{PageIndex?}/{PageSize?}")]
        public async Task<IActionResult> GetBookingBySalesUnitID(int? salesUnitId, int? PageIndex, int? PageSize)
        {
            try
            {
                //get list asynchrounosly 
                var sItems = await _dlController.GetBookingBySalesUnitIDAsync(salesUnitId, PageIndex, PageSize);

                //if there are not any result, then return not found
                if (sItems == null)
                    return NotFound();

                //return the result
                return Ok(sItems);
            }
            catch (IndexOutOfRangeException ex)
            {
                //log exception
                return NotFound();
            }
            catch (ArgumentNullException ex)
            {
                //log exception
                return NotFound();
            }
            catch (Exception ex)
            {
                //log exception
                return NotFound();
            }
        }
        /// <summary>
        /// Search bookings of specific sales unit by bookingId or by Ship name then return a page of result
        /// </summary>
        /// <param name="salesUnitId"> sales unit Id to search the bookings of it</param>
        /// <param name="searchText">search keyword</param>
        /// <param name="PageSize">page size of result</param>
        /// <returns>List of json models which provide information about the sales units, {TotalCount:,PageCount:,BookingList:{Id:,Currency:,BookingDate:,Price:,ShipName}}</returns>
        [HttpGet, ActionName("BookingSearch")]
        [Route("BookingSearch/{salesUnitId?}/{searchText?}/{PageSize?}")]
        public async Task<IActionResult> BookingSearch(int? salesUnitId, string searchText, int? PageSize)
        {
            try
            {
                //get list asynchrounosly 
                var sItems = await _dlController.BookingSearchAsync(salesUnitId, searchText, PageSize);

                //if there are not any result, then return not found
                if (sItems == null)
                    return NotFound();

                //return the result
                return Ok(sItems.ToList());
            }
            catch (Exception ex)
            {
                //log exception
                return NotFound();
            }


        }
    }
}