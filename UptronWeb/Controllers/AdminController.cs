﻿using DataLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UptronWeb.BAL;
using UptronWeb.BAL.Common;
using UptronWeb.BAL.Master;
using UptronWeb.Global;
using UptronWeb.Infrastructure;
using UptronWeb.Infrastructure.Utility;

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
            if (resumePdf != null)
                return File(resumePdf, "application/pdf");
            return null;
        }

        public ActionResult ViewCandidateImage(int Id)
        {
            JobRegistrationDetails detail = new JobRegistrationDetails();
            JobRegistration registrationDetail = detail.GetJobPortalRegistrationById(Convert.ToInt32(Id));
            byte[] image = registrationDetail.ResumeImage;
            return File(image, "image/jpg");
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
                ViewData["GoCircular"] = result;
            }
            return View();
        }
        [HttpPost]
        public ActionResult GOCircularEntry(string GoNumber, string Subject, string GoDate, HttpPostedFileBase GOFile, int? Id)
        {
            byte[] fileAttachment = null;
            fileAttachment = Utility.serilizeImagetoByte(GOFile, fileAttachment);
            MasterBAL bal = new MasterBAL();
            GOCircular goCircular = new GOCircular()
            {
                Id = Id != null ? Id.Value : 0,
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
        public ActionResult ViewFileGoCircular(int Id)
        {
            MasterBAL bal = new MasterBAL();
            var result = bal.GetGOCircular(Id);
            byte[] fileByte = result.GOFile;
            return File(fileByte, "application/pdf");
        }


        public ActionResult TenderEntry(int? Id)
        {
            if (Id != null && Id > 0)
            {
                MasterBAL bal = new MasterBAL();
                var result = bal.GetTender(Id.Value);
                ViewData["Tenders"] = result;
            }
            return View();
        }
        [HttpPost]
        public ActionResult TenderEntry(string TenderNumber, string TenderDate, string Subject, HttpPostedFileBase TenderFile, int? Id)
        {
            byte[] fileattachment = null;
            fileattachment = Utility.serilizeImagetoByte(TenderFile, fileattachment);
            MasterBAL bal = new MasterBAL();
            Tender tender = new Tender()
            {
                TenderNumber = TenderNumber,
                TenderDate = Convert.ToDateTime(TenderDate),
                TenderFile = fileattachment,
                Subject = Subject,
                id = Id != null ? Id.Value : 0,
            };
            if (TenderFile != null)
                tender.TenderFile = fileattachment;
            var result = bal.SaveTender(tender);
            if (result == Enums.CrudStatus.Saved)
            {
                SetAlertMessage("Tender has been saved", "Tender");
            }
            else if (result == Enums.CrudStatus.Updated)
            {
                SetAlertMessage("Tender has been Updated", "Tender");
            }
            else
            {
                SetAlertMessage("Tender alreday exists");
            }
            return RedirectToAction("TenderEntry");
        }
        public ActionResult TenderView()
        {
            MasterBAL bal = new MasterBAL();
            var result = bal.GetAllTenderViewList();
            return View(result);
        }
        public ActionResult ViewTenderFile(int Id)
        {
            MasterBAL bal = new MasterBAL();
            var result = bal.GetTender(Id);
            byte[] fileByte = result.TenderFile;
            return File(fileByte, "application/pdf");
        }



        public ActionResult GalleryCategory()
        {
            MasterBAL bal = new MasterBAL();
            var gallerycategory = bal.GetAllGalleryName();
            return View(gallerycategory);
        }
        [HttpPost]
        public ActionResult GalleryCategory(string GalleryName, HttpPostedFileBase GalleryImage)
        {
            byte[] fileattachment = null;
            fileattachment = Utility.serilizeImagetoByte(GalleryImage, fileattachment);
            MasterBAL bal = new MasterBAL();
            GalleryMaster gallery = new GalleryMaster()
            {
                GalleryName = GalleryName,
                GalleryImage = fileattachment

            };
            var result = bal.SaveGalleryMaster(gallery);
            if (result == Enums.CrudStatus.Saved)
            {
                SetAlertMessage("Gallery has been saved", "Tender");
            }
            else
            {
                SetAlertMessage("Gallery number alreday exists");
            }
            return View();
        }
        public ActionResult PhotoGallery()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PhotoGallery(string GalleryCategory, string PhotoName, HttpPostedFileBase GalleryPhotoFile)
        {
            byte[] fileattachment = null;
            fileattachment = Utility.serilizeImagetoByte(GalleryPhotoFile, fileattachment);
            MasterBAL bal = new MasterBAL();
            GalleryPhotoMaster galleryPhoto = new GalleryPhotoMaster()
            {
                GalleryId = Convert.ToInt32(GalleryCategory),
                Photo = fileattachment,
                PhotoName = PhotoName

            };
            var result = bal.SaveGalleryPhotoMaster(galleryPhoto);
            if (result == Enums.CrudStatus.Saved)
            {
                SetAlertMessage("Gallery has been saved", "Tender");
            }
            else
            {
                SetAlertMessage("Gallery number alreday exists");
            }
            return View();
        }
        public JsonResult GetGalleryDetail()
        {
            MasterBAL bal = new MasterBAL();
            var data = bal.GetAllGalleryName();
            data.ForEach(x => x.GalleryImage = null);
            var result = JsonConvert.SerializeObject(data, Formatting.Indented,
                         new JsonSerializerSettings
                         {
                             ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                         });
            return Json(result);
        }


        public ActionResult NewsAndUpdate()
        {
            MasterBAL bal = new MasterBAL();
            var news = bal.GetAllNewsUpdate();
            return View(news);
        }
        [HttpPost]
        public ActionResult NewsAndUpdate(string NewsTitle, string IsNew, string IsActive, HttpPostedFileBase NewsFile, int? Id)
        {
            byte[] fileattachment = null;
            fileattachment = Utility.serilizeImagetoByte(NewsFile, fileattachment);
            MasterBAL bal = new MasterBAL();
            NewsUpdateMaster newsUpdate = new NewsUpdateMaster()
            {
                Title = NewsTitle,
                IsActive = IsActive == "on" ? true : false,
                IsNew = IsNew == "on" ? true : false,
                Id = Id != null ? Id.Value : 0,
            };
            if (Id != null)
                newsUpdate.ModifiedDate = DateTime.Now;
            else
                newsUpdate.CreatedDate = DateTime.Now;
            if (NewsFile != null)
                newsUpdate.NewsFile = fileattachment;

            var result = bal.SaveNewsAndUpdate(newsUpdate);
            if (result == Enums.CrudStatus.Saved)
            {
                SetAlertMessage("News Update has been saved", "News Update");
            }
            else if (result == Enums.CrudStatus.Updated)
            {
                SetAlertMessage("News Detail has been Updated", "News Update");
            }
            else
            {
                SetAlertMessage("News & Update alreday exists", "News Update");
            }
            var news = bal.GetAllNewsUpdate();
            return View(news);
        }
        public ActionResult DeleteNewsAndUpdate(int Id)
        {
            MasterBAL bal = new MasterBAL();
            var result = bal.DeleteNewsAndUpdate(Id);
            return RedirectToAction("NewsAndUpdate");
        }


        public ActionResult EventsUpcoming()
        {
            MasterBAL bal = new MasterBAL();
            var Events = bal.GetAllUpcomingEvents();
            return View(Events);
        }
        [HttpPost]
        public ActionResult EventsUpcoming(string EventsTitle, string IsNew, string IsActive, HttpPostedFileBase EventsFile, int? Id)
        {
            byte[] fileAttachment = null;
            fileAttachment = Utility.serilizeImagetoByte(EventsFile, fileAttachment);
            MasterBAL bal = new MasterBAL();
            UpcomingEventsMaster upcomingEvents = new UpcomingEventsMaster()
            {
                Title = EventsTitle,
                IsNew = IsNew == "on" ? true : false,
                IsActive = IsActive == "on" ? true : false,
                Id = Id != null ? Id.Value : 0
            };
            if (Id != null)
                upcomingEvents.ModifiedDate = DateTime.Now;
            else
                upcomingEvents.CreatedDate = DateTime.Now;
            if (EventsFile != null)
            {
                upcomingEvents.EventsFile = fileAttachment;
            }
            var result = bal.SaveEventsUpcoming(upcomingEvents);
            if (result == Enums.CrudStatus.Saved)
            {
                SetAlertMessage("Upcoming Events has been saved", "UpcomingEvents");
            }
            else if (result == Enums.CrudStatus.Updated)
            {
                SetAlertMessage("Upcoming Events has been Updated", "UpcomingEvents");
            }
            else
            {
                SetAlertMessage("Upcoming Events already Exists", "UpcomingEvents");
            }
            var Events = bal.GetAllUpcomingEvents();
            return View(Events);
        }
        public ActionResult DeleteUpcomingEvents(int Id)
        {
            MasterBAL bal = new MasterBAL();
            var result = bal.DeleteUpcomingEvents(Id);
            return RedirectToAction("EventsUpcoming");
        }


        public ActionResult ContactUsDetail()
        {
            GeneralDetailBAL bal = new GeneralDetailBAL();
            var list = bal.GetAllContactUsDetails();
            return View(list);
        }
        public ActionResult ContactUsArchiveDetail()
        {
            GeneralDetailBAL bal = new GeneralDetailBAL();
            var list = bal.GetAllContactUsArchiveDetails();
            return View(list);
        }
        public ActionResult ArchiveContactUs(int Id)
        {
            GeneralDetailBAL bal = new GeneralDetailBAL();
            var status = bal.ArchiveContactUsDetails(Id);
            if (status == Enums.CrudStatus.Updated)
            {
                SetAlertMessage("Contact Us detail is Archived");
            }
            else
            {
                SetAlertMessage("Contact Us detail is not Archived");
            }
            return RedirectToAction("ContactUsDetail");
        }


        public ActionResult DirectorMessage()
        {
            GeneralDetailBAL bal = new GeneralDetailBAL();
            var news = bal.GetAllDirectorMessageDetail();
            return View(news);
        }
        [HttpPost]
        public ActionResult DirectorMessage(string Directorname, string Directordesignation,
                                            string Message, HttpPostedFileBase DirectorFile, int? Id)
        {
            byte[] fileattachment = null;
            fileattachment = Utility.serilizeImagetoByte(DirectorFile, fileattachment);
            GeneralDetailBAL bal = new GeneralDetailBAL();
            DirectorMessageDetail directorMessage = new DirectorMessageDetail()
            {
                Designation = Directordesignation,
                Message = Message,
                Name = Directorname,
                Id = Id != null ? Id.Value : 0,
            };
            if (Id == null)
                directorMessage.CreatedDate = DateTime.Now;

            if (DirectorFile != null)
                directorMessage.Photo = fileattachment;

            var result = bal.SaveDirectorMessage(directorMessage);
            if (result == Enums.CrudStatus.Saved)
            {
                SetAlertMessage("Director Message has been saved", "News Update");
            }
            else if (result == Enums.CrudStatus.Updated)
            {
                SetAlertMessage("Director Message has been Updated", "News Update");
            }
            else
            {
                SetAlertMessage("Director Message alreday exists", "News Update");
            }
            var messages = bal.GetAllDirectorMessageDetail();
            return View(messages);
        }


        public ActionResult KeyFunctionaries()
        {
            GeneralDetailBAL bal = new GeneralDetailBAL();
            var functinaries = bal.GetAllFunctionariesList();
            return View(functinaries);
        }
        [HttpPost]
        public ActionResult KeyFunctionaries(string name, string Location, string Designation, HttpPostedFileBase keyfunctionariesFile, int? Id)
        {
            byte[] fileattachment = null;
            fileattachment = Utility.serilizeImagetoByte(keyfunctionariesFile, fileattachment);
            GeneralDetailBAL bal = new GeneralDetailBAL();
            KeyFunctionary keyFunctionaries = new KeyFunctionary()
            {
                Name = name,
                Location = Location,
                Designation = Designation,
                Id = Id != null ? Id.Value : 0,
            };
            if (Id == null)
            {
                keyFunctionaries.CreatedDate = DateTime.Now;
            }
            if (keyfunctionariesFile != null)
            {
                keyFunctionaries.Image = fileattachment;
            }
            var result = bal.SaveKeyFunctionaries(keyFunctionaries);
            if (result == Enums.CrudStatus.Saved)
            {
                SetAlertMessage("Key Functionaries Has been Saved", "Key Functionaries Saved");
            }
            else if (result == Enums.CrudStatus.Updated)
            {
                SetAlertMessage("Key Functionaries has been Updated", "Key Functionaries Updated");
            }
            else
            {
                SetAlertMessage("Key Functionaries has been Already Exists", "Key Functionaries Exists");
            }
            var message = bal.GetAllFunctionarieDetailLatestTwo();
            return View(message);
        }

        public ActionResult DeleteKeyFunctionaries(int Id)
        {
            GeneralDetailBAL bal = new GeneralDetailBAL();
            var result = bal.DeleteKeyFunctionariesById(Id);
            return RedirectToAction("KeyFunctionaries");
        }

        public ActionResult Partner()
        {
            GeneralDetailBAL bal = new GeneralDetailBAL();
            var partner = bal.GetAllPartnerDetail();
            return View(partner);
        }
        [HttpPost]
        public ActionResult Partner(string Partnername, string PartnerUrl, HttpPostedFileBase PartnerFile, int? Id)
        {
            byte[] fileattachment = null;
            fileattachment = Utility.serilizeImagetoByte(PartnerFile, fileattachment);
            GeneralDetailBAL bal = new GeneralDetailBAL();
            Partner partner = new Partner()
            {
                PartnerName = Partnername,
                PartnerUrl = PartnerUrl,
                Id = Id != null ? Id.Value : 0,
            };

            if (Id == null)
                partner.CreatedDate = DateTime.Now;

            if (PartnerFile != null)
                partner.PartnerImage = fileattachment;

            var result = bal.SavePartner(partner);
            if (result == Enums.CrudStatus.Saved)
            {
                SetAlertMessage("Partner has been saved", "Partner Added");
            }
            else if (result == Enums.CrudStatus.Updated)
            {
                SetAlertMessage("Partner has been Updated", "Partner Update");
            }
            else
            {
                SetAlertMessage("Partner alreday exists", "Partner Update");
            }
            var messages = bal.GetAllPartnerDetail();
            return View(messages);
        }


        public ActionResult Majorprojects()
        {
            GeneralDetailBAL bal = new GeneralDetailBAL();
            var majorprojects = bal.GetAllMajorProjects();
            return View(majorprojects);
        }

        [HttpPost]
        public ActionResult Majorprojects(string DepartmentTitle, string Departmentname, string OrderNumber, string IsActive, string WorkDescription, HttpPostedFileBase ProjectsImage, int? Id)
        {
            byte[] fileattachment = null;
            fileattachment = Utility.serilizeImagetoByte(ProjectsImage, fileattachment);
            GeneralDetailBAL bal = new GeneralDetailBAL();
            MajorProject majorprojects = new MajorProject()
            {
                Title = DepartmentTitle,
                DepartmentName = Departmentname,
                IsActive = IsActive == "on" ? true : false,
                WorkDescription = WorkDescription,
                ID = Id != null ? Id.Value : 0,
            };
            if (!string.IsNullOrEmpty(OrderNumber))
                majorprojects.OrderNumber = Convert.ToInt32(OrderNumber);
            else
                majorprojects.OrderNumber = null;

            if (Id == null)
                majorprojects.Createddate = DateTime.Now;
            if (ProjectsImage != null)
            {
                majorprojects.Image = fileattachment;
            }
            var result = bal.SaveMajorprojects(majorprojects);
            if (result == Enums.CrudStatus.Saved)
            {
                SetAlertMessage("Major Projects has beed saved", "Major Projects saved");
            }
            else if (result == Enums.CrudStatus.Updated)
            {
                SetAlertMessage("Major Projects has been Updated", "Major Projects updated");
            }
            else
            {
                SetAlertMessage("Major Projects data already exists", "Major Projects Exists");
            }
            var Projects = bal.GetAllMajorProjects();
            return View(Projects);
        }
        public ActionResult DeleteMajorProjectsById(int Id)
        {
            GeneralDetailBAL bal = new GeneralDetailBAL();
            var result = bal.DeleteMajorProjects(Id);
            return RedirectToAction("Majorprojects");
        }
        public ActionResult Services()
        {
            var bal = new GeneralDetailBAL();
            var news = bal.GetAllServiceDetail();
            return View(news);
        }
        [HttpPost]
        public ActionResult Services(string ServiceName, string ImageCaption, string OrderNumber, string IsActive, int? Id, HttpPostedFileBase PageHeaderImage, HttpPostedFileBase SliderImage)
        {
            HttpRequestBase request = ControllerContext.HttpContext.Request;
            string ServicDescription = request.Unvalidated.Form.Get("ServicDescription");
            byte[] fileattachment = null;
            fileattachment = Utility.serilizeImagetoByte(PageHeaderImage, fileattachment);

            byte[] fileattachment1 = null;
            fileattachment1 = Utility.serilizeImagetoByte(SliderImage, fileattachment1);
            var bal = new GeneralDetailBAL();
            ServiceDetail serviceDetail = new ServiceDetail()
            {
                Name = ServiceName,
                Caption = ImageCaption,
                ServiceDescription = ServicDescription,
                IsActive = IsActive == "on" ? true : false,
                Id = Id != null ? Id.Value : 0,
            };
            if (!string.IsNullOrEmpty(OrderNumber))
                serviceDetail.OrderNumber = Convert.ToInt32(OrderNumber);
            else
                serviceDetail.OrderNumber = null;
            if (Id == null)
                serviceDetail.CreatedDate = DateTime.Now;

            if (PageHeaderImage != null)
                serviceDetail.PageHeaderImage = fileattachment;

            if (SliderImage != null)
                serviceDetail.SliderImage = fileattachment1;

            var result = bal.SaveServiceDetail(serviceDetail);
            if (result == Enums.CrudStatus.Saved)
            {
                SetAlertMessage("Service Detail has been saved", "News Update");
            }
            else if (result == Enums.CrudStatus.Updated)
            {
                SetAlertMessage("Service Detail has been Updated", "News Update");
            }
            else
            {
                SetAlertMessage("Service Detail alreday exists", "News Update");
            }
            var news = bal.GetAllServiceDetail();
            return View(news);
        }
        public ActionResult ViewServiceImage(int Id)
        {
            var bal = new GeneralDetailBAL();
            var result = bal.GetServiceDetailById(Id);
            byte[] fileByte = result.PageHeaderImage;
            return File(fileByte, "image/jpg");
        }

        public ActionResult DeleteServiceById(int Id)
        {
            GeneralDetailBAL bal = new GeneralDetailBAL();
            var result = bal.DeleteService(Id);
            return RedirectToAction("Services");
        }
        public ActionResult Slider()
        {
            GeneralDetailBAL bal = new GeneralDetailBAL();
            var result = bal.GetAllSliderDetail();
            return View(result);
        }

        [HttpPost]
        public ActionResult Slider(string SliderName, string Caption1, string Caption2, string CaptionAuthor,
                                   string OrderNumber, string IsActive, HttpPostedFileBase SliderFile, int? Id)
        {
            byte[] fileattachment = null;
            fileattachment = Utility.serilizeImagetoByte(SliderFile, fileattachment);
            GeneralDetailBAL bal = new GeneralDetailBAL();
            Slider slider = new Slider()
            {
                SliderName = SliderName,
                Caption1 = Caption1,
                Caption2 = Caption2,
                CaptionAuthor = CaptionAuthor,
                IsActive = IsActive == "on" ? true : false,
                Id = Id != null ? Id.Value : 0
            };
            if (!string.IsNullOrEmpty(OrderNumber))
                slider.OrderNumber = Convert.ToInt32(OrderNumber);
            else
                slider.OrderNumber = null;
            if (Id == null)
                slider.CreatedDate = DateTime.Now;
            if (SliderFile != null)
            {
                slider.SliderImage = fileattachment;
            }
            var result = bal.SaveSliderDetail(slider);
            if (result == Enums.CrudStatus.Saved)
            {
                SetAlertMessage("Slider has been Saved", "Slider Saved");
            }
            else if (result == Enums.CrudStatus.Updated)
            {
                SetAlertMessage("Slider has been Updated", "Slider updated");
            }
            else
            {
                SetAlertMessage("Slider Already Exists", "Slider Exists");
            }

            var message = bal.GetAllSliderDetail();
            return View(message);
        }

        public ActionResult ViewSliderImage(int Id)
        {
            var bal = new GeneralDetailBAL();
            var result = bal.GetSliderDetailById(Id);
            byte[] fileByte = result.SliderImage;
            return File(fileByte, "image/jpg");
        }

        public ActionResult DeleteSlider(int Id)
        {
            GeneralDetailBAL bal = new GeneralDetailBAL();
            var result = bal.DeleteSliderById(Id);
            return RedirectToAction("Slider");
        }


        public ActionResult QuickEnquiryDetail()
        {
            GeneralDetailBAL bal = new GeneralDetailBAL();
            var list = bal.GetAllQuickEnquiryDetails();
            return View(list);
        }
        public ActionResult QuickEnquiryArchiveDetail()
        {
            GeneralDetailBAL bal = new GeneralDetailBAL();
            var list = bal.GetAllQuickEnquiryArchiveDetails();
            return View(list);
        }
        public ActionResult ArchiveQuickEnquiry(int Id)
        {
            GeneralDetailBAL bal = new GeneralDetailBAL();
            var status = bal.ArchiveQuickEnquiryDetails(Id);
            if (status == Enums.CrudStatus.Updated)
            {
                SetAlertMessage("Quick Enquiry detail is Archived");
            }
            else
            {
                SetAlertMessage("Quick Enquiry detail is not Archived");
            }
            return RedirectToAction("QuickEnquiryDetail");
        }
        public ActionResult JobPortalRegistrationForm(string ActionForm)
        {
            JobRegistrationDetails detail = new JobRegistrationDetails();
            List<JobRegistrationForm> list = detail.GetJobPortalRegistrationForm();
            ViewData["JobFormList"] = list;
            if (ActionForm == "update")
            {
                SetAlertMessage("Registration Form Updated");
            }
            return View();
        }

        public ActionResult UpdateJobRegistrationForm(int Id, string ActionForm)
        {
            JobRegistrationDetails detail = new JobRegistrationDetails();
            var result = detail.UpdateJobRegistrationForm(Id, ActionForm);
            return RedirectToAction("JobPortalRegistrationForm", new { ActionForm = "update" });
        }

        public ActionResult VendorJobEntry(string ActionForm)
        {
            GeneralDetailBAL bAL = new GeneralDetailBAL();
            var result = bAL.GetAllVendorJob();
            if (ActionForm == "delete")
            {
                SetAlertMessage("Vendor job has been deleted", "vendor job");
            }
            return View(result);
        }
        [HttpPost]
        public ActionResult VendorJobEntry(string Vendor, string JobType, string NoofRequirement, string Skills, int? Id)
        {
            GeneralDetailBAL bal = new GeneralDetailBAL();
            VendorJob vendorjob = new VendorJob()
            {
                VendorId = Convert.ToInt32(Vendor),
                Jobtype = JobType,
                NoofRequirement = Convert.ToInt32(NoofRequirement),
                SkillsRequired = Skills,
                Id = Id != null ? Id.Value : 0
            };
            if (Id == null)
                vendorjob.CreatedDate = DateTime.Now;
            var result = bal.SaveVendorJob(vendorjob);
            if (result == Enums.CrudStatus.Saved)
            {
                SetAlertMessage("Vendor job has been saved", "vendor job Saved");
            }
            else if (result == Enums.CrudStatus.Updated)
            {
                SetAlertMessage("Vendor Job has been Updated", "Vendor job Updated");
            }
            else
            {
                SetAlertMessage("Vendor job has been Already Exists", "Data Already Exists");
            }
            var message = bal.GetAllVendorJob();
            return View(message);
        }

        public ActionResult DeleteVendorJob(int Id)
        {
            GeneralDetailBAL bal = new GeneralDetailBAL();
            var result = bal.DeletevendorJobById(Id);
            return RedirectToAction("VendorJobEntry", new { ActionForm = "delete" });
        }
        [HttpPost]
        public ActionResult AllotJobToSeeker(int jobSeekerId, int vendorJobId)
        {
            GeneralDetailBAL bal = new GeneralDetailBAL();
            var result = bal.AllotJobToSeeker(jobSeekerId, vendorJobId);
            return Json(true);
        }
        [HttpPost]
        public ActionResult PermitJobToSeeker(int jobSeekerId)
        {
            GeneralDetailBAL bal = new GeneralDetailBAL();
            var result = bal.PermitJobToSeeker(jobSeekerId);
            SendMailForJobPortal(result.Name, result.EmailId);
            return Json(true);
        }
        private async Task SendMailForJobPortal(string Name, string Email)
        {
            await Task.Run(() =>
            {
                Message msg = new Message()
                {
                    MessageTo = Email,
                    MessageNameTo = Name,
                    Subject = "Uptron Job Profile",
                    Body = EmailHelper.GetJobRegistrationActiveEmail(Name, Email)
                };

                ISendMessageStrategy sendMessageStrategy = new SendMessageStrategyForEmail(msg);
                sendMessageStrategy.SendMessages();
            });
        }
    }
}