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
  public class ContainerController : Controller
  {
    private readonly AddContainerService _addContainerService;
    private readonly ListContainerService _listContainerService;
    private readonly UpdateContainerService _updateContainerService;
    private readonly DeleteContainerService _deleteContainerService;
    private readonly ListBillOfLadingService _listBillOfLadingService;

    public ContainerController(
      AddContainerService addContainerService,
      ListContainerService listContainerService,
      UpdateContainerService updateContainerService,
      DeleteContainerService deleteContainerService,
      ListBillOfLadingService listBillOfLadingService
    )
    {
      this._addContainerService = addContainerService;
      this._listContainerService = listContainerService;
      this._updateContainerService = updateContainerService;
      this._deleteContainerService = deleteContainerService;
      this._listBillOfLadingService = listBillOfLadingService;
    }

    // GET: Container
    public async Task<IActionResult> Index()
    {
      var containers = await this._listContainerService.Execute();

      return base.View(containers);
    }

    // GET: Container/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var containers = await this._listContainerService.Execute(new Container()
      {
        Id = id.Value
      });

      if (containers == null)
      {
        return NotFound();
      }

      return View(containers.First());
    }

    // GET: Container/Create
    public async Task<IActionResult> Create()
    {
      ViewBag.BillsOfLading = await this._listBillOfLadingService.Execute();

      return View();
    }

    // POST: Container/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Numero,Tipo,Tamanho,BillOfLandingId")] Container container)
    {
      if (ModelState.IsValid)
      {
        await this._addContainerService.Execute(container);

        return RedirectToAction(nameof(Index));
      }

      return View(container);
    }

    // GET: Container/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var containers = await this._listContainerService.Execute(new Container()
      {
        Id = id.Value
      });

      if (!containers.Any())
        return NotFound();

      ViewBag.BillsOfLading = await this._listBillOfLadingService.Execute();

      return View(containers.First());
    }

    // POST: Container/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Numero,Tipo,Tamanho,Id, BillOfLandingId")] Container container)
    {
      if (id != container.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        await this._updateContainerService.Execute(container);
        return RedirectToAction(nameof(Index));
      }

      return View(container);
    }

    // GET: Container/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var containers = await this._listContainerService.Execute(new Container()
      {
        Id = id.Value
      });

      if (!containers.Any())
      {
        return NotFound();
      }

      return View(containers.First());
    }

    // POST: Container/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
      await this._deleteContainerService.Execute(new Container()
      {
        Id = id
      });

      return RedirectToAction(nameof(Index));
    }
  }
}
