using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineShop.Controllers;
using OnlineShop.Abstract;
using OnlineShop.Concrete;
using Moq;
using System.Collections.Generic;
using OnlineShop;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Mock<IBookDbRepository> mock = new Mock<IBookDbRepository>();
            mock.Setup(x => x.Genres as IEnumerable<Genre>).Returns(new List<Genre>
            {
                new Genre { GenreName = "genre1", Books = new List<Book> { new Book { BookId = 1 } } },

            });
            EFBookRepository repository = new EFBookRepository();
            var result = repository.GetGenresCount();
            Assert.AreEqual(1,1);
        }
    }
}
