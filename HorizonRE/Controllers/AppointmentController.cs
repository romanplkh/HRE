using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HorizonRE.Models;

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
        public ActionResult GetCustomer(string email)
        {
            Customer customer = db.Customers.First(cx => cx.Email.ToLower() == email.ToLower());

            Appointment app = new Appointment();

            app.Customer = customer;

            return View("Add", app);
        }
    }
}