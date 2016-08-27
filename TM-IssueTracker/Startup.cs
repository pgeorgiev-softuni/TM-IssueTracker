using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TM_IssueTracker.Startup))]
namespace TM_IssueTracker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

    }
}
