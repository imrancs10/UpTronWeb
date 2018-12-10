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
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string Religion { get; set; }
        public string IdentityType { get; set; }
        public string IdentityNo { get; set; }
        public string MobileNo { get; set; }
        public string AlternateNo { get; set; }
        public string EmailId { get; set; }
        public string Pincode { get; set; }
        public Nullable<int> CityId { get; set; }
        public Nullable<int> StateId { get; set; }
        public byte[] Resume { get; set; }
        public byte[] ResumeImage { get; set; }
        public string Experience { get; set; }
        public Nullable<bool> Disability { get; set; }
        public Nullable<bool> ExServiceMan { get; set; }
        public Nullable<decimal> PersonHeight { get; set; }
        public string EyeSight { get; set; }
        public List<JobRegistrationSkillModel> JobRegistrationSkills { get; set; }
    }
}