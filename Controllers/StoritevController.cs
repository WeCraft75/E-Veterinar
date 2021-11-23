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
    public class StoritevController : Controller
    {
        private readonly eveterinarContext _context;

        public StoritevController(eveterinarContext context)
        {
            _context = context;
        }

        // GET: Storitev
        public async Task<IActionResult> Index()
        {
            return View(await _context.Storitevs.ToListAsync());
        }

        // GET: Storitev/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storitev = await _context.Storitevs
                .FirstOrDefaultAsync(m => m.IdStoritev == id);
            if (storitev == null)
            {
                return NotFound();
            }

            return View(storitev);
        }

        // GET: Storitev/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Storitev/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdStoritev,OpisStoritve")] Storitev storitev)
        {
            if (ModelState.IsValid)
            {
                _context.Add(storitev);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(storitev);
        }

        // GET: Storitev/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storitev = await _context.Storitevs.FindAsync(id);
            if (storitev == null)
            {
                return NotFound();
            }
            return View(storitev);
        }

        // POST: Storitev/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("IdStoritev,OpisStoritve")] Storitev storitev)
        {
            if (id != storitev.IdStoritev)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(storitev);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoritevExists(storitev.IdStoritev))
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
            return View(storitev);
        }

        // GET: Storitev/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storitev = await _context.Storitevs
                .FirstOrDefaultAsync(m => m.IdStoritev == id);
            if (storitev == null)
            {
                return NotFound();
            }

            return View(storitev);
        }

        // POST: Storitev/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var storitev = await _context.Storitevs.FindAsync(id);
            _context.Storitevs.Remove(storitev);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StoritevExists(decimal id)
        {
            return _context.Storitevs.Any(e => e.IdStoritev == id);
        }
    }
}
