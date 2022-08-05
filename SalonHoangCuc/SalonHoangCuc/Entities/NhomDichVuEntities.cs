using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using CongViecGiaDinh.Models;

namespace CongViecGiaDinh.Entities
{
    public class NhomDichVuEntities : DbContext
    {
        public NhomDichVuEntities() : base("DefaultConnection")
        {
        }
        public DbSet<NhomDichVu> NhomDichVu { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<NhomDichVu>().ToTable("NhomDichVu");
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}