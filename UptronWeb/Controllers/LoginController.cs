﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UptronWeb.BAL.Login;
using UptronWeb.Global;

namespace UptronWeb.Controllers
{
    public class LoginController : CommonController
    {
        // GET: Login
        public ActionResult Index()
        {
            ViewData["LoginPage"] = true;
            return View();
        }

        public ActionResult GetLogin(string username, string password)
        {
            LoginDetails _details = new LoginDetails();
            string _response = string.Empty;
            Enums.LoginMessage message = _details.GetLogin(username, password);
            _response = LoginResponse(message);
            if (message == Enums.LoginMessage.Authenticated)
            {
                return RedirectToAction("AddDepartments", "Masters");
            }
            else
            {
                SetAlertMessage(_response, "Login Response");
                return View("index");
            }
        }

    }
}