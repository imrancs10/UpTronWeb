using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UptronWeb.Controllers
{
    public class EmployeeProfileController : CommonController
    {
        // GET: EmployeeProfile
        public ActionResult EmployeeProfile()
        {
            return View();
        }
    }
}