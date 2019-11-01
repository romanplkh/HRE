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
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string Comment { get; set; }

        [Required]
        public int ListingId { get; set; }
  

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }





    }
}