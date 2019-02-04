using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer;
using UptronWeb.Global;
using System.Data.Entity;
using UptronWeb.Models.JobPortal;

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
            return _db.VendorDetails.Where(x => x.IsActive == true && x.Permitted == true).ToList();
        }

        public List<VendorJobModel> GetJobType(int VendorId)
        {
            _db = new UptronWebEntities();
            _db.Configuration.LazyLoadingEnabled = false;
            var result = _db.VendorJobs.Where(x => x.VendorId == VendorId).ToList();
            List<VendorJobModel> list = new List<VendorJobModel>();
            result.ForEach(x =>
            {
                list.Add(new VendorJobModel()
                {
                    Id = x.Id,
                    Jobtype = x.Jobtype,
                    NoofRequirement = x.NoofRequirement,
                    SkillsRequired = x.SkillsRequired,
                    VendorId = x.VendorId
                });
            });
            return list;
        }
        public int? GetRemainingSeat(int VendorJobId)
        {
            _db = new UptronWebEntities();
            _db.Configuration.LazyLoadingEnabled = false;
            var vendorJob = _db.VendorJobs.Where(x => x.Id == VendorJobId).FirstOrDefault();
            if (vendorJob != null)
            {
                var remainingSeat = vendorJob.NoofRequirement;
                var allotedJob = _db.JobRegistrations.Where(x => x.VendorJobId == VendorJobId).ToList();
                if (allotedJob != null)
                {
                    remainingSeat = remainingSeat - allotedJob.Count;
                }
                return remainingSeat;
            }
            return null;
        }
    }
}