using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Taskv2.Models
{
    [Table("Gorev")]
    public class Gorev
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GorevID { get; set; }
        [StringLength(50), Required]
        public string Name { get; set; }
        [StringLength(100), Required]
        public string Expression { get; set; }
        [StringLength(50), Required]
        public string Status { get; set; }

        [StringLength(100)]
        public string whyRejected { get; set; }
        [Required]
        public int ProjeID { get; set; }
        public virtual Proje Proje { get; set; }
        [StringLength(128), Required]
        public string UserID { get; set; }
        public virtual AspNetUser User { get; set; }
    }
}