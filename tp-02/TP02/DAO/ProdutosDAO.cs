using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP02.Models;

namespace TP02.DAO
{
    public class ProdutosDAO
    {
        private readonly EstoqueContext _estoqueContext;

        public ProdutosDAO(EstoqueContext estoqueContext)
        {
            this._estoqueContext = estoqueContext;
        }

        internal IList<Produto> Listar()
        {
            return this._estoqueContext.Produtos;
        }

        internal Produto ListarPorId(int id)
        {
            return this._estoqueContext.Produtos
                .FirstOrDefault(p => p.Id == id);
        }
    }
}
