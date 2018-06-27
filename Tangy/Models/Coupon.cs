using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Tangy.Models
{
    public class Coupon
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public String Name { get; set; }

        [Required]
        public string CouponType { get; set; }
        public enum ECouponType { Percent=0, Dollar=1}

        [Required]
        public double Discount { get; set; }

        [Required]
        public double MinimumAmount { get; set; }

        [Column(TypeName = "varbinary(100)")]
        public Byte[] Picture { get; set; }

        public bool isActive { get; set; }



    }
}
