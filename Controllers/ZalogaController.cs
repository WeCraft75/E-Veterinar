using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_Veterinar.Data;
using E_Veterinar.Models;
using Microsoft.AspNetCore.Authorization;

namespace E_Veterinar.Controllers
{
    [Authorize]
    public class ZalogaController : Controller
    {
        private readonly eveterinarContext _context;

        public ZalogaController(eveterinarContext context)
        {
            _context = context;
        }

        // GET: Zaloga
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var eveterinarContext = _context.Zalogas.Include(z => z.IdIzdelekNavigation).Include(z => z.IdVeterinarNavigation).AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                eveterinarContext = eveterinarContext.Where(z => z.IdIzdelekNavigation.Ime.Contains(searchString));
            }

            return View(await eveterinarContext.ToListAsync());
        }

        // GET: Zaloga/Details/5
        public async Task<IActionResult> Details(decimal IdIzdelek, decimal IdVeterinar)
        {
            if (IdIzdelek == null || IdVeterinar == null)
            {
                return NotFound();
            }

            var zaloga = await _context.Zalogas
                .Include(z => z.IdIzdelekNavigation)
                .Include(z => z.IdVeterinarNavigation)
                .FirstOrDefaultAsync(m => m.IdIzdelek == IdIzdelek);
            if (zaloga == null)
            {
                return NotFound();
            }

            return View(zaloga);
        }

        // GET: Zaloga/Create
        public IActionResult Create()
        {
            ViewData["IdIzdelek"] = new SelectList(_context.Izdeleks, "IdIzdelek", "Ime");
            ViewData["IdVeterinar"] = new SelectList(_context.Veterinars, "IdVeterinar", "FullName");
            return View();
        }

        // POST: Zaloga/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdIzdelek,IdVeterinar,Kolicina")] Zaloga zaloga)
        {
            ModelState.Remove("IdIzdelekNavigation");
            ModelState.Remove("IdVeterinarNavigation");
            if (ModelState.IsValid)
            {
                zaloga.IdIzdelekNavigation = _context.Izdeleks.Find(zaloga.IdIzdelek);
                zaloga.IdVeterinarNavigation = _context.Veterinars.Find(zaloga.IdVeterinar);
                _context.Add(zaloga);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdIzdelek"] = new SelectList(_context.Izdeleks, "IdIzdelek", "IdIzdelek", zaloga.IdIzdelek);
            ViewData["IdVeterinar"] = new SelectList(_context.Veterinars, "IdVeterinar", "IdVeterinar", zaloga.IdVeterinar);
            return View(zaloga);
        }

        // GET: Zaloga/Edit/5
        public async Task<IActionResult> Edit(decimal IdIzdelek, decimal IdVeterinar)
        {
            if (IdIzdelek == null || IdVeterinar == null)
            {
                return NotFound();
            }

            var zaloga = await _context.Zalogas.FindAsync(IdIzdelek, IdVeterinar);
            if (zaloga == null)
            {
                return NotFound();
            }
            ViewData["IdIzdelek"] = new SelectList(_context.Izdeleks, "IdIzdelek", "IdIzdelek", zaloga.IdIzdelek);
            ViewData["IdVeterinar"] = new SelectList(_context.Veterinars, "IdVeterinar", "IdVeterinar", zaloga.IdVeterinar);
            return View(zaloga);
        }

        // POST: Zaloga/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal IdIzdelek, decimal IdVeterinar, [Bind("IdIzdelek,IdVeterinar,Kolicina")] Zaloga zaloga)
        {
            if (IdIzdelek != zaloga.IdIzdelek && IdVeterinar != zaloga.IdVeterinar)
            {
                return NotFound();
            }

            ModelState.Remove("IdIzdelekNavigation");
            ModelState.Remove("IdVeterinarNavigation");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zaloga);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZalogaExists(zaloga.IdIzdelek, zaloga.IdVeterinar))
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
            ViewData["IdIzdelek"] = new SelectList(_context.Izdeleks, "IdIzdelek", "IdIzdelek", zaloga.IdIzdelek);
            ViewData["IdVeterinar"] = new SelectList(_context.Veterinars, "IdVeterinar", "IdVeterinar", zaloga.IdVeterinar);
            return View(zaloga);
        }

        // GET: Zaloga/Delete/5
        public async Task<IActionResult> Delete(decimal IdIzdelek, decimal IdVeterinar)
        {
            if (IdIzdelek == null || IdVeterinar == null)
            {
                return NotFound();
            }

            var zaloga = await _context.Zalogas
                .Include(z => z.IdIzdelekNavigation)
                .Include(z => z.IdVeterinarNavigation)
                .FirstOrDefaultAsync(m => m.IdIzdelek == IdIzdelek);
            if (zaloga == null)
            {
                return NotFound();
            }

            return View(zaloga);
        }

        // POST: Zaloga/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal IdIzdelek, decimal IdVeterinar)
        {
            var zaloga = await _context.Zalogas.FindAsync(IdIzdelek, IdVeterinar);
            _context.Zalogas.Remove(zaloga);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZalogaExists(decimal IdIzdelek, decimal IdVeterinar)
        {
            return _context.Zalogas.Any(e => e.IdIzdelek == IdIzdelek && e.IdVeterinar == IdVeterinar);
        }
    }
}
