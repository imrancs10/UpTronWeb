using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UptronWeb.BAL.Login;

namespace UptronWeb.Controllers
{
    public class AdminController : CommonController
    {
        // GET: Admin
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult JobPortalRegistrationList()
        {
            return View();
        }

        public ActionResult JobPortalRegistrationViewDetail()
        {
            return View();
        }
    }
}