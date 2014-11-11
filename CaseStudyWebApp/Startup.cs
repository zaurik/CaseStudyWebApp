using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CaseStudyWebApp.Startup))]
namespace CaseStudyWebApp
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
