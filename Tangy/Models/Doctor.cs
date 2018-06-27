using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Tangy.Models
{
    public class Doctor
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name="שם הרופא")]
        public String Name { get; set; }

        [Display(Name ="מקום עבודה")]
        public String PlaceName { get; set; }

        [Display(Name ="טלפון")]
        public String Phone { get; set; }

        [Display(Name ="כותבת")]
        public String Address { get; set; }

        [Display(Name ="עיר")]
        public String City { get; set; }

        [Display(Name="הארות")]
        public String Description { get; set; }

        [Display(Name = "דוא\"ל")]
        public String Email { get; set; }

        public String CreateUserId { get; set; }

        public DateTime CreateDate { get; set; }

        [Required]
        public int MedicalFieldId { get; set; }

        [ForeignKey("MedicalFieldId")]
        public virtual MedicalField MedicalField { get; set; }

        public List<DoctorReview> DoctorReviews { get; set; }
    }
}
