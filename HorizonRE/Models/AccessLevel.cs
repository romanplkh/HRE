using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace HorizonRE.Models
{
   public class AccessLevel
   {
      public int AccessLevelId { get; set; }
      public string Type { get; set; }

      
      public virtual ICollection<Employee> Employees { get; set; }
   }
}