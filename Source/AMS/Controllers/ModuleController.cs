using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AMS.Models.Entities;
using AMS.Libraries;
using System.Net;

namespace AMS.Controllers
{
    public class ModuleController : Controller, IDisposable
    {
        private AMSEntities context;

        public ModuleController()
        {
            this.context = new AMSEntities();
        }

        public ActionResult Access(Guid id)
        {
            Module module = this.context.Modules.SingleOrDefault(i => i.ID == id);
            if (module != null)
            {
                List<Module> subModules = this.context.Modules.Where(i => i.ParentID == id).ToList();
                if (subModules.Count > 0)
                {
                    ViewBag.Title = App_GlobalResources.Modules.ResourceManager.GetString(module.ModuleCode) ?? module.ModuleName;
                    return View(subModules);
                }
                else
                {
                    return Redirect("~/" + module.ControllerCode + "/" + module.ActionCode + "/" + module.Parameters);
                }
            }
            else
            {
                return RedirectToAction("Error", "Shared", new { id = HttpStatusCode.NotFound });
            }
        }

        void IDisposable.Dispose()
        {
            this.context.Dispose();
            base.Dispose();
        }
    }
}
