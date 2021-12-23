using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Tp04.WebApi.Data;
using Tp04.WebApi.Entities;

namespace Tp04.WebApi.Controllers
{
  public class CustomController : ControllerBase
  {
    protected readonly Tp04Context _tp04Context;

    public CustomController(Tp04Context tp04Context)
    {
      _tp04Context = tp04Context;
    }

    protected Usuario GetUsuarioLogado()
    {
      var token = this.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

      var handler = new JwtSecurityTokenHandler();
      var tokenValido = handler.ReadJwtToken(token);

      if (tokenValido.ValidTo < System.DateTime.Now) return null;

      var login = tokenValido.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;

      Usuario usuario = this._tp04Context.Usuarios.FirstOrDefault(p => p.Nome == login && p.Status);

      return usuario;
    }
  }
}
