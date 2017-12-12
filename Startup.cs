using Hangfire;
using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartup(typeof(BookService.Startup))]
namespace BookService
{
    public class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage("BookServiceContext");

            GlobalConfiguration.Configuration.UseNinjectActivator(new Ninject.Web.Common.Bootstrapper().Kernel);

            app.UseHangfireDashboard();
            app.UseHangfireServer();

        }

    }
}