using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HorizonRE.Models;
namespace HorizonRE.ViewModel
{
    public class CustomerListing
    {
        public Customer Customer { get; set; }
        public Listing Listing { get; set; }
        public Appointment Appointment { get; set; }
    }
}