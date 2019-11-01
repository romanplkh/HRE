using HorizonRE.Models;
using HorizonRE.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HorizonRE.Controllers
{
    public class AppointmentController : Controller
    {

        HorizonContext db = new HorizonContext();
        // GET: Appointment
        [HttpGet]
        public ActionResult Add()
        {

            return View();
        }


        [HttpPost]
        public ActionResult Add(Appointment app = null, string email = "", string listingId = "")
        {

            //DROPDOWN
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "FullName");


            Customer customer = null;
            Listing listing = null;

            string currEmail = "";
            string currentListing = "";


            if (!string.IsNullOrEmpty(email))
            {
                customer = db.Customers.Where(cx => cx.Email.ToLower() == email.ToLower()).Single();
                currEmail = email;


            }

            if (!string.IsNullOrEmpty(listingId))
            {

                int lisId = Convert.ToInt32(listingId);
                listing = db.Listings.Where(l => l.ListingId == lisId).Single();
                currentListing = listingId;

            }






            if (customer != null)
            {
                ViewBag.Customer = customer;

                if (listingId != null)
                {
                    ViewBag.Listing = listing;
                    ViewBag.CurrentListing = currentListing;

                    app.CustomerId = customer.CustomerId;

                    if (app != null && ModelState.IsValid)
                    {

                        db.Appointments.Add(app);
                        db.SaveChanges();

                        return RedirectToAction("Add"); 
                    }


                }

            }


            ViewBag.EmailCurrent = currEmail;
            return View("Add");
        }




        public Listing GetListing(int listingId)
        {
            return db.Listings.Where(l => l.ListingId == listingId).Single();
        }

    }
}