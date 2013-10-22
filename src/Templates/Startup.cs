using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Templates.Startup))]
namespace Templates
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
