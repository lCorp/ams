using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Globalization;
using System.Threading;
using AMS.Controllers;

namespace AMS
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Define what will run when the web server start
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        /// <summary>
        /// This method is called before every request
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            //It's important to check whether session object is ready
            if (HttpContext.Current.Session != null)
            {
                //Get the current culture info from session
                CultureInfo ci = (CultureInfo)this.Session["Culture"];
                //Checking first if there is no value in session 
                //and set default language 
                //this can happen for user's first request
                if (ci == null)
                {
                    //Sets default culture from web config
                    string langName = SharedController.GetSetting("DefaultCulture");

                    //Try to get values from Accept lang HTTP header
                    if (HttpContext.Current.Request.UserLanguages != null && HttpContext.Current.Request.UserLanguages.Length != 0)
                    {
                        //Gets accepted list 
                        langName = HttpContext.Current.Request.UserLanguages[0].Substring(0, 5);
                    }

                    //Create culture info from lang name and set it back to session
                    ci = new CultureInfo(langName);
                    this.Session["Culture"] = ci;
                }
                //Finally setting culture for each request
                Thread.CurrentThread.CurrentUICulture = ci;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
            }
        }

        /// <summary>
        /// This method allow caching and localization can work together
        /// </summary>
        /// <param name="context"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override string GetVaryByCustomString(HttpContext context, string value)
        {
            if (value.Equals("lang"))
            {
                return Thread.CurrentThread.CurrentUICulture.Name;
            }
            return base.GetVaryByCustomString(context, value);
        }
    }
}