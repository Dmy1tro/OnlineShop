using System;
using System.Collections.Generic;
using System.Linq;
using OnlineShop.Models;
using System.Web.Mvc;
using OnlineShop.Abstract;
using OnlineShop.Infrastructure;

namespace OnlineShop.Controllers
{
    public class MainController : Controller
    {
        private IBookDbRepository repository;
        private int pageSize = 6;
        public MainController(IBookDbRepository repo)
        {
            repository = repo;
        }
        public ViewResult BookList(Cart cart, int page = 1, string genre = null)
        {
            BooksListViewModel model = new BooksListViewModel
            {
                Books = repository.Books
                    .Where(x => string.IsNullOrEmpty(genre) || x.Genre.GenreName.Equals(genre))
                    .OrderBy(x => x.BookId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize),

                BooksInCart = cart.TotalCart.Select(x => x.Book.BookId),

                Pagination = new Pagination
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = string.IsNullOrEmpty(genre) ? 
                        repository.Books.Count() : 
                        repository.Books.Where(x => x.Genre.GenreName.Equals(genre)).Count()
                }
            };
            return View(model);
        }

        public ActionResult BookListFilter(Cart cart, MenuFilterViewModel model, int page = 1)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("BookList");
            IEnumerable<Book> books = repository.Books
                .Where(x => (string.IsNullOrEmpty(model.Genre) || x.Genre.GenreName.Equals(model.Genre))
                    && x.Price >= model.MinPrice && x.Price <= model.MaxPrice);
            switch (model.SortItem)
            {
                case "Default":
                    {
                        books = books.OrderBy(x => x.BookId);
                        break;
                    }
                case "Name_ASC":
                    {
                        books = books.OrderBy(x => x.Name);
                        break;
                    }
                case "Name_DESC":
                    {
                        books = books.OrderByDescending(x => x.Name);
                        break;
                    }
                case "Price_ASC":
                    {
                        books = books.OrderBy(x => x.Price);
                        break;
                    }
                case "Price_DESC":
                    {
                        books = books.OrderByDescending(x => x.Price);
                        break;
                    }
            };

            BooksListViewModel bookList = new BooksListViewModel
            {
                Books = books,

                BooksInCart = cart.TotalCart.Select(x => x.Book.BookId),

                Pagination = new Pagination { ItemsPerPage = 1 }
            };

            return View("BookList", bookList);
        }
    }
}