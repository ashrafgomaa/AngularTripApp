using System;
using System.Collections.Generic;
using System.Text;

namespace DreamLines.Models.UI.Models
{
    public class UIBooking
    {
        public int Id { get; set; }
        public string Currency { get; set; }
        public DateTime BookingDate { get; set; }
        public double Price { get; set; }
        public string ShipName { get; set; }
    }
}
