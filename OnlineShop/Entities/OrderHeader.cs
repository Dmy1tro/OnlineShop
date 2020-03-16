using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineShop.Entities
{
    public class OrderHeader
    {
        [Key]
        public int OrderId { get; set; }

        [ForeignKey(nameof(User))]
        public int? UserId { get; set; }

        [Required]
        [MaxLength(300)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(500)]
        public string Address { get; set; }

        public DateTime Date { get; set; }

        [Required]
        [MaxLength(200)]
        public string UserEmail { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new HashSet<OrderDetail>();
    }
}