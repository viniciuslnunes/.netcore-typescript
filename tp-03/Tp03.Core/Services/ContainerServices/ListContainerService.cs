using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tp03.Core.Data;
using Tp03.Core.Entities;

namespace Tp03.Core.Services.ContainerServices
{
  public class ListContainerService : BaseContainerService
  {
    public ListContainerService(Tp03Context tp03Context) : base(tp03Context)
    {
    }

    public async Task<IList<Container>> Execute(Container containerFilter = null)
    {
      var containers = _context.Containers
        .AsNoTracking();

      if (containerFilter == null)
      {
        return await PopulateBillOfLading(containers);
      };


      if (containerFilter.Id != Guid.Empty)
        containers = containers.Where(c => c.Id == containerFilter.Id);

      if (containerFilter.Tipo != null)
      {
        var tipo = containerFilter.Tipo.ToLower();

        containers = containers.Where(c => c.Tipo.ToLower() == tipo);
      }

      if (containerFilter.Tamanho != default(decimal))
        containers = containers.Where(c => c.Tamanho == containerFilter.Tamanho);

      return await PopulateBillOfLading(containers);
    }

    private async Task<IList<Container>> PopulateBillOfLading(IQueryable<Container> containers)
    {
      var fetchedContainers = await containers.ToListAsync();

      fetchedContainers.ForEach(async container =>
      {
        container.BillOfLading = await this._context.BillsOfLading
          .FirstOrDefaultAsync(billOfLading =>
            billOfLading.Id.Equals(container.BillOfLandingId)
          );
      });

      return fetchedContainers;
    }
  }
}
