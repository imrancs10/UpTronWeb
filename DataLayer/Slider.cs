//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class Slider
    {
        public int Id { get; set; }
        public string SliderName { get; set; }
        public string Caption1 { get; set; }
        public string Caption2 { get; set; }
        public string CaptionAuthor { get; set; }
        public byte[] SliderImage { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> OrderNumber { get; set; }
    }
}
