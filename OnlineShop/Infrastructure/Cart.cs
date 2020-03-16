using OnlineShop.Entities;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.Infrastructure
{
    public class Cart
    {
        private List<ProductDetail> ProductDetails = new List<ProductDetail>();
        public IEnumerable<ProductDetail> TotalCart { get => ProductDetails; }
        public void AddBook(Book book, int amount)
        {
            ProductDetail product = ProductDetails.FirstOrDefault(x => x.Book.BookId == book.BookId);
            if (product is null)
            {
                ProductDetails.Add(new ProductDetail { Book = book, Amount = amount });
            }
            else
            {
                product.Amount += amount;
            }
        }

        public void SetAmount(int bookId, int amount)
        {
            ProductDetail product = ProductDetails.FirstOrDefault(x => x.Book.BookId == bookId);
            if (product != null)
            {
                product.Amount = amount;
            }
        }

        public void RemoveBook(int bookId)
        {
            ProductDetails.RemoveAll(x => x.Book.BookId == bookId);
        }

        public decimal TotalPrice()
        {
            return ProductDetails.Sum(x => x.Book.Price * x.Amount);
        }
        public void Clear()
        {
            ProductDetails.Clear();
        }
    }
    public class ProductDetail
    {
        public Book Book { get; set; }
        public int Amount { get; set; }
    }
}