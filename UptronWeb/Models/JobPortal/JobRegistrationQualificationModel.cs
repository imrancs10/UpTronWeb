using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UptronWeb.Models.JobPortal
{
    public class JobRegistrationQualificationModel
    {
        public int Id { get; set; }
        public int? RegistrationId { get; set; }
        public string Qualification { get; set; }
        public string Board { get; set; }
        public string YearOfPassing { get; set; }
        public string Marks { get; set; }
        public string Specialization { get; set; }
        public string CourseType { get; set; }
    }
}