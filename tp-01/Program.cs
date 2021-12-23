using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Command.Controllers;
using Command.Data;
using Command.Entities;
using Command.Extensions;
using Command.Tests;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace Command
{
  class Program
  {
    static void Main(string[] args)
    {
      if (args.Contains("--test"))
      {
        Console.WriteLine("Initiating tests.");

        var bookTest = new BookTest();

        if (args.Contains("--log"))
          bookTest.EnableStepsLogs();

        bookTest
          .CreateBook()
          .DefineBookTitle("O teste de ós")
          .DefineBookPrice(999)
          .DefineBookQuantity(12)
          .AddAuthor(new Author()
          {
            Name = "Wellington",
            Email = "email@mail.com",
            Gender = 'M',
          })
          .AddAuthor(new Author()
          {
            Name = "Anne",
            Email = "email@mail.com",
            Gender = 'F',
          });

        var book = bookTest.ExportBookCopy();

        Console.WriteLine("Executing \".ToolerString()\" Book method: ");
        Console.WriteLine(book.ToolerString());
      }
      else
      {
        Console.WriteLine("Initiating server.");

        using (var mongoDbManager = new MongoDbManager())
        {
          var thereAreNoBooks = !mongoDbManager.Books.Find((p) => true).Any();

          if (thereAreNoBooks)
            mongoDbManager.Books.InsertMany(
              JsonConvert.DeserializeObject<IList<Book>>(
                File.ReadAllText(
                  Path.Combine(Directory.GetCurrentDirectory(), "mock.json")
                )
              )
            );
        }

        CreateHostBuilder().Build().Run();
      }
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
