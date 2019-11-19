using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HorizonRE.Models
{

    //public enum ListingStatus
    //{
    //    Available,
    //    NotAvaiable,
    //    Expired,
    //    Sold
    //}

    public class Listing
    {
        public int ListingId { get; set; }
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }
        public string Area { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public decimal Price { get; set; }
        [Display(Name = "Contract Signed")]
        public bool ContractSigned { get; set; }
        [Display(Name = "Contract Start")]
        public DateTime ListingStartDate { get; set; }
        public DateTime ListingEndDate { get; set; }
        [Required]
        public string Status { get; set; }

        public bool RenewNotificationSent { get; set; } = false;



        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public int? EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }




        public virtual ICollection<ImageFile> Images { get; set; }


        [Display(Name = "City Area")]
        public int AreaId { get; set; }
        public CityArea CityArea { get; set; }


        public virtual ICollection<Feature> Features { get; set; }

    }
}