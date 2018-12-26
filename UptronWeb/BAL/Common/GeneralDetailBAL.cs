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
    }
}