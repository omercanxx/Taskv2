using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.Identity;
using Microsoft.Owin.Security;
using Taskv2.Models;


namespace Taskv2.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        TaskDbContext db = new TaskDbContext();
        // GET: Admin
        public ActionResult DeletedProject()
        {
            var model = db.Projeler.Where(m => m.isVisible == false).ToList();
            return View(model);
        }
        public ActionResult kurtar(int id)
        {
            var data = db.Projeler.Find(id);

            data.isVisible = true;
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            return Json("Kurtarma islemi tamamlandi", JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateManager()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateManager(FormCollection form)
        {
            bool isAvailable = true;
            ViewBag.error = "";
            var UserManager = new Microsoft.AspNet.Identity.UserManager<AspNetUser>(new UserStore<AspNetUser>(context));
            string UserName = form["txtEmail"];
            string email = form["txtEmail"];
            string pwd = "Caretta.97";

            List<AspNetUser> uList = db.Users.ToList();
            foreach(var item in uList)
            {
                if(item.Email == email)
                {
                    ViewBag.error = "The user you want to add is already registered.";
                    isAvailable = false;
                }
            }
            if (isAvailable)
            {
                var user = new AspNetUser();
                user.UserName = UserName;
                user.Email = email;
                user.pwdChanged = false;
                var newUser = UserManager.Create(user, pwd);
                UserManager.AddToRole(user.Id, "yonetici");
                ViewBag.error = "The manager you want to add has been successfully registered.";
            }

            return View();
        }



        public ActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateRole(FormCollection form)
        {
            ViewBag.RoleUyari = "";
            string RoleName = form["RoleName"];
            var roleManager = new Microsoft.AspNet.Identity.RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            if (!roleManager.RoleExists(RoleName))
            {
                var role = new IdentityRole(RoleName);
                roleManager.Create(role);
            }
            else
            {
                ViewBag.RoleUyari = "The role you want to add is already registered.";
                return View();
            }
            return View();
        }
        
        public ActionResult AssignRole()
        {
            ViewBag.Roles = context.Roles.Select(r => new SelectListItem { Value = r.Name, Text = r.Name }).ToList();
            ViewBag.User = context.Users.Where(r => r.Roles.Count == 0).Select(r => new SelectListItem { Value = r.Email, Text = r.Email }).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult AssignRole(FormCollection form)
        {
            string username = form["txtUserName"];
            string rolname = form["RoleName"];

        AspNetUser user = context.Users.Where(u => u.UserName.Equals(username, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            var userManager = new Microsoft.AspNet.Identity.UserManager<AspNetUser>(new UserStore<AspNetUser>(context));
        userManager.AddToRole(user.Id, rolname);
        return View("Index");
    }
}
}