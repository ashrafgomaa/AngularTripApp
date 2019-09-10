using System;
using System.Collections.Generic;
using System.Text;

namespace DreamLines.Models
{
    /// <summary>
    /// Model which represents the Ship table in database.
    /// </summary>
    public class Ship
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SalesUnitId { get; set; }
        public SalesUnit SalesUnitItem { get; set; }

        public List<Booking> BokkingsList { get; set; }
    }
}
