using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AMS.Models;

namespace AMS.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/LogOn
        [AllowAnonymous]
        public ActionResult LogOn(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/LogOn
        [AllowAnonymous]
        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            // If we got this far, something failed, redisplay form
            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }
    }
}
