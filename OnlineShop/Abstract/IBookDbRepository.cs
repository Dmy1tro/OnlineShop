using OnlineShop.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Abstract
{
    public interface IBookDbRepository
    {
        IQueryable<Book> Books { get; }
        IQueryable<Genre> Genres { get; }
        IQueryable<OrderHeader> OrderHeaders { get; }
        IQueryable<OrderDetail> OrderDetails { get; }
        IQueryable<User> Users { get; }

        Dictionary<string, int> GetGenresCount();
        Task SavePurchase(OrderDetailsViewModel model);
        Task SavePurchase(OrderDetailsViewModel model, string userName);
        User UserCheckCredentials(string userName, string PasswordHash);
        string SaveUser(User user);
        bool ConfirmUser(int userId, string email);
        IEnumerable<OrderHeader> GetUserOrders(string userName);
    }
}
