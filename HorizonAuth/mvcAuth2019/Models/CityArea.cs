using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HorizonRE.Models
{
    public class CityArea
    {
        [Key]
        public int AreaId { get; set; }
        public string Name { get; set; }


        public ICollection<Listing> Listings { get; set; }
    }
}