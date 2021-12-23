using Tp03.Core.Data;

namespace Tp03.Core.Services.BillOfLadingServices
{
  public abstract class BaseBillOfLadingService
  {
    protected readonly Tp03Context _context;

    protected BaseBillOfLadingService(Tp03Context context)
    {
      this._context = context;
    }
  }
}
