using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP02.DAO;
using TP02.Models;

namespace TP02.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ProdutosDAO _produtosDAO;

        public ProdutoController(ProdutosDAO produtosDAO)
        {
            this._produtosDAO = produtosDAO;
        }

        public IActionResult Index()
        {
            IList<Produto> produtos = _produtosDAO.Listar();

            ViewBag.Produtos = produtos;

            return View();
        }
    }
}
