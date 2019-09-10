using System;
using System.Collections.Generic;
using System.Text;

namespace DreamLines.Models.UI.Models
{
    public class BookingPage
    {
        public int TotalCount { get; set; }
        public int PageCount { get; set; }
        public List<UIBooking> BookingList { get; set; }
        
    }
}
