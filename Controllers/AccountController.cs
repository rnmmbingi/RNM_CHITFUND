using RNM_CHITFUND.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RNM_CHITFUND.Controllers
{
    public class AccountController : Controller
    {
        RNM_CHITFUNDEntities _objDB;
        // GET: Account
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Models.Membership model)
        {
            if(ModelState.IsValid)
            {
                using (_objDB = new RNM_CHITFUNDEntities())
                {
                    bool isValid = _objDB.tbl_Users.Any(u => u.UserName == model.UserName && u.Password == model.Password);
                    if (isValid)
                    {
                        FormsAuthentication.SetAuthCookie(model.UserName, false);
                        return RedirectToAction("Index", "Customer");
                    }
                    ModelState.AddModelError("", "Invalid Username and Password.");                  
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(tbl_Users model)
        {
            if (ModelState.IsValid)
            {
                using (_objDB = new RNM_CHITFUNDEntities())
                {
                    RNM_DBContext.RegisterUser(model);                    
                    _objDB.SaveChanges();
                    return RedirectToAction("Login");
                }
            }
            return View(model);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}