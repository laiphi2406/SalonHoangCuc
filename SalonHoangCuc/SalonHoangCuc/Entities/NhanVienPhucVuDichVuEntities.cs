using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using CongViecGiaDinh.Models;

namespace CongViecGiaDinh.Entities
{
    public class NhanVienPhucVuDichVuEntities : DbContext
    {
        public NhanVienPhucVuDichVuEntities() : base("DefaultConnection")
        {
        }
        public DbSet<NhanVienPhucVuDichVu> NhanVienPhucVuDichVu { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<NhanVienPhucVuDichVu>().ToTable("NhanVienPhucVuDichVu");
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}