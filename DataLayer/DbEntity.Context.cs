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
    
        public virtual DbSet<RoleMaster> RoleMasters { get; set; }
        public virtual DbSet<UserMaster> UserMasters { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<JobRegistration> JobRegistrations { get; set; }
        public virtual DbSet<JobRegistrationQualification> JobRegistrationQualifications { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<JobRegistrationEmployement> JobRegistrationEmployements { get; set; }
        public virtual DbSet<JobRegistrationLanguage> JobRegistrationLanguages { get; set; }
        public virtual DbSet<JobRegistrationSkill> JobRegistrationSkills { get; set; }
        public virtual DbSet<GOCircular> GOCirculars { get; set; }
        public virtual DbSet<Tender> Tenders { get; set; }
        public virtual DbSet<GalleryMaster> GalleryMasters { get; set; }
        public virtual DbSet<GalleryPhotoMaster> GalleryPhotoMasters { get; set; }
        public virtual DbSet<NewsUpdateMaster> NewsUpdateMasters { get; set; }
    }
}
