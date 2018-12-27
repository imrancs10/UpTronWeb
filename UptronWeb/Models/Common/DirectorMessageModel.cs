using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UptronWeb.Models.Common
{
    public class DirectorMessageModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Message { get; set; }
        public string Photo { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}