using OnlineShop.ViewModels;

namespace OnlineShop.Abstract
{
    public interface IOrderProcessor
    {
        void EmailConfirm(string email, string link);

        void SendShoppingInfo(OrderDetailsViewModel info);
    }
}
