using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TempWeb.Startup))]
namespace TempWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
