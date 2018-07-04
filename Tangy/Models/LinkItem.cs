using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tangy.Models
{
    public class LinkItem
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "מיקום תצוגה")]
        public int OrderId { get; set; }

        [Display(Name = "תמונה")]
        public String Image{ get; set; }

        [Required]
        [Display(Name = "קישור")]
        public String Url { get; set; }

        [Required]
        [Display(Name = "כותרת")]
        public String Subject { get; set; }

        [Required]
        [Display(Name = "תיאור")]
        public String Description { get; set; }

        [Required]
        [Display(Name = "פעיל")]
        public bool isActive { get; set; }
    }
}
