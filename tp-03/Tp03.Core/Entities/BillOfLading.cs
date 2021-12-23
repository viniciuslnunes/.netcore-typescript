using System;
using System.Collections.Generic;

namespace Tp03.Core.Entities
{
  public class BillOfLading : BaseEntity
  {
    public string Numero { get; set; }
    public string Navio { get; set; }
    public string Consignee { get; set; }
    public IList<Container> Containers { get; set; } = new List<Container>();
  }
}
