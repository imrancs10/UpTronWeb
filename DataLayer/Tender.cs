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
    
    public partial class Tender
    {
        public int id { get; set; }
        public string TenderNumber { get; set; }
        public Nullable<System.DateTime> TenderDate { get; set; }
        public string Subject { get; set; }
        public byte[] TenderFile { get; set; }
    }
}
