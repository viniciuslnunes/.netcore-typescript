using System;

namespace Tp03.Core.Entities
{
  public class Container : BaseEntity
  {
    public string Numero { get; set; }
    public string Tipo { get; set; }
    public decimal Tamanho { get; set; }

    public Guid BillOfLandingId { get; set; }
    public BillOfLading BillOfLading { get; set; }
  }
}
