using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HorizonRE.Models
{
   public class Authentication
   {

      public int AuthenticationId { get; set; }

      public string Username { get; set; }
      public string Password { get; set; }

     // public virtual AccessLevel AccessLevel { get; set; }
      //public  int AuthEmployeeId { get; set; }
      //public  Employee Employee { get; set; }
   }
}