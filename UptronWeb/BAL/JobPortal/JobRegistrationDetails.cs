using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer;
using UptronWeb.Global;
using System.Data.Entity;
using UptronWeb.Models.JobPortal;

namespace UptronWeb.BAL
{
    public class JobRegistrationDetails
    {
        UptronWebEntities _db = null;

        public Enums.CrudStatus SaveJobPortal(JobRegistrationModel model)
        {
            _db = new UptronWebEntities();
            int _effectRow = 0;
            var _deptRow = _db.JobRegistrations.Where(x => x.EmailId.Equals(model.EmailId)).FirstOrDefault();
            if (_deptRow == null)
            {
                JobRegistration registration = new JobRegistration
                {
                    Name = model.Name,
                    Gender = model.Gender,
                    EmailId = model.EmailId,

                };
                foreach (var skill in model.JobRegistrationSkills)
                {
                    registration.JobRegistrationSkills.Add(new JobRegistrationSkill()
                    {
                        Skill = skill.Skill
                    });
                }

                _db.Entry(registration).State = EntityState.Added;
                _effectRow = _db.SaveChanges();
                return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
            }
            else
                return Enums.CrudStatus.DataAlreadyExist;
        }
    }
}