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
    
    public partial class JobRegistrationQualification
    {
        public int Id { get; set; }
        public Nullable<int> RegistrationId { get; set; }
        public string Qualification { get; set; }
        public string Board { get; set; }
        public string YearOfPassing { get; set; }
        public string Marks { get; set; }
        public string Specialization { get; set; }
        public string CourseType { get; set; }
    
        public virtual JobRegistration JobRegistration { get; set; }
    }
}
