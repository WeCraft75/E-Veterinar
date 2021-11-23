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
            ViewData["IdStranka"] = new SelectList(_context.Strankas, "IdStranka", "IdStranka");
            ViewData["IdVeterinar"] = new SelectList(_context.Veterinars, "IdVeterinar", "IdVeterinar");
            return View();
        }

        // POST: Termin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdVeterinar,DatumZacetka,DatumKonca,IdStranka,JeZaseden,JePotrjen")] Termin termin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(termin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdStranka"] = new SelectList(_context.Strankas, "IdStranka", "IdStranka", termin.IdStranka);
            ViewData["IdVeterinar"] = new SelectList(_context.Veterinars, "IdVeterinar", "IdVeterinar", termin.IdVeterinar);
            return View(termin);
        }

        // GET: Termin/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var termin = await _context.Termins.FindAsync(id);
            if (termin == null)
            {
                return NotFound();
            }
            ViewData["IdStranka"] = new SelectList(_context.Strankas, "IdStranka", "IdStranka", termin.IdStranka);
            ViewData["IdVeterinar"] = new SelectList(_context.Veterinars, "IdVeterinar", "IdVeterinar", termin.IdVeterinar);
            return View(termin);
        }

        // POST: Termin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("IdVeterinar,DatumZacetka,DatumKonca,IdStranka,JeZaseden,JePotrjen")] Termin termin)
        {
            if (id != termin.IdVeterinar)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(termin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TerminExists(termin.IdVeterinar))
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
            ViewData["IdStranka"] = new SelectList(_context.Strankas, "IdStranka", "IdStranka", termin.IdStranka);
            ViewData["IdVeterinar"] = new SelectList(_context.Veterinars, "IdVeterinar", "IdVeterinar", termin.IdVeterinar);
            return View(termin);
        }

        // GET: Termin/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
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

        // POST: Termin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var termin = await _context.Termins.FindAsync(id);
            _context.Termins.Remove(termin);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TerminExists(decimal id)
        {
            return _context.Termins.Any(e => e.IdVeterinar == id);
        }
    }
}
