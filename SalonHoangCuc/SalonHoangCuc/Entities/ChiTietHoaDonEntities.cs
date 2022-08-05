using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using CongViecGiaDinh.Models;

namespace CongViecGiaDinh.Entities
{
    public class ChiTietHoaDonEntities : DbContext
    {
        public ChiTietHoaDonEntities() : base("DefaultConnection")
        {
        }
        public DbSet<ChiTietHoaDon> ChiTietHoaDon { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ChiTietHoaDon>().ToTable("ChiTietHoaDon");
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}