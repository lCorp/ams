using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AMS.Models.Entities;
using AMS.Utilities;
using System.Net;
using AMS.Models.Services;

namespace AMS.Controllers
{
    public class ModuleController : Controller, IDisposable
    {
        private UnitOfWork unitOfWork;

        public ModuleController()
        {
            this.unitOfWork = new UnitOfWork();
        }

        public ActionResult Access(Guid id)
        {
            Module module = this.unitOfWork.ModuleRepository.GetModule(id);
            if (module != null)
            {
                IEnumerable<Module> subModules = this.unitOfWork.ModuleRepository.GetModulesFor(module.ID, User.Identity.Name);
                if (subModules.Count() > 0)
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
            this.unitOfWork.Dispose();
            base.Dispose();
        }
    }
}
