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

        public ActionResult GOCircularEntry(int? Id)
        {
            if (Id != null && Id > 0)
            {
                MasterBAL bal = new MasterBAL();
                var result = bal.GetGOCircular(Id.Value);
                return View(result);
            }
            return View(new GOCircular());
        }
        [HttpPost]
        public ActionResult GOCircularEntry(string GoNumber, string Subject, string GoDate, HttpPostedFileBase GOFile, int Id)
        {
            byte[] fileAttachment = null;
            fileAttachment = Utility.serilizeImagetoByte(GOFile, fileAttachment);
            MasterBAL bal = new MasterBAL();
            GOCircular goCircular = new GOCircular()
            {
                Id = Id,
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

        public ActionResult GOCircularView()
        {
            MasterBAL bal = new MasterBAL();
            var result = bal.GetAllCircularList();
            return View(result);
        }
        public ActionResult DeleteGOCircular(int Id)
        {
            MasterBAL bal = new MasterBAL();
            var result = bal.DeleteGOCircular(Id);
            return RedirectToAction("GOCircularView");
        }
        public ActionResult TenderEntry()
        {
            return View();
        }
        [HttpPost]
        public ActionResult TenderEntry(string TenderNumber, string TenderDate, string Subject, HttpPostedFileBase TenderFile)
        {
            byte[] fileattachment = null;
            fileattachment = Utility.serilizeImagetoByte(TenderFile, fileattachment);
            MasterBAL bal = new MasterBAL();
            Tender tender = new Tender()
            {
                TenderNumber = TenderNumber,
                TenderDate = Convert.ToDateTime(TenderDate),
                TenderFile = fileattachment,
                Subject = Subject

            };
            var result = bal.SaveTender(tender);
            if (result == Enums.CrudStatus.Saved)
            {
                SetAlertMessage("Tender has been saved", "Tender");
            }
            else
            {
                SetAlertMessage("Tender number alreday exists");
            }
            return RedirectToAction("TenderEntry");
        }
        public ActionResult TenderView()
        {
            return View();
        }

        public ActionResult GalleryCategory()
        {
            return View();
        }

        public ActionResult PhotoGallery()
        {
            return View();
        }
    }
}