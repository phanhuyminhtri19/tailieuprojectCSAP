﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UniHostel.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class UniHostelDB : DbContext
    {
        public UniHostelDB()
            : base("name=UniHostelDB")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AdvancedService> AdvancedServices { get; set; }
        public virtual DbSet<BillAdvancedServiceDetail> BillAdvancedServiceDetails { get; set; }
        public virtual DbSet<BillCompulsoryServiceDetail> BillCompulsoryServiceDetails { get; set; }
        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<CompulsoryService> CompulsoryServices { get; set; }
        public virtual DbSet<Host> Hosts { get; set; }
        public virtual DbSet<Renter> Renters { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
