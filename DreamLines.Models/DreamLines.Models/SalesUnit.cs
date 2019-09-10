using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DreamLines.Models
{
    /// <summary>
    /// Model which represents the SalesUnit table in database.
    /// </summary>
    public class SalesUnit
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Currency { get; set; }

        public List<Ship> ShipsList { get; set; }
    }
}
