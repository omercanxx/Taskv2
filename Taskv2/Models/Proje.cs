using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Taskv2.Models
{
    [Table("Proje")]
    public class Proje
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjeID { get; set; }
        [StringLength(50), Required]
        public string Name { get; set; }
        [StringLength(100), Required]
        public string Expression { get; set; }
        [StringLength(50), Required]
        public string Status { get; set; }
        public bool isVisible { get; set; }
        public virtual List<Gorev> Gorevler { get; set; }
        public string UserID { get; set; }
        public virtual AspNetUser User { get; set; }

    }
}