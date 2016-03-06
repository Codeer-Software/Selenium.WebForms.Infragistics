using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InfragisticsWebFormsSample.Startup))]
namespace InfragisticsWebFormsSample
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
