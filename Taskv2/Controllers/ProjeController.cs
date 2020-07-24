using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taskv2.Models;

namespace Taskv2.Controllers
{
    [Authorize(Roles ="yonetici")]
    [_PasswordController]
    public class ProjeController : Controller
    {
        // GET: Proje
        TaskDbContext db = new TaskDbContext();
        // GET: Proje
        public ActionResult Proje()
        {
            var model = db.Projeler.Where(m=> m.isVisible == true).ToList();
            return View(model);
        }
        public ActionResult sil(int id)
        {
            var data = db.Projeler.Find(id);

            data.isVisible = false;
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
            return Json("Silme islemi tamamlandi", JsonRequestBehavior.AllowGet);
        }
        public ActionResult projeEkle()
        {
            List<object> durum = new List<object>();

            durum.Add(new SelectListItem { Text = "Aktif", Value = "aktif" });
            durum.Add(new SelectListItem { Text = "Pasif", Value = "pasif" });

            ViewBag.drm = durum;

            return View();
        }

        [HttpPost]
        public ActionResult projeEkle(Proje proje)
        {
            string userID = User.Identity.GetUserId();
            Proje prj = new Proje();
            prj.Name = proje.Name;
            prj.Status = proje.Status;
            prj.Expression = proje.Expression;
            prj.isVisible = true;
            prj.UserID = userID;
            db.Projeler.Add(prj);
            db.SaveChanges();
            return RedirectToAction("Proje");
        }
    }
}