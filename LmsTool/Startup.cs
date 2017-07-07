using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LmsTool.Startup))]
namespace LmsTool
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
