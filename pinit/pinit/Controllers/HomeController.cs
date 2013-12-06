using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using pinit.Models;

namespace pinit.Controllers
{
     [Authorize]
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            var username = User.Identity.GetUserName();
            var model = new OverviewModel(username);
            
            return View(model);
        }

       
        public ActionResult About()
        {
            ViewBag.Message = "Pinterest clone for NYU Poly CS6083";

            return View();
        }

      
        public ActionResult Contact()
        {
            ViewBag.Message = "Contact info";

            return View();
        }
    }
}