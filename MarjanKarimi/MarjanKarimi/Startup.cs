using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MarjanKarimi.Startup))]
namespace MarjanKarimi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
