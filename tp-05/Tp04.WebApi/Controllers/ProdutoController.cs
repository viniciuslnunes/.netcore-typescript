using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tp04.WebApi.Data;
using Tp04.WebApi.Entities;

namespace Tp04.WebApi.Controllers
{
  [ApiController]
  [Route("/produtos")]
  public class ProdutoController : CustomController
  {
    public ProdutoController(Tp04Context tp04Context):base(tp04Context)
    {
    }

    [HttpGet]
    public ActionResult<IEnumerable<Produto>> Get()
    {
      return this._tp04Context.Produtos.ToList().Select(x =>
      {
        x.UsuarioCadastro = this._tp04Context.Usuarios.Find(x.IdUsuarioCadastro);
        x.UsuarioUpdate = this._tp04Context.Usuarios.Find(x.IdUsuarioUpdate);

        return x;
      }).ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<Produto> Get(int id)
    {
      Produto produto = this._tp04Context.Produtos.Find(id);

      produto.UsuarioCadastro = this._tp04Context.Usuarios.Find(produto.IdUsuarioCadastro);
      produto.UsuarioUpdate = this._tp04Context.Usuarios.Find(produto.IdUsuarioUpdate);

      return produto;
    }

    [HttpPost]
    public IActionResult Post([FromBody] Produto user)
    {
      var usuarioLogado = this.GetUsuarioLogado();

      user.IdUsuarioCadastro = usuarioLogado.Id;
      user.IdUsuarioUpdate = usuarioLogado.Id;

      this._tp04Context.Produtos.Add(user);
      this._tp04Context.SaveChanges();

      return Ok(user);
    }

    [HttpPut]
    public IActionResult Put([FromBody] Produto user)
    {
      var produtoSalvo = this._tp04Context.Produtos.AsNoTracking().First(p => p.Id == user.Id);
      var usuarioLogado = this.GetUsuarioLogado();

      user.IdUsuarioCadastro = produtoSalvo.IdUsuarioCadastro;
      user.IdUsuarioUpdate = usuarioLogado.Id;

      this._tp04Context.Produtos.Update(user);
      this._tp04Context.SaveChanges();

      return Ok(user);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      this._tp04Context.Produtos.Remove(this._tp04Context.Produtos.Find(id));
      this._tp04Context.SaveChanges();

      return Ok();
    }
  }
}
