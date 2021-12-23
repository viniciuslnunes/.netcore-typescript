using System.ComponentModel.DataAnnotations;

namespace Tp04.WebApi.Entities
{
  public class Livro
  {
    [Key]
    public int? Id { get; set; }
    public string Titulo { get; set; }
    public string Autor { get; set; }
  }
}
