using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UptronWeb.BAL.Login;

namespace UptronWeb.Controllers
{
    public class EmployeeController : CommonController
    {
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Dashboard()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CheckLogin(string UserName,string Password)
        {
            EmployeeDetails detail = new EmployeeDetails();
            var result = detail.CheckEmployeeLogin(UserName, Password);
            if (result)
            {
                return RedirectToAction("Dashboard");

            }
            else
                SetAlertMessage("Invalid Login credentials", "Login Response");
            return RedirectToAction("Login");
        }
    }
}