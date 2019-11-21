using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HorizonRE.Models;
using System.Web.Security;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace HorizonRE.Controllers
{

    [Authorize(Roles = RoleName.BROKER)]
    [Authorize(Roles = RoleName.AGENT)]
    [Authorize(Roles = RoleName.MANAGER)]
    public class EmployeeController : Controller
    {
        private HorizonContext db = new HorizonContext();

        private ApplicationUserManager _userManager;

        // GET: AllEmployees
        [HttpGet]
        public ActionResult Index()
        {
            var prov = db.Provinces.ToList();
            var coun = db.Countries.ToList();

            foreach (var emp in db.Employees.ToList())
            {
                var p = prov.First(el => el.ProvinceId == emp.EmployeeProvinceId).Name;
                var countId = prov.First(pr => pr.ProvinceId == emp.EmployeeProvinceId).CountryId;
                var countryName = coun.First(c => c.CountryId == countId).Name;

                emp.Province = p;
                emp.Country = countryName;
            }

            return View(db.Employees.OrderBy(e => e.LastName).ToList());
        }

        // GET: AddEmployee
        [HttpGet]
        [Authorize(Roles=RoleName.BROKER)]
        public ActionResult AddEmployee()
        {
            ViewBag.CountryList = new SelectList(db.Countries, "CountryId", "Name");
            ViewBag.ProvincesList = new SelectList(db.Provinces.Where(p => p.CountryId == 1), "ProvinceId", "Name");
            return View();
        }


        // POST: AddEmployee
        [HttpPost]
        [Authorize(Roles = RoleName.BROKER)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddEmployee([Bind(Include = "EmployeeId,FirstName," +
            "LastName, MiddleName,SIN,StreetAddress,City,PostalCode," +
            "HomePhone,CellPhone,OfficePhone,OfficeEmail,DOB,AddedBy," +
            "HireDate, ProvincesList, ProvinceEmployee, EmployeeProvinceId")]
         Employee employee)
        {
            if (ModelState.IsValid)
            {
                int provId = Convert.ToInt32(Request.Form["ProvincesList"]);

                employee.EmployeeProvinceId = provId;
                //get currently logged user from Identity
                string currentUser = User.Identity.GetUserName();
                var addedBy = db.Employees.Where(e => e.OfficeEmail.Contains(currentUser)).FirstOrDefault();

                //Need this for the 1st user. 
                employee.AddedBy = 1;
                //employee.AddedBy = addedBy.EmployeeId;

                employee.Password = GeneratePassword(8, 1);

                db.Employees.Add(employee);
                db.SaveChanges();

                //save province info
                int empId = employee.EmployeeId;
                ProvinceEmployee pe = new ProvinceEmployee();
                pe.EmployeeId = empId;
                pe.ProvinceId = provId;

                db.ProvincesEmployees.Add(pe);

                db.SaveChanges();

                //create new employee auth record
                await CreateAuthRecordAsync(employee);                

                return RedirectToAction("Index");
            }

            ViewBag.CountryList = new SelectList(db.Countries, "CountryId", "Name");
            ViewBag.ProvincesList = new SelectList(db.Provinces, "ProvinceId", "Name");


            return View();
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext()
                    .GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private async Task CreateAuthRecordAsync(Employee employee)
        {
            var user = new ApplicationUser
            { UserName = employee.OfficeEmail, Email = employee.OfficeEmail };
            var result = await UserManager.CreateAsync(user, employee.Password);
            //assign Agent role
            if (result.Succeeded)
            {
                var newAgent = UserManager.FindByName(user.UserName);
                var roleResult = UserManager.AddToRole(newAgent.Id, "Agent");

                //send email
                // var subject = "Account created";
                // var body = "Your account was sucessfully created. " +
                //  $"Please log in using following email {newAgent.UserName} " +
                //  $"and password {newAgent.PasswordHash}";
                //var user = await UserManager.FindByEmailAsync("xxx@gmail.com");
                // await UserManager.SendEmailAsync(newAgent.Id, subject, body);


                //string code = await UserManager.GenerateEmailConfirmationTokenAsync(newAgent.Id);
                //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = newAgent.Id, code = code }, protocol: Request.Url.Scheme);
                //await UserManager.SendEmailAsync(newAgent.Id, "Horizon Agent Account",
                //    "Your account was sucessfully created. " +
                //    $"Please log in using following email {newAgent.UserName} " +
                //    $"and password {newAgent.PasswordHash}");

                //UserManager.EmailService = new EmailService();
                //await UserManager.SendEmailAsync(newAgent.Id, "Horizon Agent Account", 
                //    "Your account was sucessfully created. " +
                //    $"Please log in using following email {newAgent.UserName} " +
                //    $"and password {newAgent.PasswordHash}");
            }

        }


        // GET: Edit
        [HttpGet]
       [Authorize(Roles = RoleName.BROKER)]
       [Authorize(Roles = RoleName.MANAGER)]
        public ActionResult EditEmployee(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee e = db.Employees.Find(id);
            if (e == null)
            {
                return HttpNotFound();
            }


            //Find employee province 
            int provId = e.EmployeeProvinceId;

            //Find employee country
            var countId = db.Provinces.ToList().First(pr => pr.ProvinceId == e.EmployeeProvinceId).CountryId;

            //Add country to list
            ViewBag.CountryList = new SelectList(db.Countries, "CountryId", "Name", countId);
            ViewBag.ProvincesList = new SelectList(db.Provinces.Where(p => p.CountryId == countId), "ProvinceId", "Name", provId);
            return View(e);
        }

        //POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.BROKER)]
        [Authorize(Roles = RoleName.MANAGER)]
        public ActionResult EditEmployee([Bind(Include = "EmployeeId,FirstName,LastName,MiddleName,SIN,StreetAddress,City,PostalCode,HomePhone,CellPhone,OfficePhone,OfficeEmail,DOB,AddedBy,HireDate, EmployeeProvinceId, CountryList, ProvincesList, ProvinceEmployee")]
         Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.EmployeeProvinceId = Convert.ToInt32(Request.Form["ProvinceList"]);
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CountryList = new SelectList(db.Countries, "CountryId", "Name");
            ViewBag.ProvincesList = new SelectList(db.Provinces, "ProvinceId", "Name");

            return View();
        }

        // POST: Employee/Delete/5
        [HttpPost]
        [Authorize(Roles = RoleName.BROKER)]
        public ActionResult Delete()
        {
            int employeeId = Convert.ToInt32(Request.Form["empId"]);
            Employee emp = db.Employees.Find(employeeId);
            db.Employees.Remove(emp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        // GET: Provinces in Country for AddEmployee
        [HttpGet]
        public JsonResult GetProvincesByCountry(int countryId)
        {
            var provinces = db.Provinces.Where(p => p.CountryId == countryId).Select(t => new
            {
                ProvinceId = t.ProvinceId,
                Name = t.Name
            });
            return Json(provinces, JsonRequestBehavior.AllowGet);
        }


        public static string GeneratePassword(int len, int spec)
        {
            string password = Membership.GeneratePassword(len, spec);

            return password;
        }
    }

}