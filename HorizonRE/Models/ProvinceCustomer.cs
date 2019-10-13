using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HorizonRE.Models
{
    public class ProvinceCustomer
    {
        public int Id { get; set; }

        public int ProvinceId { get; set; }
        public virtual Province Province { get; set; }

        public int CustomerId { get; set; }

    }
}