using Microsoft.Owin;
using Owin;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

[assembly: OwinStartup(typeof(demo.values.Startup))]

namespace demo.values
{
  public class Startup
  {
    public void Configuration(IAppBuilder app)
    {
      // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
      #region Optional. Global.asax.cs `protected void Application_Start()` code can be move to `Startup.cs`. 

      // optional. disable certificate validation as it 
      // depends on the environment.
      ServicePointManager.ServerCertificateValidationCallback =
        delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

      AreaRegistration.RegisterAllAreas();
      FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
      RouteConfig.RegisterRoutes(RouteTable.Routes);
      BundleConfig.RegisterBundles(BundleTable.Bundles);

      #endregion

      app.ConfigureApp("development");
      app.ConfigureAuth();
    }
  }
}
