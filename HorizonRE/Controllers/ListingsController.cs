using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HorizonRE.Models;

namespace HorizonRE.Controllers
{
    public class ListingsController : Controller
    {

        private HorizonContext db = new HorizonContext();
        // GET: Listings
        [HttpGet]
        public ActionResult Index()
        {


            var listings = db.Listings.Include(i => i.Images).ToList();
            


            ViewBag.CountryList = new SelectList(db.Countries.ToList(), "CountryId","Name" );
            ViewBag.ProvincesList = new SelectList(db.Provinces.ToList(), "ProvinceId", "Name");


            
            return View(listings);

        }
    }
}