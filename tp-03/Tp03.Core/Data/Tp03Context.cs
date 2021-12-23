using Microsoft.EntityFrameworkCore;
using Tp03.Core.Entities;

namespace Tp03.Core.Data
{
  public class Tp03Context : DbContext
  {
    public Tp03Context(DbContextOptions dbContextOptions): base(dbContextOptions)
    {

    }

    public virtual DbSet<Container> Containers { get; set; }
    public virtual DbSet<BillOfLading> BillsOfLading { get; set; }
  }
}
