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
            //GlobalConfiguration.Configuration.UseSqlServerStorage("BookServiceContext");

            //app.UseHangfireDashboard();
            //app.UseHangfireServer();

            //RecurringJob.AddOrUpdate(() => Console.WriteLine("Minutely Job executed"), Cron.Minutely);

            GlobalConfiguration.Configuration.UseSqlServerStorage("BookServiceContext");

            app.UseHangfireDashboard();
            app.UseHangfireServer();

        }

    }
}