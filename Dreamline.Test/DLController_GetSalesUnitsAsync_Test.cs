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
    public class DLController_GetSalesUnitsAsync_Test
    {
        
        [Fact]
        public async System.Threading.Tasks.Task GetSalesUnitsAsync_NullItems()
        {
            //Arrange
            var option = DbContextHelper.CreateNewContextOptions();
            var context = new AppDbContext(option);
            DbContextHelper.SeedWrongDuration(context);
            var _dlController = new DLController(context);

            //Act
            var items = await _dlController.GetSalesUnitsAsync();

            //Assert
            Assert.True(items == null);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetSalesUnitsAsync_ReturnType()
        {
            //Arrange
            var option = DbContextHelper.CreateNewContextOptions();
            var context = new AppDbContext(option);

            DbContextHelper.SeedCorrectDuration(context);
            var _dlController = new DLController(context);

            //Act
            var items = await _dlController.GetSalesUnitsAsync();

            //Assert
            Assert.IsAssignableFrom<List<UISalesUnit>>(items);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetSalesUnitsAsync_CheckReturnDataCorrectness()
        {
            //Arrange
            var option = DbContextHelper.CreateNewContextOptions();
            var context = new AppDbContext(option);
            DbContextHelper.SeedCorrectDuration(context);
            var _dlController = new DLController(context);

            //Act
            var items = await _dlController.GetSalesUnitsAsync();

            //Assert
            Assert.Equal(4321, items.First(a=>a.Id == 3).TotalPrice);
        }

        

      
    }
}
