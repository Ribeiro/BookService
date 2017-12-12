using BookService.ExceptionHandling;
using BookService.Persistence;
using Hangfire;
using Hangfire.SimpleInjector;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
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

            Container container = new SimpleInjector.Container();
            container.Register<IUnitOfWork, UnitOfWork>();
            JobActivator.Current = new SimpleInjectorJobActivator(container);

        }
        
    }

}
