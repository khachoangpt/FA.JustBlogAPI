using Newtonsoft.Json;
using Swashbuckle.Application;
using System.Web.Http;

namespace FA.JustBlogAPI.WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
