using CongViecGiaDinh.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace CongViecGiaDinh.Entities
{
    public class CongViecKhongThuongXuyenEntities : DbContext
    {
        public CongViecKhongThuongXuyenEntities() : base("DefaultConnection")
        {
        }
        public DbSet<CongViecKhongThuongXuyen> CongViecKhongThuongXuyen { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<CongViecKhongThuongXuyen>().ToTable("CongViecKhongThuongXuyen");
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);


        }
    }
}