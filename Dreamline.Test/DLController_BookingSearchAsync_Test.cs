using DreamLines.BLL;
using DreamLines.Entity;
using DreamLines.Models;
using DreamLines.Models.UI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DreamLines.Testing
{
    public class DLController_BookingSearchAsync_Test
    {
        

        [Fact]
        public async System.Threading.Tasks.Task BookingSearchAsync_ReturnType()
        {
            //Arrange
            var option = DbContextHelper.CreateNewContextOptions();
            var context = new AppDbContext(option);
            DbContextHelper.SeedCorrectDuration(context);
            var _dlController = new DLController(context);

            //Act
            var items = await _dlController.BookingSearchAsync(1,"1", 1);
            //Assert
            Assert.IsAssignableFrom<List<UIBooking>>(items);
        }


        [Fact]
        public async System.Threading.Tasks.Task GetBookingBySalesUnitIDAsync_CheckReturnDataCorrectness()
        {
            //Arrange
            var option = DbContextHelper.CreateNewContextOptions();
            var context = new AppDbContext(option);
            DbContextHelper.SeedCorrectDuration(context);
            var _dlController = new DLController(context);

            //Act
            var items = await _dlController.BookingSearchAsync(1, "AIDAbella", 10);
            //Assert
            Assert.Equal(2, items.Count);
        }

    }
}
