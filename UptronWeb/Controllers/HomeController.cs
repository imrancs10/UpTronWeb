using DataLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UptronWeb.BAL;
using UptronWeb.BAL.Master;
using UptronWeb.Models.JobPortal;

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
            MasterBAL bal = new MasterBAL();
            var result = bal.GetAllCircularList();
            return View(result);
        }

        public ActionResult AboutUS_Tender()
        {
            return View();
        }

        public ActionResult AboutUS_Objective()
        {
            return View();
        }

        public ActionResult Service_Hospital()
        {
            return View();
        }

        public ActionResult Service_Eprocurement()
        {
            return View();
        }

        public ActionResult Service_Manpower()
        {
            return View();
        }

        public ActionResult Service_Software()
        {
            return View();
        }

        public ActionResult Service_ITEnabled()
        {
            return View();
        }

        public ActionResult Service_Amc()
        {
            return View();
        }

        public ActionResult Service_Training()
        {
            return View();
        }

        public ActionResult Service_Miscelleneous()
        {
            return View();
        }

        public ActionResult MajorProject()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Gallery()
        {
            MasterBAL bal = new MasterBAL();
            var result = bal.GetAllGalleryName();
            return View(result);
        }
        public ActionResult GalleryDetail()
        {
            return View();
        }

        public ActionResult JobPortal()
        {
            return View();
        }

        [HttpPost]
        public JsonResult JobPortalSave(JobRegistrationModel model)
        {
            JobRegistrationDetails detail = new JobRegistrationDetails();
            var result = detail.SaveJobPortal(model);
            return Json(CrudResponse(result), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult JobPortalFileSave()
        {
            JobRegistrationDetails detail = new JobRegistrationDetails();
            JobRegistration registration = new JobRegistration();
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    //Resume
                    HttpPostedFileBase file = files[0];
                    MemoryStream target = new MemoryStream();
                    file.InputStream.CopyTo(target);
                    byte[] data = target.ToArray();
                    registration.Resume = data;

                    //Image
                    file = files[1];
                    target = new MemoryStream();
                    file.InputStream.CopyTo(target);
                    data = target.ToArray();
                    registration.ResumeImage = data;

                    registration.Id = Convert.ToInt32(Session["registrationId"]);
                    detail.UpdateJobPortal(registration);
                    Session["registrationId"] = null;
                    // Returns message that successfully uploaded  
                    return Json("Registration for Job is successful.");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }


        }

    }
}