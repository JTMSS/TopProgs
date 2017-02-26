using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TopProgs.Startup))]
namespace TopProgs
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
