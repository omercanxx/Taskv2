using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taskv2.Models;

namespace Taskv2.Controllers
{
    [_PasswordController]
    public class TaskController : Controller
    {
        [Authorize(Roles = "yonetici")]
        public ActionResult CreateEmployee()
        {
            return View();
        }
        [Authorize(Roles = "yonetici")]
        [HttpPost]
        public ActionResult CreateEmployee(FormCollection form)
        {
            bool isAvailable = true;
            ViewBag.error = "";
            var UserManager = new Microsoft.AspNet.Identity.UserManager<AspNetUser>(new UserStore<AspNetUser>(context));
            string UserName = form["txtEmail"];
            string email = form["txtEmail"];
            string pwd = "Caretta.97";

            List<AspNetUser> uList = db.Users.ToList();
            foreach (var item in uList)
            {
                if (item.Email == email)
                {
                    ViewBag.error = "The employee you want to add is already registered.";
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
                UserManager.AddToRole(user.Id, "personel");
                ViewBag.error = "The employee you want to add has been successfully registered.";
            }

            return View();
        }
        // GET: Task
        TaskDbContext db = new TaskDbContext();
        ApplicationDbContext context = new ApplicationDbContext();
        [Authorize(Roles = "yonetici")]
        public ActionResult Task()
        {
            List<Gorev> Gorevler = db.Gorevler.Where(m => m.Status == "yapıldı").ToList();
            return View(Gorevler);
        }
        [Authorize(Roles = "yonetici")]
        public ActionResult ExpectedTask()
        {
            List<Gorev> Gorevler = db.Gorevler.Where(m => m.Status == "Onay bekliyor").ToList();
            return View(Gorevler);
        }
        [Authorize(Roles = "personel")]
        public ActionResult todoTask()
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

            string id = User.Identity.GetUserId();
            List<Gorev> Gorevler = db.Gorevler.Where(m => m.UserID == id && m.Status == "yapılmadı").ToList();


            return View(Gorevler);
        }
        public ActionResult onayla(int id)
        {
            var data = db.Gorevler.Find(id);

            data.Status = "yapıldı";
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
            return Json("Degistirme islemi tamamlandi", JsonRequestBehavior.AllowGet);
        }
        public ActionResult gonder(int id)
        {
            var data = db.Gorevler.Find(id);

            data.Status = "Onay bekliyor";
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
            return Json("Degistirme islemi tamamlandi", JsonRequestBehavior.AllowGet);
        }
        public ActionResult reddet(int id)
        {
            var data = db.Gorevler.Find(id);

            data.Status = "yapılmadı";
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
            return Json("Degistirme islemi tamamlandi", JsonRequestBehavior.AllowGet);
        }
            [Authorize(Roles ="yonetici")]
        public ActionResult taskEkle()
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
            var proje = db.Projeler.Where(m => m.isVisible == true).ToList();

            List<object> pList = new List<object>();
            foreach (var item in proje)
            {
                pList.Add(new SelectListItem { Text = item.Name, Value = Convert.ToString(item.ProjeID) });

            }
            ViewBag.pList = pList;

            string name = User.Identity.GetUserName();

            List<object> userList = new List<object>();
            foreach (var item in personelList)
            {
            userList.Add(new SelectListItem { Text = item.Username, Value = Convert.ToString(item.UserID) });
            }
            ViewBag.userList = userList;
            return View(new Gorev());
        }

        [HttpPost]
        public ActionResult taskEkle(Gorev task)
        {
            Gorev tsk = new Gorev();
            tsk.Name = task.Name;
            tsk.Status = "yapılmadı";
            tsk.Expression = task.Expression;
            tsk.ProjeID = task.ProjeID;
            tsk.UserID = task.UserID;
            db.Gorevler.Add(tsk);
            db.SaveChanges();
            return RedirectToAction("task");
        }
    }
}
