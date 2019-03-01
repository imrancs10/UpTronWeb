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
    
    public partial class VendorDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VendorDetail()
        {
            this.EmployeeSlips = new HashSet<EmployeeSlip>();
            this.JobDocumentUploads = new HashSet<JobDocumentUpload>();
            this.JobResignations = new HashSet<JobResignation>();
            this.VendorJobs = new HashSet<VendorJob>();
            this.VendorDocuments = new HashSet<VendorDocument>();
        }
    
        public int Id { get; set; }
        public string VendorCode { get; set; }
        public string VendorName { get; set; }
        public string PhoneNumber { get; set; }
        public string TypeofFirm { get; set; }
        public string TypeOfBusiness { get; set; }
        public string GSTNumber { get; set; }
        public string TypeofStream { get; set; }
        public string FullAddress { get; set; }
        public string ContactPerson { get; set; }
        public Nullable<bool> Permitted { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string EmailId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeSlip> EmployeeSlips { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JobDocumentUpload> JobDocumentUploads { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JobResignation> JobResignations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VendorJob> VendorJobs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VendorDocument> VendorDocuments { get; set; }
    }
}
