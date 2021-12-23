using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Command
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Initiating server.");

      CreateHostBuilder().Build().Run();
    }

    private static IWebHostBuilder CreateHostBuilder() =>
      new WebHostBuilder()
        .UseKestrel()
        .ConfigureKestrel(options =>
        {
          if (!int.TryParse(
              Environment.GetEnvironmentVariable("APP_PORT"),
              out var port
            )
          )
            port = 3333;

          options.ListenAnyIP(port);
        })
        .UseStartup<Startup>();
  }
}
