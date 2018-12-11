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
                    FatherName = model.FatherName,
                    MotherName = model.MotherName,
                    DOB = model.DOB,
                    Gender = model.Gender,
                    MaritalStatus = model.MaritalStatus,
                    Religion = model.Religion,
                    IdentityType = model.IdentityType,
                    IdentityNo = model.IdentityNo,
                    MobileNo = model.MobileNo,
                    AlternateNo = model.AlternateNo,
                    EmailId = model.EmailId,
                    Pincode = model.Pincode,
                    CityId = model.CityId,
                    StateId = model.StateId,
                    Experience = model.Experience,
                    Disability = model.Disability,
                    ExServiceMan = model.ExServiceMan,
                    PersonHeight = model.PersonHeight,
                    EyeSight = model.EyeSight

                };
                foreach (var Language in model.JobRegistrationLanguage)
                {
                    registration.JobRegistrationLanguages.Add(new JobRegistrationLanguage()
                    {
                        Language = Language.Language
                    });
                }
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