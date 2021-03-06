﻿using DataLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UptronWeb.BAL;
using UptronWeb.BAL.Common;
using UptronWeb.BAL.Master;
using UptronWeb.Global;
using UptronWeb.Models.JobPortal;
using UptronWeb.Infrastructure;
using UptronWeb.Infrastructure.Utility;
using System.Configuration;
using UptronWeb.Models.Common;
using UptronWeb.Models;
using System.Web.UI;

namespace UptronWeb.Controllers
{
    public class HomeController : CommonController
    {
        // GET: Home
        public ActionResult Index()
        {
            GeneralDetailBAL bal = new GeneralDetailBAL();
            var services = bal.GetAllActiveServiceDetail();
            var sliders = bal.GetAllActiveSliderDetail();
            List<ServiceModel> modelList = new List<ServiceModel>();
            services.ForEach(x =>
            {
                var base64 = Convert.ToBase64String(x.SliderImage);
                var imgsrc = string.Format("data:image/jpg;base64,{0}", base64);
                modelList.Add(new ServiceModel()
                {
                    Name = x.Name,
                    Id = x.Id,
                    SliderImage = imgsrc,
                });
            });
            ViewData["ServiceSlider"] = modelList;
            List<SliderModel> slidermodelList = new List<SliderModel>();
            sliders.ForEach(x =>
            {
                var base64 = Convert.ToBase64String(x.SliderImage);
                var imgsrc = string.Format("data:image/jpg;base64,{0}", base64);
                slidermodelList.Add(new SliderModel()
                {
                    Id = x.Id,
                    SliderImage = imgsrc,
                    SliderName = x.SliderName,
                    Caption1 = x.Caption1,
                    Caption2 = x.Caption2,
                    CaptionAuthor = x.CaptionAuthor,
                });
            });
            ViewData["Slider"] = slidermodelList;
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
            MasterBAL bal = new MasterBAL();
            var result = bal.GetAllTenderViewList();
            return View(result);
        }

        public ActionResult AboutUS_Objective()
        {
            return View();
        }

        public ActionResult FullDirector_Message()
        {
            return View();
        }

        public ActionResult MajorProject()
        {
            GeneralDetailBAL bal = new GeneralDetailBAL();
            var result = bal.GetAllMajorProjects();
            return View(result);
        }

        public ActionResult Gallery()
        {
            MasterBAL bal = new MasterBAL();
            var result = bal.GetAllGalleryName();
            return View(result);
        }
        public ActionResult GalleryDetail(int Id)
        {
            MasterBAL bal = new MasterBAL();
            var result = bal.GetAllGalleryPhotoByGalleryId(Id);
            return View(result);
        }

        public JsonResult GetAllGalleryMasterList()
        {
            GeneralDetailBAL bal = new GeneralDetailBAL();
            var services = bal.GetAllGalleryListLatestnine();
            List<GalleryMasterModel> GalleryMasterList = new List<GalleryMasterModel>();
            services.ForEach(x =>
            {
                var base64 = Convert.ToBase64String(x.GalleryImage);
                var imgsrc = string.Format("data:image/jpg;base64,{0}", base64);
                GalleryMasterList.Add(new GalleryMasterModel()
                {
                    GalleryName = x.GalleryName,
                    Id = x.Id,
                    GalleryImage = imgsrc,
                });
            });

            return Json(GalleryMasterList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult JobPortal()
        {
            JobRegistrationDetails detail = new JobRegistrationDetails();
            var jobForm = detail.GetJobPortalRegistrationForm();
            return View(jobForm);
        }

        [HttpPost]
        public JsonResult JobPortalSave(JobRegistrationModel model)
        {
            JobRegistrationDetails detail = new JobRegistrationDetails();
            string defaultPassword = ConfigurationManager.AppSettings["JobRegistrationDefaultPassword"].ToString();
            defaultPassword += VerificationCodeGeneration.GetDefaultPasswordCode();
            model.Password = defaultPassword;
            var result = detail.SaveJobPortal(model);
            //send mail for user credential
            SendMailForJobPortal(model.Name, model.EmailId, defaultPassword);
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
        private async Task SendMailForJobPortal(string Name, string Email, string password)
        {
            await Task.Run(() =>
            {
                Message msg = new Message()
                {
                    MessageTo = Email,
                    MessageNameTo = Name,
                    Subject = "Uptron Job Registration",
                    Body = EmailHelper.GetJobRegistrationEmail(Name, Email, password)
                };

                ISendMessageStrategy sendMessageStrategy = new SendMessageStrategyForEmail(msg);
                sendMessageStrategy.SendMessages();
            });
        }
        public ActionResult ViewNewsFile(int Id)
        {
            MasterBAL bal = new MasterBAL();
            var result = bal.GetNewsUpdateById(Id);
            byte[] fileByte = result.NewsFile;
            return File(fileByte, "application/pdf");
        }

        public JsonResult GetNewsAndUpdateList()
        {
            MasterBAL bal = new MasterBAL();
            var news = bal.GetAllActiveNewsUpdate();
            var result = JsonConvert.SerializeObject(news, Formatting.Indented,
                        new JsonSerializerSettings
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
            return Json(result);
        }

        public JsonResult GetAllMajorProjects()
        {
            GeneralDetailBAL bal = new GeneralDetailBAL();
            var projects = bal.GetAllMajorProjects();
            var result = JsonConvert.SerializeObject(projects, Formatting.Indented,
                        new JsonSerializerSettings
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
            return Json(result);
        }

        public ActionResult ViewUpcomingEventsFile(int Id)
        {
            MasterBAL bal = new MasterBAL();
            var result = bal.GetAllUpcomingEventsById(Id);
            byte[] fileByte = result.EventsFile;
            return File(fileByte, "application/pdf");
        }

        public JsonResult GetUpcomingEventsList()
        {
            MasterBAL bal = new MasterBAL();
            var Events = bal.GetAllActiveUpcomingEvents();
            var result = JsonConvert.SerializeObject(Events, Formatting.Indented,
                            new JsonSerializerSettings
                            {
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                            });
            return Json(result);
        }
        public JsonResult GetDirectorMessage()
        {
            GeneralDetailBAL bal = new GeneralDetailBAL();
            var director = bal.GetLatestDirectorMessageDetail();
            var base64 = Convert.ToBase64String(director.Photo);
            var imgsrc = string.Format("data:image/jpg;base64,{0}", base64);
            DirectorMessageModel model = new DirectorMessageModel()
            {
                Designation = director.Designation,
                Id = director.Id,
                Message = director.Message,
                Name = director.Name,
                Photo = imgsrc
            };
            return Json(model);
        }

        public JsonResult GetDirectorMessagePage()
        {
            GeneralDetailBAL bal = new GeneralDetailBAL();
            var director = bal.GetDirectorMessagePageDetail();
            var base64 = Convert.ToBase64String(director.Photo);
            var imgsrc = string.Format("data:image/jpg;base64,{0}", base64);
            DirectorMessageModel model = new DirectorMessageModel()
            {
                Designation = director.Designation,
                Id = director.Id,
                Message = director.Message,
                Name = director.Name,
                Photo = imgsrc
            };
            return Json(model);
        }
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contact(string Name, string Email, string Phone, string Message)
        {
            GeneralDetailBAL bal = new GeneralDetailBAL();
            ContactUsDetail contactus = new ContactUsDetail()
            {
                CreatedDate = DateTime.Now,
                Email = Email,
                Message = Message,
                Name = Name,
                PhoneNumber = Phone,
                IsArchive = false
            };

            var result = bal.SaveContactUs(contactus);
            if (result == Enums.CrudStatus.Saved)
            {
                SendMailForContactUs(Name, Email, Phone, Message);
                SetAlertMessage("Your Detail has been sent");
            }
            else
            {
                SetAlertMessage("Your Detail has not been sent");
            }
            return View();

        }
        private async Task SendMailForContactUs(string Name, string Email, string Phone, string Message)
        {
            await Task.Run(() =>
            {
                Message msg = new Message()
                {
                    MessageTo = ConfigurationManager.AppSettings["RecivingEmailAddress"].ToString(),
                    MessageNameTo = ConfigurationManager.AppSettings["RecivingEmailName"].ToString(),
                    Subject = "Contact Us Request",
                    Body = EmailHelper.GetContactUsEmail(Name, Email, Phone, Message)
                };

                ISendMessageStrategy sendMessageStrategy = new SendMessageStrategyForEmail(msg);
                sendMessageStrategy.SendMessages();
            });
        }


        public JsonResult GetServiceMenus()
        {
            GeneralDetailBAL bal = new GeneralDetailBAL();
            var services = bal.GetAllServiceDetail();
            services.ForEach(x => { x.PageHeaderImage = null; x.SliderImage = null; x.ServiceDescription = null; });
            return Json(services);
        }
        public ActionResult Services(int Id)
        {
            GeneralDetailBAL bal = new GeneralDetailBAL();
            var service = bal.GetServiceDetailById(Id);
            var base64 = Convert.ToBase64String(service.PageHeaderImage);
            var imgsrc = string.Format("data:image/jpg;base64,{0}", base64);
            service.Name = imgsrc; //it just a hack to pass image to view
            return View(service);
        }
        [OutputCache(Location = OutputCacheLocation.Server, Duration = 600, VaryByParam = "")]
        public JsonResult GetAllServiceSliderList()
        {
            GeneralDetailBAL bal = new GeneralDetailBAL();
            var services = bal.GetAllActiveServiceDetail();
            List<ServiceModel> modelList = new List<ServiceModel>();
            services.ForEach(x =>
            {
                var base64 = Convert.ToBase64String(x.SliderImage);
                var imgsrc = string.Format("data:image/jpg;base64,{0}", base64);
                modelList.Add(new ServiceModel()
                {
                    Name = x.Name,
                    Id = x.Id,
                    SliderImage = imgsrc,
                });
            });

            return Json(modelList, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetPartnerList()
        {
            GeneralDetailBAL bal = new GeneralDetailBAL();
            var partners = bal.GetLatestPartnerDetail();
            List<PartnerModel> modelList = new List<PartnerModel>();
            partners.ForEach(x =>
            {
                var base64 = Convert.ToBase64String(x.PartnerImage);
                var imgsrc = string.Format("data:image/jpg;base64,{0}", base64);
                modelList.Add(new PartnerModel()
                {
                    PartnerName = x.PartnerName,
                    Id = x.Id,
                    PartnerUrl = x.PartnerUrl,
                    PartnerImage = imgsrc

                });
            });

            return Json(modelList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetKeyFunctionariesList()
        {
            GeneralDetailBAL bal = new GeneralDetailBAL();
            var keyfunctionaries = bal.GetAllFunctionarieDetailLatestTwo();
            List<KeyFunctionaryModel> keyFunctionariesmodelList = new List<KeyFunctionaryModel>();
            keyfunctionaries.ForEach(x =>
            {
                var base64 = Convert.ToBase64String(x.Image);
                var imgsrc = string.Format("data:image/jpg;base64,{0}", base64);
                keyFunctionariesmodelList.Add(new KeyFunctionaryModel()
                {
                    Id = x.Id,
                    Image = imgsrc,
                    Name = x.Name,
                    Designation = x.Designation,
                    Location = x.Location
                });
            });
            return Json(keyFunctionariesmodelList, JsonRequestBehavior.AllowGet);
        }
        //public JsonResult GetFunctionariesList()
        //{
        //    GeneralDetailBAL bal = new GeneralDetailBAL();
        //    var functionaries = bal.GetAllFunctionarieDetailLatestTwo();

        //    var base64 = Convert.ToBase64String(director.Photo);
        //    var imgsrc = string.Format("data:image/jpg;base64,{0}", base64);
        //    var result = JsonConvert.SerializeObject(functionaries, Formatting.Indented,
        //                    new JsonSerializerSettings
        //                    {
        //                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        //                    });
        //    return Json(result);
        //}


        public JsonResult GetAllSliderDetailList()
        {
            GeneralDetailBAL bal = new GeneralDetailBAL();
            var sliders = bal.GetAllActiveSliderDetail();
            List<SliderModel> slidermodelList = new List<SliderModel>();
            sliders.ForEach(x =>
            {
                var base64 = Convert.ToBase64String(x.SliderImage);
                var imgsrc = string.Format("data:image/jpg;base64,{0}", base64);
                slidermodelList.Add(new SliderModel()
                {
                    Id = x.Id,
                    SliderImage = imgsrc,
                    SliderName = x.SliderName,
                    Caption1 = x.Caption1,
                    Caption2 = x.Caption2,
                    CaptionAuthor = x.CaptionAuthor,
                });
            });
            return Json(slidermodelList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Enquiry()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Enquiry(string Name, string Mobile, string Email, string EnquiryMessage)
        {
            GeneralDetailBAL bal = new GeneralDetailBAL();
            QuickEnquiry quickenquiry = new QuickEnquiry()
            {
                Name = Name,
                Mobile = Mobile,
                Email = Email,
                Enquiry = EnquiryMessage,
                IsArchive = false,
                CreatedDate = DateTime.Now
            };
            var result = bal.SaveQuickEnquiry(quickenquiry);
            if (result == Enums.CrudStatus.Saved)
            {
                SendMailForQuickEnquiry(Name, Email, Mobile, EnquiryMessage);
                SetAlertMessage("Your Enquiry Has been Saved and mail to Admin", "Enquiry Saved");
            }
            else
            {
                SetAlertMessage("Enquiry has not been Saved", "Not Saveds");
            }
            return RedirectToAction("Index");
        }

        private async Task SendMailForQuickEnquiry(string Name, string Mobile, string Email, string EnquiryMessage)
        {
            await Task.Run(() =>
            {
                Message msg = new Message()
                {
                    MessageTo = ConfigurationManager.AppSettings["RecivingEmailAddress"].ToString(),
                    MessageNameTo = ConfigurationManager.AppSettings["RecivingEmailName"].ToString(),
                    Subject = "Quick Enquiry Request",
                    Body = EmailHelper.GetQuickEnquiryEmail(Name, Mobile, Email, EnquiryMessage)
                };

                ISendMessageStrategy sendMessageStrategy = new SendMessageStrategyForEmail(msg);
                sendMessageStrategy.SendMessages();
            });
        }
        [HttpPost]
        public ActionResult CheckJobSeekerEmailId(string emailId)
        {
            JobRegistrationDetails detail = new JobRegistrationDetails();
            var result = detail.CheckJobSeekerEmailId(emailId);
            if (result != null)
                return Json(true);
            else
                return Json(false);
        }

        //public JsonResult GetAllWhyUptron()
        //{
        //    GeneralDetailBAL bal = new GeneralDetailBAL();
        //    var whyuptron = bal.GetwhyuptroneDetail();
        //    WhyUptron abc = new WhyUptron()
        //    {
        //        Id = abc.Id,
        //        Counter = abc.Counter,
        //        CounterName = abc.CounterName,
        //        OrderNumber = abc.OrderNumber
        //    };
        //    return Json(abc);
        //}

        public JsonResult GetAllWhyUptron()
        {
            GeneralDetailBAL bal = new GeneralDetailBAL();
            var whyuptron = bal.GetwhyuptroneDetail();
            return Json(whyuptron, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CheckVendorDetail(VendorModel vendorModel)
        {
            GeneralDetailBAL bal = new GeneralDetailBAL();
            var vendor = bal.CheckVendorDetail(vendorModel.VenderCode, vendorModel.MobileNumber, vendorModel.EmailID);
            if (vendor != null)
            {
                string verificationCode = VerificationCodeGeneration.GenerateDeviceVerificationCode();
                Session["otp"] = verificationCode;
                Session["vendorId"] = vendor.Id;
                SendMailForVendorVerification(vendor.VendorName, vendor.EmailId, verificationCode);
                return Json("OTP Sent");
            }
            return Json("OTP Sent Fail");
        }

        [HttpPost]
        public JsonResult CheckVendorOTP(VendorModel vendorModel)
        {
            if (Convert.ToString(vendorModel.OTPText) == Convert.ToString(Session["otp"]))
            {
                Session["otp"] = null;
                return Json("OTP Mached");
            }
            return Json("OTP Not Mached");
        }

        [HttpPost]
        public ActionResult VendorDocument(HttpPostedFileBase documentFile)
        {
            byte[] fileattachment = null;
            fileattachment = Utility.serilizeImagetoByte(documentFile, fileattachment);
            string extension = documentFile.FileName.Substring(documentFile.FileName.LastIndexOf('.'));
            GeneralDetailBAL bal = new GeneralDetailBAL();
            VendorDocument document = new VendorDocument()
            {
                VendorId = Convert.ToInt32(Session["vendorId"]),
                DocumentFile = fileattachment,
                FileExtension = extension,
                CreatedDate = DateTime.Now
            };
            var result = bal.SaveVendorDocument(document);
            SetAlertMessage("Vendor Document has been saved", "Document");
            TempData["hasDocumentUpload"] = true;
            return RedirectToAction("UploadDocument");
        }
        private async Task SendMailForVendorVerification(string Name, string Email, string verificationCode)
        {
            await Task.Run(() =>
            {
                Message msg = new Message()
                {
                    MessageTo = Email,
                    MessageNameTo = Name,
                    Subject = "Uptron OTP Verification",
                    Body = EmailHelper.GetVendorVerificationEmail(Name, verificationCode)
                };

                ISendMessageStrategy sendMessageStrategy = new SendMessageStrategyForEmail(msg);
                sendMessageStrategy.SendMessages();
            });
        }

        public ActionResult UploadDocument()
        {
            if (Convert.ToBoolean(TempData["hasDocumentUpload"]) == true)
            {
                ViewData["DocumentUpload"] = true;
            }
            return View();
        }
    }
}