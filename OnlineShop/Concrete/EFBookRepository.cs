using OnlineShop.Abstract;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

namespace OnlineShop.Concrete
{
    public class EFBookRepository : IBookDbRepository
    {
        private ShopDatabaseContext context = new ShopDatabaseContext();
        public IQueryable<Book> Books => context.Books.Include(x => x.Genre).AsNoTracking();

        public IQueryable<Genre> Genres => context.Genres.AsNoTracking();

        public IQueryable<OrderHeader> OrderHeaders => context.OrderHeaders.AsNoTracking();

        public IQueryable<OrderHeader> OrderHeadersWithDetails => context.OrderHeaders
            .Include(x => x.OrderDetails.Select(o => o.Book))
            .AsNoTracking();

        public IQueryable<OrderDetail> OrderDetails => context.OrderDetails.AsNoTracking();

        public IQueryable<User> Users => context.Users.AsNoTracking();

        public Dictionary<string, int> GetGenresCount()
        {
            return Genres.ToDictionary(x => x.GenreName, x => x.Books.Count);
        }

        public async Task SavePurchase(OrderDetailsViewModel model)
        {
            OrderHeader orderHeader = context.OrderHeaders.Add(new OrderHeader
            {
                Address = $"Country: {model.Country}; City: {model.City}; Street: {model.Street}",
                Date = DateTime.Now,
                UserName = model.Name,
                UserEmail = model.Email,
                UserId = null,
            });

            IEnumerable<OrderDetail> orderDetails = model.Cart.TotalCart
                .Select(x => new OrderDetail
                {
                    BookId = x.Book.BookId,
                    Count = x.Amount,
                    OrderId = orderHeader.OrderId,
                    TotalPrice = x.Amount * x.Book.Price
                });
            context.OrderDetails.AddRange(orderDetails);

            await context.SaveChangesAsync();
        }

        public async Task SavePurchase(OrderDetailsViewModel model, string userName)
        {
            int userId = Users.FirstOrDefault(x => x.UserName.Equals(userName)).UserId;
            OrderHeader orderHeader = context.OrderHeaders.Add(new OrderHeader
            {
                Address = $"Country: {model.Country}; City: {model.City}; Street: {model.Street}",
                Date = DateTime.Now,
                UserName = model.Name,
                UserEmail = model.Email,
                UserId = userId,
            });

            IEnumerable<OrderDetail> orderDetails = model.Cart.TotalCart
                .Select(x => new OrderDetail
                {
                    BookId = x.Book.BookId,
                    Count = x.Amount,
                    OrderId = orderHeader.OrderId,
                    TotalPrice = x.Amount * x.Book.Price,
                });
            context.OrderDetails.AddRange(orderDetails);

            await context.SaveChangesAsync();
        }

        public User UserCheckCredentials(string userName, string Password)
        {
            return Users.FirstOrDefault(x => (x.UserName.Equals(userName) || x.Email.Equals(userName)) && x.Password.Equals(Password));
        }

        public string SaveUser(User user)
        {
            User dbUser = Users.FirstOrDefault(x => x.UserName.Equals(user.UserName));
            User dbEmailUser = Users.FirstOrDefault(x => x.Email.Equals(user.Email));
            if (dbUser != null && dbUser.UserId != user.UserId)
                return $"User with this username '{user.UserName}' already exist";
            if (dbEmailUser != null && dbEmailUser.UserId != user.UserId)
                return $"This email '{user.Email}' is already in use";

            if (user.UserId == 0)
            {
                context.Users.Add(user);
            }
            else
            {
                User dbEntry = context.Users.Find(user.UserId);
                if (dbEntry != null)
                {
                    dbEntry.UserName = user.UserName;
                    dbEntry.Password = string.IsNullOrEmpty(user.Password) ? dbEntry.Password : user.Password;
                    dbEntry.Email = user.Email;
                }
            }
            if (context.SaveChanges() > 0)
            {
                return "OK";
            }
            return "Error";
        }

        public bool ConfirmUser(int userId, string email)
        {
            User user = context.Users.Find(userId);
            if (user != null && user.Email.Equals(email))
            {
                user.ActivatedEmail = true;
            }
            return context.SaveChanges() > 0;
        }

        public IEnumerable<OrderHeader> GetUserOrders(string userName)
        {
            var userId = Users.FirstOrDefault(x => x.UserName.Equals(userName)).UserId;
            return OrderHeadersWithDetails.Where(x => x.UserId != null && x.UserId == userId);
        }
    }
}