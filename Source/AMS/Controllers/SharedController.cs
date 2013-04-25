using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Globalization;

namespace AMS.Controllers
{
    public class SharedController : Controller
    {
        #region Instance methods

        /// <summary>
        /// Return the page that handle the common error
        /// </summary>
        /// <returns></returns>
        public ActionResult Error()
        {
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

        #region Static methods

        /// <summary>
        /// Get app setting value by key in web.config
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        #endregion
    }
}
