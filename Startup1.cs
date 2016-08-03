using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Owin;


[assembly: OwinStartup(typeof(OAparIdentityApp.Startup))]

namespace OAparIdentityApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {


            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie
            });

                //LoginPath = new PathString("/Login.aspx") });
        }
    }
}
