using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using Taskv2.Models;

[assembly: OwinStartupAttribute(typeof(Taskv2.Startup))]
namespace Taskv2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateUserAndRoles();
        }

        public void CreateUserAndRoles()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<AspNetUser>(new UserStore<AspNetUser>(context));
            if (!roleManager.RoleExists("admin"))
            {

                var role = new IdentityRole("admin");
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                   

                var user = new AspNetUser();
                user.UserName = "admin@domain.net";
                user.Email = "admin@domain.net";
                string userPWD = "admin.123";
                user.pwdChanged = true;

                var chkUser = UserManager.Create(user, userPWD);

                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "admin");

                }
            }

        }
    }
}