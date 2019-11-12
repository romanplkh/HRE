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
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "Id,StartDate, EndDate, Comment, ListingId, CustomerId, EmployeeId, email, listingId, Customer, Listing, CurrentListing,EmailCurrent, Error")] Appointment app = null, string email = "", string listingId = "")
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

            ViewBag.EmailCurrent = currEmail;



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




                    if (/*app != null &&*/ ModelState.IsValid)
                    {


                        app.StartDate = app.StartDate.AddMinutes(-15);
                        app.EndDate = app.EndDate.AddMinutes(15);

                        ValidateAppointment(app, listing, currentListing, currEmail, customer);



                    }







                }

            }



            return View("Add");
        }


        private ActionResult ValidateAppointment(Appointment appointment, Listing listing, string currentListing, string currEmail, Customer customer)
        {


            bool isValid = true;


            bool appSameExist = db.Appointments.Where(
              app => (app.CustomerId == appointment.CustomerId) && (app.ListingId == appointment.ListingId)
              && (app.StartDate == appointment.StartDate) && (app.EndDate == appointment.EndDate)
              ).Count() > 0;

            bool agentHasAntoherAppointment = db.Appointments.Where(app => app.EmployeeId == appointment.EmployeeId && appointment.StartDate < app.EndDate && appointment.EndDate > app.StartDate && app.CustomerId != appointment.CustomerId).Count() > 0;


            bool anotherAgentHasAppointmentForThisProperty = db.Appointments.Where(app => app.ListingId == appointment.ListingId && appointment.StartDate < app.EndDate && appointment.EndDate > app.StartDate && app.EmployeeId != appointment.EmployeeId).Count() > 0;

            //if (appointment.StartDate.Date != appointment.EndDate.Date)
            //{
            //    ViewBag.Error = "Showing can start and end only in same day";
            //    isValid = false;
            //}
            //else


            if (appointment.StartDate.TimeOfDay == appointment.EndDate.TimeOfDay)
            {
                ViewBag.Error = "Showing cannot start and end in same time";
                isValid = false;
            }
            else



            if (appointment.StartDate.Date > appointment.EndDate.Date)
            {
                ViewBag.Error = "Showing start date cannot be greater than end date";
                isValid = false;
            }
            else
            if (appointment.StartDate.Date < DateTime.Now.Date)
            {
                ViewBag.Error = "Showing start date cannot be in the past";
                isValid = false;
            }
            else



            if (appointment.StartDate.Hour > appointment.EndDate.Hour)
            {
                ViewBag.Error = "Showing start time cannot be greater than end time";
                isValid = false;
            }
            else


            if (appointment.StartDate.Hour < 8 || appointment.StartDate.Hour > 16 && appointment.EndDate.Hour > 17)
            {
                ViewBag.Error = "Showing can only happen between 8AM and 5PM";
                isValid = false;
            }
            else

            if (appSameExist)
            {
                ViewBag.Error = "Showing with this customer already exist";
                isValid = false;
            }
            else

            if (agentHasAntoherAppointment)
            {
                ViewBag.Error = "Agent already has a showing between selected period of time";
                isValid = false;
            }
            else

            if (anotherAgentHasAppointmentForThisProperty)
            {
                ViewBag.Error = "Another agent already has a showing for this property between selected period of time";
                isValid = false;
            }



            if (isValid)
            {
                db.Appointments.Add(appointment);
                db.SaveChanges();
                ViewBag.Msg = "Appointment successfully added";

                ViewBag.EmailCurrent = "";
                ViewBag.Listing = null;
                ViewBag.CurrentListing = null;
                ViewBag.Customer = null;
                currEmail = "";
                currentListing = "";
                return RedirectToAction("Add", "Appointment");
            }



            ViewBag.EmailCurrent = currEmail;
            ViewBag.Listing = listing;
            ViewBag.CurrentListing = currentListing;
            ViewBag.Customer = customer;



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
            app.StartDate = app.StartDate.AddMinutes(15);
            app.EndDate = app.EndDate.AddMinutes(-15);

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