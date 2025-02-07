using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Neighbourly_application.Startup))]
namespace Neighbourly_application
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
