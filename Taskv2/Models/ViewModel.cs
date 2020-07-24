using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Taskv2.Models
{
    public class ViewModel : IdentityUser
    {
        public Proje Proje { get; set; }
        public Gorev Task { get; set; }
        public AspNetUser User { get; set; }
    }
}