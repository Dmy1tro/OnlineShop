using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using OnlineShop.Concrete;
using OnlineShop.Infrastructure;
using OnlineShop.SampleData;
using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace OnlineShop
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());

            this.ConfigureDi();
            this.SeedDataBase();
        }

        private void SeedDataBase()
        {
            var context = new EFDbContext();

            new DBInitializer().Seed(context);
        }

        private void ConfigureDi()
        {
            //Dependency Resolver
            NinjectModule registrations = new DependencyInjection();
            var kernel = new StandardKernel(registrations);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}
