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
    public class IzdelekController : Controller
    {
        private readonly eveterinarContext _context;

        public IzdelekController(eveterinarContext context)
        {
            _context = context;
        }

        // GET: Izdelek
        public async Task<IActionResult> Index()
        {
            return RedirectToAction("Index", "Zaloga");
        }

        // GET: Izdelek/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var izdelek = await _context.Izdeleks
                .FirstOrDefaultAsync(m => m.IdIzdelek == id);
            if (izdelek == null)
            {
                return NotFound();
            }

            return View(izdelek);
        }

        // GET: Izdelek/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Izdelek/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdIzdelek,Ime,Cena,Opis")] Izdelek izdelek)
        {
            if (ModelState.IsValid)
            {
                _context.Add(izdelek);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Index", "Zaloga");
        }

        // GET: Izdelek/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var izdelek = await _context.Izdeleks.FindAsync(id);
            if (izdelek == null)
            {
                return NotFound();
            }
            return View(izdelek);
        }

        // POST: Izdelek/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("IdIzdelek,Ime,Cena,Opis")] Izdelek izdelek)
        {
            if (id != izdelek.IdIzdelek)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(izdelek);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IzdelekExists(izdelek.IdIzdelek))
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
            return RedirectToAction("Index", "Zaloga");
        }

        // GET: Izdelek/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var izdelek = await _context.Izdeleks
                .FirstOrDefaultAsync(m => m.IdIzdelek == id);
            if (izdelek == null)
            {
                return NotFound();
            }

            return View(izdelek);
        }

        // POST: Izdelek/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var izdelek = await _context.Izdeleks.FindAsync(id);
            _context.Izdeleks.Remove(izdelek);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IzdelekExists(decimal id)
        {
            return _context.Izdeleks.Any(e => e.IdIzdelek == id);
        }
    }
}
