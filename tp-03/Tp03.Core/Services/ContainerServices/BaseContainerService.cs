using Tp03.Core.Data;

namespace Tp03.Core.Services.ContainerServices
{
  public abstract class BaseContainerService
  {
    protected readonly Tp03Context _context;

    protected BaseContainerService(Tp03Context context)
    {
      this._context = context;
    }
  }
}
