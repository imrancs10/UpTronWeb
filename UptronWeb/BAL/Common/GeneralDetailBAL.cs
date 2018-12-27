using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer;
using UptronWeb.Global;
using System.Data.Entity;

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
    }
}