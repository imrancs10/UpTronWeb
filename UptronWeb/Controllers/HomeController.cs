using DataLayer;
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
            MasterBAL bal = new MasterBAL();
            var result = bal.GetAllTenderViewList();
            return View(result);
        }

        public ActionResult AboutUS_Objective()
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
        public ActionResult GalleryDetail(int Id)
        {
            MasterBAL bal = new MasterBAL();
            var result = bal.GetAllGalleryPhotoByGalleryId(Id);
            return View(result);
        }

        public ActionResult JobPortal()
        {
            return View();
        }

        [HttpPost]
        public JsonResult JobPortalSave(JobRegistrationModel model)
        {
            JobRegistrationDetails detail = new JobRegistrationDetails();
            model.Password = Convert.ToString(ConfigurationManager.AppSettings["JobRegistrationDefaultPassword"]);
            var result = detail.SaveJobPortal(model);
            //send mail for user credential
            SendMailForJobPortal(model.Name, model.EmailId);
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
        private async Task SendMailForJobPortal(string Name, string Email)
        {
            await Task.Run(() =>
            {
                Message msg = new Message()
                {
                    MessageTo = Email,
                    MessageNameTo = Name,
                    Subject = "Uptron Job Registration",
                    Body = EmailHelper.GetJobRegistrationEmail(Name, Email)
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
            services.ForEach(x => { x.Image = null; x.ServiceDescription = null; });
            return Json(services);
        }
        public ActionResult Services(int Id)
        {
            GeneralDetailBAL bal = new GeneralDetailBAL();
            var service = bal.GetServiceDetailById(Id);
            var base64 = Convert.ToBase64String(service.Image);
            var imgsrc = string.Format("data:image/jpg;base64,{0}", base64);
            service.Name = imgsrc; //it just a hack to pass image to view
            return View(service);
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
            var keyfunctionaries = bal.GetAllFunctionariesList();
            List<KeyFunctionaryModel> keyFunctionariesmodelList = new List<KeyFunctionaryModel>();
            keyfunctionaries.ForEach(x =>
            {
                var base64 = Convert.ToBase64String(x.Image);
                var imgsrc = string.Format("data.image/jpg, {0}", base64);
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


        public JsonResult GetFunctionariesList()
        {
            GeneralDetailBAL bal = new GeneralDetailBAL();
            var functionaries = bal.GetAllFunctionarieDetail();
            var result = JsonConvert.SerializeObject(functionaries, Formatting.Indented,
                            new JsonSerializerSettings
                            {
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                            });
            return Json(result);
        }
    }
}