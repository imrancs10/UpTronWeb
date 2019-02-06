using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UptronWeb.BAL.Common;
using UptronWeb.BAL.Login;
using UptronWeb.Global;
using UptronWeb.Infrastructure.Authentication;

namespace UptronWeb.Controllers
{
    public class CommonController : Controller
    {
        public string LoginResponse(Enums.LoginMessage inputMessage)
        {
            if (inputMessage == Enums.LoginMessage.InvalidCreadential)
                return Resource.Login_InvalidCredential;
            else
                return Global.Resource.Common_NoResponseFromServer;
        }

        public string CrudResponse(Enums.CrudStatus inputMessage)
        {
            if (inputMessage == Enums.CrudStatus.DataAlreadyExist)
                return Resource.Crud_DataAlreadyExist;
            else if (inputMessage == Enums.CrudStatus.Saved)
                return Global.Resource.Crud_DataSaved;
            else if (inputMessage == Enums.CrudStatus.NotSaved)
                return Global.Resource.Crud_DataNotSaved;
            else if (inputMessage == Enums.CrudStatus.Deleted)
                return Global.Resource.Crud_DataDelete;
            else if (inputMessage == Enums.CrudStatus.NotDeleted)
                return Global.Resource.Crud_NotDelete;
            else if (inputMessage == Enums.CrudStatus.Updated)
                return Global.Resource.Crud_DataUpdated;
            else if (inputMessage == Enums.CrudStatus.NotUpdated)
                return Global.Resource.Crud_DataNotUpdated;
            else if (inputMessage == Enums.CrudStatus.NotSaved)
                return Global.Resource.Crud_DataNotSaved;
            else
                return Resource.Common_NoResponseFromServer;
        }

        public void SetAlertMessage(string message, string title = "Alert")
        {
            TempData["Alert_Message"] = message;
            TempData["Alert_Title"] = title;
        }

        [HttpPost]
        public JsonResult GetSates()
        {
            CommonDetails _details = new CommonDetails();
            return Json(_details.GetStates());
        }

        [HttpPost]
        public JsonResult GetCities(int stateId)
        {
            CommonDetails _details = new CommonDetails();
            return Json(_details.GetCities(stateId));
        }

        [HttpPost]
        public JsonResult GetVendor()
        {
            CommonDetails _details = new CommonDetails();
            return Json(_details.GetVendor());
        }

        [HttpPost]
        public JsonResult GetJobType(int VendorId)
        {
            CommonDetails _details = new CommonDetails();
            var result = JsonConvert.SerializeObject(_details.GetJobType(VendorId), Formatting.Indented,
                        new JsonSerializerSettings
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
            return Json(result);
        }
        [HttpPost]
        public JsonResult GetRemainingSeat(int VendorJobId)
        {
            CommonDetails _details = new CommonDetails();
            return Json(_details.GetRemainingSeat(VendorJobId));
        }
        [HttpPost]
        public ActionResult ResetPassword(string txtoldpassowrd, string txtnewpassword, string txtconfirmpassword, string redirectUrl)
        {
            LoginDetails detail = new LoginDetails();
            if (txtnewpassword != txtconfirmpassword)
            {
                SetAlertMessage("New Password and Confirm Password are not matched");
                string action = redirectUrl.Substring(redirectUrl.LastIndexOf("/") + 1, redirectUrl.Length - (redirectUrl.LastIndexOf("/") + 1));
                return RedirectToAction(action, "Admin");
            }
            var user = User as CustomPrincipal;
            var result = detail.ResetPassword(txtoldpassowrd, txtnewpassword, user.AdminUserId);
            if (result == true)
            {
                SetAlertMessage("Password Updated.");
                string action = redirectUrl.Substring(redirectUrl.LastIndexOf("/") + 1, redirectUrl.Length - (redirectUrl.LastIndexOf("/") + 1));
                return RedirectToAction(action, "Admin");
            }
            else
            {
                SetAlertMessage("Old Password is Wrong.");
                string action = redirectUrl.Substring(redirectUrl.LastIndexOf("/") + 1, redirectUrl.Length - (redirectUrl.LastIndexOf("/") + 1));
                return RedirectToAction(action, "Admin");
            }
        }

    }
}