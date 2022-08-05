using CongViecGiaDinh.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace CongViecGiaDinh.Entities
{
    public class QuanHuyenEntities : DbContext
    {
        public QuanHuyenEntities() : base("DefaultConnection")
        {
        }
        public DbSet<QuanHuyen> QuanHuyens { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<QuanHuyen>().ToTable("Quan_Huyen");
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}