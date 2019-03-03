using DataLayer;
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
using UptronWeb.Infrastructure.Authentication;
using UptronWeb.Infrastructure.Utility;

namespace UptronWeb.Controllers
{
    [AdminAuthorize]
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

        public ActionResult TenderDelete(int Id)
        {
            MasterBAL bal = new MasterBAL();
            var result = bal.DeleteTender(Id);
            return RedirectToAction("TenderView", new { deleteMessage = true });
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

        public ActionResult DeleteGalleryCategory(int Id)
        {
            MasterBAL bal = new MasterBAL();
            var result = bal.DeleteGalleryCategory(Id);
            return RedirectToAction("GalleryCategory", new { deleteMessage = true });
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

        public ActionResult StateMaster(bool? deleteMessage)
        {
            if (deleteMessage == true)
            {
                SetAlertMessage("State has been Deleted", "State Master");
            }
            //MasterBAL bal = new MasterBAL();
            //var news = bal.GetAllStates();
            return View();
        }
        [HttpPost]
        public ActionResult StateMaster(string StateName, int? Id)
        {
            MasterBAL bal = new MasterBAL();
            State state = new State()
            {
                StateName = StateName,
                StateId = Id != null ? Id.Value : 0,
            };
            var result = bal.SaveState(state);
            if (result == Enums.CrudStatus.Saved)
            {
                SetAlertMessage("State has been saved", "State Master");
            }
            else if (result == Enums.CrudStatus.Updated)
            {
                SetAlertMessage("State has been Updated", "State Master");
            }
            else
            {
                SetAlertMessage("State alreday exists", "State Master");
            }
            //var news = bal.GetAllStates();
            return RedirectToAction("StateMaster");
        }
        public ActionResult DeleteState(int Id)
        {
            MasterBAL bal = new MasterBAL();
            var result = bal.DeleteState(Id);
            return RedirectToAction("StateMaster", new { deleteMessage = true });
        }

        [HttpPost]
        public ActionResult GetAllStateList()
        {
            MasterBAL bal = new MasterBAL();
            string draw = Request.Form.GetValues("draw").FirstOrDefault();
            string start = Request.Form.GetValues("start").FirstOrDefault();
            string length = Request.Form.GetValues("length").FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            string filterText = Request["search[value]"];
            List<State> result = bal.GetAllStates();

            if (!string.IsNullOrEmpty(filterText))
            {
                result = result.Where(x => x.StateName.Contains(filterText, StringComparison.InvariantCultureIgnoreCase)).ToList();
            }

            recordsTotal = result.Count();
            List<State> data = result.Skip(skip).Take(pageSize).ToList();
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReligionMaster(bool? deleteMessage)
        {
            if (deleteMessage == true)
            {
                SetAlertMessage("Religion has been Deleted", "Religion Master");
            }
            //MasterBAL bal = new MasterBAL();
            //var news = bal.GetAllStates();
            return View();
        }
        [HttpPost]
        public ActionResult ReligionMaster(string religionName, int? Id)
        {
            MasterBAL bal = new MasterBAL();
            Religion religion = new Religion()
            {
                ReligionName = religionName,
                ReligionId = Id != null ? Id.Value : 0,
            };
            var result = bal.SaveReligion(religion);
            if (result == Enums.CrudStatus.Saved)
            {
                SetAlertMessage("Religion has been saved", "Religion Master");
            }
            else if (result == Enums.CrudStatus.Updated)
            {
                SetAlertMessage("Religion has been Updated", "Religion Master");
            }
            else
            {
                SetAlertMessage("Religion alreday exists", "Religion Master");
            }
            //var news = bal.GetAllStates();
            return RedirectToAction("ReligionMaster");
        }

        private static Enums.CrudStatus GetUpdated()
        {
            return Enums.CrudStatus.Updated;
        }

        public ActionResult DeleteReligion(int Id)
        {
            MasterBAL bal = new MasterBAL();
            var result = bal.DeleteReligion(Id);
            return RedirectToAction("ReligionMaster", new { deleteMessage = true });
        }

        [HttpPost]
        public ActionResult GetAllReligionList()
        {
            MasterBAL bal = new MasterBAL();
            string draw = Request.Form.GetValues("draw").FirstOrDefault();
            string start = Request.Form.GetValues("start").FirstOrDefault();
            string length = Request.Form.GetValues("length").FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            string filterText = Request["search[value]"];
            List<Religion> result = bal.GetAllReligion();

            if (!string.IsNullOrEmpty(filterText))
            {
                result = result.Where(x => x.ReligionName.Contains(filterText, StringComparison.InvariantCultureIgnoreCase)).ToList();
            }

            recordsTotal = result.Count();
            List<Religion> data = result.Skip(skip).Take(pageSize).ToList();
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GenderMaster(bool? deleteMessage)
        {
            if (deleteMessage == true)
            {
                SetAlertMessage("Gender has been Deleted", "Gender Master");
            }
            //MasterBAL bal = new MasterBAL();
            //var news = bal.GetAllStates();
            return View();
        }
        [HttpPost]
        public ActionResult GenderMaster(string GenderName, string IsActive, int? Id)
        {
            MasterBAL bal = new MasterBAL();
            Gender gender = new Gender()
            {
                GenderName = GenderName,
                IsActive = IsActive == "on" ? true : false,
                GenderId = Id != null ? Id.Value : 0,
            };
            var result = bal.SaveGender(gender);
            if (result == Enums.CrudStatus.Saved)
            {
                SetAlertMessage("Gender has been saved", "Gender Master");
            }
            else if (result == Enums.CrudStatus.Updated)
            {
                SetAlertMessage("Gender has been Updated", "Gender Master");
            }
            else
            {
                SetAlertMessage("Gender alreday exists", "Gender Master");
            }
            //var news = bal.GetAllStates();
            return RedirectToAction("GenderMaster");
        }

        public ActionResult GetAllGenderList()
        {
            MasterBAL bal = new MasterBAL();
            string draw = Request.Form.GetValues("draw").FirstOrDefault();
            string start = Request.Form.GetValues("start").FirstOrDefault();
            string length = Request.Form.GetValues("length").FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            string filterText = Request["search[value]"];
            List<Gender> result = bal.GetAllGender();

            if (!string.IsNullOrEmpty(filterText))
            {
                result = result.Where(x => x.GenderName.Contains(filterText, StringComparison.InvariantCultureIgnoreCase)).ToList();
            }

            recordsTotal = result.Count();
            List<Gender> data = result.Skip(skip).Take(pageSize).ToList();
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteGender(int Id)
        {
            MasterBAL bal = new MasterBAL();
            var result = bal.DeleteGender(Id);
            return RedirectToAction("GenderMaster", new { deleteMessage = true });
        }

        public ActionResult MaritalMaster(bool? deleteMessage)
        {
            if (deleteMessage == true)
            {
                SetAlertMessage("Marital Status has been Deleted", "Marital Master");
            }
            return View();
        }
        [HttpPost]
        public ActionResult MaritalMaster(string MaritalName, string IsActive, int? Id)
        {
            MasterBAL bal = new MasterBAL();
            Marital marital = new Marital()
            {
                MaritalName = MaritalName,
                IsActive = IsActive == "on" ? true : false,
                MaritalId = Id != null ? Id.Value : 0,
            };
            var result = bal.SaveMarital(marital);
            if (result == Enums.CrudStatus.Saved)
            {
                SetAlertMessage("Marital Status has been saved", "Marital Master");
            }
            else if (result == Enums.CrudStatus.Updated)
            {
                SetAlertMessage("Marital Status has been Updated", "Marital Master");
            }
            else
            {
                SetAlertMessage("Marital Status alreday exists", "Marital Master");
            }
            //var news = bal.GetAllStates();
            return RedirectToAction("MaritalMaster");
        }

        public ActionResult GetAllMaritalList()
        {
            MasterBAL bal = new MasterBAL();
            string draw = Request.Form.GetValues("draw").FirstOrDefault();
            string start = Request.Form.GetValues("start").FirstOrDefault();
            string length = Request.Form.GetValues("length").FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            string filterText = Request["search[value]"];
            List<Marital> result = bal.GetAllMarital();

            if (!string.IsNullOrEmpty(filterText))
            {
                result = result.Where(x => x.MaritalName.Contains(filterText, StringComparison.InvariantCultureIgnoreCase)).ToList();
            }

            recordsTotal = result.Count();
            List<Marital> data = result.Skip(skip).Take(pageSize).ToList();
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteMarital(int Id)
        {
            MasterBAL bal = new MasterBAL();
            var result = bal.DeleteMarital(Id);
            return RedirectToAction("MaritalMaster", new { deleteMessage = true });
        }

        public ActionResult IdentityMaster(bool? deleteMessage)
        {
            if (deleteMessage == true)
            {
                SetAlertMessage("Identity has been Deleted", "Identity Master");
            }
            return View();
        }
        [HttpPost]
        public ActionResult IdentityMaster(string IdentityName, string IsActive, int? Id)
        {
            MasterBAL bal = new MasterBAL();
            Identity identity = new Identity()
            {
                IdentityName = IdentityName,
                IsActive = IsActive == "on" ? true : false,
                IdentityId = Id != null ? Id.Value : 0,
            };
            var result = bal.SaveIdentity(identity);
            if (result == Enums.CrudStatus.Saved)
            {
                SetAlertMessage("Identity has been saved", "Identity Master");
            }
            else if (result == Enums.CrudStatus.Updated)
            {
                SetAlertMessage("Identity has been Updated", "Identity Master");
            }
            else
            {
                SetAlertMessage("Identity alreday exists", "Identity Master");
            }
            //var news = bal.GetAllStates();
            return RedirectToAction("IdentityMaster");
        }

        public ActionResult GetAllIdentityList()
        {
            MasterBAL bal = new MasterBAL();
            string draw = Request.Form.GetValues("draw").FirstOrDefault();
            string start = Request.Form.GetValues("start").FirstOrDefault();
            string length = Request.Form.GetValues("length").FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            string filterText = Request["search[value]"];
            List<Identity> result = bal.GetAllIdentity();

            if (!string.IsNullOrEmpty(filterText))
            {
                result = result.Where(x => x.IdentityName.Contains(filterText, StringComparison.InvariantCultureIgnoreCase)).ToList();
            }

            recordsTotal = result.Count();
            List<Identity> data = result.Skip(skip).Take(pageSize).ToList();
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteIdentity(int Id)
        {
            MasterBAL bal = new MasterBAL();
            var result = bal.DeleteIdentity(Id);
            return RedirectToAction("IdentityMaster", new { deleteMessage = true });
        }

        public ActionResult LanguageMaster(bool? deleteMessage)
        {
            if (deleteMessage == true)
            {
                SetAlertMessage("Language has been Deleted", "Language Master");
            }
            return View();
        }
        [HttpPost]
        public ActionResult LanguageMaster(string LanguageName, string IsActive, int? Id)
        {
            MasterBAL bal = new MasterBAL();
            Language language = new Language()
            {
                LanguageName = LanguageName,
                IsActive = IsActive == "on" ? true : false,
                LanguageId = Id != null ? Id.Value : 0,
            };
            var result = bal.SaveLanguage(language);
            if (result == Enums.CrudStatus.Saved)
            {
                SetAlertMessage("Language has been saved", "Language Master");
            }
            else if (result == Enums.CrudStatus.Updated)
            {
                SetAlertMessage("Language has been Updated", "Language Master");
            }
            else
            {
                SetAlertMessage("Language alreday exists", "Language Master");
            }
            //var news = bal.GetAllStates();
            return RedirectToAction("LanguageMaster");
        }

        public ActionResult GetAllLanguageList()
        {
            MasterBAL bal = new MasterBAL();
            string draw = Request.Form.GetValues("draw").FirstOrDefault();
            string start = Request.Form.GetValues("start").FirstOrDefault();
            string length = Request.Form.GetValues("length").FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            string filterText = Request["search[value]"];
            List<Language> result = bal.GetAllLanguage();

            if (!string.IsNullOrEmpty(filterText))
            {
                result = result.Where(x => x.LanguageName.Contains(filterText, StringComparison.InvariantCultureIgnoreCase)).ToList();
            }

            recordsTotal = result.Count();
            List<Language> data = result.Skip(skip).Take(pageSize).ToList();
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteLanguage(int Id)
        {
            MasterBAL bal = new MasterBAL();
            var result = bal.DeleteLanguage(Id);
            return RedirectToAction("LanguageMaster", new { deleteMessage = true });
        }

        public ActionResult DeleteSkill(int Id)
        {
            MasterBAL bal = new MasterBAL();
            var result = bal.DeleteSkill(Id);
            return RedirectToAction("SkillMaster", new { deleteMessage = true });
        }

        public ActionResult SkillMaster(bool? deleteMessage)
        {
            if (deleteMessage == true)
            {
                SetAlertMessage("Skill has been Deleted", "Skill Master");
            }
            return View();
        }
        [HttpPost]
        public ActionResult SkillMaster(string SkillName, string IsActive, int? Id)
        {
            MasterBAL bal = new MasterBAL();
            Skill skill = new Skill()
            {
                SkillsName = SkillName,
                IsActive = IsActive == "on" ? true : false,
                SkillsId = Id != null ? Id.Value : 0,
            };
            var result = bal.SaveSkill(skill);
            if (result == Enums.CrudStatus.Saved)
            {
                SetAlertMessage("Skill has been saved", "Skill Master");
            }
            else if (result == Enums.CrudStatus.Updated)
            {
                SetAlertMessage("Skill has been Updated", "Skill Master");
            }
            else
            {
                SetAlertMessage("Skill alreday exists", "Skill Master");
            }
            //var news = bal.GetAllStates();
            return RedirectToAction("SkillMaster");
        }

        public ActionResult GetAllSkillList()
        {
            MasterBAL bal = new MasterBAL();
            string draw = Request.Form.GetValues("draw").FirstOrDefault();
            string start = Request.Form.GetValues("start").FirstOrDefault();
            string length = Request.Form.GetValues("length").FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            string filterText = Request["search[value]"];
            List<Skill> result = bal.GetAllSkill();

            if (!string.IsNullOrEmpty(filterText))
            {
                result = result.Where(x => x.SkillsName.Contains(filterText, StringComparison.InvariantCultureIgnoreCase)).ToList();
            }

            recordsTotal = result.Count();
            List<Skill> data = result.Skip(skip).Take(pageSize).ToList();
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteCity(int Id)
        {
            MasterBAL bal = new MasterBAL();
            var result = bal.DeleteCity(Id);
            return RedirectToAction("CityMaster", new { deleteMessage = true });
        }

        public ActionResult CityMaster(bool? deleteMessage)
        {
            if (deleteMessage == true)
            {
                SetAlertMessage("City has been Deleted", "City Master");
            }
            return View();
        }
        [HttpPost]
        public ActionResult CityMaster(string State, string CityName, int? Id)
        {
            MasterBAL bal = new MasterBAL();
            City city = new City()
            {
                StateId = Convert.ToInt32(State),
                CityName = CityName,
                CityId = Id != null ? Id.Value : 0
            };
            var result = bal.SaveCity(city);
            if (result == Enums.CrudStatus.Saved)
            {
                SetAlertMessage("City has been saved", "City Saved");
            }
            else if (result == Enums.CrudStatus.Updated)
            {
                SetAlertMessage("City has been Updated", "City Updated");
            }
            else
            {
                SetAlertMessage("City has been Already Exists", "Data Already Exists");
            }
            return RedirectToAction("CityMaster");
        }

        public ActionResult GetAllCityList()
        {
            MasterBAL bal = new MasterBAL();
            string draw = Request.Form.GetValues("draw").FirstOrDefault();
            string start = Request.Form.GetValues("start").FirstOrDefault();
            string length = Request.Form.GetValues("length").FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            string filterText = Request["search[value]"];
            var result = bal.GetAllCity();

            if (!string.IsNullOrEmpty(filterText))
            {
                result = result.Where(x => x.CityName.Contains(filterText, StringComparison.InvariantCultureIgnoreCase)
                                         || x.StateName.Contains(filterText, StringComparison.InvariantCultureIgnoreCase)).ToList();
            }

            recordsTotal = result.Count();
            var data = result.Skip(skip).Take(pageSize).ToList();
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult WhyUptron()
        {
            GeneralDetailBAL bal = new GeneralDetailBAL();
            var result = bal.GetAllWhyUptron();
            return View(result);
        }

        [HttpPost]
        public ActionResult WhyUptron(string Counter, string CounterName, string IsActive, string OrderNumber, int? Id)
        {
            GeneralDetailBAL bal = new GeneralDetailBAL();
            WhyUptron whyuptron = new WhyUptron()
            {
                Counter = Convert.ToInt32(Counter),
                CounterName = CounterName,
                IsActive = IsActive == "on" ? true : false,
                Id = Id != null ? Id.Value : 0,
            };
            if (!string.IsNullOrEmpty(OrderNumber))
            {
                whyuptron.OrderNumber = Convert.ToInt32(OrderNumber);
            }
            if (Id == null)
            {
                whyuptron.Createddate = DateTime.Now;
            }
            var result = bal.SaveWhyUptron(whyuptron);
            if (result == Enums.CrudStatus.Saved)
            {
                SetAlertMessage("Why Uptron has been Saved", "Why Uptron Saved");
            }
            else if (result == Enums.CrudStatus.Updated)
            {
                SetAlertMessage("Why Uptron has been Update", "Why Uptron Updated");
            }
            else
            {
                SetAlertMessage("Why Uptron has been Already Exists", "Why Uptron Exists");
            }

            return RedirectToAction("WhyUptron");
        }

        public ActionResult DeleteWhyUptronById(int Id)
        {
            GeneralDetailBAL bal = new GeneralDetailBAL();
            var result = bal.DeleteWhyUptron(Id);
            return RedirectToAction("WhyUptron");
        }
    }
}