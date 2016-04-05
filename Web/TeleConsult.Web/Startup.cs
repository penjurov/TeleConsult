using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TeleConsult.Web.Startup))]
namespace TeleConsult.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);
        }
    }
}
