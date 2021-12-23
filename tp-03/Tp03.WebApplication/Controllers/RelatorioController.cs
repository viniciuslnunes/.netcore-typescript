using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tp03.Core.Data;
using Tp03.Core.Entities;
using Tp03.Core.Services.BillOfLadingServices;
using Tp03.Core.Services.ContainerServices;

namespace Tp03.WebApplication.Controllers
{
  public class RelatorioController : Controller
  {
    private readonly ListContainerService _listContainerService;

    public RelatorioController(
      ListContainerService listContainerService
    )
    {
      this._listContainerService = listContainerService;
    }

    // GET: Container
    public async Task<IActionResult> Index()
    {
      var containers = await this._listContainerService.Execute();

      containers = containers
        .Where(p => p.BillOfLading != null)
        .OrderBy(p => p.BillOfLading.Numero)
        .ToList();

      return base.View(containers);
    }
  }
}
