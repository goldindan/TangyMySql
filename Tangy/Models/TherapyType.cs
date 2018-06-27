using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tangy.Models
{
    public class TherapyType
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "שם טיפול")]
        public String Name { get; set; }

        [Required]
        [Display(Name = "סדר תצוגה")]
        public int DisplayOrder { get; set; }
    }
}
