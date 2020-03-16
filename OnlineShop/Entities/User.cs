using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Entities
{
    public class User
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }

        [MaxLength(50)]
        public string Password { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        public bool ActivatedEmail { get; set; }

        public virtual ICollection<OrderHeader> OrderHeaders { get; set; } = new HashSet<OrderHeader>();
    }
}