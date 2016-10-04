using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyMVCProject.Startup))]
namespace MyMVCProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
