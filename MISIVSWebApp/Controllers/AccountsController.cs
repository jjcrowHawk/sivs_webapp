using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MISIVSWebApp.Controllers
{
    public class AccountsController : Controller
    {
        // GET: Accounts
        public ActionResult Login()
        {
            ViewBag.message = "";
            return View();
        }

        [HttpPost]
        // POST: Accounts
        public ActionResult Login(String username, String password)
        {

            if (!String.IsNullOrEmpty(username)  && !String.IsNullOrEmpty(password)  && username.Equals("luibasantes") && password.Equals("123"))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.message = "Username and/or password are incorrect";
                return View();
            }
            

        }


    }
}