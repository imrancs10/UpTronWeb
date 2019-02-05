using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using UptronWeb.BAL.Login;
using UptronWeb.Infrastructure.Authentication;

namespace UptronWeb.Controllers
{
    public class UptronAdminController : CommonController
    {
        // GET: Admin
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CheckAdminLogin(string username, string password)
        {
            LoginDetails detail = new LoginDetails();
            var result = detail.CheckAdminLogin(username, password);
            if (result != null)
            {
                setUserClaim(result);
                return RedirectToAction("Dashboard", "Admin");
                //dashbaord page redirect

            }
            else
                SetAlertMessage("Invalid Login credentials", "Login Response");
            return RedirectToAction("Login");
        }
        private void setUserClaim(UserMaster info)
        {
            CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
            serializeModel.Id = info.Id;
            serializeModel.UserId = info.Id;
            serializeModel.UserName = string.IsNullOrEmpty(info.UserName) ? string.Empty : info.UserName;
            serializeModel.Role = string.IsNullOrEmpty(info.RoleMaster.Role) ? string.Empty : info.RoleMaster.Role;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string userData = serializer.Serialize(serializeModel);

            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                     1,
                     info.UserName,
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