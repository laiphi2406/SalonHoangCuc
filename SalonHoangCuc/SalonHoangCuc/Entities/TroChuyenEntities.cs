using CongViecGiaDinh.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace CongViecGiaDinh.Entities
{
    public class TroChuyenEntities : DbContext
    {
        public TroChuyenEntities() : base("DefaultConnection")
        {
        }
        public DbSet<TroChuyen> TroChuyen { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<TroChuyen>().ToTable("TroChuyen");
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}