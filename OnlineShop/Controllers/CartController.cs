using OnlineShop.Infrastructure;
using OnlineShop.Abstract;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace OnlineShop.Controllers
{
    public class CartController : Controller
    {
        private IBookDbRepository repository;
        public CartController(IBookDbRepository repo)
        {
            repository = repo;
        }
        public ActionResult OpenCart(Cart cart)
        {
            return View(cart);
        }

        public PartialViewResult CartInfo(Cart cart)
        {
            return PartialView(cart);
        }

        [HttpPost]
        public PartialViewResult AddToCart(Cart cart, int bookId)
        {
            Book book = repository.Books.FirstOrDefault(x => x.BookId == bookId);
            if (book != null)
            {
                cart.AddBook(book, 1);
            }
            return PartialView("CartInfo",  cart);
        }

        [HttpPost]
        public PartialViewResult SetAmountForBook(Cart cart, int bookId, int amount = 1)
        {
            Book book = repository.Books.FirstOrDefault(x => x.BookId == bookId);
            if (book != null && amount > 0 && amount < 30)
            {
                cart.SetAmount(book.BookId, amount);
            }
            return PartialView("CartInfo", cart);
        }

        public ActionResult RemoveBookFromCart(Cart cart, int bookId)
        {
            Book book = repository.Books.FirstOrDefault(x => x.BookId == bookId);
            if (book != null)
            {
                cart.RemoveBook(book.BookId);
            }
            return RedirectToAction("OpenCart");
        }

        [HttpGet]
        public ViewResult Checkout()
        {
            if (Request.IsAuthenticated)
            {
                return View(new OrderDetailsViewModel
                {
                    Name = User.Identity.Name,
                    Email = repository.Users.FirstOrDefault(x => x.UserName.Equals(User.Identity.Name)).Email
                });
            }
            else
            {
                return View(new OrderDetailsViewModel());
            }
        }

        [HttpPost]
        public ActionResult Checkout(OrderDetailsViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            return RedirectToAction("CheckoutPay", model);
        }

        public ActionResult CheckoutPay(Cart cart, OrderDetailsViewModel model)
        {
            model.Cart = cart;
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> CheckoutPayFinal(Cart cart, OrderDetailsViewModel model)
        {
            model.Cart = cart;
            if (User.Identity.IsAuthenticated)
            {
                await repository.SavePurchase(model, User.Identity.Name);
            }
            else
            {
                await repository.SavePurchase(model);
            }
            await Task.Run(() => 
            {
                DependencyResolver.Current.GetService<IOrderProcessor>().SendShoppingInfo(model);
                cart.Clear();
            });

            return RedirectToAction("Completed", new { email = model.Email });
        }
        public ViewResult Completed(string email)
        {
            return View(email);
        }
    }
}