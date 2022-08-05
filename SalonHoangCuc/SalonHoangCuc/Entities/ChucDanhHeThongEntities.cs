using CongViecGiaDinh.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace CongViecGiaDinh.Entities
{
    public class ChucDanhHeThongEntities : DbContext
    {
        public ChucDanhHeThongEntities() : base("DefaultConnection")
        {
        }
        public DbSet<ChucDanhTrongGiaDinh> ChucDanhTrongGiaDinh { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ChucDanhTrongGiaDinh>().ToTable("ChucDanhTrongGiaDinh");
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}