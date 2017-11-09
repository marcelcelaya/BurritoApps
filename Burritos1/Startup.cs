using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Burritos1.Startup))]
namespace Burritos1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
