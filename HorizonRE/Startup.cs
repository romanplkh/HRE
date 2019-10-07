using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HorizonRE.Startup))]
namespace HorizonRE
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
