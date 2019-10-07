using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HorizonRE.Models
{
   internal class CustomAttribute
   {
      internal class AgeIsValid: ValidationAttribute
      {
         protected override ValidationResult IsValid( object value, ValidationContext validationContext )
         {

            DateTime date = (DateTime) value;

            if (date.AddYears(18) > DateTime.Now)
            {
               return ValidationResult.Success;
            }
            return new ValidationResult("Employee cannot be younger 18 years old");
         }
      }

      internal class HireDateIsValid: ValidationAttribute
      {
         protected override ValidationResult IsValid( object value, ValidationContext validationContext )
         {
            if (DateTime.Parse(value.ToString()) <= DateTime.Now)
            {
               return ValidationResult.Success;
            }
            return new ValidationResult("Hire date cannot be later than current date");
         }
      }
   }
}