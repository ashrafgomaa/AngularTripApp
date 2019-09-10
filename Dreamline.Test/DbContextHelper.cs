using DreamLines.Entity;
using DreamLines.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DreamLines.Testing
{
    public class DbContextHelper
    {
        public static DbContextOptions<AppDbContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh 
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseInMemoryDatabase()
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        public static void SeedWrongDuration(AppDbContext context)
        {

            var _salesUnitsList = new[]
            {
                new SalesUnit(){ Id=4, Name="dreamlines.de", Country="Germany", Currency="€" },
                new SalesUnit(){ Id=5, Name="dreamlines.com.br", Country="Brazil", Currency="R$" },
                new SalesUnit(){ Id=6, Name="dreamlines.it", Country="Italy", Currency="€" },
            };
            context.SalesUnits.AddRange(_salesUnitsList);

            var _shipsList = new[]
            {
                new Ship(){Id = 100, Name = "AIDAbella", SalesUnitId = 4},
                new Ship(){Id = 101, Name = "AIDAblu", SalesUnitId = 4},
                new Ship(){Id = 102, Name = "AIDAstella", SalesUnitId = 4},

                new Ship(){Id = 103, Name = "AIDAluna", SalesUnitId = 5},
                new Ship(){Id = 104, Name = "Atlantic Star", SalesUnitId = 5},
                new Ship(){Id = 105, Name = "Caribbean Princess", SalesUnitId = 5},

                new Ship(){Id = 106, Name = "AIDAcara", SalesUnitId = 6},
                new Ship(){Id = 107, Name = "The Big Red Boat", SalesUnitId = 6},
                new Ship(){Id = 108, Name = "Carnival Breeze", SalesUnitId = 6},
            };

            context.Ships.AddRange(_shipsList);

            var _BookingList = new[]
            {
                new Booking(){ Id = 2000, BookingDate = new DateTime(2016,05,16),Price = 4321, ShipId=100},
                new Booking(){ Id = 2001, BookingDate = new DateTime(2015,02,11),Price = 3544.59, ShipId=101},

                new Booking(){ Id = 2002, BookingDate = new DateTime(2015,04,11),Price = 500.59, ShipId=101},
                new Booking(){ Id = 2003, BookingDate = new DateTime(2015,07,11),Price = 200, ShipId=101},

                new Booking(){ Id = 2004, BookingDate = new DateTime(2015,04,11),Price = 500.59, ShipId=102},
                new Booking(){ Id = 2005, BookingDate = new DateTime(2015,07,23),Price = 200, ShipId=102},


                new Booking(){ Id = 2006, BookingDate = new DateTime(2016,04,16),Price = 4321, ShipId=103},
                new Booking(){ Id = 2007, BookingDate = new DateTime(2015,02,11),Price = 3544.59, ShipId=103},

                new Booking(){ Id = 2008, BookingDate = new DateTime(2017,04,11),Price = 500.59, ShipId=104},
                new Booking(){ Id = 2009, BookingDate = new DateTime(2017,07,11),Price = 200, ShipId=104},

                new Booking(){ Id = 2010, BookingDate = new DateTime(2018,04,11),Price = 500.59, ShipId=105 },
                new Booking(){ Id = 2011, BookingDate = new DateTime(2018,07,23),Price = 200, ShipId=105},


                new Booking(){ Id = 2012, BookingDate = new DateTime(2016,04,16),Price = 4321, ShipId=106},
                new Booking(){ Id = 2013, BookingDate = new DateTime(2015,02,11),Price = 3544.59, ShipId=106},

                new Booking(){ Id = 2014, BookingDate = new DateTime(2017,04,11),Price = 500.59, ShipId=107},
                new Booking(){ Id = 2015, BookingDate = new DateTime(2017,07,11),Price = 200, ShipId=107},

                new Booking(){ Id = 2016, BookingDate = new DateTime(2018,04,11),Price = 500.59, ShipId=108 },
                new Booking(){ Id = 2017, BookingDate = new DateTime(2018,07,23),Price = 200, ShipId=108},

            };

            context.Bookings.AddRange(_BookingList);
            context.SaveChanges();
        }

        public static void SeedCorrectDuration(AppDbContext context)
        {
            var _salesUnitsList = new[]
            {
                new SalesUnit(){ Id=1, Name="dreamlines.de", Country="Germany", Currency="€" },
                new SalesUnit(){ Id=2, Name="dreamlines.com.br", Country="Brazil", Currency="R$" },
                new SalesUnit(){ Id=3, Name="dreamlines.it", Country="Italy", Currency="€" },
            };
            context.SalesUnits.AddRange(_salesUnitsList);

            var _shipsList = new[]
            {
                new Ship(){Id = 3, Name = "AIDAbella", SalesUnitId = 1},
                new Ship(){Id = 4, Name = "AIDAblu", SalesUnitId = 1},
                new Ship(){Id = 11, Name = "AIDAstella", SalesUnitId = 1},

                new Ship(){Id = 7, Name = "AIDAluna", SalesUnitId = 2},
                new Ship(){Id = 18, Name = "Atlantic Star", SalesUnitId = 2},
                new Ship(){Id = 27, Name = "Caribbean Princess", SalesUnitId = 2},

                new Ship(){Id = 5, Name = "AIDAcara", SalesUnitId = 3},
                new Ship(){Id = 24, Name = "The Big Red Boat", SalesUnitId = 3},
                new Ship(){Id = 28, Name = "Carnival Breeze", SalesUnitId = 3},
            };

            context.Ships.AddRange(_shipsList);

            var _BookingList = new[]
            {
                new Booking(){ Id = 300, BookingDate = new DateTime(2016,01,16),Price = 4321, ShipId=3},
                new Booking(){ Id = 301, BookingDate = new DateTime(2016,02,11),Price = 3544.59, ShipId=3},

                new Booking(){ Id = 400, BookingDate = new DateTime(2016,02,11),Price = 500.59, ShipId=4},
                new Booking(){ Id = 401, BookingDate = new DateTime(2016,03,11),Price = 200, ShipId=4},

                new Booking(){ Id = 110, BookingDate = new DateTime(2015,01,11),Price = 500.59, ShipId=11},
                new Booking(){ Id = 111, BookingDate = new DateTime(2016,02,23),Price = 200, ShipId=11},


                new Booking(){ Id = 700, BookingDate = new DateTime(2016,02,20),Price = 4321, ShipId=7},
                new Booking(){ Id = 701, BookingDate = new DateTime(2015,02,11),Price = 3544.59, ShipId=7},

                new Booking(){ Id = 180, BookingDate = new DateTime(2017,04,11),Price = 500.59, ShipId=18},
                new Booking(){ Id = 181, BookingDate = new DateTime(2017,07,11),Price = 200, ShipId=18},

                new Booking(){ Id = 270, BookingDate = new DateTime(2018,04,11),Price = 500.59, ShipId=27 },
                new Booking(){ Id = 271, BookingDate = new DateTime(2018,07,23),Price = 200, ShipId=27},


                new Booking(){ Id = 500, BookingDate = new DateTime(2016,03,16),Price = 4321, ShipId=5},
                new Booking(){ Id = 501, BookingDate = new DateTime(2015,02,11),Price = 3544.59, ShipId=5},

                new Booking(){ Id = 240, BookingDate = new DateTime(2017,04,11),Price = 500.59, ShipId=24},
                new Booking(){ Id = 241, BookingDate = new DateTime(2017,07,11),Price = 200, ShipId=24},

                new Booking(){ Id = 280, BookingDate = new DateTime(2018,04,11),Price = 500.59, ShipId=28 },
                new Booking(){ Id = 281, BookingDate = new DateTime(2018,07,23),Price = 200, ShipId=28},

            };

            context.Bookings.AddRange(_BookingList);
            context.SaveChanges();
        }
    }
}
