using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HorizonRE.Models;
using HorizonRE.ViewModel;

namespace HorizonRE.Controllers
{
    public class AppointmentController : Controller
    {

        HorizonContext db = new HorizonContext();
        // GET: Appointment
        [HttpGet]
        public ActionResult Add()
        {
            CustomerListing viewModel = new CustomerListing();

            return View(viewModel);
        }

   

        [HttpPost]
        public ActionResult GetCustomer(string email = null, string listingId = null )
        {
            if(string.IsNullOrEmpty(email))
            {
                email = Request.Form["emailHidden"];
            }
            Customer customer = db.Customers.Where(cx => cx.Email.ToLower() == email.ToLower()).Single();

            CustomerListing viewModel = new CustomerListing();

            if(customer != null)
            {
                viewModel.Customer = customer;

                //get listingId
                if(listingId != null)
                {
                    int id = Convert.ToInt32(listingId);
                    Listing listing = GetListing(id);

                    viewModel.Listing = listing;
                }

                
            }
            Appointment app = new Appointment();
    
            //TODO -----> NEED TO FIGURE OUT HOW TO SAVE APP TO DB
            if (app != null)
            {
                db.Appointments.Add(app);
                db.SaveChanges();

                return View();
            }
            
            

        ViewBag.AgentsList = new SelectList(db.Employees.ToList(), "EmployeeId", "FullName");

            return View("Add", viewModel);
        }


        public Listing GetListing(int listingId)
        {
            return db.Listings.Where(l => l.ListingId == listingId).Single();
        }

    }
}