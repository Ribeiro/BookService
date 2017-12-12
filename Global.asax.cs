using BookService.ExceptionHandling;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BookService
{
    public class WebApiApplication : System.Web.HttpApplication
    {

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            System.Web.Http.GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Removing XMLFormatter in order to allways respond to JSON
            System.Web.Http.GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

            System.Web.Http.GlobalConfiguration.Configuration.MessageHandlers.Add(new ResponseWrappingHandler());

            //Using Hangfire to inject dependencies
            //Container container = new SimpleInjector.Container();
            //container.Register<IUnitOfWork, UnitOfWork>();
            //container.Register<IBookRepository, BookRepository>();
            //container.Register<IAuthorRepository, AuthorRepository>();

            //Hangfire.GlobalConfiguration.Configuration.UseActivator(new ContainerJobActivator(container));

            //JobActivator.Current = new SimpleInjectorJobActivator(container);

        }
        
    }

}
