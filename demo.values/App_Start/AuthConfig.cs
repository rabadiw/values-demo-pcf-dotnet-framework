using Microsoft.Extensions.Configuration;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Steeltoe.CloudFoundry.Connector;
using Steeltoe.CloudFoundry.Connector.Services;
using Steeltoe.Security.Authentication.CloudFoundry.Owin;
using System;
using System.Linq;
using System.Security.Claims;

namespace demo.values
{
  public static class AuthConfig
  {
    public static IConfiguration Configuration { get; set; }

    public static void ConfigureAuth(this IAppBuilder app)
    {
      app.SetDefaultSignInAsAuthenticationType("ExternalCookie");
      app.UseCookieAuthentication(new CookieAuthenticationOptions
      {
        AuthenticationType = "ExternalCookie",
        AuthenticationMode = AuthenticationMode.Active,
        CookieName = ".AspNet.ExternalCookie",
        LoginPath = new PathString("/Account/AuthorizeSSO"),
        ExpireTimeSpan = TimeSpan.FromMinutes(5),

      });

      var serviceInfos = CloudFoundryServiceInfoCreator.Instance(AppConfig.Configuration);
      var ssoInfo = serviceInfos.GetServiceInfos<SsoServiceInfo>().FirstOrDefault()
                      ?? throw new NullReferenceException("Service info for an SSO Provider was not found!");

      app.UseOpenIDConnect(new OpenIDConnectOptions()
      {
        ClientID = ssoInfo.ClientId,
        ClientSecret = ssoInfo.ClientSecret,
        AuthDomain = ssoInfo.AuthDomain,
        AppHost = ssoInfo.ApplicationInfo.ApplicationUris.First(),
        AppPort = 0,
        //AdditionalScopes = "testgroup",
        ValidateCertificates = false,
        CallbackPath = new PathString("/signin-oidc") // Default Callback
      });

      System.Web.Helpers.AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
    }
  }
}