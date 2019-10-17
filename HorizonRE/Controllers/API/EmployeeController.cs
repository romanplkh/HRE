using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using HorizonRE.Models;

namespace HorizonRE.Controllers.API
{
    public class EmployeeController : ApiController
    {

       HorizonContext db = new HorizonContext();

       // GET: AddEmployee
       [System.Web.Http.HttpGet]
       public IHttpActionResult  GetProvincesByCountry()
       {

          List<Province> provinces = db.Provinces.Where(p => p.CountryId == 2).ToList();
          return Ok(provinces);
       }
    }
}
