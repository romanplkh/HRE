using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mvcAuth2019.Models
{
    public class Contact
    {
        [Required] public int ID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(maximumLength: 25)]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        [StringLength(maximumLength: 13)]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(maximumLength: 30)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Street Address")]
        [StringLength(maximumLength: 50)]
        public string StreetAddress { get; set; }

        [Required]
        [Display(Name = "City")]
        [StringLength(maximumLength: 30)]
        public string City { get; set; }

        [Required]
        [Display(Name = "Province")]
        [StringLength(maximumLength: 12)]
        public string Province { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(maximumLength: 15)]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}