using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HorizonRE.Models
{
    public class RoleName : IdentityRole
    {
        public const string BROKER = "Broker";
        public const string AGENT = "Agent";
        public const string MANAGER = "Manager";
        public const string CUSTOMER = "Customer";
        public const string EMPLOYEE = "Employee";
    }
}