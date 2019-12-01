using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HorizonRE.Models;

namespace HorizonRE.ViewModel
{
    public class ListingCustomerViewModel
    {
        public Listing Listings { get; set; }
        public List<Customer> Customers { get; set; }
    }
}