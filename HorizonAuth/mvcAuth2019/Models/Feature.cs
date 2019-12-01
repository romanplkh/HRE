using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HorizonRE.Models
{
    public class Feature
    {
        public int Id { get; set; }
        public string Name { get; set; }

     
        public virtual ICollection<Listing> Listings { get; set; }
    }
}