using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tangy.Models.ManageViewModels
{
    public class IndexViewModel
    {
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name ="דוא\"ל")]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "מספר טלפון")]
        public string PhoneNumber { get; set; }

        public string StatusMessage { get; set; }

        [Required]
        [Display(Name ="שם פרטי")]
        public String FirstName { get; set; }

        [Required]
        [Display(Name = "שם משפחה")]
        public String LastName { get; set; }
    }
}
