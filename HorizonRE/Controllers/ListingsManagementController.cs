using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HorizonRE.Models;
using HorizonRE.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PagedList;

namespace HorizonRE.Controllers
{
   [Authorize(Roles = RoleName.EMPLOYEE)]
    public class ListingsManagementController : Controller
    {
        private HorizonContext db = new HorizonContext();
        private ApplicationUserManager _userManager;

        // GET: ListingsManagement
        public ActionResult Index(int? customerId, string search = null, string currentSearch = null)
        {


            var viewModel = new ListingCustomerAgentView();


            if (string.IsNullOrEmpty(search))
            {
                search = currentSearch;
            }


            if (search != null)
            {
                viewModel.Customers = db.Customers.Where(cx => cx.FirstName.ToLower().Contains(search.ToLower()) || cx.LastName.ToLower().Contains(search.ToLower()) || cx.Phone.Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "").Contains(search));


            }









            if (customerId != null)
            {
                ViewBag.SelectedCustomer = customerId.Value;

                viewModel.Listings = db.Listings.Include(l => l.Employee);

                viewModel.Listings = viewModel.Customers
                    .Where(c => c.CustomerId == customerId.Value).Single().Listings;


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


            ViewBag.CurrentSearch = search;


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

            ViewBag.CityAreas = new SelectList(db.CityAreas, "AreaId", "Name");
            ViewBag.Countries = new SelectList(db.Countries, "CountryId", "Name");
            ViewBag.Provinces = new SelectList(db.Provinces.Where(p => p.CountryId == 1), "ProvinceId", "Name");

            //ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "FirstName");
            ViewBag.Employees = new SelectList(db.Employees, "EmployeeId", "FirstName");

            ViewBag.SelectedCustomer = custId;

            PopulateFeatures(listing);

            return View();
        }



        // GET: ListingsManagement/Create
        [HttpPost]
        public ActionResult Create(Listing listing, HttpPostedFileBase[] selectedImages,
            string[] selectedFeature, string SelectedCustomer)
        {
            int customerId = int.Parse(SelectedCustomer);

            listing.CustomerId = customerId;

            if (selectedImages.Count() > 7)
            {

                ViewBag.Error = "You can only select no more than 7 images for a property";

                //ViewBag.CityAreas = new SelectList(db.CityAreas, "AreaId", "Name");

                //ViewBag.Employees = new SelectList(db.Employees, "EmployeeId", "FirstName");
                //PopulateFeatures(listing);
                //return View();
            }
            else
            {

                if (!listing.ContractSigned)
                {
                    listing.EmployeeId = null;
                }



                List<string> validImages = new List<string>();
                List<string> invalidImages = new List<string>();
                if (ModelState.IsValid)
                {   //iterating through multiple file collection   
                    foreach (HttpPostedFileBase file in selectedImages)
                    {
                        //Checking file is available to save.  
                        if (file != null)
                        {
                            var InputFileName = Path.GetFileName(file.FileName);


                            ImageFile img = db.Images
                                .Where(i => i.ImageName.ToLower()
                                .Contains(InputFileName.ToLower())
                                && i.Approved == true && i.ListingId == null).FirstOrDefault();


                            if (img != null)
                            {
                                validImages.Add(img.ImageName);
                            }
                            else
                            {
                                invalidImages.Add(file.FileName);
                            }


                        }

                        if (invalidImages.Count > 0)
                        {
                        

                            ViewBag.InvalidImagesError = invalidImages;

                        }

                    }

                    if (selectedFeature != null)
                    {
                        listing.Features = new List<Feature>();

                        foreach (var feat in selectedFeature)
                        {

                            var featureToAdd = db.Features.Find(int.Parse(feat));

                            listing.Features.Add(featureToAdd);
                        }
                    }


                    if (validImages.Count >= 0 && invalidImages.Count == 0)
                    {


                        //If contract is not signed
                        if (listing.ContractSigned)
                        {

                            listing.ListingStartDate = DateTime.Now.Date;
                            var todayPlus3Months = DateTime.Now.Date.AddMonths(3);
                            listing.ListingEndDate = todayPlus3Months;
                            listing.Status = "Active";
                        }
                        else
                        {
                            listing.ListingStartDate = null;
                            listing.ListingEndDate = DateTime.Now.Date.AddMonths(3);
                            listing.Status = "No contract";
                        }


                        int selectedCountry = Convert.ToInt32(Request.Form["Countries"].ToString());
                        int selectedProvince = Convert.ToInt32(Request.Form["Provinces"].ToString());

                        string countryName = db.Countries.Where(c => c.CountryId == selectedCountry).FirstOrDefault().Name;
                        string provinceName = db.Provinces.Where(c => c.ProvinceId == selectedProvince).FirstOrDefault().Name;



                        listing.Country = countryName;
                        listing.Province = provinceName;


                        db.Listings.Add(listing);
                        db.SaveChanges();


                        //get id of listing, to associate it with images
                        int id = listing.ListingId;

                        validImages.ForEach(img =>
                        {

                            ImageFile imageToUpdate = db.Images
                                .Where(im => im.ImageName.ToLower()
                                .Contains(img.ToLower())).FirstOrDefault();
                            imageToUpdate.ListingId = id;

                            db.Images.Attach(imageToUpdate);

                            db.Entry(imageToUpdate).Property(i => i.ListingId).IsModified = true;

                            // db.Entry(customer).State = EntityState.Modified;
                            db.SaveChanges();
                        });

                        return RedirectToAction("Index");
                    }
                }
            }




            PopulateFeatures(listing);
            ViewBag.CityAreas = new SelectList(db.CityAreas,
                "AreaId", "Name", listing.AreaId);

            ViewBag.Countries = new SelectList(db.Countries, "CountryId", "Name");
            ViewBag.Provinces = new SelectList(db.Provinces, "ProvinceId", "Name");

            ViewBag.Employees = new SelectList(db.Employees,
                "EmployeeId", "FullName", listing.EmployeeId);

            return View(listing);
        }


        private void PopulateFeatures(Listing listing)
        {

            var allFeattures = db.Features;
            var listingFeatures = db.Features.Select(f => f.Id).ToHashSet();

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



        [HttpGet]
        public ActionResult CreateReport(string date, int? page)
        {

            var listings = db.Listings.ToList();

            if (!string.IsNullOrEmpty(date))
            {
                //search all listings from start date till today
                DateTime start = DateTime.Parse(date);
                listings = listings.Where(l => l.ListingStartDate.HasValue ?
                l.ListingStartDate.Value.Date >= start.Date : l.ListingStartDate == null
                         && l.ListingStartDate.HasValue ?
                l.ListingStartDate.Value.Date <= DateTime.Now.Date : l.ListingStartDate == null).ToList();



                //!ASK OLENA WHAT THE HELL IT IS
                var grListings = listings
               .GroupBy(l => l.Status, (key, listing) => new ListingReportViewModel
               {
                   Status = key,
                   listing = listing
               })
               .ToList();


                var groupedListings = grListings.
                    OrderByDescending(l => l.Status == "Expired")
                    .ThenByDescending(l => l.Status.ToLower() == "no contract")
                    .ThenByDescending(l => l.Status == "Sold")
                    .ThenByDescending(l => l.Status == "Active").ToList();

                ViewBag.Report = groupedListings;
            }
            else
            {
                ViewBag.Error = "Please select start date to generate a report";
            }

            return View();
        }


        [HttpGet]
        public ActionResult RenewContract(string id)
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"].ToString();
                TempData.Remove("Message");
            }

            if (!string.IsNullOrEmpty(id))
            {
                int listId = Convert.ToInt32(id);

                Listing listing = db.Listings.Where(l => l.ListingId == listId)
                             .FirstOrDefault();

                if (listing != null)
                {
                    return View(listing);
                }
                else
                {
                    ViewBag.Error = "No listing found";
                    return View();
                }

            }

            return View();
        }

        [HttpPost]
        public ActionResult RenewContractManually(Listing listing)
        {
            if (listing != null)
            {
                var lsToRenew = db.Listings.Where(l => l.ListingId == listing.ListingId)
                    .SingleOrDefault();
                if (lsToRenew != null)
                {
                    if (lsToRenew.ListingEndDate < DateTime.Now)
                    {
                        TempData["Message"] = "You cannot renew a contract for " +
                        "expired listing";
                        return RedirectToAction("RenewContract");
                    }

                    double daysToAdd = (lsToRenew.ListingEndDate.Date - DateTime.Now.Date).Days;
                    DateTime newExpDate = lsToRenew.ListingEndDate
                        .AddMonths(3).AddDays(daysToAdd);

                    lsToRenew.RenewNotificationSent = false;
                    lsToRenew.ListingEndDate = newExpDate;

                    db.Listings.Attach(lsToRenew);
                    db.Entry(lsToRenew).Property(x => x.RenewNotificationSent).IsModified = true;
                    db.Entry(lsToRenew).Property(x => x.ListingEndDate).IsModified = true;
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();

                    TempData["Message"] = $"Contract for listing #{lsToRenew.ListingId} " +
                        $"was successfully renewed";
                    return RedirectToAction("RenewContract");
                }
            }

            TempData["Message"] = "Sorry, something went wrong.";

            return RedirectToAction("RenewContract");
        }




        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [HttpPost]
        public async Task<ActionResult> RenewContract(int? id = null)
        {

            DateTime deadline = DateTime.Now.AddDays(7);

            var listings = db.Listings.ToList();


            var listToNotify = listings
                .Where(l => l.ListingEndDate <= deadline
                && l.RenewNotificationSent == false).ToList();

            if (listings.Count == 0)
            {
                return View();
            }

            foreach (Listing lis in listToNotify)
            {
                var customerToSend = UserManager.FindByName(lis.Customer.Email);

                if (customerToSend != null)
                {
                    string msg = $"Dear {lis.Customer.FullName}, " +
                        $"<br/> <br/> Your contract for listing # {lis.ListingId} " +
                        $"is about to expire on {lis.ListingEndDate.ToShortDateString()} " +
                        $"<br/> Please click  " +
                        $"<a href='https://localhost:44343/ListingsManagement/CustomerRenewContract/?customerId={lis.CustomerId}'>this link</a>  " +
                        $"to renew contract. <br/> You will be required to login to " +
                        $"your account. <br/><br/>" +
                   $"Kind regards, <br/>" +
                   $"{lis.Employee.FullName}";

                    await UserManager.SendEmailAsync(customerToSend.Id,
                        "Renew Contract", msg);

                    //change notify field
                    lis.RenewNotificationSent = true;

                    db.Listings.Attach(lis);
                    db.Entry(lis).Property(x => x.RenewNotificationSent).IsModified = true;
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();

                }
            }

            //IF CUSTOMER did not replay to email about renew
            //set listing to expired on expiration date
            var listToExpire = listings.Where
                (l => l.ListingEndDate <= DateTime.Now && l.Status == "Active").ToList();
            if (listToExpire.Count() != 0)
            {
                foreach (var item in listToExpire)
                {
                    item.Status = "Expired";

                    db.Listings.Attach(item);
                    db.Entry(item).Property(x => x.Status).IsModified = true;
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();
                }
            }


            ViewBag.Msg = $"{listToNotify.Count()} customers were notified. \n " +
                $"{listToExpire.Count()} expired contracts";


            return View();
        }


        [HttpGet]
        //[AllowAnonymous]
       [Authorize(Roles = "Customer")]
        public ActionResult CustomerRenewContract(int? customerId)
        {

            if (customerId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var lst = from list in db.Listings
                      let notifDate = SqlFunctions.DateAdd("dd", -7, list.ListingEndDate)
                      where list.CustomerId == customerId &&
                      list.Status == "Active" &&
                      notifDate >= DateTime.Now
                      && DateTime.Now <= list.ListingEndDate
                      && list.RenewDenialReason == null
                      select list;

            if (lst.Count() == 0)
            {
                ViewBag.Message = "No contracts to renew";

                return View();
            }

            return View(lst.ToList());

        }


        [HttpPost]
        [Authorize(Roles = RoleName.CUSTOMER)]
        public ActionResult CustomerRenewContract(List<Listing> list)
        {
            foreach (Listing l in list)
            {
                if (l.RenewDenialReason != null)
                {
                    //Listing scheduled to delete
                    db.Listings.Attach(l);
                    db.Entry(l).Property(x => x.RenewDenialReason).IsModified = true;
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();
                }
                else
                {
                    //Renew contract + add amount of days left
                    var listingToUpdate = db.Listings
                        .Where(lis => lis.ListingId == l.ListingId).FirstOrDefault();


                    if (listingToUpdate == null)
                    {

                        //Add error
                        return View();
                    }

                    DateTime endDateListing = listingToUpdate.ListingEndDate;
                    listingToUpdate.RenewNotificationSent = false;

                    endDateListing = endDateListing
                        .AddDays(endDateListing.Subtract(DateTime.Now).Days).AddMonths(3);
                    listingToUpdate.ListingEndDate = endDateListing;
                    db.Listings.Attach(listingToUpdate);
                    db.Entry(listingToUpdate).Property(x => x.ListingEndDate)
                        .IsModified = true;
                    db.Entry(listingToUpdate).Property(x => x.RenewNotificationSent)
                       .IsModified = true;
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();
                }
            }

            ViewBag.Message = "Your changes were saved";

            return View();
        }





        //------THIS PART IS NOT IMPLEMENTED ON UI-------

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
