using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(mvcAuth2019.Startup))]
namespace mvcAuth2019
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
