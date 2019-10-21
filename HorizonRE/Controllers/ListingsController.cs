using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HorizonRE.Models;

namespace HorizonRE.Controllers
{
    public class ListingsController : Controller
    {
        private HorizonContext db = new HorizonContext();

        // GET: Listings
        public ActionResult Index()
        {
            var listings = db.Listings.Include(l => l.CityArea).Include(l => l.Customer).Include(l => l.Employee);
            return View(listings.ToList());
        }

        // GET: Listings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Listing listing = db.Listings.Find(id);
            if (listing == null)
            {
                return HttpNotFound();
            }
            return View(listing);
        }

        // GET: Listings/Create
        public ActionResult Create()
        {
            ViewBag.AreaId = new SelectList(db.CityAreas, "AreaId", "Name");
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "FirstName");
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "FirstName");
            return View();
        }

        // POST: Listings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ListingId,StreetAddress,City,Province,Country,PostalCode,Area,Bedrooms,Bathrooms,Price,ContractSigned,ListingStartDate,ListingEndDate,CustomerId,EmployeeId,AreaId")] Listing listing)
        {
            if (ModelState.IsValid)
            {
                db.Listings.Add(listing);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AreaId = new SelectList(db.CityAreas, "AreaId", "Name", listing.AreaId);
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "FirstName", listing.CustomerId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "FirstName", listing.EmployeeId);
            return View(listing);
        }

        // GET: Listings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Listing listing = db.Listings.Find(id);
            if (listing == null)
            {
                return HttpNotFound();
            }
            ViewBag.AreaId = new SelectList(db.CityAreas, "AreaId", "Name", listing.AreaId);
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "FirstName", listing.CustomerId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "FirstName", listing.EmployeeId);
            return View(listing);
        }

        // POST: Listings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ListingId,StreetAddress,City,Province,Country,PostalCode,Area,Bedrooms,Bathrooms,Price,ContractSigned,ListingStartDate,ListingEndDate,CustomerId,EmployeeId,AreaId")] Listing listing)
        {
            if (ModelState.IsValid)
            {
                db.Entry(listing).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AreaId = new SelectList(db.CityAreas, "AreaId", "Name", listing.AreaId);
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "FirstName", listing.CustomerId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "FirstName", listing.EmployeeId);
            return View(listing);
        }

        // GET: Listings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Listing listing = db.Listings.Find(id);
            if (listing == null)
            {
                return HttpNotFound();
            }
            return View(listing);
        }

        // POST: Listings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Listing listing = db.Listings.Find(id);
            db.Listings.Remove(listing);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
