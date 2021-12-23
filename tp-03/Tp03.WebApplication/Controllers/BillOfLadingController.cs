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

namespace Tp03.WebApplication.Controllers
{
  public class BillOfLadingController : Controller
  {
    private readonly AddBillOfLadingService _addBillOfLadingService;
    private readonly ListBillOfLadingService _listBillOfLadingService;
    private readonly UpdateBillOfLadingService _updateBillOfLadingService;
    private readonly DeleteBillOfLadingService _deleteBillOfLadingService;

    public BillOfLadingController(
      AddBillOfLadingService addBillOfLadingService,
      ListBillOfLadingService listBillOfLadingService,
      UpdateBillOfLadingService updateBillOfLadingService,
      DeleteBillOfLadingService deleteBillOfLadingService)
    {
      _addBillOfLadingService = addBillOfLadingService;
      _listBillOfLadingService = listBillOfLadingService;
      _updateBillOfLadingService = updateBillOfLadingService;
      _deleteBillOfLadingService = deleteBillOfLadingService;
    }

    // GET: BillOfLading
    public async Task<IActionResult> Index()
    {
      return View(await this._listBillOfLadingService.Execute());
    }

    // GET: BillOfLading/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var billsOfLading = await this._listBillOfLadingService.Execute(new BillOfLading
      {
        Id = id.Value
      });

      if (!billsOfLading.Any())
      {
        return NotFound();
      }

      return View(billsOfLading.First());
    }

    // GET: BillOfLading/Create
    public IActionResult Create()
    {
      return View();
    }

    // POST: BillOfLading/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BillOfLading billOfLading)
    {
      if (ModelState.IsValid)
      {
        await this._addBillOfLadingService.Execute(billOfLading);

        return RedirectToAction(nameof(Index));
      }

      return View(billOfLading);
    }

    // GET: BillOfLading/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var billsOfLading = await this._listBillOfLadingService.Execute(new BillOfLading
      {
        Id = id.Value
      });

      if (!billsOfLading.Any())
      {
        return NotFound();
      }

      return View(billsOfLading.First());
    }

    // POST: BillOfLading/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, BillOfLading billOfLading)
    {
      if (id != billOfLading.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        await this._updateBillOfLadingService.Execute(billOfLading);

        return RedirectToAction(nameof(Index));
      }
      return View(billOfLading);
    }

    // GET: BillOfLading/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var billsOfLading = await this._listBillOfLadingService.Execute(new BillOfLading
      {
        Id = id.Value
      });

      if (!billsOfLading.Any())
      {
        return NotFound();
      }

      return View(billsOfLading.First());
    }

    // POST: BillOfLading/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
      await this._deleteBillOfLadingService.Execute(new BillOfLading
      {
        Id = id
      });

      return RedirectToAction(nameof(Index));
    }
  }
}
