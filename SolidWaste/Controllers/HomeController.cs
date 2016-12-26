using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SolidWaste.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.menu = "home";
            return View();
        }


        public ActionResult About()
        {
            ViewBag.menu = "about";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.menu = "contact";

            return View();
        }
    }
}
