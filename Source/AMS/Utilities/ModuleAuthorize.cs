using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Net;
using AMS.Models.Repositories;

namespace AMS.Utilities
{
    public class ModuleAuthorize : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            string userName=filterContext.HttpContext.User.Identity.Name;
            string controllerCode=filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string actionCode=filterContext.ActionDescriptor.ActionName;
            bool isAuthorized = AccountRepository.HasPermisstion(userName, controllerCode, actionCode);
            if (!isAuthorized)
            {
                if (string.IsNullOrWhiteSpace(userName))
                {
                    filterContext.Result = new RedirectResult("~/Shared/Error/" + HttpStatusCode.Unauthorized);
                }
                else
                {
                    filterContext.Result = new HttpUnauthorizedResult();
                }
            }
        }
    }
}