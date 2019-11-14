using HorizonRE.Models;
using HorizonRE.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HorizonRE.Controllers
{
    public class ListingsManagementController : Controller
    {
        private HorizonContext db = new HorizonContext();

        // GET: ListingsManagement
        public ActionResult Index(int? customerId)
        {
            //var listings = db.Listings.Include(l => l.CityArea).Include(l => l.Customer).Include(l => l.Employee);

            var viewModel = new ListingCustomerAgentView();

            viewModel.Customers = db.Customers;//.Include(l => l.Listings);


            if (customerId != null)
            {
                ViewBag.SelectedCustomer = customerId.Value;

                viewModel.Listings = db.Listings.Include(l => l.Employee);


                viewModel.Listings = viewModel.Customers.Where(c => c.CustomerId == customerId.Value).Single().Listings;


                if (viewModel.Listings.Count() == 0)
                {
                    ViewBag.ShowMsg = true;
                }

            }

            if (customerId == null)
            {
                ViewBag.ShowMsg = false;
            }

            //!When customer is retrieved retrieve all his listings 
            //viewModel.Listings = viewModel.Customers.Where(c => c.CustomerId == customerId.Value).Single().Listings;


            //viewModel


            return View(viewModel);
        }

        // GET: ListingsManagement/Details/5
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

        // GET: ListingsManagement/Create
        [HttpGet]
        public ActionResult Create(int? custId)
        {



            if (custId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            var listing = new Listing();
            listing.Features = new List<Feature>();


            //GET FEATURES 

            PopualteFeatures(listing);




            ViewBag.CityAreas = new SelectList(db.CityAreas, "AreaId", "Name");

            //ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "FirstName");
            ViewBag.Employees = new SelectList(db.Employees, "EmployeeId", "FirstName");
            return View();
        }



        // GET: ListingsManagement/Create
        [HttpPost]
        public ActionResult Create(Listing listing, HttpPostedFileBase[] Images)
        {



            if (Images.Count() > 7)
            {

                ViewBag.Error = "You can only select no more than 7 images for a property";

                ViewBag.CityAreas = new SelectList(db.CityAreas, "AreaId", "Name");

                //ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "FirstName");
                ViewBag.Employees = new SelectList(db.Employees, "EmployeeId", "FirstName");
                PopualteFeatures(listing);
                return View();
            }


            List<string> validImages = new List<string>();
            if (ModelState.IsValid)
            {   //iterating through multiple file collection   
                foreach (HttpPostedFileBase file in Images)
                {
                    //Checking file is available to save.  
                    if (file != null)
                    {
                        var InputFileName = Path.GetFileName(file.FileName);


                        ImageFile img = db.Images.Where(i => i.ImageName.ToLower().Contains(InputFileName.ToLower()) && i.Approved == true).FirstOrDefault();


                        if (img != null)
                        {
                            validImages.Add(img.ImageName);
                        }

                    }

                }


                if (validImages.Count > 0)
                {

                }
            }



            ViewBag.CityAreas = new SelectList(db.CityAreas, "AreaId", "Name");

            //ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "FirstName");
            ViewBag.Employees = new SelectList(db.Employees, "EmployeeId", "FirstName");


            return View();
        }


        private void PopualteFeatures(Listing listing)
        {


            var allFeattures = db.Features;
            var listingFeatures = db.Features.Select(f=> f.Id).ToHashSet();

            //GET ALL FEATURES THAT ASSOSIATED WITH THE CURRENT LISTING

            if (listing.ListingId != 0)
            {
                listingFeatures = new HashSet<int>(listing.Features.Select(f => f.Id));
            }
           

            //!INIT LIST OF VIEWMODEL TO POPULATE PROPETIES
            var viewModel = new List<AssociatedFeatures>();


            foreach (var feature in allFeattures)
            {
                viewModel.Add(new AssociatedFeatures()
                {
                    FeatureId = feature.Id,
                    Title = feature.Name,
                    Available = listingFeatures.Contains(feature.Id)
                });
            }


            ViewBag.Features = viewModel;







        }

        //// POST: ListingsManagement/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ListingId,StreetAddress,City,Province,Country,PostalCode,Area,Bedrooms,Bathrooms,Price,ContractSigned,ListingStartDate,ListingEndDate,CustomerId,EmployeeId,AreaId")] Listing listing)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Listings.Add(listing);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.AreaId = new SelectList(db.CityAreas, "AreaId", "Name", listing.AreaId);
        //    ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "FirstName", listing.CustomerId);
        //    ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "FirstName", listing.EmployeeId);
        //    return View(listing);
        //}

        // GET: ListingsManagement/Edit/5
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

        // POST: ListingsManagement/Edit/5
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

        // GET: ListingsManagement/Delete/5
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

        // POST: ListingsManagement/Delete/5
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
