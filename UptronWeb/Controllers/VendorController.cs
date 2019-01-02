using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UptronWeb.Controllers
{
    public class VendorController : Controller
    {
        // GET: Vendor
        public ActionResult VendorEntry()
        {
            return View();
        }
        public ActionResult VendorViewDetails()
        {
            return View();
        }
    }
}