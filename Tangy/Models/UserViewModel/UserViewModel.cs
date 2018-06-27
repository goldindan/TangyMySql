using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tangy.Models.UserViewModel
{
    public class UserViewModel
    {
        public String Id { get; set; }

        [Required]
        [Display(Name = "שם פרטי")]
        public String FirstName { get; set; }

        [Required]
        [Display(Name = "שם משפחה")]
        public String LastName { get; set; }

        [Display(Name = "טלפון")]
        public String PhoneNumber { get; set; }

        [Display(Name = "דוא\"ל")]
        public String Email { get; set; }

        [Display(Name = "סיבת נעילה")]
        public String LockoutReason { get; set; }

        [Display(Name = "סיום נעילה")]
        public DateTime? LockoutEnd { get; set; }

        [Display(Name = "ניסיונות כשלים")]
        public int AccessFailedCount { get; set; }

    }
}
