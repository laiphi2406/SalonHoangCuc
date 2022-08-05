using CongViecGiaDinh.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace CongViecGiaDinh.Entities
{
    public class HoGiaDinhEntities : DbContext
    {
        public HoGiaDinhEntities() : base("DefaultConnection")
        {
        }
        public DbSet<HoGiaDinh> HoGiaDinh { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<HoGiaDinh>().ToTable("HoGiaDinh");
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);


        }
    }
}