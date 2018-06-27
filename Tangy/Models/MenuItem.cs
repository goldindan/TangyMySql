using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Tangy.Models
{
    public class MenuItem
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public String  Name { get; set; }

        public String Description { get; set; }
        public String Image { get; set; }
        public String Spicyness { get; set; }

        public enum ESpicy { NA=0, Spicy=1, VerySpicy=2}

        [Range(1,int.MaxValue,ErrorMessage ="Price should be greater than ${1}")]
        public double Price { get; set; }

        [Display(Name ="Category")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { set; get; }

        [Display(Name = "SubCategory")]
        public int SubCategoryId { get; set; }

        [ForeignKey("SubCategoryId")]
        public virtual SubCategory SubCategory { set; get; }


    }
}
