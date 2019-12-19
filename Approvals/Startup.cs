using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Approvals.Startup))]
namespace Approvals
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
