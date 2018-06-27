using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tangy.Models
{
    public class MedicalField
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "תחום רפואי (מחלקה)")]
        public String Name { get; set; }

        [Required]
        [Display(Name = "סדר תצוגה")]
        public int DisplayOrder { get; set; }
    }
}
