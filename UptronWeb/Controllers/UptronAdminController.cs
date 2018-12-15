using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UptronWeb.BAL.Login;

namespace UptronWeb.Controllers
{
    public class UptronAdminController : CommonController
    {
        // GET: Admin
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CheckAdminLogin(string username, string password)
        {
            LoginDetails detail = new LoginDetails();
            var result = detail.CheckAdminLogin(username, password);
            if (result)
            {
                return RedirectToAction("Dashboard", "Admin");
                //dashbaord page redirect

            }
            else
                SetAlertMessage("Invalid Login credentials", "Login Response");
            return RedirectToAction("Login");
        }
    }
}