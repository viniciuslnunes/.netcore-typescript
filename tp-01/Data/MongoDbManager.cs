using System;
using Command.Entities;
using MongoDB.Driver;

namespace Command.Data
{
  public class MongoDbManager : IDisposable
  {
    private bool disposed;
    private static MongoClient _mongoClient;
    private IMongoDatabase GetDatabase() => GetClient().GetDatabase("command_database");

    private MongoClient GetClient()
    {
      if (MongoDbManager._mongoClient is null)
        CreateClient();

      return MongoDbManager._mongoClient;
    }

    private void CreateClient()
    {
      var host = Environment.GetEnvironmentVariable("MONGO_HOST");
      var username = Environment.GetEnvironmentVariable("MONGO_USER");
      var password = Environment.GetEnvironmentVariable("MONGO_PASSWORD");

      MongoDbManager._mongoClient = new MongoClient(
        $"mongodb://{username}:{password}@{host}:27017/?authSource=admin"
      );
    }

    public IMongoCollection<Book> Books { get => GetDatabase().GetCollection<Book>("books"); }

    protected virtual void Dispose(bool disposing)
    {
      if (!disposed)
      {
        if (disposing)
        {
          //dispose managed resources
        }
      }

      //dispose unmanaged resources
      disposed = true;
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }
  }
}