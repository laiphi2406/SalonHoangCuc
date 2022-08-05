using CongViecGiaDinh.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace CongViecGiaDinh.Entities
{
    public class ThanhVienEntities : DbContext
    {
        public ThanhVienEntities() : base("DefaultConnection")
        {
        }
        public DbSet<ThanhVien> ThanhVien { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ThanhVien>().ToTable("ThanhVien");
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);


        }
    }
}