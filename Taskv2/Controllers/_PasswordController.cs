using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI;
using Taskv2.Models;

namespace Taskv2.Controllers
{
    public class _PasswordControllerAttribute : ActionFilterAttribute, IActionFilter
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: _Password
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            List<AspNetUser> userList = context.Users.Where(m => m.pwdChanged == false).ToList();
            string name = filterContext.HttpContext.User.Identity.GetUserName();

            if (!HttpContext.Current.Response.IsRequestBeingRedirected)
            {
                foreach (var item in userList)
                {
                    if (item.UserName == name)
                    {
                        filterContext.HttpContext.Response.Redirect("/Manage/ChangePassword");
                    }
                }
            }
        }
    }
}