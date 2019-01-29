using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using UptronWeb.BAL;
using UptronWeb.BAL.Login;
using UptronWeb.Infrastructure.Authentication;

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
            var user = User as CustomPrincipal;
            JobRegistrationDetails detail = new JobRegistrationDetails();
            var result = detail.GetJobPortalRegistrationById(user.Id);
            return View(result);
        }

        [HttpPost]
        public ActionResult CheckLogin(string UserName, string Password)
        {
            EmployeeDetails detail = new EmployeeDetails();
            var result = detail.CheckEmployeeLogin(UserName, Password);
            if (result != null)
            {
                setUserClaim(result);
                return RedirectToAction("Dashboard");
            }
            else
                SetAlertMessage("Invalid Login credentials", "Login Response");
            return RedirectToAction("Login");
        }

        public ActionResult EmployeeProfile()
        {
            var user = User as CustomPrincipal;
            JobRegistrationDetails detail = new JobRegistrationDetails();
            var result = detail.GetJobPortalRegistrationById(user.Id);
            return View(result);
        }

        public ActionResult EmployeeResignation()
        {
            return View();
        }
        public ActionResult EmployeeSlip()
        {
            return View();
        }
        private void setUserClaim(JobRegistration info)
        {
            CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
            serializeModel.Id = info.Id;
            serializeModel.EmailId = string.IsNullOrEmpty(info.EmailId) ? string.Empty : info.EmailId;
            serializeModel.MobileNo = string.IsNullOrEmpty(info.MobileNo) ? string.Empty : info.MobileNo;
            serializeModel.Name = string.IsNullOrEmpty(info.Name) ? string.Empty : info.Name;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string userData = serializer.Serialize(serializeModel);

            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                     1,
                     info.EmailId,
                     DateTime.Now,
                     DateTime.Now.AddMinutes(15),
                     false,
                     userData);

            string encTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            Response.Cookies.Add(faCookie);
        }
    }
}