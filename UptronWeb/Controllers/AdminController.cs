﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UptronWeb.BAL;
using UptronWeb.BAL.Login;

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
            var list = detail.GetJobPortalList();
            ViewData["JobList"] = list;
            return View();
        }

        public ActionResult JobPortalRegistrationViewDetail(string registrationId)
        {
            JobRegistrationDetails detail = new JobRegistrationDetails();
            var registrationDetail = detail.GetJobPortalRegistrationById(Convert.ToInt32(registrationId));
            ViewData["JobRegistrationDetail"] = registrationDetail;
            return View();
        }

        public ActionResult ViewResumePdf(int Id)
        {
            JobRegistrationDetails detail = new JobRegistrationDetails();
            var registrationDetail = detail.GetJobPortalRegistrationById(Convert.ToInt32(Id));
            var resumePdf = registrationDetail.Resume;
            return File(resumePdf, "application/pdf");
        }

        public ActionResult ViewCandidateImage(int Id)
        {
            JobRegistrationDetails detail = new JobRegistrationDetails();
            var registrationDetail = detail.GetJobPortalRegistrationById(Convert.ToInt32(Id));
            var image = registrationDetail.ResumeImage;
            return File(image, "application/jpeg");
        }
    }
}