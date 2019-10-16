using System;
using System.Collections.Generic;
using System.Linq;
using System.Web; 
using System.Web.Mvc;
using HorizonRE.Models;
using System.Data.Entity;

namespace HorizonRE.Controllers
{
    public class CustomerController : Controller
    {
        private HorizonContext db = new HorizonContext();
        // GET: all customers
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

        // GET: country and province for form
        [HttpGet]
        public ActionResult AddCustomer()
        {
            ViewBag.CountryList = new SelectList(db.Countries, "CountryId", "Name");
            ViewBag.ProvinceList = new SelectList(db.Provinces, "ProvinceId", "Name");
            return View();
        }

        //POST: add new customer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCustomer([Bind(Include = "CustomerId,FirstName, LastName, MiddleName, " +
            "StreetAddress, City, PostalCode, Phone, Email, DOB, ProvinceList," +
            "ProvinceCustomer, CustomerProvinceId")] Customer customer
            )
        {
            if (ModelState.IsValid)
            {
                int provId = Convert.ToInt32(Request.Form["ProvinceList"]);
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
            ViewData["ProvinceList"] = new SelectList(db.Provinces, "ProvinceId", "Name");
            return View();
        }
    }
}