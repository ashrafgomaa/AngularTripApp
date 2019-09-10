using DreamLines.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DreamLines.Entity
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }
        /// <summary>
        /// Database context for sales unit table
        /// </summary>
        public DbSet<SalesUnit> SalesUnits { get; set; }
        /// <summary>
        /// Database context for Ships table
        /// </summary>
        public DbSet<Ship> Ships { get; set; }
        /// <summary>
        /// Database context for bookings table
        /// </summary>
        public DbSet<Booking> Bookings { get; set; }
    }
}
