using System;
using System.Collections.Generic;
using System.Text;

namespace DreamLines.Models.UI.Models
{
    public class UISalesUnit
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string Currency { get; set; }
        public string Name { get; set; }
        public double TotalPrice { get; set; }
    }
}
