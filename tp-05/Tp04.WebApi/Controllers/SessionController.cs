//implement a session controller using the entity "Usuario" and JWT

using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tp04.WebApi.Data;
using Tp04.WebApi.Entities;

namespace Tp04.WebApi.Controllers
{
  [Route("sessions")]
  [ApiController]
  public class SessionController : CustomController
  {
    public SessionController(Tp04Context tp04Context):base(tp04Context)
    {
    }

    [HttpPost]
    [AllowAnonymous]
    public IActionResult Post([FromBody] Usuario usuario)
    {
      if (usuario == null) return BadRequest();

      var usuarioExistente = this._tp04Context.Usuarios.FirstOrDefault(p => p.Nome == usuario.Nome && p.Status);

      if (usuarioExistente == null) return Unauthorized();

      if (usuarioExistente.Senha != usuario.Senha) return Unauthorized();

      var claims = new List<Claim>
      {
        new Claim(ClaimTypes.Name, usuarioExistente.Nome),
      };

      var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("uiui_sou_secreto_e_ninguem_sabe_o_que_eu_sou"));
      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

      var token = new JwtSecurityToken(
        issuer: "Tp04.WebApi",
        audience: "Tp04.WebApi",
        claims: claims,
        expires: System.DateTime.Now.AddMinutes(30),
        signingCredentials: creds
      );

      return Ok(new
      {
        token = new JwtSecurityTokenHandler().WriteToken(token)
      });
    }

    [Route("check")]
    public IActionResult Check()
    {
      var token = this.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

      if (string.IsNullOrEmpty(token)) return BadRequest();

      try
      {
        Usuario usuario = this.GetUsuarioLogado();

        if(usuario == null) return Unauthorized();

        return base.Ok(usuario);
      }
      catch (System.Exception ex)
      {
        return Unauthorized();
      }
    }
  }
}
