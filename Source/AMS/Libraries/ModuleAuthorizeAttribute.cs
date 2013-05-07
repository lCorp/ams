using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AMS.Models.Entities;
using AMS.Libraries;
using System.Web.Security;

namespace AMS.Libraries
{
    public class ModuleAuthorizeAttribute : AuthorizeAttribute, IDisposable
    {
        private AMSEntities context;

        public ModuleAuthorizeAttribute()
        {
            this.context = new AMSEntities();
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            Module module = this.context.Modules.SingleOrDefault(i => i.ControllerCode == filterContext.ActionDescriptor.ControllerDescriptor.ControllerName && i.ActionCode == filterContext.ActionDescriptor.ActionName);
            if (module != null && module.ModuleType.HasValue && !(module.ModuleType == (int)ModuleType.Public))
            {
                Account account = this.context.Accounts.SingleOrDefault(i => i.UserName == filterContext.HttpContext.User.Identity.Name);
                if (account != null)
                {
                    if (module.ModuleType == (int)ModuleType.Authorize)
                    {
                        return;
                    }
                    if (module.ModuleType == (int)ModuleType.ModuleAuthorize)
                    {
                        List<Guid> roleIds = this.context.UserInRoles.Where(i => i.UserID == account.ID).Select(i => i.RoleID).ToList();
                        foreach (var roleId in roleIds)
                        {
                            if (this.context.Permissions.Count(i => i.ModuleID == module.ID && (i.AccountType == (int)AccountType.User && i.AccountID == account.ID || i.AccountType == (int)AccountType.Role && i.AccountID == roleId)) > 0)
                            {
                                return;
                            }
                        }
                        filterContext.Result = new RedirectResult("~/Module/AccessDenied");
                        return;
                    }
                }
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        public void Dispose()
        {
            this.context.Dispose();
        }
    }
}