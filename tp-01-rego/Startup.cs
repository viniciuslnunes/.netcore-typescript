using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Command
{
  public class Startup : IStartup
  {
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
      this._configuration = configuration;
    }

    public void Configure(IApplicationBuilder app)
    {
      var applicationRouter = new ApplicationRouter();

      app.Run(applicationRouter.Router);
    }

    public IServiceProvider ConfigureServices(IServiceCollection services)
    {
      return services.BuildServiceProvider();
    }
  }
}