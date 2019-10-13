using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HorizonRE.Models
{
   public class Province
   {
      public int ProvinceId { get; set; }
      public string Name { get; set; }

        public int CountryId { get; set; }
        public virtual Country Country { get; set; }


        //fk ProvinceEmployee
        public ICollection<ProvinceEmployee> ProvinceEmployees { get; set; }

        //fk to join table ProvinceCustomer
        public ICollection<ProvinceCustomer> ProvinceCustomers { get; set; }

    }
}