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
    public class ProveedorController : Controller
    {
        private readonly SistemaComprasContext _context;

        public ProveedorController(SistemaComprasContext context)
        {
            _context = context;
        }

        // GET: Proveedor
        public async Task<IActionResult> Index()
        {
            return _context.Proveedores != null ?
                        View(await _context.Proveedores.ToListAsync()) :
                        Problem("Entity set 'SistemaComprasContext.Proveedores' is null.");
        }

        // GET: Proveedor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Proveedores == null)
            {
                return NotFound();
            }

            var proveedor = await _context.Proveedores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (proveedor == null)
            {
                return NotFound();
            }

            return View(proveedor);
        }

        // GET: Proveedor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Proveedor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CedulaORNC,NombreComercial,Estado")] Proveedor proveedor)
        {
            if (!esCedulaValida(proveedor.CedulaORNC) && !esUnRNCValido(proveedor.CedulaORNC))
            {
                ModelState.AddModelError("CedulaORNC", "La cédula o RNC es inválido.");
            }
            else if (!esCedulaValida(proveedor.CedulaORNC))
            {
                ModelState.AddModelError("CedulaORNC", "La cédula es inválida.");
            }
            else if (!esUnRNCValido(proveedor.CedulaORNC))
            {
                ModelState.AddModelError("CedulaORNC", "El RNC es inválido.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(proveedor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(proveedor);
        }

        // GET: Proveedor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Proveedores == null)
            {
                return NotFound();
            }

            var proveedor = await _context.Proveedores.FindAsync(id);
            if (proveedor == null)
            {
                return NotFound();
            }
            return View(proveedor);
        }

        // POST: Proveedor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CedulaORNC,NombreComercial,Estado")] Proveedor proveedor)
        {
            if (id != proveedor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proveedor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProveedorExists(proveedor.Id))
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
            return View(proveedor);
        }

        // GET: Proveedor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Proveedores == null)
            {
                return NotFound();
            }

            var proveedor = await _context.Proveedores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (proveedor == null)
            {
                return NotFound();
            }

            return View(proveedor);
        }

        // POST: Proveedor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Proveedores == null)
            {
                return Problem("Entity set 'SistemaComprasContext.Proveedores' is null.");
            }
            var proveedor = await _context.Proveedores.FindAsync(id);
            if (proveedor != null)
            {
                _context.Proveedores.Remove(proveedor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProveedorExists(int id)
        {
            return (_context.Proveedores?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // Métodos de validación
        public static bool esCedulaValida(string pCedula)
        {
            int vnTotal = 0;
            string vcCedula = pCedula.Replace("-", "");
            int pLongCed = vcCedula.Trim().Length;
            int[] digitoMult = new int[11] { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1 };

            if (pLongCed != 11) // Cambiar a "no es igual a 11"
                return false;

            for (int vDig = 1; vDig <= pLongCed; vDig++)
            {
                int vCalculo = Int32.Parse(vcCedula.Substring(vDig - 1, 1)) * digitoMult[vDig - 1];
                if (vCalculo < 10)
                    vnTotal += vCalculo;
                else
                    vnTotal += Int32.Parse(vCalculo.ToString().Substring(0, 1)) + Int32.Parse(vCalculo.ToString().Substring(1, 1));
            }
            return vnTotal % 10 == 0;
        }

        private bool esUnRNCValido(string pRNC)
        {
            int vnTotal = 0;
            int[] digitoMult = new int[8] { 7, 9, 8, 6, 5, 4, 3, 2 };
            string vcRNC = pRNC.Replace("-", "").Replace(" ", "");

            if (vcRNC.Length != 9)
                return false;

            string vDigito = vcRNC.Substring(8, 1);

            if (!"145".Contains(vcRNC.Substring(0, 1)))
                return false;

            for (int vDig = 1; vDig <= 8; vDig++)
            {
                int vCalculo = Int32.Parse(vcRNC.Substring(vDig - 1, 1)) * digitoMult[vDig - 1];
                vnTotal += vCalculo;
            }

            return (vnTotal % 11 == 0 && vDigito == "1") ||
                   (vnTotal % 11 == 1 && vDigito == "1") ||
                   ((11 - (vnTotal % 11)).Equals(vDigito));
        }
    }
}
