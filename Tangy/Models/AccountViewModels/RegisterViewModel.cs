using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tangy.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "דוא\"ל")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "סיסמה")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "אישור סיסמה")]
        [Compare("Password", ErrorMessage = "הסיסמה והסיסמה לאישור לא מתאימים")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "שם פרטי")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "שם משפחה")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "טלפון")]
        public string PhoneNumber { get; set; }

    }
}
