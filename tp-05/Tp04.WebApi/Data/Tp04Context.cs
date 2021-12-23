using System;
using Microsoft.EntityFrameworkCore;
using Tp04.WebApi.Entities;

namespace Tp04.WebApi.Data
{
  public class Tp04Context : DbContext
  {
    public DbSet<Livro> Livros { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Produto> Produtos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlite("Data Source=sqlite.db");
    }
  }
}
