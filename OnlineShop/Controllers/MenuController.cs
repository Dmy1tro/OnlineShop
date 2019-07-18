using System;
using System.Collections.Generic;
using System.Linq;
using OnlineShop.Abstract;
using OnlineShop.Models;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class MenuController : Controller
    {
        IBookDbRepository repository;
        public MenuController(IBookDbRepository repo)
        {
            repository = repo;
        }
        public PartialViewResult MenuItems(string genre)
        {
            return PartialView(new MenuGenreViewModel { Genres = repository.GetGenresCount(), CurrentGenre = genre });
        }

        public PartialViewResult FilterMenu(MenuFilterViewModel model)
        {
            model.MinPrice = model.MinPrice == 0 ? repository.Books.Min(x => x.Price) : model.MinPrice;
            model.MaxPrice = model.MaxPrice == 0 ? repository.Books.Max(x => x.Price) : model.MaxPrice;
            return PartialView(model);
        }
    }
}