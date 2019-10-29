using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Linq;
using System.Web;

namespace HorizonRE.Models
{
   public class Employee
   {
      public int EmployeeId { get; set; }

      [Required(ErrorMessage = "First Name is required")]
      [Display(Name = "First Name" )]
      [StringLength(20, MinimumLength = 3)]
      public string FirstName { get; set; }

      [Required(ErrorMessage = "Last Name is required")]
      [Display(Name = "Last Name" )]
      [StringLength(30, MinimumLength = 3)]
      public string  LastName { get; set; }


      [Display(Name = "Middle Name" )]
      [StringLength(20)]
      public string  MiddleName { get; set; }

      [Required(ErrorMessage = "SIN is required")]
      [Display(Name = "SIN" )]
      [StringLength(11, MinimumLength = 11)]
      [RegularExpression(@"^\d{3}-\d{3}-\d{3}$", ErrorMessage = "SIN has incorrect format")]
      public string SIN { get; set; }

      [NotMapped] 
      public string Country { get; set; }
      [NotMapped] 
      public string Province { get; set; }

      [Required(ErrorMessage = "Street address is required")]
      [Display(Name = "Street address" )]
      [StringLength(100, MinimumLength = 3)]
      public string StreetAddress { get; set; }


      [Required(ErrorMessage = "City is required")]
      [StringLength(50, MinimumLength = 3)]
      public string City { get; set; }

      [Required(ErrorMessage = "Postal code is required")]
      [Display(Name = "Postal Code" )]
      [StringLength(6, MinimumLength = 6)]
      [RegularExpression(@"^[ABCEGHJ-NPRSTVXY]\d[ABCEGHJ-NPRSTV-Z]\d[ABCEGHJ-NPRSTV-Z]\d$", ErrorMessage = "Postal code has incorrect format")]
      public string PostalCode { get; set; }

      [Required(ErrorMessage = "Home phone is required")]
      [StringLength(14)]
      [RegularExpression(@"^[(]\d{3}[)][\s]\d{3}[-]\d{4}$", ErrorMessage = "Phone has a wrong format.Format should be (XXX) XXX-XXXX")]
      [Display(Name = "Home Phone")]
      public string HomePhone  { get; set; }

      [Required(ErrorMessage = "Cell phone is required")]
      [StringLength(14)]
      [RegularExpression(@"^[(]\d{3}[)][\s]\d{3}[-]\d{4}$", ErrorMessage = "Phone has a wrong format.Format should be (XXX) XXX-XXXX")]
      [Display(Name = "Cell Phone")]
      public string CellPhone { get; set; }
      
      [Required(ErrorMessage = "Office phone is required")]
      [StringLength(14)]
      [RegularExpression(@"^[(]\d{3}[)][\s]\d{3}[-]\d{4}$", ErrorMessage = "Phone has a wrong format.Format should be (XXX) XXX-XXXX")]
      [Display(Name = "Office Phone")]
      public string OfficePhone { get; set; }

      [Required(ErrorMessage = "Office email is required")]
      [StringLength(50, MinimumLength = 3)]
      [EmailAddress]
      [Display(Name = "Office email")]
      public string OfficeEmail { get; set; }

      [Required(ErrorMessage = "Date of birth is required")]
      [DataType(DataType.Date)]
      [CustomAttribute.AgeIsValid]
      [Display(Name = "Date of birth")]
      public DateTime DOB { get; set; }

   
      [Required(ErrorMessage = "Reference added by is required")]
      [IntegerValidator]
      [Display(Name = "Employee added by")]
      public int AddedBy { get; set; }

      [Required(ErrorMessage = "Hire date is required")]
      [DataType(DataType.Date)]
      [CustomAttribute.HireDateIsValid]
      [Display(Name = "Hire date")]
      public DateTime HireDate { get; set; }

      
      public string Password { get; set; }



      public int EmployeeProvinceId { get; set; }
      public ICollection<ProvinceEmployee> ProvinceEmployees { get; set; }


      public virtual ICollection<ImageFile> Images { get; set; }

      
     // public Authentication Authentication { get; set; }
      
      
      public  int AccessLevelId { get; set; }
      public virtual  AccessLevel AccessLevel { get; set; }


      public virtual ICollection<Listing> Listings { get; set; }
      public virtual ICollection<Appointment> Appointments { get; set; }




   }
}