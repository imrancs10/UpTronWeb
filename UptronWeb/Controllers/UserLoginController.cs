using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UptronWeb.Controllers
{
    public class UserLoginController : CommonController
    {
        // GET: UserLogin
        public ActionResult Login()
        {
            return View();
        }
    }
}