﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class UptronWebEntities : DbContext
    {
        public UptronWebEntities()
            : base("name=UptronWebEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<ContactUsDetail> ContactUsDetails { get; set; }
        public virtual DbSet<DirectorMessageDetail> DirectorMessageDetails { get; set; }
        public virtual DbSet<GalleryMaster> GalleryMasters { get; set; }
        public virtual DbSet<GalleryPhotoMaster> GalleryPhotoMasters { get; set; }
        public virtual DbSet<GOCircular> GOCirculars { get; set; }
        public virtual DbSet<JobDocumentUpload> JobDocumentUploads { get; set; }
        public virtual DbSet<JobRegistration> JobRegistrations { get; set; }
        public virtual DbSet<JobRegistrationEmployement> JobRegistrationEmployements { get; set; }
        public virtual DbSet<JobRegistrationForm> JobRegistrationForms { get; set; }
        public virtual DbSet<JobRegistrationLanguage> JobRegistrationLanguages { get; set; }
        public virtual DbSet<JobRegistrationQualification> JobRegistrationQualifications { get; set; }
        public virtual DbSet<JobRegistrationSkill> JobRegistrationSkills { get; set; }
        public virtual DbSet<JobResignation> JobResignations { get; set; }
        public virtual DbSet<KeyFunctionary> KeyFunctionaries { get; set; }
        public virtual DbSet<MajorProject> MajorProjects { get; set; }
        public virtual DbSet<NewsUpdateMaster> NewsUpdateMasters { get; set; }
        public virtual DbSet<Partner> Partners { get; set; }
        public virtual DbSet<QuickEnquiry> QuickEnquiries { get; set; }
        public virtual DbSet<RoleMaster> RoleMasters { get; set; }
        public virtual DbSet<ServiceDetail> ServiceDetails { get; set; }
        public virtual DbSet<Slider> Sliders { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<Tender> Tenders { get; set; }
        public virtual DbSet<UpcomingEventsMaster> UpcomingEventsMasters { get; set; }
        public virtual DbSet<UserMaster> UserMasters { get; set; }
        public virtual DbSet<VendorDetail> VendorDetails { get; set; }
        public virtual DbSet<VendorJob> VendorJobs { get; set; }
        public virtual DbSet<EmployeeSlip> EmployeeSlips { get; set; }
        public virtual DbSet<Religion> Religions { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
    }
}
