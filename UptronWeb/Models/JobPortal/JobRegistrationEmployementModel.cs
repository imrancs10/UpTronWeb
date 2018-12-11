using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UptronWeb.Models.JobPortal
{
    public class JobRegistrationEmployementModel
    {
        public int Id { get; set; }
        public string OrganizationName { get; set; }
        public string Post { get; set; }
        public string FromMonth { get; set; }
        public string FromYear { get; set; }
        public string ToMonth { get; set; }
        public string ToYear { get; set; }
        public string IndustryType { get; set; }
        public string Salary { get; set; }
        public int? RegistrationId { get; set; }

    }
}