using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HorizonRE.Models
{
    public class Listing
    {
        public int ListingId { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string Area { get; set; }
        public string Bedrooms { get; set; }
        public double Bathrooms { get; set; }
        public decimal Price { get; set; }
        public bool ContractSigned { get; set; }
        public DateTime ListingStartDate { get; set; }
        public DateTime ListingEndDate { get; set; }



        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }



        public virtual ICollection<ImageFile> Images { get; set; }


        public int AreaId { get; set; }
        public CityArea CityArea { get; set; }



    }
}