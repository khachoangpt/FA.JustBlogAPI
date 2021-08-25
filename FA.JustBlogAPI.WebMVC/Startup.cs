using Owin;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(FA.JustBlogAPI.WebMVC.Startup))]
namespace FA.JustBlogAPI.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
