using OnlineShop.Abstract;
using OnlineShop.Entities;
using OnlineShop.Infrastructure;
using OnlineShop.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;

namespace OnlineShop.Controllers
{
    public class AccountController : Controller
    {
        private IBookDbRepository repository;
        public AccountController(IBookDbRepository repo)
        {
            repository = repo;
        }
        public ActionResult Login()
        {
            if (!User.Identity.IsAuthenticated)
                return View(new LoginViewModel());
            else
                return RedirectToAction("BookList", "Main");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Cart cart, LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(new LoginViewModel { UserName = model.UserName });
            User user = repository.UserCheckCredentials(model.UserName, model.Password.GetHashCode().ToString());
            if (user != null)
            {
                if (!user.ActivatedEmail)
                {
                    return RedirectToAction("EmailConfirm", "Account", new { email = user.Email });
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(user.UserName, model.RememberMe);
                    return RedirectToAction("BookList", "Main");
                }
            }
            else
            {
                ModelState.AddModelError("", "Incorrect username or password");
                return View(new LoginViewModel { UserName = model.UserName });
            }
        }

        public ActionResult EmailConfirm(string email)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("BookList", "Main");
            }
            return View(model: email);
        }

        public ActionResult Register()
        {
            if (!User.Identity.IsAuthenticated)
                return View(new RegisterViewModel());
            else
                return RedirectToAction("BookList", "Main");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { UserName = model.UserName, Password = model.Password.GetHashCode().ToString(), Email = model.Email, ActivatedEmail = false };
                string otvet = repository.SaveUser(user);
                if (otvet.Equals("OK"))
                {
                    string link = Url.Action("CheckLinkFromEmail", "Account", new { userId = user.UserId, email = user.Email }, Request.Url.Scheme);
                    await Task.Run(() =>
                    {
                        DependencyResolver.Current.GetService<IOrderProcessor>().EmailConfirm(user.Email, link);
                    });
                    return RedirectToAction("EmailConfirm", "Account", new { email = user.Email });
                }
                else
                {
                    ModelState.AddModelError("", otvet);
                }
            }
            return View(model);
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CheckLinkFromEmail(Cart cart, int userId, string email)
        {
            if (repository.ConfirmUser(userId, email))
            {
                FormsAuthentication.SetAuthCookie(repository.Users.FirstOrDefault(u => u.UserId == userId).UserName, false);
                return RedirectToAction("List", "Books");
            }
            else
            {
                ViewBag.EmailError = "Data is not valid";
                return RedirectToAction("EmailConfirm", "Account", new { email = email });
            }
        }

        [Authorize]
        public ActionResult Logout(Cart cart, string returnURL)
        {
            FormsAuthentication.SignOut();
            cart.Clear();
            return RedirectToAction("BookList", "Main");
        }

        [Authorize]
        public ViewResult UserOrders()
        {
            var model = repository.GetUserOrders(User.Identity.Name);
            return View(model);
        }

        [Authorize]
        public ViewResult UserProfile()
        {
            var model = repository.Users.FirstOrDefault(x => x.UserName.Equals(User.Identity.Name));
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult UserProfile(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            if (!string.IsNullOrEmpty(user.Password))
            {
                user.Password = user.Password.GetHashCode().ToString();
            }
            string otvet = repository.SaveUser(user);
            if (!otvet.Equals("OK"))
            {
                ModelState.AddModelError("", otvet);
                return View(user);
            }
            TempData["message"] = "Changes saved";
            FormsAuthentication.SignOut();
            FormsAuthentication.SetAuthCookie(user.UserName, false);
            return RedirectToAction(actionName: "UserProfile");
        }
    }
}