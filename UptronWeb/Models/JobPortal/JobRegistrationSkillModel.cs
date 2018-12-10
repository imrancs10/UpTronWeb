using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UptronWeb.Models.JobPortal
{
    public class JobRegistrationSkillModel
    {
        public int Id { get; set; }
        public string Skill { get; set; }
        public int? RegistrationId { get; set; }
    }
}