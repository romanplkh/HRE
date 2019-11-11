using HorizonRE.Models;
using HorizonRE.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Net;

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


        [HttpGet]
        public ActionResult Index(string lname, string date, int? page)
        {

            //Search appointments
            var appList = db.Appointments.Include("Employee").ToList()
                          .OrderBy(a => a.StartDate).AsQueryable();


            if (!string.IsNullOrEmpty(lname))
            {
                appList = appList.Where(s => s.Employee.LastName.ToLower()
                          .Contains(lname.ToLower()));
            }
            if (!string.IsNullOrEmpty(date))
            {
                DateTime searchDate = DateTime.Parse(date);
                appList = appList.Where(s => s.StartDate.Date == searchDate);
            }
            if (!string.IsNullOrEmpty(lname) && !string.IsNullOrEmpty(date))
            {
                DateTime searchDate = DateTime.Parse(date);
                appList = appList.Where(s => s.Employee.LastName.ToLower()
                          .Contains(lname.ToLower()) && s.StartDate.Date == searchDate);

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


            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "FullName", app.EmployeeId);
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "FullName", app.CustomerId);

            return View(app);
        }

        //POST: edit appointment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, StartDate, EndDate, ListingId, Comment, " +
            "CustomerId, EmployeeId")] Appointment app)
        {


            if (ModelState.IsValid)
            {
                app.StartDate = app.StartDate.AddMinutes(-15);

                app.EndDate = app.EndDate.AddMinutes(15);

                string errorMsg = CanAppBeScheduled(app);

                if (!string.IsNullOrEmpty(errorMsg))
                {
                    ViewBag.Msg = errorMsg;
                }
                else
                {
                    db.Entry(app).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                
            }

            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "FullName", app.EmployeeId);
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "FullName", app.CustomerId);



            return View();
        }

        //check for intersections
        private string CanAppBeScheduled(Appointment app)
        {
            string msg = string.Empty;

           //listing showing is booked for specified time
            var appInDB = db.Appointments.AsNoTracking().Where(a => a.ListingId == app.ListingId &&
                            a.Id != app.Id &&
                            (a.StartDate > app.StartDate && a.EndDate > app.EndDate) || 
                            (a.StartDate < app.StartDate && a.EndDate > app.EndDate)).FirstOrDefault();

            //agent has another app an that time
            var appInDbAgent = db.Appointments.AsNoTracking().Where(a => a.EmployeeId == app.EmployeeId &&
                            a.Id != app.Id &&
                            (a.StartDate > app.StartDate && a.EndDate > app.EndDate) ||
                            (a.StartDate < app.StartDate && a.EndDate > app.EndDate)).FirstOrDefault();

            if(appInDB != null)
            {
                return msg = "Sorry, another appointment is scheduled for this listing at " +
                    "that time";
                
            }
            else if(appInDbAgent != null)
            {
                return msg = "Sorry, another appointment is scheduled with this agent at" +
                   "that time";
            }

            return msg;

        }

    }
}