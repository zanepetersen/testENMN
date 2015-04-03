using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ENMN.Startup))]
namespace ENMN
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
