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
                    if (serviceDetail.Image != null)
                        _deptRow.Image = serviceDetail.Image;
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
    }
}