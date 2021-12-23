using System;
using System.Threading.Tasks;
using Tp03.Core.Data;
using Tp03.Core.Entities;

namespace Tp03.Core.Services.BillOfLadingServices
{
  public class AddBillOfLadingService : BaseBillOfLadingService
  {
    public AddBillOfLadingService(Tp03Context context) : base(context)
    {
    }

    public async Task<BillOfLading> Execute(BillOfLading billOfLading)
    {
      billOfLading.Id = Guid.NewGuid();

      this._context.BillsOfLading.Add(billOfLading);
      await this._context.SaveChangesAsync();

      return billOfLading;
    }
  }
}
