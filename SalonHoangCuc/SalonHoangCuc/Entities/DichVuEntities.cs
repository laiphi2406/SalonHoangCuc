using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using CongViecGiaDinh.Models;

namespace CongViecGiaDinh.Entities
{
    public class DichVuEntities : DbContext
    {
        public DichVuEntities() : base("DefaultConnection")
        {
        }
        public DbSet<DichVu> DichVu { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<DichVu>().ToTable("DichVu");
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}