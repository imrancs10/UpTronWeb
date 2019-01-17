using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UptronWeb.Models.Common
{
    public class KeyFunctionaryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Location { get; set; }
        public string Image { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}