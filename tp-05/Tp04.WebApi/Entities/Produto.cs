using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tp04.WebApi.Entities
{
  public class Produto
  {
    [Key]
    public int? Id { get; set; }
    public int? IdUsuarioUpdate { get; set; }
    [NotMapped]
    public Usuario UsuarioUpdate { get; set; }
    public int? IdUsuarioCadastro { get; set; }
    [NotMapped]
    public Usuario UsuarioCadastro { get; set; }
    public string Nome { get; set; }
    public decimal Preco { get; set; }
    public bool Status { get; set; }
  }
}
