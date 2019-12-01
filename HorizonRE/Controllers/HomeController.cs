using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HorizonRE.Controllers
{
   [AllowAnonymous]
    public class HomeController : Controller
   {
      // GET: Horizon
      public ActionResult Index()
      {
         return View();
      }
   }
}