using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SampleTracking.Startup))]
namespace SampleTracking
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
