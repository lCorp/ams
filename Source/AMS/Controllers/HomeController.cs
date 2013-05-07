using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AMS.Libraries;

namespace AMS.Controllers
{
    [ModuleAuthorize]
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
