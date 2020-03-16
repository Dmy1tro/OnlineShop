using OnlineShop.Entities;
using System.Collections.Generic;

namespace OnlineShop.ViewModels
{
    public class BooksListViewModel
    {
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<int> BooksInCart { get; set; }
        public Pagination Pagination { get; set; }
    }
}