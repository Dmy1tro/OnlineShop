using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineShop.Controllers;
using OnlineShop.Abstract;
using OnlineShop.Concrete;
using Moq;
using System.Collections.Generic;
using OnlineShop;
using OnlineShop.Infrastructure;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Add_range_cart()
        {
            Mock<IBookDbRepository> mock = new Mock<IBookDbRepository>();
            mock.Setup(x => x.Books as IEnumerable<Book>).Returns(new List<Book>
            {
                new Book{ BookId= 1, Name = "Name1" },
                new Book{ BookId= 1, Name = "Name1" },
                new Book{ BookId= 2, Name = "Name2" },
                new Book{ BookId= 2, Name = "Name2" },
                new Book{ BookId= 2, Name = "Name2" },
                new Book{ BookId= 3, Name = "Name3" },
                new Book{ BookId= 3, Name = "Name3" },
                new Book{ BookId= 4, Name = "Name4" },
                new Book{ BookId= 5, Name = "Name5" },
            });

            Cart cart = new Cart();
            CartController controller = new CartController(mock.Object);

            controller.AddToCart(cart, 1);
            controller.AddToCart(cart, 2);

            Assert.AreEqual(cart.TotalCart.Count(), 2);
            Assert.AreEqual(cart.TotalCart.FirstOrDefault(x => x.Book.BookId == 1).Amount, 1);
            Assert.AreEqual(cart.TotalCart.FirstOrDefault(x => x.Book.BookId == 2).Amount, 1);
        }
    }
}
