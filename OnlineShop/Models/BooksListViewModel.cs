using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    public class BooksListViewModel
    {
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<int> BooksInCart { get; set; }
        public Pagination Pagination { get; set; }
    }
}