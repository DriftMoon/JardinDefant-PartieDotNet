using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Solution.Web2.Startup))]
namespace Solution.Web2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
