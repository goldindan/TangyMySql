using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Tangy.Models
{
    public class OrderHeader
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public String UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public double OrderTotal { get; set; }

        [Required]
        public DateTime PickUpTime { get; set; }

        public String CouponCode { get; set; }
        public String Status { get; set; }
        public String Comments { get; set; }
    }
}
