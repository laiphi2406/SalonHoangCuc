using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using CongViecGiaDinh.Models;


namespace CongViecGiaDinh.Entities
{
    public class HoaDonEntities : DbContext
    {
        public HoaDonEntities() : base("DefaultConnection")
        {
        }
        public DbSet<HoaDon> HoaDon { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<HoaDon>().ToTable("HoaDon");
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}