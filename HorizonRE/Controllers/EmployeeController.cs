using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
         ViewBag.CountryList = new SelectList(db.Countries, "CountryId", "Name");
         ViewBag.ProvinceList = new SelectList(db.Provinces.Where(p => p.CountryId == 1), "ProvinceId", "Name");
         return View();
      }


      // POST: AddEmployee
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Add([Bind(Include = "EmployeeId,FirstName,LastName," +
          "MiddleName,SIN,StreetAddress,City,PostalCode,HomePhone," +
          "CellPhone,OfficePhone,OfficeEmail,DOB,AddedBy,HireDate, " +
          "Provinces, ProvinceEmployee, EmployeeProvinceId")]
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


      // GET: Edit
      public ActionResult Edit(int? id)
      {
         if (id == null)
         {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }

         Employee e = db.Employees.Find(id);
         if (e == null)
         {
            return HttpNotFound();
         }


         //Find employee province 
         int provId = e.EmployeeProvinceId;

         //Find employee country
         var countId = db.Provinces.ToList().First(pr => pr.ProvinceId == e.EmployeeProvinceId).CountryId;

         //Add country to list
         ViewBag.CountryList = new SelectList(db.Countries, "CountryId", "Name", countId);
         ViewBag.ProvinceList = new SelectList(db.Provinces.Where(p => p.CountryId == 1), "ProvinceId", "Name", provId);
         return View(e);
      }

      //POST: Edit
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Edit([Bind(Include = "EmployeeId,FirstName,LastName,MiddleName,SIN,StreetAddress,City,PostalCode,HomePhone,CellPhone,OfficePhone,OfficeEmail,DOB,AddedBy,HireDate, EmployeeProvinceId, CountryList, ProvinceList")]
         Employee employee)
      {
         if (ModelState.IsValid)
         {
            employee.EmployeeProvinceId = Convert.ToInt32(Request.Form["ProvincesList"]);
            db.Entry(employee).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
         }

         ViewBag.CountryList = new SelectList(db.Countries, "CountryId", "Name");
         ViewBag.ProvinceList = new SelectList(db.Provinces, "ProvinceId", "Name");

         return View();
      }

      // POST: Employee/Delete/5
      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public ActionResult DeleteConfirmed(int id)
      {
         Employee emp = db.Employees.Find(id);
         db.Employees.Remove(emp);
         db.SaveChanges();
         return RedirectToAction("Index");
      }



      // GET: Provinces in Country for AddEmployee
      [HttpGet]
      public JsonResult  GetProvincesByCountry(int countryId)
      {
         var provinces = db.Provinces.Where(p => p.CountryId == countryId).Select(t=> new
         {
            ProvinceId = t.ProvinceId,
            Name = t.Name
         });
         return Json(provinces, JsonRequestBehavior.AllowGet);
      }

     
   }

   


}