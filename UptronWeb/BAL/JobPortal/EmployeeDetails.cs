using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer;
using UptronWeb.Global;
using System.Data.Entity;

namespace UptronWeb.BAL.Login
{
    public class EmployeeDetails
    {
        UptronWebEntities _db = null;

        /// <summary>
        /// Get Authenticate User credentials
        /// </summary>
        /// <param name="UserName">Username</param>
        /// <param name="Password">Password</param>
        /// <returns>Enums</returns>
        public JobRegistration CheckEmployeeLogin(string UserName, string Password)
        {
            _db = new UptronWebEntities();
            var result = _db.JobRegistrations.Where(x => x.EmailId == UserName && x.Password == Password && x.IsActive == true).FirstOrDefault();
            if (result != null)
                return result;
            else
                return null;
        }

        public JobResignation SaveEmployeeResignation(JobResignation jobresignation)
        {
            _db = new UptronWebEntities();
            int _effectRow = 0;
            if (jobresignation.Id > 0)
            {
                var _deptRow = _db.JobResignations.Include("VendorDetail").Include("JobRegistration").Where(x => x.Id == jobresignation.Id).FirstOrDefault();
                if (_deptRow!=null)
                {
                    _deptRow.Id = jobresignation.Id;
                    _deptRow.VendorId = jobresignation.VendorId;
                    _deptRow.ResignationDate = jobresignation.ResignationDate;
                    _deptRow.ResignationReason = jobresignation.ResignationReason;
                    _deptRow.CreatedDate = jobresignation.CreatedDate;
                    _db.Entry(_deptRow).State = EntityState.Modified;
                    _effectRow = _db.SaveChanges();
                    return _deptRow;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                var _deptRow = _db.JobResignations.Where(x => x.Id == jobresignation.Id).FirstOrDefault();
                if (_deptRow == null)
                {
                    _db.Entry(jobresignation).State = EntityState.Added;
                    _effectRow = _db.SaveChanges();
                    _deptRow = _db.JobResignations.Include("VendorDetail").Include("JobRegistration").Where(x => x.Id == jobresignation.Id).FirstOrDefault();
                    return _deptRow;
                    
                }
                else
                {
                    return null;
                }
            }

        }

        public List<JobResignation> GetJObResignation()
        {
            _db = new UptronWebEntities();
            var result = _db.JobResignations.ToList();
            return result;
        }

        public Enums.CrudStatus SaveEmployeeResignation(EmployeeSlip employeeslip)
        {
            _db = new UptronWebEntities();
            int _effectRow = 0;
            if (employeeslip.Id > 0)
            {
                var _deptRow = _db.EmployeeSlips.Where(x => x.Id == employeeslip.Id).FirstOrDefault();
                if (_deptRow != null)
                {
                    _deptRow.Id = employeeslip.Id;
                    _deptRow.JoiningDate = employeeslip.JoiningDate;
                    if (employeeslip.SalarySlip != null)
                    {
                        _deptRow.SalarySlip = employeeslip.SalarySlip;
                    }
                    if (employeeslip.PfSlip != null)
                    {
                        _deptRow.PfSlip = employeeslip.PfSlip;
                    }
                    if (employeeslip.EsiSlip != null)
                    {
                        _deptRow.EsiSlip = employeeslip.EsiSlip;
                    }
                    _deptRow.CreatedDate = employeeslip.CreatedDate;
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
                var _deptRow = _db.EmployeeSlips.Where(x => x.Id == employeeslip.Id).FirstOrDefault();
                if (_deptRow == null)
                {
                    _db.Entry(employeeslip).State = EntityState.Added;
                    _effectRow = _db.SaveChanges();
                    return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
                }
                else
                {
                    return Enums.CrudStatus.DataAlreadyExist;
                }
            }
        }

        public List<EmployeeSlip> GetAllEmployeeSlip()
        {
            _db = new UptronWebEntities();
            var result = _db.EmployeeSlips.ToList();
            return result;
        }

        public EmployeeSlip GetSalarySlip(int Id)
        {
            _db = new UptronWebEntities();
            var result = _db.EmployeeSlips.FirstOrDefault(x => x.Id == Id);
            return result;
        }

        public EmployeeSlip GetPFSlip(int Id)
        {
            _db = new UptronWebEntities();
            var result = _db.EmployeeSlips.FirstOrDefault(x => x.Id == Id);
            return result;
        }

        public EmployeeSlip GetESISlip(int Id)
        {
            _db = new UptronWebEntities();
            var result = _db.EmployeeSlips.FirstOrDefault(x => x.Id == Id);
            return result;
        }
    }
}