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
            var customers = db.Customers.ToList().OrderBy(c => c.LastName);
            //var cust = db.Customers.Include(c => c.ProvinceCustomers)
               // .Where(c => c.CustomerId == 3)
               // .ToList();

            return View(customers);

            //return View();
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
            "StreetAddress, City, Country, Province, PostalCode, Phone, Email, DOB, ProvinceList," +
            "ProvinceCustomer")] Customer customer
            )
        {
            if (ModelState.IsValid)
            {
                int provId = Convert.ToInt32(Request.Form["ProvinceList"]);

                db.Customers.Add(customer);
                db.SaveChanges();

                int customerId = customer.CustomerId;

                ProvinceCustomer pc = new ProvinceCustomer();
                pc.CustomerId = customerId;
                pc.ProvinceId = provId;

                db.ProvinceCustomers.Add(pc);

                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewData["CountryList"] = new SelectList(db.Countries, "CountryId", "Name");
            ViewData["ProvinceList"] = new SelectList(db.Provinces, "ProvinceId", "Name");
            return View();
        }
    }
}