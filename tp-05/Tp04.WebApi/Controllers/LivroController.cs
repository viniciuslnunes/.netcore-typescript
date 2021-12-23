using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tp04.WebApi.Data;
using Tp04.WebApi.Entities;

namespace Tp04.WebApi.Controllers
{
  [ApiController]
  [Route("/livros")]
  public class LivroController : CustomController
  {
    public LivroController(Tp04Context tp04Context):base(tp04Context)
    {
    }

    [HttpGet]
    public async Task<ActionResult> Get()
    {
      var livros = await _tp04Context.Livros.ToListAsync();

      return Ok(livros);
    }

    [HttpGet]
    [Route("{livroId}")]
    public async Task<ActionResult> Get([FromRoute] int livroId)
    {
      var storedLivro = await _tp04Context.Livros.FindAsync(livroId);

      if (storedLivro == null)
        return NotFound();

      return Ok(storedLivro);
    }

    [HttpPost]
    public async Task<ActionResult> Post(
      [FromBody] Livro livro
    )
    {
      await _tp04Context.Livros.AddAsync(livro);
      await _tp04Context.SaveChangesAsync();

      return Ok(livro);
    }

    [HttpPut]
    public async Task<ActionResult> Put(
      [FromBody] Livro livro
    )
    {
      var storedLivro = await _tp04Context.Livros.FindAsync(livro.Id);

      if (storedLivro == null)
        return NotFound();

      storedLivro.Titulo = livro.Titulo;
      storedLivro.Autor = livro.Autor;

      _tp04Context.Entry<Livro>(storedLivro).State = EntityState.Modified;
      await _tp04Context.SaveChangesAsync();

      return Ok(storedLivro);
    }

    [HttpDelete]
    [Route("{livroId}")]
    public async Task<ActionResult> Delete([FromRoute] int livroId)
    {
      var storedLivro = await _tp04Context.Livros.FindAsync(livroId);

      if (storedLivro == null)
        return NotFound();

      _tp04Context.Livros.Remove(storedLivro);
      await _tp04Context.SaveChangesAsync();

      return NoContent();
    }
  }
}
