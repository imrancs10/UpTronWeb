using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer;
using UptronWeb.Global;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace UptronWeb.BAL.Vendor
{
    public class VendorDetailBAL
    {
        UptronWebEntities _db = null;
        public Enums.CrudStatus SaveVendorDetail(VendorDetail vendor)
        {
            _db = new UptronWebEntities();
            int _effectRow = 0;
            _db.Entry(vendor).State = EntityState.Added;
            _effectRow = _db.SaveChanges();
            return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
        }
        public List<VendorDetail> GetAllVendorDetails()
        {
            _db = new UptronWebEntities();
            var result = _db.VendorDetails.ToList();
            return result;
        }

        public VendorDetail GetVendorDetailByVendorCode(string vendorCode)
        {
            _db = new UptronWebEntities();
            var result = _db.VendorDetails.Where(x => x.VendorCode == vendorCode).FirstOrDefault();
            return result;
        }
        public Enums.CrudStatus SetStatusVendorDetails(int Id)
        {
            _db = new UptronWebEntities();
            int _effectRow = 0;
            var result = _db.VendorDetails.Where(x => x.Id == Id).FirstOrDefault();
            result.Permitted = result.Permitted == true ? false : true;
            _db.Entry(result).State = EntityState.Modified;
            _effectRow = _db.SaveChanges();
            return _effectRow > 0 ? Enums.CrudStatus.Updated : Enums.CrudStatus.NotUpdated;
        }

        
    }
}