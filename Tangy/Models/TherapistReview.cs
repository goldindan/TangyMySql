using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Tangy.Models
{
    public class TherapistReview
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int TherapistId { get; set; }

        [Required]
        [Display(Name = "ציון")]
        public int Score { get; set; }

        [Required]
        [Display(Name ="פירוט")]
        public String Description { get; set; }

        public String CreateUserId { get; set; }

        public DateTime CreateDate { get; set; }

        [ForeignKey("TherapistId")]
        public virtual Therapist Therapist { get; set; }
    }
}
