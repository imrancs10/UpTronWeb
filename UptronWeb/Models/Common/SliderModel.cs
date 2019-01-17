using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UptronWeb.Models.Common
{
    public class SliderModel
    {
        public int Id { get; set; }
        public string SliderName { get; set; }
        public string Caption1 { get; set; }
        public string Caption2 { get; set; }
        public string CaptionAuthor { get; set; }
        public string SliderImage { get; set; }
        public string IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public int OrderNumber { get; set; }
    }
}