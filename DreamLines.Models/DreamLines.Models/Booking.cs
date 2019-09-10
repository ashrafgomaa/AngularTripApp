using System;
using System.Collections.Generic;
using System.Text;

namespace DreamLines.Models
{
    /// <summary>
    /// Model which represents the Bookings table in database.
    /// </summary>
    public class Booking
    {
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public double Price { get; set; }

        public int ShipId { get; set; }
        public Ship ShipItem { get; set; }

    }
}
