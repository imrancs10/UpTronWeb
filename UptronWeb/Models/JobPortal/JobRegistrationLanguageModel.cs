using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UptronWeb.Models.JobPortal
{
    public class JobRegistrationLanguageModel
    {
        public int Id { get; set; }
        public string Language { get; set; }
        public int? RegistrationId { get; set; }
    }
}