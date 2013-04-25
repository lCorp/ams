using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMS.Controllers
{
    public class HomeController : Controller
    {
        //Home/Index
        public ActionResult Index()
        {
            return View();
        }

        //Home/About
        public ActionResult About()
        {
            return View();
        }
    }
}
