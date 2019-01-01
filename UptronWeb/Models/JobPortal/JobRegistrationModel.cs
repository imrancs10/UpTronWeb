using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UptronWeb.Models.JobPortal
{
    public class JobRegistrationModel
    {
        public JobRegistrationModel()
        {
            this.JobRegistrationSkills = new List<JobRegistrationSkillModel>();
            this.JobRegistrationEmployement = new List<JobRegistrationEmployementModel>();
            this.JobRegistrationQualification = new List<JobRegistrationQualificationModel>();
            this.JobRegistrationLanguage = new List<JobRegistrationLanguageModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string Religion { get; set; }
        public string IdentityType { get; set; }
        public string IdentityNo { get; set; }
        public string MobileNo { get; set; }
        public string AlternateNo { get; set; }
        public string EmailId { get; set; }
        public string Pincode { get; set; }
        public string CityId { get; set; }
        public string StateId { get; set; }
        public byte[] Resume { get; set; }
        public byte[] ResumeImage { get; set; }
        public string Experience { get; set; }
        public string Disability { get; set; }
        public string ExServiceMan { get; set; }
        public string PersonHeight { get; set; }
        public string EyeSight { get; set; }
        public string Password { get; set; }
        public List<JobRegistrationSkillModel> JobRegistrationSkills { get; set; }
        public List<JobRegistrationEmployementModel> JobRegistrationEmployement { get; set; }
        public List<JobRegistrationQualificationModel> JobRegistrationQualification { get; set; }
        public List<JobRegistrationLanguageModel> JobRegistrationLanguage { get; set; }
    }
}