﻿using DataLayer;
using System;
using System.Data.Entity;
using System.Linq;
using UptronWeb.Global;
using UptronWeb.Models.JobPortal;

namespace UptronWeb.BAL
{
    public class JobRegistrationDetails
    {
        private UptronWebEntities _db = null;

        public Enums.CrudStatus SaveJobPortal(JobRegistrationModel model)
        {
            _db = new UptronWebEntities();
            int _effectRow = 0;
            JobRegistration _deptRow = _db.JobRegistrations.Where(x => x.EmailId.Equals(model.EmailId)).FirstOrDefault();
            if (_deptRow == null)
            {
                JobRegistration registration = new JobRegistration()
                {
                    Name = model.Name,
                    FatherName = model.FatherName,
                    MotherName = model.MotherName,
                    Gender = model.Gender,
                    MaritalStatus = model.MaritalStatus,
                    Religion = model.Religion,
                    IdentityType = model.IdentityType,
                    IdentityNo = model.IdentityNo,
                    MobileNo = model.MobileNo,
                    AlternateNo = model.AlternateNo,
                    EmailId = model.EmailId,
                    Pincode = model.Pincode,
                    CityId = Convert.ToInt32(model.CityId),
                    StateId = Convert.ToInt32(model.StateId),
                    Experience = model.Experience,
                    Disability = model.Disability == "Yes" ? true : false,
                    ExServiceMan = model.ExServiceMan == "Yes" ? true : false,
                    PersonHeight = Convert.ToDecimal(model.PersonHeight),
                    EyeSight = model.EyeSight,
                    //DOB = Convert.ToDateTime(model.DOB)
                };

                foreach (JobRegistrationLanguageModel Language in model.JobRegistrationLanguage)
                {
                    registration.JobRegistrationLanguages.Add(new JobRegistrationLanguage()
                    {
                        Language = Language.Language
                    });
                }
                foreach (JobRegistrationSkillModel skill in model.JobRegistrationSkills)
                {
                    registration.JobRegistrationSkills.Add(new JobRegistrationSkill()
                    {
                        Skill = skill.Skill
                    });
                }


                foreach (JobRegistrationEmployementModel emp in model.JobRegistrationEmployement)
                {
                    registration.JobRegistrationEmployements.Add(new JobRegistrationEmployement()
                    {
                        OrganizationName = emp.OrganizationName,
                        Post = emp.Post,
                        FromMonth = emp.FromMonth,
                        FromYear = emp.FromYear,
                        ToMonth = emp.ToMonth,
                        ToYear = emp.ToYear,
                        IndustryType = emp.IndustryType,
                        Salary = emp.Salary
                    });
                }
                foreach (JobRegistrationQualificationModel qualification in model.JobRegistrationQualification)
                {
                    registration.JobRegistrationQualifications.Add(new JobRegistrationQualification()
                    {
                        Qualification = qualification.Qualification,
                        Board = qualification.Board,
                        YearOfPassing = qualification.YearOfPassing,
                        Marks = qualification.Marks,
                        Specialization = qualification.Specialization,
                        CourseType = qualification.CourseType

                    });
                }

                _db.Entry(registration).State = EntityState.Added;
                _effectRow = _db.SaveChanges();
                return _effectRow > 0 ? Enums.CrudStatus.Saved : Enums.CrudStatus.NotSaved;
            }
            else
            {
                return Enums.CrudStatus.DataAlreadyExist;
            }
        }
    }
}