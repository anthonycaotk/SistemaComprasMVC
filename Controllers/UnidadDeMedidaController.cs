using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaComprasMVC.Data;
using SistemaComprasMVC.Models;

namespace SistemaComprasMVC.Controllers
{
    public class UnidadDeMedidaController : Controller
    {
        private readonly SistemaComprasContext _context;

        public UnidadDeMedidaController(SistemaComprasContext context)
        {
            _context = context;
        }

        // GET: UnidadDeMedida
        public async Task<IActionResult> Index()
        {
              return _context.UnidadesDeMedida != null ? 
                          View(await _context.UnidadesDeMedida.ToListAsync()) :
                          Problem("Entity set 'SistemaComprasContext.UnidadesDeMedida'  is null.");
        }

        // GET: UnidadDeMedida/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UnidadesDeMedida == null)
            {
                return NotFound();
            }

            var unidadDeMedida = await _context.UnidadesDeMedida
                .FirstOrDefaultAsync(m => m.Id == id);
            if (unidadDeMedida == null)
            {
                return NotFound();
            }

            return View(unidadDeMedida);
        }

        // GET: UnidadDeMedida/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UnidadDeMedida/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,Estado")] UnidadDeMedida unidadDeMedida)
        {
            if (ModelState.IsValid)
            {
                _context.Add(unidadDeMedida);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(unidadDeMedida);
        }

        // GET: UnidadDeMedida/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UnidadesDeMedida == null)
            {
                return NotFound();
            }

            var unidadDeMedida = await _context.UnidadesDeMedida.FindAsync(id);
            if (unidadDeMedida == null)
            {
                return NotFound();
            }
            return View(unidadDeMedida);
        }

        // POST: UnidadDeMedida/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,Estado")] UnidadDeMedida unidadDeMedida)
        {
            if (id != unidadDeMedida.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(unidadDeMedida);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UnidadDeMedidaExists(unidadDeMedida.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(unidadDeMedida);
        }

        // GET: UnidadDeMedida/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UnidadesDeMedida == null)
            {
                return NotFound();
            }

            var unidadDeMedida = await _context.UnidadesDeMedida
                .FirstOrDefaultAsync(m => m.Id == id);
            if (unidadDeMedida == null)
            {
                return NotFound();
            }

            return View(unidadDeMedida);
        }

        // POST: UnidadDeMedida/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UnidadesDeMedida == null)
            {
                return Problem("Entity set 'SistemaComprasContext.UnidadesDeMedida'  is null.");
            }
            var unidadDeMedida = await _context.UnidadesDeMedida.FindAsync(id);
            if (unidadDeMedida != null)
            {
                _context.UnidadesDeMedida.Remove(unidadDeMedida);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UnidadDeMedidaExists(int id)
        {
          return (_context.UnidadesDeMedida?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
