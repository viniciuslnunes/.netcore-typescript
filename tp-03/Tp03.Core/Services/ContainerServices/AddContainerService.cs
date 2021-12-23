using System;
using System.Threading.Tasks;
using Tp03.Core.Data;
using Tp03.Core.Entities;

namespace Tp03.Core.Services.ContainerServices
{
  public class AddContainerService: BaseContainerService
  {
    public AddContainerService(Tp03Context context) : base(context)
    {
    }

    public async Task<Container> Execute(Container container)
    {
      container.Id = Guid.NewGuid();

      this._context.Containers.Add(container);
      await this._context.SaveChangesAsync();

      return container;
    }
  }
}
