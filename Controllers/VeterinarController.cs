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
    public class VeterinarController : Controller
    {
        private readonly eveterinarContext _context;

        public VeterinarController(eveterinarContext context)
        {
            _context = context;
        }

        // GET: Veterinar
        public async Task<IActionResult> Index()
        {
            var eveterinarContext = _context.Veterinars.Include(v => v.StevilkaNavigation);
            return View(await eveterinarContext.ToListAsync());
        }

        // GET: Veterinar/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veterinar = await _context.Veterinars
                .Include(v => v.StevilkaNavigation)
                .FirstOrDefaultAsync(m => m.IdVeterinar == id);
            if (veterinar == null)
            {
                return NotFound();
            }

            return View(veterinar);
        }

        // GET: Veterinar/Create
        public IActionResult Create()
        {
            ViewData["Stevilka"] = new SelectList(_context.Posta, "Stevilka", "Stevilka");
            return View();
        }

        // POST: Veterinar/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdVeterinar,Stevilka,Ime,Priimek,Kraj,NaDom")] Veterinar veterinar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(veterinar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Stevilka"] = new SelectList(_context.Posta, "Stevilka", "Stevilka", veterinar.Stevilka);
            return View(veterinar);
        }

        // GET: Veterinar/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veterinar = await _context.Veterinars.FindAsync(id);
            if (veterinar == null)
            {
                return NotFound();
            }
            ViewData["Stevilka"] = new SelectList(_context.Posta, "Stevilka", "Stevilka", veterinar.Stevilka);
            return View(veterinar);
        }

        // POST: Veterinar/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("IdVeterinar,Stevilka,Ime,Priimek,Kraj,NaDom")] Veterinar veterinar)
        {
            if (id != veterinar.IdVeterinar)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(veterinar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VeterinarExists(veterinar.IdVeterinar))
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
            ViewData["Stevilka"] = new SelectList(_context.Posta, "Stevilka", "Stevilka", veterinar.Stevilka);
            return View(veterinar);
        }

        // GET: Veterinar/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veterinar = await _context.Veterinars
                .Include(v => v.StevilkaNavigation)
                .FirstOrDefaultAsync(m => m.IdVeterinar == id);
            if (veterinar == null)
            {
                return NotFound();
            }

            return View(veterinar);
        }

        // POST: Veterinar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var veterinar = await _context.Veterinars.FindAsync(id);
            _context.Veterinars.Remove(veterinar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VeterinarExists(decimal id)
        {
            return _context.Veterinars.Any(e => e.IdVeterinar == id);
        }
    }
}
