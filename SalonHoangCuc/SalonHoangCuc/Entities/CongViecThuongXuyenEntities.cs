using CongViecGiaDinh.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace CongViecGiaDinh.Entities
{
    public class CongViecThuongXuyenEntities : DbContext
    {
        public CongViecThuongXuyenEntities() : base("DefaultConnection")
        {
        }
        public DbSet<CongViecThuongXuyen> CongViecThuongXuyen { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<CongViecThuongXuyen>().ToTable("CongViecThuongXuyen");
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}