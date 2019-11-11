using HorizonRE.Models;
using HorizonRE.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
                customer = db.Customers.Where(cx => cx.Email.ToLower() == email.ToLower()).SingleOrDefault();

                if (customer == null)
                {

                    ViewBag.Error = "Customer with this email does not exist.";
                    
                }
        
                currEmail = email;

                ViewBag.Customer = customer;
              
            }
            




            if (!string.IsNullOrEmpty(listingId))
            {

                if (!int.TryParse(listingId, out int listId))
                {
                    ViewBag.Error = "Listing with this id does not exist";
                    ViewBag.Customer = customer;
                    return View("Add");
                }

                int lisId = Convert.ToInt32(listingId);
                listing = db.Listings.Where(l => l.ListingId == lisId).SingleOrDefault();

                if (listing == null)
                {
                    ViewBag.Error = "Listing with this id does not exist";
                    ViewBag.Customer = customer;
                    return View("Add");
                }
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


                    app.StartDate = app.StartDate.AddMinutes(-15);
                    app.EndDate = app.EndDate.AddMinutes(15);


                    if(!ValidateAppointment(app.StartDate, app.EndDate))
                    {

                        if (/*app != null &&*/ ModelState.IsValid)
                        {

                            db.Appointments.Add(app);
                            db.SaveChanges();

                            return RedirectToAction("Add");
                        }

                    }

                 



                }

            }


            ViewBag.EmailCurrent = currEmail;
            return View("Add");
        }


        private bool ValidateAppointment(DateTime start, DateTime end, int customerId)
        {



            if(start.Date != end.Date)
            {
                ViewBag.Error = "Showing can start and end only in same day";
                return false;
            }



            if(start.Date > end.Date)
            {
                ViewBag.Error = "Showing start date cannot be greater than end date";
                return false;
            }

            if(start.Date < DateTime.Now.Date)
            {
                ViewBag.Error = "Showing start date cannot be in the past";
                return false;
            }



            if(start.Hour > end.Hour)
            {
                ViewBag.Error = "Showing start time cannot be greater than end time";
                return false;
            }


            if (start.Hour < 8 || start.Hour > 16 && end.Hour > 17)
            {
                ViewBag.Error = "Showing can only happen between 8AM and 5PM";
                return false;
            }


            bool anotherAgent = db.Appointments.Where(
                app => app.CustomerId == customerId 
                && app.StartDate == start && app.EndDate == end
                ).Count() > 0;



            return true;
        }

        public Listing GetListing(int listingId)
        {
            return db.Listings.Where(l => l.ListingId == listingId).Single();
        }


        [HttpGet]
        public ActionResult Index(string searchString, string date, int? page)
        {

            //Search appointments
            var appList = db.Appointments.Include("Employee").ToList()
                          .OrderBy(a => a.StartDate).AsQueryable();


            if (!string.IsNullOrEmpty(searchString))
            {
                appList = appList.Where(s => s.Employee.LastName.ToLower()
                          .Contains(searchString.ToLower()));
            }
            if (!string.IsNullOrEmpty(date))
            {
                DateTime searchDate = DateTime.Parse(date);
                appList = appList.Where(s => s.StartDate.Date == searchDate);
            }
            if (!string.IsNullOrEmpty(searchString) && !string.IsNullOrEmpty(date))
            {
                DateTime searchDate = DateTime.Parse(date);
                appList = appList.Where(s => s.Employee.LastName.ToLower()
                          .Contains(searchString.ToLower()) && s.StartDate.Date == searchDate);

            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(appList.ToPagedList(pageNumber, pageSize));
        }

        // GET: edit appointment
        [HttpGet]
        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Appointment app = db.Appointments.Find(Id);
            if (app == null)
            {
                return HttpNotFound();
            }

            int agentId = app.EmployeeId;
            int customerId = app.CustomerId;

            ViewBag.AgentsList = new SelectList(db.Employees.Where(e => e.EmployeeId == agentId), "EmployeeId", "FullName", agentId);
            ViewBag.CustomersList = new SelectList(db.Customers.Where(c => c.CustomerId == customerId), "CustomerId", "FullName", customerId);

            return View(app);
        }

        //POST: edit appointment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, StartDate, EndDate, ListingId, Comment, " +
            "CustomerId, EmployeeId")] Appointment app)
        {
            //CHECK if there is time intersections


            if (ModelState.IsValid)
            {

                app.CustomerId = Convert.ToInt32(Request.Form["CustomersList"]);
                app.EmployeeId = Convert.ToInt32(Request.Form["AgentsList"]);

                app.StartDate = app.StartDate.AddMinutes(-15);

                app.EndDate = app.EndDate.AddMinutes(15);

                db.Entry(app).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AgentsList = new SelectList(db.Employees, "EmployeeId", "FullName");
            ViewBag.CustomersList = new SelectList(db.Customers, "CustomerId", "FullName");


            return View();
        }

    }
}