using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Tangy.Models
{
    public class Event
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "שם אירוע")]
        public String Name { get; set; }

        [Required]
        [Display(Name = "תיאור")]
        public String Description { get; set; }

        public String Placement { get; set; }

        [Display(Name = "תחילת אירוע")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "סיום אירוע")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "תמונה")]
        public String Image { get; set; }

        [Display(Name = "פעיל")]
        public bool IsActive { get; set; }

        [Required]
        [Display(Name = "מיקום תצוגה")]
        public int OrderId { get; set; }
    }
}
