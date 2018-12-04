using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UptronWeb.Controllers
{
    public class HomeController : CommonController
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AboutUS_Company()
        {
            return View();
        }

        public ActionResult AboutUS_Circular()
        {
            return View();
        }

        public ActionResult AboutUS_Objective()
        {
            return View();
        }
    }
}