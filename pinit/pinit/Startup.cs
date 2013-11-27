using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(pinit.Startup))]
namespace pinit
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
