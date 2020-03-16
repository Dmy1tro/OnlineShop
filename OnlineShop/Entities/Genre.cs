using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Entities
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }

        [Required]
        [MaxLength(200)]
        public string GenreName { get; set; }

        public virtual ICollection<Book> Books { get; set; } = new HashSet<Book>();
    }
}