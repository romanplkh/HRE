using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HorizonRE.Models
{
    public class Appointment
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Please provide start date and time")]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Please provide end date and time")]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Please provide a comment")]
        public string Comment { get; set; }

        [Required(ErrorMessage = "Please provide a listing number")]
        public int ListingId { get; set; }
 
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }





    }
}