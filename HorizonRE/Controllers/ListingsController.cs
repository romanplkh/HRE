using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HorizonRE.Models;
using PagedList;

namespace HorizonRE.Controllers
{
    public class ListingsController : Controller
    {

        private HorizonContext db = new HorizonContext();
        // GET: Listings
        [HttpGet]
        public ActionResult Index(int? page)
        {


            var listings = db.Listings.Include(i => i.Images).ToList();
            


            ViewBag.CountryList = new SelectList(db.Countries.ToList(), "CountryId","Name" );
            ViewBag.ProvincesList = new SelectList(db.Provinces.ToList(), "ProvinceId", "Name");



            int pageSize = 5;
            int pageNumber = (page ?? 1);
            
            return View(listings.ToPagedList(pageNumber, pageSize));

        }

        //GET: listing/details/id
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var listings = db.Listings.Include(img => img.Images).Include(area => area.CityArea).ToList();

            var listing = listings.Find(l => l.ListingId == id);

            if (listing == null)
            {
                return HttpNotFound();
            }
            return View(listing);
        }
    }
}