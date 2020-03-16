using OnlineShop.Abstract;
using OnlineShop.ViewModels;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace OnlineShop.Concrete
{
    public class OrderProcessor : IOrderProcessor
    {
        public void EmailConfirm(string userEmail, string link)
        {
            using (var smtp = new SmtpClient())
            {
                MailAddress from = new MailAddress("escobar.tipson@gmail.com", "OnlineShop");
                MailAddress to = new MailAddress(userEmail);
                MailMessage message = new MailMessage(from, to)
                {
                    Subject = "Email confirmation",
                    Body = string.Format("Welcome to OnlineShop!" + Environment.NewLine +
                    "To complete registration, follow the link: " +
                    "<a href=\"{0}\" title=\"Confirm registration\">click me</a>", link),
                    IsBodyHtml = true
                };
                smtp.EnableSsl = true;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("escobar.tipson@gmail.com", "rfat_gnfx");

                smtp.Send(message);

            }
        }

        public void SendShoppingInfo(OrderDetailsViewModel orderInfo)
        {
            using (var smtp = new SmtpClient())
            {
                MailAddress from = new MailAddress("escobar.tipson@gmail.com", "OnlineShop");
                MailAddress to = new MailAddress(orderInfo.Email);

                StringBuilder strBuilder = new StringBuilder();
                strBuilder.AppendLine("Your order")
                    .AppendLine("-------")
                    .AppendLine("Books:");
                foreach (var item in orderInfo.Cart.TotalCart)
                {
                    var totalItemPrice = item.Amount * item.Book.Price;
                    strBuilder.AppendFormat("{0} x {1} total: {2:c}\n", item.Amount, item.Book.Name, totalItemPrice);
                }
                strBuilder.AppendFormat("Total price: {0:c}", orderInfo.Cart.TotalPrice())
                    .AppendLine("------")
                    .AppendLine("Deliver to")
                    .AppendFormat("Name: {0}", orderInfo.Name)
                    .AppendFormat("Country: {0}", orderInfo.Country)
                    .AppendFormat("City: {0}", orderInfo.City)
                    .AppendFormat("Street: {0}", orderInfo.Street);

                MailMessage message = new MailMessage(from, to)
                {
                    Subject = "Order details",
                    Body = strBuilder.ToString()
                };
                smtp.EnableSsl = true;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("escobar.tipson@gmail.com", "rfat_gnfx");
                smtp.Send(message);
            }
        }
    }
}