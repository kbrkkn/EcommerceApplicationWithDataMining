using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCWebUI.Startup))]
namespace MVCWebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
