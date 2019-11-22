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
    [AllowAnonymous]
    public class ListingsController : Controller
    {

        private HorizonContext db = new HorizonContext();
        // GET: Listings
        [HttpGet]
        public ActionResult Index(int? page, string currentCity, int? currentProvince, int? currentCountry, int? currentBedroom, int? currentBathrooms, string citySearch = null, decimal? currentPriceFrom = null, decimal? currentPriceTo = null, string currentPriceOrder = null, int? CountryList = null, int? ProvincesList = null, int? bedrooms = null, int? bathrooms = null, decimal? priceFrom = null, decimal? priceTo = null, string priceOrder = null)
        {

            var listingsFound = db.Listings.Where(l => l.Status == "Active").AsQueryable();

            string countrySearch = null;
            string provSearch = null;

            if (CountryList != null && ProvincesList != null)
            {

                //countrySearch = db.Countries.Where(c => c.CountryId == CountryList).FirstOrDefault().Name;
                // provSearch = db.Provinces.Where(c => c.ProvinceId == ProvincesList).FirstOrDefault().Name;
                //page = 1;

            }
            else
            {
                CountryList = currentCountry;
                ProvincesList = currentProvince;
            }



            if (citySearch != null)
            {

                //page =1

            }
            else
            {
                citySearch = currentCity;
            }

            if (bedrooms == null || bedrooms == 0)
            {
                bedrooms = currentBedroom;
            }

            if (bathrooms == null || bathrooms == 0)
            {
                bathrooms = currentBathrooms;
            }

            if (priceFrom == null)
            {
                priceFrom = currentPriceFrom;
            }


            if (priceTo == null)
            {
                priceTo = currentPriceTo;
            }

            if (priceOrder == null)
            {
                priceOrder = currentPriceOrder;
            }

            if (!string.IsNullOrEmpty(citySearch))
            {
                listingsFound = listingsFound.Where(l => l.City.ToLower().Contains(citySearch.ToLower()));
            }


            if (CountryList != null && ProvincesList != null)
            {
                countrySearch = db.Countries.Where(c => c.CountryId == CountryList).FirstOrDefault().Name;
                provSearch = db.Provinces.Where(c => c.ProvinceId == ProvincesList).FirstOrDefault().Name;
            }


            if (countrySearch != null) listingsFound = listingsFound.Where(l => l.Country.Contains(countrySearch));
            if (provSearch != null) listingsFound = listingsFound.Where(l => l.Province.Contains(provSearch));


            //int ? bedroomsSaved = null;

            if (bedrooms != null && bedrooms != 0)
            {


                listingsFound = listingsFound.Where(l => l.Bedrooms >= bedrooms);


            }
            else
            {

            }
            if (bathrooms != null && bathrooms != 0) listingsFound = listingsFound.Where(l => l.Bathrooms >= bathrooms);
            if (priceFrom != null) listingsFound = listingsFound.Where(l => l.Price >= priceFrom);
            if (priceTo != null) listingsFound = listingsFound.Where(l => l.Price <= priceTo);



            listingsFound = listingsFound.Include(i => i.Images).OrderBy(o => o.Price);



            if (priceOrder == "on")
            {
                listingsFound = listingsFound.Include(i => i.Images).OrderByDescending(o => o.Price);
            }


            ViewBag.CurrentProvince = ProvincesList;
            ViewBag.CurrentCountry = CountryList;
            ViewBag.CurrentCity = citySearch;
            ViewBag.CurrentBedrooms = bedrooms;
            ViewBag.CurrentBathrooms = bathrooms;
            ViewBag.CurrentPriceFrom = priceFrom;
            ViewBag.CurrentPriceTo = priceTo;
            ViewBag.CurrentOrder = priceOrder;
            ViewBag.CountryList = new SelectList(db.Countries.ToList(), "CountryId", "Name", CountryList != null ? CountryList : 1);
            ViewBag.ProvincesList = new SelectList(CountryList == null ? db.Provinces.Where(l => l.CountryId == 1).ToList() : db.Provinces.Where(l => l.CountryId == CountryList).ToList(), "ProvinceId", "Name", ProvincesList != null ? ProvincesList : 4);



            int pageSize = 2;
            int pageNumber = (page ?? 1);


            var listListing = listingsFound.ToList();

            return View(listListing.ToPagedList(pageNumber, pageSize));

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