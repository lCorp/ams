using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AMS.Models.Entities;
using AMS.Libraries;

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
                if (module == null)
                {
                    ViewBag.Title = Configuration.GetSetting(module.ModuleCode);
                    List<Module> subModules = this.context.Modules.Where(i => i.ParentID == id).ToList();
                    if (subModules.Count > 0)
                    {
                        return View(subModules);
                    }
                    else
                    {
                        return RedirectToAction(module.ActionCode, module.ControllerCode);
                    }
                }
                else
                {
                    return this.HttpNotFound();
                }
        }

        void IDisposable.Dispose()
        {
            this.context.Dispose();
            base.Dispose();
        }
    }
}
