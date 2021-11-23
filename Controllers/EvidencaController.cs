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
    public class EvidencaController : Controller
    {
        private readonly eveterinarContext _context;

        public EvidencaController(eveterinarContext context)
        {
            _context = context;
        }

        // GET: Evidenca
        public async Task<IActionResult> Index()
        {
            var eveterinarContext = _context.Evidencas.Include(e => e.IdNarociloNavigation).Include(e => e.Termin);
            return View(await eveterinarContext.ToListAsync());
        }

        // GET: Evidenca/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evidenca = await _context.Evidencas
                .Include(e => e.IdNarociloNavigation)
                .Include(e => e.Termin)
                .FirstOrDefaultAsync(m => m.IdEvidence == id);
            if (evidenca == null)
            {
                return NotFound();
            }

            return View(evidenca);
        }

        // GET: Evidenca/Create
        public IActionResult Create()
        {
            ViewData["IdNarocilo"] = new SelectList(_context.Narocilos, "IdNarocilo", "IdNarocilo");
            ViewData["IdVeterinar"] = new SelectList(_context.Termins, "IdVeterinar", "IdVeterinar");
            return View();
        }

        // POST: Evidenca/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEvidence,IdVeterinar,DatumZacetka,DatumKonca,IdNarocilo,Cena")] Evidenca evidenca)
        {
            if (ModelState.IsValid)
            {
                _context.Add(evidenca);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdNarocilo"] = new SelectList(_context.Narocilos, "IdNarocilo", "IdNarocilo", evidenca.IdNarocilo);
            ViewData["IdVeterinar"] = new SelectList(_context.Termins, "IdVeterinar", "IdVeterinar", evidenca.IdVeterinar);
            return View(evidenca);
        }

        // GET: Evidenca/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evidenca = await _context.Evidencas.FindAsync(id);
            if (evidenca == null)
            {
                return NotFound();
            }
            ViewData["IdNarocilo"] = new SelectList(_context.Narocilos, "IdNarocilo", "IdNarocilo", evidenca.IdNarocilo);
            ViewData["IdVeterinar"] = new SelectList(_context.Termins, "IdVeterinar", "IdVeterinar", evidenca.IdVeterinar);
            return View(evidenca);
        }

        // POST: Evidenca/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("IdEvidence,IdVeterinar,DatumZacetka,DatumKonca,IdNarocilo,Cena")] Evidenca evidenca)
        {
            if (id != evidenca.IdEvidence)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(evidenca);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EvidencaExists(evidenca.IdEvidence))
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
            ViewData["IdNarocilo"] = new SelectList(_context.Narocilos, "IdNarocilo", "IdNarocilo", evidenca.IdNarocilo);
            ViewData["IdVeterinar"] = new SelectList(_context.Termins, "IdVeterinar", "IdVeterinar", evidenca.IdVeterinar);
            return View(evidenca);
        }

        // GET: Evidenca/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evidenca = await _context.Evidencas
                .Include(e => e.IdNarociloNavigation)
                .Include(e => e.Termin)
                .FirstOrDefaultAsync(m => m.IdEvidence == id);
            if (evidenca == null)
            {
                return NotFound();
            }

            return View(evidenca);
        }

        // POST: Evidenca/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var evidenca = await _context.Evidencas.FindAsync(id);
            _context.Evidencas.Remove(evidenca);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EvidencaExists(decimal id)
        {
            return _context.Evidencas.Any(e => e.IdEvidence == id);
        }
    }
}
