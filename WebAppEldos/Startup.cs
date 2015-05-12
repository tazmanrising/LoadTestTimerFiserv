using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebAppEldos.Startup))]
namespace WebAppEldos
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
