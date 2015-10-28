using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ReviewStartup.Startup))]
namespace ReviewStartup
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
