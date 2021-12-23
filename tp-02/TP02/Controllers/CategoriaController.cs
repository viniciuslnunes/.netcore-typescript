using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP02.DAO;
using TP02.Models;

namespace TP02.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly CategoriasDAO _categoriasDAO;

        public CategoriaController(CategoriasDAO categoriasDAO)
        {
            this._categoriasDAO = categoriasDAO;
        }

        [HttpGet]
        public ActionResult CriaOsMockProPai()
        {
            var random = new Random();

            for (int i = 0; i < 10; i++)
            {
                int id;

                do
                    id = random.Next();
                while (this._categoriasDAO.ListarPorId(id) != null);

                string idFormatado = id.ToString().PadLeft(3, '0');

                this._categoriasDAO.Adicionar(new CategoriaDoProduto()
                {
                    Id = id,
                    Nome = $"Categoria {idFormatado}",
                    Descricao = $"Descrição da categoria {idFormatado}"
                });
            }

            return Ok("Já é filhão, abre lá");
        }

        public IActionResult Index()
        {
            var categorias = this._categoriasDAO.Listar();

            ViewBag.Categorias = categorias;

            return View();
        }
    }
}
