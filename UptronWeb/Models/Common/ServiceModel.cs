using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UptronWeb.Models
{
    public class ServiceModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PageHeaderImage { get; set; }
        public string SliderImage { get; set; }
        public string Caption { get; set; }
        public string IsActive { get; set; }
        public int OrderNumber { get; set; }
        public string ServiceDescription { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}