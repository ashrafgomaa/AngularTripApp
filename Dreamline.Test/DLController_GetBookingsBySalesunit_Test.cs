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
    public class DLController_GetBookingsBySalesunit_Test
    {
        
        [Fact]
        public async System.Threading.Tasks.Task GetBookingBySalesUnitIDAsync_Check_ArgumentNullException()
        {
            //Arrange
            var option = DbContextHelper.CreateNewContextOptions();
            var context = new AppDbContext(option);
            DbContextHelper.SeedCorrectDuration(context);
            var _dlController = new DLController(context);

            //Act
            //Assert
            Assert.ThrowsAsync(typeof(ArgumentNullException), async () => { await _dlController.GetBookingBySalesUnitIDAsync(null, null, null); });
        }

        [Fact]
        public async System.Threading.Tasks.Task GetBookingBySalesUnitIDAsync_ReturnType()
        {
            //Arrange
            var option = DbContextHelper.CreateNewContextOptions();
            var context = new AppDbContext(option);
            DbContextHelper.SeedCorrectDuration(context);
            var _dlController = new DLController(context);

            //Act
            var items = await _dlController.GetBookingBySalesUnitIDAsync(1, 1, 1);
            //Assert
            Assert.IsAssignableFrom<BookingPage>(items);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetBookingBySalesUnitIDAsync_Check_InvalidPageCount()
        {
            //Arrange
            var option = DbContextHelper.CreateNewContextOptions();
            var context = new AppDbContext(option);
            DbContextHelper.SeedCorrectDuration(context);
            var _dlController = new DLController(context);

            //Act
            //Assert
            Assert.ThrowsAsync(typeof(IndexOutOfRangeException), async () => { await _dlController.GetBookingBySalesUnitIDAsync(1, 10, 100); });
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
            var items = await _dlController.GetBookingBySalesUnitIDAsync(1, 1, 10);
            //Assert
            Assert.Equal(5, items.TotalCount);
        }

    }
}
