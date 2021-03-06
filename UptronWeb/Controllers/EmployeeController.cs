﻿using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using UptronWeb.BAL;
using UptronWeb.BAL.Login;
using System.Configuration;
using UptronWeb.Global;
using UptronWeb.Infrastructure.Authentication;
using UptronWeb.Infrastructure;
using UptronWeb.Infrastructure.Utility;
using UptronWeb.BAL.Common;

namespace UptronWeb.Controllers
{
    public class EmployeeController : CommonController
    {
        public ActionResult Login()
        {
            return View();
        }
        [EmployeeAuthorize]
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
                SetAlertMessage("Invalid Login credentials or Your profile is not active yet.", "Login Response");
            return RedirectToAction("Login");
        }

        [EmployeeAuthorize]
        public ActionResult EmployeeProfile()
        {
            var user = User as CustomPrincipal;
            JobRegistrationDetails detail = new JobRegistrationDetails();
            var result = detail.GetJobPortalRegistrationById(user.Id);
            return View(result);
        }

        [EmployeeAuthorize]
        public ActionResult EmployeeResignation()
        {
            EmployeeDetails bal = new EmployeeDetails();
            var result = bal.GetJObResignation();
            return View(result);
        }
        [HttpPost]
        public JsonResult GetSelectedVendor()
        {
            CommonDetails _details = new CommonDetails();
            var user = User as CustomPrincipal;
            return Json(_details.GetSelectedVendor(user.Id));
        }
        [HttpPost]
        public ActionResult EmployeeResignation(string Vendor, string ResignationDate, string ResignationReason, int? Id)
        {
            EmployeeDetails bal = new EmployeeDetails();
            var user = User as CustomPrincipal;
            JobResignation jobresignation = new JobResignation()
            {
                Id = Id != null ? Id.Value : 0,
                VendorId = Convert.ToInt32(Vendor),
                JobRegistrationId = user.Id,
                ResignationDate = Convert.ToDateTime(ResignationDate),
                ResignationReason = ResignationReason,
            };
            if (Id == null)
            {
                jobresignation.CreatedDate = DateTime.Now;
            }
            var result = bal.SaveEmployeeResignation(jobresignation);
            if (result != null)
            {
                SendMailForEmployeeSlip(result);
                SetAlertMessage("Employee Resignation data has been Saved", "Resignation");
            }
            else
            {
                SetAlertMessage("Employee Resignation data has not been sent.", "Resignation");
            }

            return RedirectToAction("EmployeeResignation");
        }

        private async Task SendMailForEmployeeSlip(JobResignation vendor)
        {
            await Task.Run(() =>
            {
                Message msg = new Message()
                {
                    MessageTo = ConfigurationManager.AppSettings["RecivingEmailAddress"].ToString(),
                    MessageNameTo = ConfigurationManager.AppSettings["RecivingEmailName"].ToString(),
                    Subject = "Employee Resignation Request",
                    Body = EmailHelper.GetEmployeeResignationEmail(vendor)
                };

                ISendMessageStrategy sendMessageStrategy = new SendMessageStrategyForEmail(msg);
                sendMessageStrategy.SendMessages();

                msg = new Message()
                {
                    MessageTo = vendor.JobRegistration.EmailId,
                    MessageNameTo = vendor.JobRegistration.Name,
                    Subject = "Your Resignation Request",
                    //change email body
                    Body = EmailHelper.GetEmployeeResignationEmail(vendor)
                };

                sendMessageStrategy = new SendMessageStrategyForEmail(msg);
                sendMessageStrategy.SendMessages();
            });
        }
        [EmployeeAuthorize]
        public ActionResult EmployeeSlip()
        {
            EmployeeDetails emp = new EmployeeDetails();
            var result = emp.GetAllEmployeeSlip();
            return View(result);
        }
        [HttpPost]
        public ActionResult EmployeeSlip(string Vendor, string joiningDate, HttpPostedFileBase SalarySlip, HttpPostedFileBase PFSlip, HttpPostedFileBase ESISlip, int? Id)
        {
            byte[] fileattachment = null;
            fileattachment = Utility.serilizeImagetoByte(SalarySlip, fileattachment);
            byte[] fileattachment1 = null;
            fileattachment1 = Utility.serilizeImagetoByte(PFSlip, fileattachment1);
            byte[] fileattachment2 = null;
            fileattachment2 = Utility.serilizeImagetoByte(ESISlip, fileattachment2);
            EmployeeDetails emp = new EmployeeDetails();
            EmployeeSlip employeeslip = new EmployeeSlip()
            {
                Id = Id != null ? Id.Value : 0,
                VendorId = Convert.ToInt32(Vendor),
            };
            if (Id == null)
            {
                employeeslip.CreatedDate = DateTime.Now;
            }
            if (SalarySlip != null)
            {
                employeeslip.SalarySlip = fileattachment;
            }
            if (PFSlip != null)
            {
                employeeslip.PfSlip = fileattachment1;
            }
            if (ESISlip != null)
            {
                employeeslip.EsiSlip = fileattachment2;
            }
            var result = emp.SaveEmployeeResignation(employeeslip);
            if (result == Enums.CrudStatus.Saved)
            {
                SetAlertMessage("Employee Slip has been saved", "Employee Slip");
            }
            else if (result == Enums.CrudStatus.Updated)
            {
                SetAlertMessage("Employee Slip has been Update", "Employee Slip");
            }
            else
            {
                SetAlertMessage("Employee Slip has been Already exists", "Already exists");
            }
            var message = emp.GetAllEmployeeSlip();
            return View(message);
        }

        public ActionResult ViewSalarySlip(int Id)
        {
            EmployeeDetails emp = new EmployeeDetails();
            var result = emp.GetSalarySlip(Id);
            byte[] fileByte = result.SalarySlip;
            return File(fileByte, "application/pdf");
        }

        public ActionResult ViewPFSlip(int Id)
        {
            EmployeeDetails bal = new EmployeeDetails();
            var result = bal.GetPFSlip(Id);
            byte[] fileByte = result.PfSlip;
            return File(fileByte, "application/pdf");
        }

        public ActionResult ViewESISlip(int Id)
        {
            EmployeeDetails bal = new EmployeeDetails();
            var result = bal.GetESISlip(Id);
            byte[] fileByte = result.EsiSlip;
            return File(fileByte, "application/pdf");
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

        [HttpPost]
        public ActionResult ChangePassword(string txtoldpassowrd, string txtnewpassword, string txtconfirmpassword, string redirectUrl)
        {
            EmployeeDetails detail = new EmployeeDetails();
            if (txtnewpassword != txtconfirmpassword)
            {
                SetAlertMessage("New Password and Confirm Password are not matched");
                string action = redirectUrl.Substring(redirectUrl.LastIndexOf("/") + 1, redirectUrl.Length - (redirectUrl.LastIndexOf("/") + 1));
                return RedirectToAction(action, "Employee");
            }
            var user = User as CustomPrincipal;
            var result = detail.ChangePassword(txtoldpassowrd, txtnewpassword, user.EmailId);
            if (result == true)
            {
                SetAlertMessage("Password Updated.");
                string action = redirectUrl.Substring(redirectUrl.LastIndexOf("/") + 1, redirectUrl.Length - (redirectUrl.LastIndexOf("/") + 1));
                return RedirectToAction(action, "Employee");
            }
            else
            {
                SetAlertMessage("Old Password is Wrong.");
                string action = redirectUrl.Substring(redirectUrl.LastIndexOf("/") + 1, redirectUrl.Length - (redirectUrl.LastIndexOf("/") + 1));
                return RedirectToAction(action, "Employee");
            }
        }
        [EmployeeAuthorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Login", "Employee");
        }
        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}