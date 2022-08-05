using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using CongViecGiaDinh.Models;


namespace CongViecGiaDinh.Entities
{
    public class TheKhachHangEntities : DbContext
    {
        public TheKhachHangEntities() : base("DefaultConnection")
        {
        }
        public DbSet<TheKhachHang> TheKhachHang { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<TheKhachHang>().ToTable("TheKhachHang");
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}