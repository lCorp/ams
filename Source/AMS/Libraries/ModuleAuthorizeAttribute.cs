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
            if (module != null && module.ModuleType.HasValue && !module.ModuleType.Value.Equals(ModuleType.Public))
            {
                Account account = this.context.Accounts.SingleOrDefault(i => i.UserName == filterContext.HttpContext.User.Identity.Name);
                if (account != null)
                {
                    if (module.ModuleType.Value.Equals(ModuleType.Authorize))
                    {
                        return;
                    }
                    if (module.ModuleType.Value.Equals(ModuleType.ModuleAuthorize))
                    {
                        List<Guid> roleIds = this.context.UserInRoles.Where(i => i.UserID == account.ID).Select(i => i.RoleID).ToList();
                        foreach (var roleId in roleIds)
                        {
                            if (this.context.Permissions.Count(i => i.ModuleID == module.ID && (AccountType.User.Equals(i.AccountType) && i.AccountID == account.ID || AccountType.Role.Equals(i.AccountType) && i.AccountID == roleId)) > 0)
                            {
                                return;
                            }
                        }
                    }
                }
                FormsAuthentication.SignOut();
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        private bool IsModuleAuthorize(string userName, string controllerCode, string actionCode)
        {
            Module module = this.context.Modules.SingleOrDefault(i => i.ControllerCode == controllerCode && i.ActionCode == actionCode);
            if (module != null && module.ModuleType.HasValue && module.ModuleType.Value.Equals(ModuleType.Public))
            {
                Account account = this.context.Accounts.SingleOrDefault(i => i.UserName == userName);
                if (account != null)
                {
                    if (module.ModuleType.Value.Equals(ModuleType.Authorize))
                    {
                        return true;
                    }
                    else
                    {
                        List<Guid> roleIds = this.context.UserInRoles.Where(i => i.UserID == account.ID).Select(i => i.RoleID).ToList();
                        foreach (var roleId in roleIds)
                        {
                            if (this.context.Permissions.Count(i => i.ModuleID == module.ID && (AccountType.User.Equals(i.AccountType) && i.AccountID == account.ID || AccountType.Role.Equals(i.AccountType) && i.AccountID == roleId)) > 0)
                            {
                                return true;
                            }
                        }
                    }
                }
                return false;
            }
            return true;
        }

        public void Dispose()
        {
            this.context.Dispose();
        }
    }
}