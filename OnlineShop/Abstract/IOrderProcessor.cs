using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Models;

namespace OnlineShop.Abstract
{
    public interface IOrderProcessor
    {
        void EmailConfirm(string email, string link);

        void SendShoppingInfo(OrderDetailsViewModel info);
    }
}
