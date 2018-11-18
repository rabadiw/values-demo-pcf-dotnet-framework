using Microsoft.Extensions.Configuration;
using Owin;
using Steeltoe.Extensions.Configuration.CloudFoundry;
using System;

namespace demo.values
{
  public static class AppConfig
  {
    public static IConfiguration Configuration { get; set; }

    public static void ConfigureApp(this IAppBuilder app, string environment)
    {
      // Set up configuration sources.
      var builder = new ConfigurationBuilder()
          .SetBasePath(GetContentRoot())
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
          .AddJsonFile($"appsettings.{environment}.json", optional: true)
          .AddEnvironmentVariables()
          .AddCloudFoundry();

      Configuration = builder.Build();
    }
    public static string GetContentRoot()
    {
      var basePath = (string)AppDomain.CurrentDomain.GetData("APP_CONTEXT_BASE_DIRECTORY") ??
         AppDomain.CurrentDomain.BaseDirectory;
      return System.IO.Path.GetFullPath(basePath);
    }
  }
}