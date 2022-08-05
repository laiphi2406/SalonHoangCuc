using CongViecGiaDinh.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace CongViecGiaDinh.Entities
{
    public class BangTinEntities : DbContext
    {
        public BangTinEntities() : base("DefaultConnection")
        {
        }
        public DbSet<BangTin> BangTin { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<BangTin>().ToTable("BangTin");
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}