using OnlineShop.Abstract;
using OnlineShop.ViewModels;
using System.Linq;
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
            model.MinPrice = model.MinPrice == 0 
                ? repository.Books.Min(x => (int?)x.Price) ?? 0 
                : model.MinPrice;

            model.MaxPrice = model.MaxPrice == 0 
                ? repository.Books.Max(x => (int?)x.Price) ?? 10_000 
                : model.MaxPrice;

            return PartialView(model);
        }
    }
}