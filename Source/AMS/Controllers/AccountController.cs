using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AMS.Models.ViewModels;
using System.Web.Security;
using AMS.Models.Entities;
using AMS.Libraries;

namespace AMS.Controllers
{
    [ModuleAuthorize]
    public class AccountController : Controller, IDisposable
    {
        private AMSEntities context;

        public AccountController()
        {
            this.context = new AMSEntities();
        }

        private ActionResult RedirectToLocalUrl(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //
        // GET: /Account/LogOn
        public ActionResult LogOn(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/LogOn
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                string encryptedPassword = Cryptography.EncryptPassword(model.Password);
                if (this.context.Accounts.Count(i => i.UserName == model.UserName && i.Password == encryptedPassword && i.Status == (int)EntityStatus.Active) == 1)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    return RedirectToLocalUrl(returnUrl);
                }
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", App_LocalResources.AccountController.WrongUserNameOrPassword);
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Register(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                string encryptedPassword = Cryptography.EncryptPassword(model.Password);
                if (this.context.Accounts.Count(i => i.UserName == model.UserName && i.Password == encryptedPassword && i.Status == (int)EntityStatus.Active) == 1)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    return RedirectToLocalUrl(returnUrl);
                }
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", App_LocalResources.AccountController.WrongUserNameOrPassword);
            return View(model);
        }

        public ActionResult Manage()
        {
            return View();
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        public ActionResult ForgetPassword()
        {
            return View();
        }

        void IDisposable.Dispose()
        {
            this.context.Dispose();
            base.Dispose();
        }
    }
}
