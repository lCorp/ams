﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Globalization;
using AMS.Utilities;
using System.Net;

namespace AMS.Controllers
{
    [ModuleAuthorize]
    public class SharedController : Controller
    {
        #region Instance methods

        /// <summary>
        /// Return the page that handle the common error
        /// </summary>
        /// <returns></returns>
        public ActionResult Error(HttpStatusCode id)
        {
            ViewBag.ErrorMessage = App_GlobalResources.Messages.ResourceManager.GetString("HttpStatusCode_" + (int)id, App_GlobalResources.Messages.Culture);
            return View();
        }

        /// <summary>
        /// Change the current culture and set it to session
        /// </summary>
        /// <param name="lang"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public ActionResult ChangeCulture(string lang, string returnUrl)
        {
            Session["Culture"] = new CultureInfo(lang);
            return Redirect(returnUrl);
        }

        #endregion
    }
}
