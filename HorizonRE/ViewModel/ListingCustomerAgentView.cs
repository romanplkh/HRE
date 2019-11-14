using HorizonRE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HorizonRE.ViewModel
{
    public class ListingCustomerAgentView
    {
        public IEnumerable<Listing> Listings { get; set; }
        public IEnumerable<Customer> Customers { get; set; }
        public IEnumerable<Employee> Agents { get; set; }
        public HttpPostedFileBase[] Files { get; set; }

    }
}