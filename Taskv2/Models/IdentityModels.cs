using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Taskv2.Models
{
    // You can add profile data for the user by adding more properties to your AspNetUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class AspNetUser : IdentityUser
    {

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AspNetUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        public virtual List<Gorev> Gorevler { get; set; }

        public virtual List<Proje> Projeler { get; set; }
        public bool pwdChanged { get; set; }


    }

    public class ApplicationDbContext : IdentityDbContext<AspNetUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}