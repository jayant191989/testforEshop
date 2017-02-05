using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HR_Management.Web.Startup))]
namespace HR_Management.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
