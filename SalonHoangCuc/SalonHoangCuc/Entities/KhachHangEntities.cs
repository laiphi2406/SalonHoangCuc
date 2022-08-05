using CongViecGiaDinh.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace CongViecGiaDinh.Entities
{
    public class KhachHangEntities : DbContext
    {
        public KhachHangEntities() : base("DefaultConnection")
        {
        }
        public DbSet<KhachHang> KhachHang { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<KhachHang>().ToTable("KhachHang");
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }

    }
}