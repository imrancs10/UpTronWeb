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
using UptronWeb.Global;
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
            EmployeeDetails bal = new EmployeeDetails();
            var result = bal.GetJObResignation();
            return View(result);
        }

        [HttpPost]
        public ActionResult EmployeeResignation(string Vendor, string ResignationDate, string ResignationReason, int? Id)
        {
            EmployeeDetails bal = new EmployeeDetails();
            JobResignation jobresignation = new JobResignation()
            {
                Id = Id != null ? Id.Value : 0,
                VendorId = Convert.ToInt32(Vendor),
                ResignationDate = Convert.ToDateTime(ResignationDate),
                ResignationReason = ResignationReason,
            };
            if (Id==null)
            {
                jobresignation.CreatedDate = DateTime.Now;
            }
            var result = bal.SaveEmployeeResignation(jobresignation);
            if (result == Enums.CrudStatus.Saved)
            {
                SetAlertMessage("Employee Resignation data has been Saved", "Resignation Saved");
            }
            else if (result == Enums.CrudStatus.Updated)
            {
                SetAlertMessage("Employee Resignation data has been Updated", "Resignation Updated");
            }
            else
            {
                SetAlertMessage("Employee Resignation data has been All ready Exists", "Resignation Exists");
            }

            return RedirectToAction("EmployeeResignation");
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