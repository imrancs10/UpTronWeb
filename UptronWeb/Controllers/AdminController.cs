using DataLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UptronWeb.BAL;
using UptronWeb.BAL.Common;
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
            return View();
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
            return View();
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
            Partner partner = new Partner
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
            return View();
        }
        public ActionResult Services()
        {
            var bal = new GeneralDetailBAL();
            var news = bal.GetAllServiceDetail();
            return View(news);
        }
        [HttpPost]
        public ActionResult Services(string ServiceName, string ImageCaption, string OrderNumber, string IsActive, int? Id, HttpPostedFileBase SliderImage)
        {
            HttpRequestBase request = ControllerContext.HttpContext.Request;
            string ServicDescription = request.Unvalidated.Form.Get("ServicDescription");
            byte[] fileattachment = null;
            fileattachment = Utility.serilizeImagetoByte(SliderImage, fileattachment);
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

            if (SliderImage != null)
                serviceDetail.Image = fileattachment;

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
            byte[] fileByte = result.Image;
            return File(fileByte, "image/jpg");
        }

    }
}