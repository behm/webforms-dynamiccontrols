using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DynamicControls.Startup))]
namespace DynamicControls
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
