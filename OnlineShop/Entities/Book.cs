using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Entities
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Required]
        [MaxLength(300)]
        public string Name { get; set; }

        [Required]
        [MaxLength(300)]
        public string Author { get; set; }

        public int? Year { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [ForeignKey(nameof(Genre))]
        public int GenreId { get; set; }

        [Required]
        public decimal Price { get; set; }

        public virtual Genre Genre { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new HashSet<OrderDetail>();
    }
}