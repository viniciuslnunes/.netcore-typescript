using System.ComponentModel.DataAnnotations;

namespace Tp04.WebApi.Entities
{
  public class Usuario
  {
    [Key]
    public int? Id { get; set; }
    public string Nome { get; set; }
    public string Senha { get; set; }
    public bool Status { get; set; }
  }
}
