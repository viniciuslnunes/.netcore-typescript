using System;
using System.Threading.Tasks;
using Tp03.Core.Data;
using Tp03.Core.Entities;

namespace Tp03.Core.Services.ContainerServices
{
  public class DeleteContainerService : BaseContainerService
  {
    public DeleteContainerService(Tp03Context tp03Context) : base(tp03Context)
    {
    }

    public async Task Execute(Container container)
    {
      var foundContainer = await this._context.Containers.FindAsync(container.Id);

      if(foundContainer == null)
        throw new ArgumentException("Container not found");

      this._context.Containers.Remove(foundContainer);
      await this._context.SaveChangesAsync();
    }
  }
}
