using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HorizonRE.Models
{
    public class Listing
    {
        public int ListingId { get; set; }
        [Display(Name = "Street Address")]
        [Required(ErrorMessage = "Provide street address")]
        public string StreetAddress { get; set; }

        [Required(ErrorMessage = "Provide city")]
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        [Display(Name = "Postal Code")]
        [Required(ErrorMessage = "Provide a postal code")]
       // [CustomAttribute.PostalCodeIsValid]
        public string PostalCode { get; set; }
        [Display(Name = "Sq. Area")]
        [Required(ErrorMessage = "Provide house area")]
        public string Area { get; set; }
        [Required(ErrorMessage = "Provide number of bedrooms")]
        public int Bedrooms { get; set; }
        [Required(ErrorMessage = "Provide number of bathrooms")]
        public int Bathrooms { get; set; }
        [Required(ErrorMessage = "Provide the price")]
        public decimal Price { get; set; }
        [Display(Name = "Contract Signed")]
        public bool ContractSigned { get; set; }
        [Display(Name = "Contract Start")]
        public DateTime? ListingStartDate { get; set; }
        public DateTime ListingEndDate { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public bool RenewNotificationSent { get; set; } 

        public string RenewDenialReason { get; set; }



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