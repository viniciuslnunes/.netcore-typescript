using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TP02.Models;

namespace TP02.DAO
{
    public class EstoqueContext
    {
        private const string _dbPath = "./db.json";
        private DbFormat _cachedDb;
        private bool _cachedDbIsValid;

        class DbFormat
        {
            public DbFormat()
            {
                this.Produtos = new List<Produto>();
                this.Usuarios = new List<Usuario>();
                this.Categorias = new List<CategoriaDoProduto>();
            }

            public IList<Produto> Produtos { get; set; }
            public IList<Usuario> Usuarios { get; set; }
            public IList<CategoriaDoProduto> Categorias { get; set; }
        }

        private DbFormat Database { get => this.ReadDb(); }

        public void SaveDb()
        {
            CheckAndCreateDb();

            var snapshot = JsonConvert.SerializeObject(this._cachedDb ?? new DbFormat());

            File.WriteAllText(_dbPath, snapshot);
        }

        private static void CheckAndCreateDb()
        {
            var dbExists = File.Exists(_dbPath);


            if (!dbExists)
                using (var stream = new StreamWriter(_dbPath))
                {
                    var initialData = new DbFormat();

                    stream.Write(JsonConvert.SerializeObject(initialData));
                }
        }

        private DbFormat ReadDb()
        {
            if (_cachedDbIsValid)
                return _cachedDb;

            CheckAndCreateDb();

            var rawJson = File.ReadAllText(_dbPath);

            var serializedDb = JsonConvert.DeserializeObject<DbFormat>(rawJson);

            _cachedDb = serializedDb;
            _cachedDbIsValid = true;

            return _cachedDb;
        }

        #region Produtos
        public IList<Produto> Produtos
        {
            get
            {
                var produtos = this.Database.Produtos;
                var concurrentProdutoBag = new ConcurrentBag<Produto>();

                Parallel.ForEach(produtos, produto =>
                {
                    var categoria = this.Database.Categorias
                        .FirstOrDefault(p => p.Id == produto.CategoriaId);

                    concurrentProdutoBag.Add(new Produto()
                    {
                        Id = produto.Id,
                        Nome = produto.Nome,
                        Descricao = produto.Descricao,
                        Preco = produto.Preco,
                        Quantidade = produto.Quantidade,
                        CategoriaId = produto.CategoriaId,
                        Categoria = categoria
                    });
                });

                return concurrentProdutoBag.ToList();
            }
        }

        public EstoqueContext AddEntity(Produto produto)
        {
            this.Database.Produtos.Add(produto);

            this._cachedDbIsValid = false;

            return this;
        }

        public EstoqueContext UpdateEntity(Produto produto)
        {
            var produtoSalvo = this.Database.Produtos
                .First(p => p.Id == produto.Id);

            produtoSalvo = produto;

            this._cachedDbIsValid = false;

            return this;
        }
        
        #endregion

        #region Categoria
        public IList<CategoriaDoProduto> CategoriaDoProdutos
        {
            get
            {
                var categorias = this.Database.Categorias;

                return categorias;
            }
        }

        public EstoqueContext AddEntity(CategoriaDoProduto categoriaDoProduto)
        {
            this.Database.Categorias.Add(categoriaDoProduto);

            this._cachedDbIsValid = false;

            return this;
        }

        public EstoqueContext UpdateEntity(CategoriaDoProduto categoria)
        {
            var categoriaSalva = this.Database.Categorias
                .First(p => p.Id == categoria.Id);

            categoriaSalva = categoria;

            this._cachedDbIsValid = false;

            return this;
        }

        #endregion

        #region Usuarios
        public IList<Usuario> Usuarios
        {
            get
            {
                var usuarios = this.Database.Usuarios;

                return usuarios;
            }
        }

        public EstoqueContext AddEntity(Usuario usuario)
        {
            this.Database.Usuarios.Add(usuario);

            this._cachedDbIsValid = false;

            return this;
        }

        public EstoqueContext UpdateEntity(Usuario usuario)
        {
            var usuarioSalvo = this.Database.Usuarios
                .First(p => p.Id == usuario.Id);

            usuarioSalvo = usuario;

            this._cachedDbIsValid = false;

            return this;
        }
        #endregion
    }
}
