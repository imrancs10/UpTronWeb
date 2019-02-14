using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer;
using UptronWeb.Global;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace UptronWeb.BAL.Common
{
    public class GeneralDetailBAL
    {
        UptronWebEntities _db = null;

        public Enums.CrudStatus SaveContactUs(ContactUsDetail contactus)
        {
            _db = new UptronWebEntities();
            int _effectRow = 0;
            _db.Entry(contactus).State = EntityState.Added;
            _effectRow = _db.SaveChanges();
            return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
        }
        public List<ContactUsDetail> GetAllContactUsDetails()
        {
            _db = new UptronWebEntities();
            var result = _db.ContactUsDetails.Where(x => x.IsArchive == false).ToList();
            return result;
        }
        public List<ContactUsDetail> GetAllContactUsArchiveDetails()
        {
            _db = new UptronWebEntities();
            var result = _db.ContactUsDetails.Where(x => x.IsArchive == true).ToList();
            return result;
        }
        public Enums.CrudStatus ArchiveContactUsDetails(int Id)
        {
            _db = new UptronWebEntities();
            int _effectRow = 0;
            var result = _db.ContactUsDetails.Where(x => x.Id == Id).FirstOrDefault();
            result.IsArchive = true;
            _db.Entry(result).State = EntityState.Modified;
            _effectRow = _db.SaveChanges();
            return _effectRow > 0 ? Enums.CrudStatus.Updated : Enums.CrudStatus.NotUpdated;
        }
        public Enums.CrudStatus SaveDirectorMessage(DirectorMessageDetail directorMessage)
        {
            _db = new UptronWebEntities();
            int _effectRow = 0;
            if (directorMessage.Id > 0)
            {
                var _deptRow = _db.DirectorMessageDetails.Where(x => x.Id == directorMessage.Id).FirstOrDefault();
                if (_deptRow != null)
                {
                    _deptRow.Id = directorMessage.Id;
                    _deptRow.CreatedDate = directorMessage.CreatedDate;
                    _deptRow.Designation = directorMessage.Designation;
                    _deptRow.Message = directorMessage.Message;
                    _deptRow.Name = directorMessage.Name;
                    if (directorMessage.Photo != null)
                        _deptRow.Photo = directorMessage.Photo;

                    _db.Entry(_deptRow).State = EntityState.Modified;
                    _effectRow = _db.SaveChanges();
                    return _effectRow > 0 ? Enums.CrudStatus.Updated : Enums.CrudStatus.NotUpdated;
                }
                else
                {
                    return Enums.CrudStatus.DataNotFound;
                }
            }
            else
            {
                var _deptRow = _db.DirectorMessageDetails.Where(x => x.Name == directorMessage.Name).FirstOrDefault();
                if (_deptRow == null)
                {
                    _db.Entry(directorMessage).State = EntityState.Added;
                    _effectRow = _db.SaveChanges();
                    return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
                }
                else
                {
                    return Enums.CrudStatus.DataAlreadyExist;
                }
            }
        }
        public List<DirectorMessageDetail> GetAllDirectorMessageDetail()
        {
            _db = new UptronWebEntities();
            var result = _db.DirectorMessageDetails.ToList();
            return result;
        }
        public DirectorMessageDetail GetLatestDirectorMessageDetail()
        {
            _db = new UptronWebEntities();
            var result = _db.DirectorMessageDetails.OrderByDescending(x => x.CreatedDate).FirstOrDefault();
            return result;
        }

        public DirectorMessageDetail GetDirectorMessagePageDetail()
        {
            _db = new UptronWebEntities();
            var result = _db.DirectorMessageDetails.OrderByDescending(x => x.CreatedDate).FirstOrDefault();
            return result;
        }

        public Enums.CrudStatus SavePartner(Partner partner)
        {
            _db = new UptronWebEntities();
            int _effectRow = 0;
            if (partner.Id > 0)
            {
                var _deptRow = _db.Partners.Where(x => x.Id == partner.Id).FirstOrDefault();
                if (_deptRow != null)
                {
                    _deptRow.Id = partner.Id;
                    _deptRow.CreatedDate = partner.CreatedDate;
                    _deptRow.PartnerName = partner.PartnerName;
                    _deptRow.PartnerUrl = partner.PartnerUrl;
                    if (partner.PartnerImage != null)
                        _deptRow.PartnerImage = partner.PartnerImage;

                    _db.Entry(_deptRow).State = EntityState.Modified;
                    _effectRow = _db.SaveChanges();
                    return _effectRow > 0 ? Enums.CrudStatus.Updated : Enums.CrudStatus.NotUpdated;
                }
                else
                {
                    return Enums.CrudStatus.DataNotFound;
                }
            }
            else
            {
                var _deptRow = _db.Partners.Where(x => x.PartnerName == partner.PartnerName).FirstOrDefault();
                if (_deptRow == null)
                {
                    _db.Entry(partner).State = EntityState.Added;
                    _effectRow = _db.SaveChanges();
                    return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
                }
                else
                {
                    return Enums.CrudStatus.DataAlreadyExist;
                }
            }
        }

        public List<Partner> GetAllPartnerDetail()
        {
            _db = new UptronWebEntities();
            var result = _db.Partners.ToList();
            return result;
        }
        public List<Partner> GetLatestPartnerDetail()
        {
            _db = new UptronWebEntities();
            var result = _db.Partners.OrderByDescending(x => x.CreatedDate).ToList();
            return result;
        }
        public List<ServiceDetail> GetAllServiceDetail()
        {
            _db = new UptronWebEntities();
            var result = _db.ServiceDetails.OrderBy(x => x.OrderNumber).ToList();
            return result;
        }
        public List<ServiceDetail> GetAllActiveServiceDetail()
        {
            _db = new UptronWebEntities();
            var result = _db.ServiceDetails.Where(x => x.IsActive == true).OrderBy(x => x.OrderNumber).ToList();
            return result;
        }
        public ServiceDetail GetServiceDetailById(int Id)
        {
            _db = new UptronWebEntities();
            var result = _db.ServiceDetails.Where(x => x.Id == Id).FirstOrDefault();
            return result;
        }
        public Enums.CrudStatus SaveServiceDetail(ServiceDetail serviceDetail)
        {
            _db = new UptronWebEntities();
            int _effectRow = 0;
            if (serviceDetail.Id > 0)
            {
                var _deptRow = _db.ServiceDetails.Where(x => x.Id == serviceDetail.Id).FirstOrDefault();
                if (_deptRow != null)
                {
                    _deptRow.Id = serviceDetail.Id;
                    _deptRow.CreatedDate = serviceDetail.CreatedDate;
                    _deptRow.Name = serviceDetail.Name;
                    _deptRow.Caption = serviceDetail.Caption;
                    _deptRow.IsActive = serviceDetail.IsActive;
                    _deptRow.OrderNumber = serviceDetail.OrderNumber;
                    _deptRow.ServiceDescription = serviceDetail.ServiceDescription;
                    if (serviceDetail.PageHeaderImage != null)
                        _deptRow.PageHeaderImage = serviceDetail.PageHeaderImage;

                    if (serviceDetail.SliderImage != null)
                        _deptRow.SliderImage = serviceDetail.SliderImage;
                    //if (serviceDetail.OrderNumber == null || serviceDetail.OrderNumber == 0)
                    //{
                    //    var orderNo = _db.ServiceDetails.Max(p => p.OrderNumber);
                    //    if (orderNo != null)
                    //        _deptRow.OrderNumber = orderNo + 1;
                    //    else
                    //        _deptRow.OrderNumber = 1;
                    //}
                    //else
                    //{
                    //    var existingOrderNo = _db.ServiceDetails.Where(x => x.OrderNumber == serviceDetail.OrderNumber).FirstOrDefault();
                    //    if (existingOrderNo != null)
                    //    {
                    //        var orderNo = _db.ServiceDetails.Max(p => p.OrderNumber);
                    //        if (orderNo != null)
                    //            _deptRow.OrderNumber = _deptRow.OrderNumber;
                    //        else
                    //            _deptRow.OrderNumber = 1;
                    //    }
                    //    else
                    //    {
                    //        var orderNo = _db.ServiceDetails.Max(p => p.OrderNumber);
                    //        if (orderNo != null)
                    //        {
                    //            if ((serviceDetail.OrderNumber - 1) == orderNo)
                    //                _deptRow.OrderNumber = serviceDetail.OrderNumber;
                    //            else
                    //                _deptRow.OrderNumber = _deptRow.OrderNumber;
                    //        }
                    //        else
                    //            _deptRow.OrderNumber = _deptRow.OrderNumber;
                    //    }
                    //}

                    _db.Entry(_deptRow).State = EntityState.Modified;
                    _effectRow = _db.SaveChanges();
                    return _effectRow > 0 ? Enums.CrudStatus.Updated : Enums.CrudStatus.NotUpdated;
                }
                else
                {
                    return Enums.CrudStatus.DataNotFound;
                }
            }
            else
            {
                var _deptRow = _db.ServiceDetails.Where(x => x.Name == serviceDetail.Name).FirstOrDefault();
                if (_deptRow == null)
                {
                    //if (serviceDetail.OrderNumber == null || serviceDetail.OrderNumber == 0)
                    //{
                    //    var orderNo = _db.ServiceDetails.Max(p => p.OrderNumber);
                    //    if (orderNo != null)
                    //        serviceDetail.OrderNumber = orderNo + 1;
                    //    else
                    //        serviceDetail.OrderNumber = 1;
                    //}
                    //else
                    //{
                    //    var existingOrderNo = _db.ServiceDetails.Where(x => x.OrderNumber == serviceDetail.OrderNumber).FirstOrDefault();
                    //    if (existingOrderNo != null)
                    //    {
                    //        var orderNo = _db.ServiceDetails.Max(p => p.OrderNumber);
                    //        if (orderNo != null)
                    //            serviceDetail.OrderNumber = orderNo + 1;
                    //        else
                    //            serviceDetail.OrderNumber = 1;
                    //    }
                    //    else
                    //    {
                    //        var orderNo = _db.ServiceDetails.Max(p => p.OrderNumber);
                    //        if (orderNo != null)
                    //        {
                    //            if ((serviceDetail.OrderNumber - 1) == orderNo)
                    //                serviceDetail.OrderNumber = serviceDetail.OrderNumber;
                    //            else
                    //                serviceDetail.OrderNumber = orderNo + 1;
                    //        }
                    //        else
                    //            serviceDetail.OrderNumber = 1;
                    //    }
                    //}
                    _db.Entry(serviceDetail).State = EntityState.Added;

                    try
                    {
                        _effectRow = _db.SaveChanges();
                    }
                    catch (DbEntityValidationException e)
                    {
                        foreach (var eve in e.EntityValidationErrors)
                        {
                            Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                eve.Entry.Entity.GetType().Name, eve.Entry.State);
                            foreach (var ve in eve.ValidationErrors)
                            {
                                Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                    ve.PropertyName, ve.ErrorMessage);
                            }
                        }
                        throw;
                    }


                    return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
                }
                else
                {
                    return Enums.CrudStatus.DataAlreadyExist;
                }
            }
        }

        public bool DeleteService(int Id)
        {
            _db = new UptronWebEntities();
            var result = _db.ServiceDetails.FirstOrDefault(x => x.Id == Id);
            _db.ServiceDetails.Remove(result);
            _db.SaveChanges();
            return true;
        }
        public Enums.CrudStatus SaveKeyFunctionaries(KeyFunctionary keyfunctionary)
        {
            _db = new UptronWebEntities();
            int _effecttRow = 0;
            if (keyfunctionary.Id > 0)
            {
                var _deptRow = _db.KeyFunctionaries.Where(x => x.Id == keyfunctionary.Id).FirstOrDefault();
                if (_deptRow != null)
                {
                    _deptRow.Id = keyfunctionary.Id;
                    _deptRow.Name = keyfunctionary.Name;
                    _deptRow.Designation = keyfunctionary.Designation;
                    _deptRow.Location = keyfunctionary.Location;
                    if (keyfunctionary.Image != null)
                    {
                        _deptRow.Image = keyfunctionary.Image;
                    }
                    _db.Entry(_deptRow).State = EntityState.Modified;
                    _effecttRow = _db.SaveChanges();
                    return _effecttRow > 0 ? Enums.CrudStatus.Updated : Enums.CrudStatus.NotUpdated;
                }
                else
                {
                    return Enums.CrudStatus.DataNotFound;
                }
            }
            else
            {
                var _deptRow = _db.KeyFunctionaries.Where(x => x.Name == keyfunctionary.Name).FirstOrDefault();
                if (_deptRow == null)
                {
                    _db.Entry(keyfunctionary).State = EntityState.Added;
                    _effecttRow = _db.SaveChanges();
                    return _effecttRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
                }
                else
                {
                    return Enums.CrudStatus.DataAlreadyExist;
                }
            }
        }
        public List<KeyFunctionary> GetAllFunctionariesList()
        {
            _db = new UptronWebEntities();
            var result = _db.KeyFunctionaries.ToList();
            return result;
        }
        public List<KeyFunctionary> GetAllFunctionarieDetailLatestTwo()
        {
            _db = new UptronWebEntities();
            var result = _db.KeyFunctionaries.OrderByDescending(x => x.CreatedDate).Take(2).ToList();
            return result;
        }

        public bool DeleteKeyFunctionariesById(int Id)
        {
            _db = new UptronWebEntities();
            var result = _db.KeyFunctionaries.Where(x => x.Id == Id).FirstOrDefault();
            _db.KeyFunctionaries.Remove(result);
            _db.SaveChanges();
            return true;
        }

        public Enums.CrudStatus SaveSliderDetail(Slider slider)
        {
            _db = new UptronWebEntities();
            int _effetRow = 0;
            if (slider.Id > 0)
            {
                var _deptRow = _db.Sliders.Where(x => x.Id == slider.Id).FirstOrDefault();
                if (_deptRow != null)
                {
                    _deptRow.Id = slider.Id;
                    _deptRow.SliderName = slider.SliderName;
                    _deptRow.Caption1 = slider.Caption1;
                    _deptRow.Caption2 = slider.Caption2;
                    _deptRow.CaptionAuthor = slider.CaptionAuthor;
                    _deptRow.IsActive = slider.IsActive;
                    _deptRow.OrderNumber = slider.OrderNumber;
                    _deptRow.CreatedDate = slider.CreatedDate;
                    if (slider.SliderImage != null)
                    {
                        _deptRow.SliderImage = slider.SliderImage;
                    }
                    _db.Entry(_deptRow).State = EntityState.Modified;
                    _effetRow = _db.SaveChanges();
                    return _effetRow > 0 ? Enums.CrudStatus.Updated : Enums.CrudStatus.NotUpdated;
                }
                else
                {
                    return Enums.CrudStatus.DataNotFound;
                }
            }
            else
            {
                var _deptRow = _db.Sliders.Where(x => x.SliderName == slider.SliderName).FirstOrDefault();
                if (_deptRow == null)
                {
                    _db.Entry(slider).State = EntityState.Added;
                    try
                    {
                        _effetRow = _db.SaveChanges();
                    }
                    catch (DbEntityValidationException dbEx)
                    {
                        Exception raise = dbEx;
                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                string message = string.Format("{0}:{1}",
                                    validationErrors.Entry.Entity.ToString(),
                                    validationError.ErrorMessage);
                                // raise a new exception nesting  
                                // the current instance as InnerException  
                                raise = new InvalidOperationException(message, raise);
                            }
                        }
                        throw raise;
                    }
                    return _effetRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
                }
                else
                {
                    return Enums.CrudStatus.DataAlreadyExist;
                }
            }

        }

        public List<Slider> GetAllSliderDetail()
        {
            _db = new UptronWebEntities();
            var result = _db.Sliders.ToList();
            return result;
        }
        public Slider GetSliderDetailById(int Id)
        {
            _db = new UptronWebEntities();
            var result = _db.Sliders.Where(x => x.Id == Id).FirstOrDefault();
            return result;
        }

        public List<GalleryMaster> GetAllGalleryListLatestnine()
        {
            _db = new UptronWebEntities();
            var result = _db.GalleryMasters.Take(9).ToList();
            return result;
        }

        public List<Slider> GetAllActiveSliderDetail()
        {
            _db = new UptronWebEntities();
            var result = _db.Sliders.Where(x => x.IsActive == true).OrderBy(x => x.OrderNumber).ToList();
            return result;
        }

        public bool DeleteSliderById(int Id)
        {
            _db = new UptronWebEntities();
            var result = _db.Sliders.FirstOrDefault(x => x.Id == Id);
            _db.Sliders.Remove(result);
            _db.SaveChanges();
            return true;
        }

        public Enums.CrudStatus SaveQuickEnquiry(QuickEnquiry quickEnquiry)
        {
            _db = new UptronWebEntities();
            int _effectRow = 0;
            _db.Entry(quickEnquiry).State = EntityState.Added;
            _effectRow = _db.SaveChanges();
            return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
        }

        public List<QuickEnquiry> GetAllQuickEnquiryDetails()
        {
            _db = new UptronWebEntities();
            var result = _db.QuickEnquiries.Where(x => x.IsArchive == false).ToList();
            return result;
        }
        public List<QuickEnquiry> GetAllQuickEnquiryArchiveDetails()
        {
            _db = new UptronWebEntities();
            var result = _db.QuickEnquiries.Where(x => x.IsArchive == true).ToList();
            return result;
        }
        public Enums.CrudStatus ArchiveQuickEnquiryDetails(int Id)
        {
            _db = new UptronWebEntities();
            int _effectRow = 0;
            var result = _db.QuickEnquiries.Where(x => x.Id == Id).FirstOrDefault();
            result.IsArchive = true;
            _db.Entry(result).State = EntityState.Modified;
            _effectRow = _db.SaveChanges();
            return _effectRow > 0 ? Enums.CrudStatus.Updated : Enums.CrudStatus.NotUpdated;
        }

        public Enums.CrudStatus SaveMajorprojects(MajorProject majorprojects)
        {
            _db = new UptronWebEntities();
            int _effectRow = 0;
            if (majorprojects.ID > 0)
            {
                var _deptRow = _db.MajorProjects.Where(x => x.ID == majorprojects.ID).FirstOrDefault();
                if (_deptRow != null)
                {
                    _deptRow.ID = majorprojects.ID;
                    _deptRow.Title = majorprojects.Title;
                    _deptRow.DepartmentName = majorprojects.DepartmentName;
                    _deptRow.OrderNumber = majorprojects.OrderNumber;
                    _deptRow.IsActive = majorprojects.IsActive;
                    _deptRow.WorkDescription = majorprojects.WorkDescription;
                    if (_deptRow.Image != null)
                        _deptRow.Image = majorprojects.Image;
                    _db.Entry(_deptRow).State = EntityState.Modified;
                    _effectRow = _db.SaveChanges();
                    return _effectRow > 0 ? Enums.CrudStatus.Updated : Enums.CrudStatus.NotUpdated;
                }
                else
                {
                    return Enums.CrudStatus.DataNotFound;
                }
            }
            else
            {
                var _deptRow = _db.MajorProjects.Where(x => x.DepartmentName == majorprojects.DepartmentName).FirstOrDefault();
                if (_deptRow == null)
                {
                    _db.Entry(majorprojects).State = EntityState.Added;
                    _effectRow = _db.SaveChanges();
                    return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
                }
                else
                {
                    return Enums.CrudStatus.DataAlreadyExist;
                }

            }
        }

        public List<MajorProject> GetAllMajorProjects()
        {
            _db = new UptronWebEntities();
            var result = _db.MajorProjects.Where(x => x.IsActive == true).OrderBy(x => x.OrderNumber).ToList();
            return result;
        }

        public bool DeleteMajorProjects(int Id)
        {
            _db = new UptronWebEntities();
            var result = _db.MajorProjects.FirstOrDefault(x => x.ID == Id);
            _db.MajorProjects.Remove(result);
            _db.SaveChanges();
            return true;
        }

        public Enums.CrudStatus SaveVendorJob(VendorJob vendorjob)
        {
            _db = new UptronWebEntities();
            int _effectRow = 0;
            if (vendorjob.Id > 0)
            {
                var _deptRow = _db.VendorJobs.Where(x => x.Id == vendorjob.Id).FirstOrDefault();
                if (_deptRow != null)
                {
                    _deptRow.Id = vendorjob.Id;
                    _deptRow.VendorId = vendorjob.VendorId;
                    _deptRow.Jobtype = vendorjob.Jobtype;
                    _deptRow.NoofRequirement = vendorjob.NoofRequirement;
                    _deptRow.SkillsRequired = vendorjob.SkillsRequired;
                    _deptRow.CreatedDate = vendorjob.CreatedDate;
                    _db.Entry(_deptRow).State = EntityState.Modified;
                    _effectRow = _db.SaveChanges();
                    return _effectRow > 0 ? Enums.CrudStatus.Updated : Enums.CrudStatus.NotUpdated;
                }
                else
                {
                    return Enums.CrudStatus.DataNotFound;
                }

            }
            else
            {
                var _deptRow = _db.VendorJobs.Where(x => x.VendorId == vendorjob.VendorId && x.Jobtype == vendorjob.Jobtype).FirstOrDefault();
                if (_deptRow == null)
                {
                    _db.Entry(vendorjob).State = EntityState.Added;
                    _effectRow = _db.SaveChanges();
                    return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
                }
                else
                {
                    return Enums.CrudStatus.DataAlreadyExist;
                }

            }
        }

        public List<VendorJob> GetAllVendorJob()
        {
            _db = new UptronWebEntities();
            var result = _db.VendorJobs.ToList();
            return result;
        }

        public bool DeletevendorJobById(int Id)
        {
            _db = new UptronWebEntities();
            var result = _db.VendorJobs.FirstOrDefault(x => x.Id == Id);
            _db.VendorJobs.Remove(result);
            int _effectRow = _db.SaveChanges();
            return true;
        }

        public bool AllotJobToSeeker(int jobSeekerId, int vendorJobId)
        {
            _db = new UptronWebEntities();
            var jobSeeker = _db.JobRegistrations.Where(x => x.Id == jobSeekerId).FirstOrDefault();
            if (jobSeeker != null)
            {
                jobSeeker.VendorJobId = vendorJobId;
                jobSeeker.EmployementStatus = "Employed";
                _db.SaveChanges();
                return true;
            }
            return false;
        }
        public JobRegistration PermitJobToSeeker(int jobSeekerId)
        {
            _db = new UptronWebEntities();
            var jobSeeker = _db.JobRegistrations.Where(x => x.Id == jobSeekerId).FirstOrDefault();
            if (jobSeeker != null)
            {
                jobSeeker.IsActive = true;
                _db.SaveChanges();
                return jobSeeker;
            }
            return null;
        }
    }
}