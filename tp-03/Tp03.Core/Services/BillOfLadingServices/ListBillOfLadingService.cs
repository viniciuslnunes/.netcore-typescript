using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tp03.Core.Data;
using Tp03.Core.Entities;

namespace Tp03.Core.Services.BillOfLadingServices
{
  public class ListBillOfLadingService : BaseBillOfLadingService
  {
    public ListBillOfLadingService(Tp03Context tp03Context) : base(tp03Context)
    {
    }

    public async Task<IList<BillOfLading>> Execute(BillOfLading billOfLadingFilter = null)
    {
      var billsOfLading = _context.BillsOfLading.AsNoTracking();

      if (billOfLadingFilter == null)
        return await billsOfLading.ToListAsync();

      if (billOfLadingFilter.Id != Guid.Empty)
        billsOfLading = billsOfLading.Where(c => c.Id == billOfLadingFilter.Id);

      if (billOfLadingFilter.Navio != null)
      {
        var tipo = billOfLadingFilter.Navio.ToLower();

        billsOfLading = billsOfLading.Where(c => c.Navio.ToLower() == tipo);
      }

      if (billOfLadingFilter.Numero != null)
        billsOfLading = billsOfLading.Where(c => c.Numero == billOfLadingFilter.Numero);

      return await billsOfLading.ToArrayAsync();
    }
  }
}
