using DataLayer;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using UptronWeb.BAL;
using UptronWeb.BAL.Master;
using UptronWeb.Global;

namespace UptronWeb.Controllers
{
    public class AdminController : CommonController
    {
        // GET: Admin
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult JobPortalRegistrationList()
        {
            JobRegistrationDetails detail = new JobRegistrationDetails();
            List<JobRegistration> list = detail.GetJobPortalList();
            ViewData["JobList"] = list;
            return View();
        }

        public ActionResult JobPortalRegistrationViewDetail(string registrationId)
        {
            JobRegistrationDetails detail = new JobRegistrationDetails();
            JobRegistration registrationDetail = detail.GetJobPortalRegistrationById(Convert.ToInt32(registrationId));
            ViewData["JobRegistrationDetail"] = registrationDetail;
            return View();
        }

        public ActionResult ViewResumePdf(int Id)
        {
            JobRegistrationDetails detail = new JobRegistrationDetails();
            JobRegistration registrationDetail = detail.GetJobPortalRegistrationById(Convert.ToInt32(Id));
            byte[] resumePdf = registrationDetail.Resume;
            return File(resumePdf, "application/pdf");
        }

        public ActionResult ViewCandidateImage(int Id)
        {
            JobRegistrationDetails detail = new JobRegistrationDetails();
            JobRegistration registrationDetail = detail.GetJobPortalRegistrationById(Convert.ToInt32(Id));
            byte[] image = registrationDetail.ResumeImage;
            return File(image, "application/jpeg");
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Login", "UptronAdmin");
        }

        public ActionResult GOCircularEntry()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SaveGOCircularDetail(string GoNumber, string Subject, string GoDate, HttpPostedFileBase GOFile)
        {
            byte[] fileAttachment = null;
            fileAttachment = Utility.serilizeImagetoByte(GOFile, fileAttachment);
            MasterBAL bal = new MasterBAL();
            GOCircular goCircular = new GOCircular()
            {
                GODate = Convert.ToDateTime(GoDate),
                GOFile = fileAttachment,
                GONumber = GoNumber,
                Subject = Subject
            };
            var result = bal.SaveGOCircluar(goCircular);
            if (result == Enums.CrudStatus.Saved)
            {
                SetAlertMessage("Data has been saved", "Go & Circular");
            }
            else
            {
                SetAlertMessage("Go No already exists", "Go & Circular");
            }
            
            return RedirectToAction("GOCircularEntry");
        }


    }
}