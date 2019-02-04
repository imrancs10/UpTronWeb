using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer;
using UptronWeb.Global;
using System.Data.Entity;

namespace UptronWeb.BAL.Common
{
    public class CommonDetails
    {
        UptronWebEntities _db = null;

        public List<State> GetStates()
        {
            _db = new UptronWebEntities();
            _db.Configuration.LazyLoadingEnabled = false;
            return _db.States.ToList();
        }

        public List<City> GetCities(int stateId)
        {
            _db = new UptronWebEntities();
            _db.Configuration.LazyLoadingEnabled = false;
            return _db.Cities.Where(x => x.StateId == stateId).ToList();
        }

        public List<VendorDetail> GetVendor()
        {
            _db = new UptronWebEntities();
            _db.Configuration.LazyLoadingEnabled = false;
            return _db.VendorDetails.ToList();
        }

        public List<VendorJob> GetJobType()
        {
            _db = new UptronWebEntities();
            _db.Configuration.LazyLoadingEnabled = false;
            return _db.VendorJobs.ToList();
        }
    }
}