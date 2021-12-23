using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP02.Models;

namespace TP02.DAO
{
    public class CategoriasDAO
    {
        private readonly EstoqueContext _estoqueContext;

        public CategoriasDAO(EstoqueContext estoqueContext)
        {
            this._estoqueContext = estoqueContext;
        }

        internal void Adicionar(CategoriaDoProduto categoriaDoProduto)
        {
            this._estoqueContext
                .AddEntity(categoriaDoProduto)
                .SaveDb();
        }

        internal void Atualizar(CategoriaDoProduto categoriaDoProduto)
        {
            this._estoqueContext
                .UpdateEntity(categoriaDoProduto)
                .SaveDb();
        }

        internal IList<CategoriaDoProduto> Listar()
        {
            return this._estoqueContext.CategoriaDoProdutos;
        }

        internal CategoriaDoProduto ListarPorId(int id)
        {
            return this._estoqueContext.CategoriaDoProdutos
                .FirstOrDefault(p => p.Id == id);
        }
    }
}
