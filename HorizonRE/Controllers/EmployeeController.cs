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
      private HorizonContext db = new HorizonContext();

      // GET: AllEmployee
      [HttpGet]
      public ActionResult Index()
      {
         var prov = db.Provinces.ToList();
         var coun = db.Countries.ToList();

         foreach (var emp in db.Employees.ToList())
         {
            var p = prov.First(el => el.ProvinceId == emp.EmployeeProvinceId).Name;
            var countId = prov.First(pr => pr.ProvinceId == emp.EmployeeProvinceId).CountryId;
            var countryName = coun.First(c => c.CountryId == countId).Name;

            emp.Province = p;
            emp.Country = countryName;
         }


         return View(db.Employees.ToList());
      }

      // GET: AddEmployee
      [HttpGet]
      public ActionResult Add()
      {
         ViewBag.CountryList = new SelectList(db.Countries, "CountryId", "Name", 2);

         var prov = db.Provinces.ToList().FindAll(p => p.CountryId == Convert.ToInt32(Request.Form["CountryList"]));
         ViewBag.ProvinceList = new SelectList(prov, "ProvinceId", "Name");
         return View();
      }


      // POST: AddEmployee
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Add([Bind(Include = "EmployeeId,FirstName,LastName,MiddleName,SIN,StreetAddress,City,PostalCode,HomePhone,CellPhone,OfficePhone,OfficeEmail,DOB,AddedBy,HireDate, Provinces, ProvinceEmployee, EmployeeProvinceId")]
         Employee employee)
      {
         if (ModelState.IsValid)
         {
            int provId = Convert.ToInt32(Request.Form["ProvincesList"]);

            employee.EmployeeProvinceId = provId;
            db.Employees.Add(employee);
            db.SaveChanges();


            int empId = employee.EmployeeId;


            ProvinceEmployee pe = new ProvinceEmployee();

            pe.EmployeeId = empId;
            pe.ProvinceId = provId;

            db.ProvincesEmployees.Add(pe);

            db.SaveChanges();


            return RedirectToAction("Index");
         }

         ViewBag.CountryList = new SelectList(db.Countries, "CountryId", "Name");
         ViewBag.ProvinceList = new SelectList(db.Provinces, "ProvinceId", "Name");


         return View();
      }
   }
}