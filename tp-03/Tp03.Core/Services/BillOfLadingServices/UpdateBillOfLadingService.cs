using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tp03.Core.Data;
using Tp03.Core.Entities;

namespace Tp03.Core.Services.BillOfLadingServices
{
  public class UpdateBillOfLadingService : BaseBillOfLadingService
  {
    public UpdateBillOfLadingService(Tp03Context tp03Context) : base(tp03Context)
    {
    }

    public async Task Execute(BillOfLading billOfLading)
    {
      var foundBillOfLading = await this._context.BillsOfLading
        .AsNoTracking()
        .FirstOrDefaultAsync(p => p.Id.Equals(billOfLading.Id));

      if (foundBillOfLading == null)
        throw new ArgumentException("Bill of Lading not found");

      this._context.Entry<BillOfLading>(billOfLading).State = EntityState.Modified;

      await this._context.SaveChangesAsync();
    }
  }
}
