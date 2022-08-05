using CongViecGiaDinh.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace CongViecGiaDinh.Entities
{
    public class ChucDanhEntities : DbContext
    {
        public ChucDanhEntities() : base("DefaultConnection")
        {
        }
        public DbSet<ChucDanh> ChucDanh { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ChucDanh>().ToTable("ChucDanhTrongGiaDinh");
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}