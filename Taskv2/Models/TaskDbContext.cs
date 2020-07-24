using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Taskv2.Models
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext() : base("name=DefaultConnection")
        {

        }
        public DbSet<Proje> Projeler { get; set; }
        public DbSet<Gorev> Gorevler { get; set; }

        public DbSet<AspNetUser> Users { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserRole>()
            .HasKey(r => new { r.UserId, r.RoleId })
            .ToTable("AspNetUserRoles");

            modelBuilder.Entity<IdentityUserLogin>()
                        .HasKey(l => new { l.LoginProvider, l.ProviderKey, l.UserId })
                        .ToTable("AspNetUserLogins");
        }

    }
}