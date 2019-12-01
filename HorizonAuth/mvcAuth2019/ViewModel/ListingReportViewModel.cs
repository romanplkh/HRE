using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HorizonRE.Models;

namespace HorizonRE.ViewModel
{
    public class ListingReportViewModel
    {
        public string Status { get; set; }
        public IEnumerable<Listing> listing { get; set; }     
    }
  
}