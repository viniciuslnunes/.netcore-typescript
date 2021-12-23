//implement a complete CRUD controller for entity Usuario
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Tp04.WebApi.Data;
using Tp04.WebApi.Entities;

namespace Tp04.WebApi.Controllers
{
  [ApiController]
  [Route("/usuarios")]
  public class UsuarioController : CustomController
  {
    public UsuarioController(Tp04Context tp04Context):base(tp04Context)
    {
    }

    [HttpGet]
    public ActionResult<IEnumerable<Usuario>> Get()
    {
      return this._tp04Context.Usuarios.Where(p =>p.Status).ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<Usuario> Get(int id)
    {
      return this._tp04Context.Usuarios.FirstOrDefault(p => p.Id == id && p.Status);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Usuario user)
    {
      if(this._tp04Context.Usuarios.FirstOrDefault(p => p.Nome == user.Nome && p.Status) != null)
        return BadRequest("Usuário já cadastrado");

      this._tp04Context.Usuarios.Add(user);
      this._tp04Context.SaveChanges();

      return Ok(user);
    }

    [HttpPut]
    public IActionResult Put([FromBody] Usuario user)
    {
      this._tp04Context.Usuarios.Update(user);
      this._tp04Context.SaveChanges();

      return Ok(user);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      this._tp04Context.Usuarios.Remove(this._tp04Context.Usuarios.Find(id));
      this._tp04Context.SaveChanges();

      return Ok();
    }
  }
}
