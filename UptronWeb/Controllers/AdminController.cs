﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UptronWeb.Controllers
{
    public class AdminController : CommonController
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
    }
}