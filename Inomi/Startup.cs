using System;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.Annotations;
using Hangfire.Dashboard;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Inomi.Startup))]

namespace Inomi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            GlobalConfiguration.Configuration
               .UseSqlServerStorage("Hangfire_Blog");

            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new MyAuthorizationFilter() }
            });

            app.UseHangfireServer();
        }
    }

    public class MyAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {

            var role = "Admin";


            if (role == "Admin")
            {
                return true;
            }
            else
            {
                return false;
            }
            // Allow all authenticated users to see the Dashboard (potentially dangerous).
        }

        public bool Authorize([NotNull] Hangfire.Dashboard.DashboardContext context)
        {
            throw new NotImplementedException();
        }
    }
}
