using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HorizonRE.Models;

namespace HorizonRE.Controllers
{
    public class EmployeeController : Controller
    {
       private  HorizonContext db = new HorizonContext();
       // GET: AllEmployee
       [HttpGet]
       public ActionResult Index()
       {


          return View(db.Employees.ToList());
       }

       // GET: AddEmployee
       [HttpGet]
       public ActionResult Add()
       {
          ViewBag.CountryData = new SelectList(db.Countries, "CountryId", "Name");
          ViewBag.ProvinceData = new SelectList(db.Provinces, "ProvinceId", "Name");
          return View();
       }


       // POST: AddEmployee
       [HttpPost]
       [ValidateAntiForgeryToken]
       public ActionResult Add([Bind(Include = "ID,FirstName,LastName, MiddleName, SIN, StreetAddress, Country, Province, PostalCode, HomePhone, CellPhone, OfficeEmail, OfficePhone, DOB, AddedBy, HireDate")] Employee employee)
       {

          if (ModelState.IsValid)
          {
             db.Employees.Add(employee);
             db.SaveChanges();
             return RedirectToAction("Index");
          }
        

          return View();
       }
    }
}