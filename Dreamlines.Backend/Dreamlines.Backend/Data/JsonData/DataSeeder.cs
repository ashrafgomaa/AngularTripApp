using DreamLines.Entity;
using DreamLines.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dreamlines.Backend.Data.JsonData
{
    /// <summary>
    /// Helper class which will seed data in the json file into database
    /// </summary>
    public class DataSeeder
    {
        /// <summary>
        /// Check if data already existed in database then return, otherwise seed the data to database.
        /// </summary>
        /// <param name="app"></param>
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                context.Database.EnsureCreated();
                if (context.SalesUnits.Any())
                    return;

                var salesunitsList = new List<SalesUnit>();
                var shipsList = new List<Ship>();
                var bookingList = new List<Booking>();

                //read data then seed them to database.
                const string path = @".\Data\JsonData\TrialDayData.json";
                using (var reader = System.IO.File.OpenText(path))
                {
                    string fileContent = reader.ReadToEnd();
                    var itemsList = JObject.Parse(fileContent);
                    foreach (var jt in itemsList.AsJEnumerable())
                    {
                        if (jt.ToObject<JProperty>().Name == "salesUnits")
                        {
                            foreach (var item in jt.Values().ToArray())
                            {
                                salesunitsList.Add(new SalesUnit() { Id = (int)item["id"], Country = item["country"].ToString(), Currency = item["currency"].ToString(), Name = item["name"].ToString() });
                            }

                        }
                        else if (jt.ToObject<JProperty>().Name == "ships")
                        {
                            foreach (var item in jt.Values().ToArray())
                            {
                                shipsList.Add(new Ship() { Id = (int)item["id"], SalesUnitId = (int)item["salesUnitId"], Name = item["name"].ToString() });
                            }
                        }
                        else if (jt.ToObject<JProperty>().Name == "bookings")
                        {
                            foreach (var item in jt.Values().ToArray())
                            {
                                bookingList.Add(new Booking() { Id = (int)item["id"], ShipId = (int)item["shipId"], BookingDate = DateTime.Parse(item["bookingDate"].ToString()), Price = double.Parse(item["price"].ToString()) });
                            }
                        }
                    }
                }

                context.Database.OpenConnection();
                try
                {
                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.SalesUnits ON;");
                    context.SalesUnits.AddRange(salesunitsList);
                    context.SaveChanges();
                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.SalesUnits OFF");

                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Ships ON;");
                    context.Ships.AddRange(shipsList);
                    context.SaveChanges();
                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Ships OFF;");

                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Bookings ON;");
                    context.Bookings.AddRange(bookingList);
                    context.SaveChanges();
                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Bookings OFF;");
                }
                finally
                {
                    context.Database.CloseConnection();
                }


            }
        }

    }
}
