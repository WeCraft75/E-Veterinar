using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_Veterinar.Data;
using E_Veterinar.Models;

namespace E_Veterinar.Controllers
{
    public class TerminController : Controller
    {
        private readonly eveterinarContext _context;

        public TerminController(eveterinarContext context)
        {
            _context = context;
        }

        // GET: Termin
        public async Task<IActionResult> Index()
        {
            var eveterinarContext = _context.Termins.Include(t => t.IdStrankaNavigation).Include(t => t.IdVeterinarNavigation);
            return View(await eveterinarContext.ToListAsync());
        }

        // GET: Termin/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var termin = await _context.Termins
                .Include(t => t.IdStrankaNavigation)
                .Include(t => t.IdVeterinarNavigation)
                .FirstOrDefaultAsync(m => m.IdVeterinar == id);
            if (termin == null)
            {
                return NotFound();
            }

            return View(termin);
        }

        // GET: Termin/Create
        public IActionResult Create()
        {
            ViewData["IdStranka"] = new SelectList(_context.Strankas, "IdStranka", "FullName");
            ViewData["IdVeterinar"] = new SelectList(_context.Veterinars, "IdVeterinar", "FullName");
            return View();
        }

        // POST: Termin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdVeterinar,DatumZacetka,DatumKonca,IdStranka,JeZaseden,JePotrjen")] Termin termin)
        {
            ModelState.Remove("IdStrankaNavigation");
            ModelState.Remove("IdVeterinarNavigation");

            if (ModelState.IsValid)
            {
                _context.Add(termin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdStranka"] = new SelectList(_context.Strankas, "IdStranka", "FullName", termin.IdStranka);
            ViewData["IdVeterinar"] = new SelectList(_context.Veterinars, "IdVeterinar", "FullName", termin.IdVeterinar);
            return View(termin);
        }

        // GET: Termin/Edit/5
        public async Task<IActionResult> Edit(decimal IdVeterinar, DateTime DatumZacetka, DateTime DatumKonca)
        {
            if (IdVeterinar == null || DatumZacetka == null || DatumKonca == null)
            {
                return NotFound();
            }

            var termin = await _context.Termins.FindAsync(IdVeterinar, DatumZacetka, DatumKonca);
            if (termin == null)
            {
                return NotFound();
            }
            ViewData["IdStranka"] = new SelectList(_context.Strankas, "IdStranka", "FullName", termin.IdStranka);
            ViewData["IdVeterinar"] = new SelectList(_context.Veterinars, "IdVeterinar", "IdVeterinar", termin.IdVeterinar);
            ViewData["DatumZacetka"] = new SelectList(_context.Termins, "DatumZacetka", "DatumZacetka", termin.DatumZacetka);
            ViewData["DatumKonca"] = new SelectList(_context.Termins, "DatumKonca", "DatumKonca", termin.DatumKonca);
            return View(termin);
        }

        // POST: Termin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DateTime DatumZacetka, DateTime DatumKonca, [Bind("IdVeterinar,DatumZacetka,DatumKonca,IdStranka,JeZaseden,JePotrjen")] Termin termin)
        {
            if (DatumZacetka != termin.DatumZacetka && DatumKonca != termin.DatumKonca)
            {
                return NotFound();
            }

            ModelState.Remove("IdStrankaNavigation");
            ModelState.Remove("IdVeterinarNavigation");

            var errors = ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .Select(x => new { x.Key, x.Value.Errors })
                .ToArray();
            Console.WriteLine(DatumZacetka);
            Console.WriteLine(DatumZacetka);
           
            if (ModelState.IsValid)
            {
                try
                {
                    termin.JePotrjen = true;
                    termin.IdStrankaNavigation = _context.Strankas.Find(termin.IdStranka);
                    termin.IdVeterinarNavigation = _context.Veterinars.Find(termin.IdVeterinar);
                    _context.Update(termin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TerminExists(termin.DatumZacetka, termin.DatumKonca))
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
            ViewData["IdStranka"] = new SelectList(_context.Strankas, "IdStranka", "FullName", termin.IdStranka);
            ViewData["IdVeterinar"] = new SelectList(_context.Veterinars, "IdVeterinar", "FullName", termin.IdVeterinar);
            return View(termin);
        }

        // GET: Termin/Delete/5
        public async Task<IActionResult> Delete(decimal IdVeterinar, DateTime DatumZacetka, DateTime DatumKonca)
        {
            if (DatumZacetka == null || DatumKonca == null)
            {
                return NotFound();
            }

            var termin = await _context.Termins
                .Include(t => t.IdStrankaNavigation)
                .Include(t => t.IdVeterinarNavigation)
                .FirstOrDefaultAsync(m => m.IdVeterinar == IdVeterinar && m.DatumZacetka == DatumZacetka && m.DatumKonca == DatumKonca);
            if (termin == null)
            {
                return NotFound();
            }

            return View(termin);
        }

        // POST: Termin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal IdVeterinar, DateTime DatumZacetka, DateTime DatumKonca)
        {
            Console.WriteLine(IdVeterinar);
            Console.WriteLine(DatumZacetka);
            Console.WriteLine(DatumKonca);

            var termin = await _context.Termins.FindAsync(IdVeterinar, DatumZacetka, DatumKonca);
            _context.Termins.Remove(termin);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TerminExists(DateTime DatumZacetka, DateTime DatumKonca)
        {
            return _context.Termins.Any(e => e.DatumZacetka == DatumZacetka && e.DatumKonca == DatumKonca);
        }
    }
}
