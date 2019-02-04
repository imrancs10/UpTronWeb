using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UptronWeb.Models.JobPortal
{
    public class VendorJobModel
    {
        public int Id { get; set; }
        public Nullable<int> VendorId { get; set; }
        public string Jobtype { get; set; }
        public Nullable<int> NoofRequirement { get; set; }
        public string SkillsRequired { get; set; }
        public string RemainingSeat { get; set; }
    }
}