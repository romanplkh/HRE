using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HorizonRE.Models;

namespace HorizonRE.Controllers
{
    public class CustomerController : Controller
    {
        private HorizonContext db = new HorizonContext();
        // GET: all customers
        [HttpGet]
        public ActionResult Index()
        {
            // var customers = db.Customers.ToList().OrderBy(c => c.LastName);
            //return View(customers);

            return View();
        }

        // GET: country and province for form
        [HttpGet]
        public ActionResult AddCustomer()
        {
            ViewBag.CountryData = new SelectList(db.Countries, "CountryId", "Name");
            ViewBag.ProvinceData = new SelectList(db.Provinces, "ProvinceId", "Name");
            return View();
        }

        //POST: add new customer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCustomer([Bind(Include = "CustomerId, FirstName, LastName, MiddleName, " +
            "StreetAddress, City, Country, Province, PostalCode, Phone, Email, DOB")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}