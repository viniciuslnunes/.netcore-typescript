using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tp03.Core.Data;
using Tp03.Core.Entities;

namespace Tp03.Core.Services.ContainerServices
{
  public class UpdateContainerService : BaseContainerService
  {
    public UpdateContainerService(Tp03Context tp03Context) : base(tp03Context)
    {
    }

    public async Task Execute(Container container)
    {
      var foundContainer = await this._context.Containers
        .AsNoTracking()
        .FirstOrDefaultAsync(p => p.Id.Equals(container.Id));

      if (foundContainer == null)
        throw new ArgumentException("Container not found");

      this._context.Entry<Container>(container).State = EntityState.Modified;

      await this._context.SaveChangesAsync();
    }
  }
}
