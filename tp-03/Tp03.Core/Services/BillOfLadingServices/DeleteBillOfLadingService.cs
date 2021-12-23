using System;
using System.Linq;
using System.Threading.Tasks;
using Tp03.Core.Data;
using Tp03.Core.Entities;

namespace Tp03.Core.Services.BillOfLadingServices
{
  public class DeleteBillOfLadingService : BaseBillOfLadingService
  {
    public DeleteBillOfLadingService(Tp03Context tp03Context) : base(tp03Context)
    {
    }

    public async Task Execute(BillOfLading billOfLading)
    {
      var foundBillOfLading = await this._context.BillsOfLading.FindAsync(billOfLading.Id);

      if (foundBillOfLading == null)
        throw new ArgumentException("Bill of Lading not found");

      var containers = this._context.Containers
        .Where(c => c.BillOfLandingId.Equals(billOfLading.Id));

      using (var transaction = await this._context.Database.BeginTransactionAsync())
      {
        foreach (var container in containers)
        {
          this._context.Containers.Remove(container);
        }

        this._context.BillsOfLading.Remove(foundBillOfLading);

        await this._context.SaveChangesAsync();

        transaction.Commit();
      }
    }
  }
}
