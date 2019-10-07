using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
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

            if (date.AddYears(19) > DateTime.Now)
            {
               return ValidationResult.Success;
            }
            return new ValidationResult("Employee cannot be younger than 19 years old");
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
        internal class PostalCodeIsValid : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                string usZipcodePattern = @"^\d{5}(?:[-\s]\d{4})?$";
                string caPostalcodePattern = @"^[ABCEGHJ-NPRSTVXY]\d[ABCEGHJ-NPRSTV-Z]\d[ABCEGHJ-NPRSTV-Z]\d$";

                Match matchUs = Regex.Match(value.ToString(), usZipcodePattern, RegexOptions.IgnoreCase);
                Match matchCa = Regex.Match(value.ToString(), caPostalcodePattern, RegexOptions.IgnoreCase);
                if (matchUs.Success || matchCa.Success)
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult("Postal code has incorrect format.");
            }
        }
    }
}