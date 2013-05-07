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

        //
        // GET: /Module/

        public ActionResult AccessDenied()
        {
            return View();
        }

        public ActionResult Access(string id)
        {
            try
            {
                Guid moduleID = new Guid(id);
                Module module = this.context.Modules.SingleOrDefault(i => i.ID == moduleID);
                if (module == null)
                {
                    throw new HttpException(404, "Page not found.");
                }
                ViewBag.Title = Configuration.GetSetting(module.ModuleCode);
                List<Module> subModules = this.context.Modules.Where(i => i.ParentID == moduleID).ToList();
                if (subModules.Count > 0)
                {
                    return View(subModules);
                }
                else
                {
                    return RedirectToAction(module.ActionCode, module.ControllerCode);
                }
            }
            catch (Exception ex)
            {
                throw new HttpException(404, ex.Message);
            }
        }

        void IDisposable.Dispose()
        {
            this.context.Dispose();
            base.Dispose();
        }
    }
}
