using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TP02.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public String Nome { get; set; }
        public float Preco { get; set; }
        public CategoriaDoProduto Categoria { get; set; }
        public int? CategoriaId { get; set; }
        public String Descricao { get; set; }
        public int Quantidade { get; set; }
    }
}
