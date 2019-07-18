using Ninject.Modules;
using OnlineShop.Abstract;
using OnlineShop.Concrete;

namespace OnlineShop.Infrastructure
{
    public class DependencyInjection : NinjectModule
    {
        public override void Load()
        {
            Bind<IBookDbRepository>().To<EFBookRepository>();
            Bind<IOrderProcessor>().To<OrderProcessor>();
        }
    }
}