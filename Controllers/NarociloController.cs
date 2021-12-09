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
using Microsoft.AspNetCore.Identity;

namespace E_Veterinar.Controllers
{
    [Authorize(Roles = "Administrator, User")]
    public class NarociloController : Controller
    {
        private readonly eveterinarContext _context;

        public NarociloController(eveterinarContext context)
        {
            _context = context;
        }

        // GET: Narocilo
        public async Task<IActionResult> Index()
        {
            var eveterinarContext = _context.Narocilos.Include(n => n.IdStrankaNavigation);
            return View(await eveterinarContext.ToListAsync());
        }

        // GET: Narocilo/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var narocilo = await _context.Narocilos
                .Include(n => n.IdStrankaNavigation)
                .FirstOrDefaultAsync(m => m.IdNarocilo == id);
            if (narocilo == null)
            {
                return NotFound();
            }

            return View(narocilo);
        }

        // GET: Narocilo/Create
        public IActionResult Create()
        {
            ViewData["IdStranka"] = new SelectList(_context.Strankas, "IdStranka", "IdStranka");
            return View();
        }

        // POST: Narocilo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdNarocilo,IdStranka,ZahtevanaKolicina,DatumNarocila")] Narocilo narocilo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(narocilo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdStranka"] = new SelectList(_context.Strankas, "IdStranka", "IdStranka", narocilo.IdStranka);
            return View(narocilo);
        }

        // GET: Narocilo/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var narocilo = await _context.Narocilos.FindAsync(id);
            if (narocilo == null)
            {
                return NotFound();
            }
            ViewData["IdStranka"] = new SelectList(_context.Strankas, "IdStranka", "IdStranka", narocilo.IdStranka);
            return View(narocilo);
        }

        // POST: Narocilo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("IdNarocilo,IdStranka,ZahtevanaKolicina,DatumNarocila")] Narocilo narocilo)
        {
            if (id != narocilo.IdNarocilo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(narocilo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NarociloExists(narocilo.IdNarocilo))
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
            ViewData["IdStranka"] = new SelectList(_context.Strankas, "IdStranka", "IdStranka", narocilo.IdStranka);
            return View(narocilo);
        }

        // GET: Narocilo/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var narocilo = await _context.Narocilos
                .Include(n => n.IdStrankaNavigation)
                .FirstOrDefaultAsync(m => m.IdNarocilo == id);
            if (narocilo == null)
            {
                return NotFound();
            }

            return View(narocilo);
        }

        // POST: Narocilo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var narocilo = await _context.Narocilos.FindAsync(id);
            _context.Narocilos.Remove(narocilo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NarociloExists(decimal id)
        {
            return _context.Narocilos.Any(e => e.IdNarocilo == id);
        }
    }
}
