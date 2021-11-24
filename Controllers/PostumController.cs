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
    public class PostumController : Controller
    {
        private readonly eveterinarContext _context;

        public PostumController(eveterinarContext context)
        {
            _context = context;
        }

        // GET: Postum
        public async Task<IActionResult> Index()
        {
            return View(await _context.Posta.ToListAsync());
        }

        // GET: Postum/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postum = await _context.Posta
                .FirstOrDefaultAsync(m => m.Stevilka == id);
            if (postum == null)
            {
                return NotFound();
            }

            return View(postum);
        }

        // GET: Postum/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Postum/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Stevilka,Naziv")] Postum postum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(postum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(postum);
        }

        // GET: Postum/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postum = await _context.Posta.FindAsync(id);
            if (postum == null)
            {
                return NotFound();
            }
            return View(postum);
        }

        // POST: Postum/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Stevilka,Naziv")] Postum postum)
        {
            if (id != postum.Stevilka)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostumExists(postum.Stevilka))
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
            return View(postum);
        }

        // GET: Postum/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postum = await _context.Posta
                .FirstOrDefaultAsync(m => m.Stevilka == id);
            if (postum == null)
            {
                return NotFound();
            }

            return View(postum);
        }

        // POST: Postum/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var postum = await _context.Posta.FindAsync(id);
            _context.Posta.Remove(postum);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostumExists(decimal id)
        {
            return _context.Posta.Any(e => e.Stevilka == id);
        }
    }
}
