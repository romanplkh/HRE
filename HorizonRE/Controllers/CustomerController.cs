using System;
using System.Collections.Generic;
using System.Linq;
using System.Web; 
using System.Web.Mvc;
using HorizonRE.Models;
using System.Data.Entity;
using System.Net;

namespace HorizonRE.Controllers
{
    public class CustomerController : Controller
    {
        private HorizonContext db = new HorizonContext();
        // GET: all customers and related data
        [HttpGet]
        public ActionResult Index()
        {
            var customerList = db.Customers.ToList().OrderBy(c => c.LastName);
            var provinces = db.Provinces.ToList();
            var countries = db.Countries.ToList();


            foreach (var customer in customerList)
            {
                var p = provinces.First(el => el.ProvinceId == customer.CustomerProvinceId).Name;
                var countId = provinces.First(pr => pr.ProvinceId == customer.CustomerProvinceId).CountryId;
                var countryName = countries.First(c => c.CountryId == countId).Name;

                customer.Province = p;
                customer.Country = countryName;
            }

            return View(customerList);

        }

        // GET: countries and provinces(Canadian by default) for form
        [HttpGet]
        public ActionResult AddCustomer()
        {
            ViewBag.CountryList = new SelectList(db.Countries, "CountryId", "Name");
            ViewBag.ProvincesList = new SelectList(db.Provinces.Where(c => c.CountryId == 1), "ProvinceId", "Name");
            return View();
        }

        //POST: add new customer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCustomer([Bind(Include = "CustomerId,FirstName, LastName, MiddleName, " +
            "StreetAddress, City, PostalCode, Phone, Email, DOB, ProvincesList," +
            "ProvinceCustomer, CustomerProvinceId")] Customer customer
            )
        {
            if (ModelState.IsValid)
            {
                int provId = Convert.ToInt32(Request.Form["ProvincesList"]);
                customer.CustomerProvinceId = provId;
                db.Customers.Add(customer);
                db.SaveChanges();

                int customerId = customer.CustomerId;

                ProvinceCustomer pc = new ProvinceCustomer();
                pc.CustomerId = customerId;
                pc.ProvinceId = provId;

                db.ProvincesCustomers.Add(pc);

                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewData["CountryList"] = new SelectList(db.Countries, "CountryId", "Name");
            ViewData["ProvincesList"] = new SelectList(db.Provinces.Where(c => c.CountryId == 1), "ProvinceId", "Name");
            return View();
        }

        // GET: Provinces in Country for AddCustomer
        [HttpGet]
        public JsonResult GetProvincesByCountry(int countryId)
        {
            var provinces = db.Provinces.Where(p => p.CountryId == countryId).Select(t => new
            {
               t.ProvinceId,
               t.Name
            });
            return Json(provinces, JsonRequestBehavior.AllowGet);
        }


        // GET: edit customer
        [HttpGet]
        public ActionResult EditCustomer(int? customerId)
        {
            if (customerId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Customer customer = db.Customers.Find(customerId);
            if (customer == null)
            {
                return HttpNotFound();
            }
            //Find the province 
            int provId = customer.CustomerProvinceId;

            //Find the country
            var countId = db.Provinces.ToList().First(pr => pr.ProvinceId == customer.CustomerProvinceId).CountryId;

            //Set values in dropdowns to customer's country/province
            ViewBag.CountryList = new SelectList(db.Countries, "CountryId", "Name", countId);
            ViewBag.ProvincesList = new SelectList(db.Provinces.Where(p => p.CountryId == countId), "ProvinceId", "Name", provId);
            return View(customer);
        }

        //POST: edit customer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCustomer([Bind(Include = "CustomerId,FirstName, LastName, MiddleName, " +
            "StreetAddress, City, PostalCode, Phone, Email, DOB, ProvincesList," +
            "ProvinceCustomer, CustomerProvinceId")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.CustomerProvinceId = Convert.ToInt32(Request.Form["ProvincesList"]);
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CountryList = new SelectList(db.Countries, "CountryId", "Name");
            ViewBag.ProvincesList = new SelectList(db.Provinces, "ProvinceId", "Name");

            return View();
        }

        // POST: delete customer record
        [HttpPost]       
        public ActionResult Delete()
        {
            int customerId = Convert.ToInt32(Request.Form["customerId"]);

            Customer customer = db.Customers.Find(customerId);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        
    }
}