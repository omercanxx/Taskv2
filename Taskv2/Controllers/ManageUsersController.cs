using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taskv2.Models;

namespace Taskv2.Controllers
{
    [Authorize(Roles = "admin")]
    public class ManageUsersController : Controller
    {
        // GET: ManageUsers
        ApplicationDbContext context = new ApplicationDbContext();
        public ActionResult PersonelRoles()
        {
            
            var usersWithRoles = (from user in context.Users
                                  select new
                                  {
                                      UserId = user.Id,
                                      Username = user.UserName,
                                      RoleNames = (from userRole in user.Roles
                                                   join role in context.Roles on userRole.RoleId
                                                   equals role.Id
                                                   select role.Name).ToList()
                                  }).ToList().Select(p => new UserViewModel()

                                  {
                                      UserID = p.UserId,
                                      Username = p.Username,
                                      Role = string.Join(",", p.RoleNames)
                                  });
            var personelList = usersWithRoles.Where(m => m.Role == "personel");
            return View(personelList);
        }
        public ActionResult YoneticiRoles()
        {
            var usersWithRoles = (from user in context.Users
                                  select new
                                  {
                                      UserId = user.Id,
                                      Username = user.UserName,
                                      RoleNames = (from userRole in user.Roles
                                                   join role in context.Roles on userRole.RoleId
                                                   equals role.Id
                                                   select role.Name).ToList()
                                  }).ToList().Select(p => new UserViewModel()

                                  {
                                      UserID = p.UserId,
                                      Username = p.Username,
                                      Role = string.Join(",", p.RoleNames)
                                  });
            var yoneticiList = usersWithRoles.Where(m => m.Role == "yonetici");

            return View(yoneticiList);

        }
        }
}