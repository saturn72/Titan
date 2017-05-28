using System.Web.Http;
using Microsoft.Owin.Cors;
using Owin;

namespace Calculator.WebApi.Host.Configure
{
    class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            appBuilder.UseCors(CorsOptions.AllowAll);

            appBuilder.UseWebApi(config);
        }
    }
}
