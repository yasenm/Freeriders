using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FreeRiders.Web.Startup))]
namespace FreeRiders.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
