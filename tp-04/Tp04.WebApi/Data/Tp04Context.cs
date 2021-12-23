using System;
using Microsoft.EntityFrameworkCore;
using Tp04.WebApi.Entities;

namespace Tp04.WebApi.Data
{
  public class Tp04Context : DbContext
  {
    public DbSet<Livro> Livros { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      var dbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";

      var connectionString =
        $"Host={dbHost};Database=tp04;Username=tp04;Password=DockerPostgres127!";

      optionsBuilder.UseNpgsql(connectionString);
    }
  }
}
