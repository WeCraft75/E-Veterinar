using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_Veterinar.Data;
using E_Veterinar.Models;
using Microsoft.AspNetCore.Identity;

namespace E_Veterinar.Controllers
{
    public class StrankaController : Controller
    {
        private readonly eveterinarContext _context;
        private readonly UserManager<ApplicationUser> _usermanager;

        public StrankaController(eveterinarContext context, UserManager<ApplicationUser> usermanager)
        {
            _usermanager = usermanager;
            _context = context;
        }

        // GET: Stranka
        public async Task<IActionResult> Index()
        {
            var eveterinarContext = _context.Strankas.Include(s => s.StevilkaNavigation);
            return View(await eveterinarContext.ToListAsync());
        }

        // GET: Stranka/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stranka = await _context.Strankas
                .Include(s => s.StevilkaNavigation)
                .FirstOrDefaultAsync(m => m.IdStranka == id);
            if (stranka == null)
            {
                return NotFound();
            }

            return View(stranka);
        }

        // GET: Stranka/Create
        public IActionResult Create()
        {
            ViewData["Stevilka"] = new SelectList(_context.Posta, "Stevilka", "Stevilka");
            return View();
        }

        // POST: Stranka/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdStranka,Stevilka,Ime,Priimek,Naslov,Kraj")] Stranka stranka)
        {
            var currentUser = await _usermanager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                stranka.AspNetID = currentUser;
                _context.Add(stranka);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Stevilka"] = new SelectList(_context.Posta, "Stevilka", "Stevilka", stranka.Stevilka);
            return View(stranka);
        }

        // GET: Stranka/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stranka = await _context.Strankas.FindAsync(id);
            if (stranka == null)
            {
                return NotFound();
            }
            ViewData["Stevilka"] = new SelectList(_context.Posta, "Stevilka", "Stevilka", stranka.Stevilka);
            return View(stranka);
        }

        // POST: Stranka/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("IdStranka,Stevilka,Ime,Priimek,Naslov,Kraj")] Stranka stranka)
        {
            if (id != stranka.IdStranka)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stranka);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StrankaExists(stranka.IdStranka))
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
            ViewData["Stevilka"] = new SelectList(_context.Posta, "Stevilka", "Stevilka", stranka.Stevilka);
            return View(stranka);
        }

        // GET: Stranka/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stranka = await _context.Strankas
                .Include(s => s.StevilkaNavigation)
                .FirstOrDefaultAsync(m => m.IdStranka == id);
            if (stranka == null)
            {
                return NotFound();
            }

            return View(stranka);
        }

        // POST: Stranka/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var stranka = await _context.Strankas.FindAsync(id);
            _context.Strankas.Remove(stranka);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StrankaExists(decimal id)
        {
            return _context.Strankas.Any(e => e.IdStranka == id);
        }
    }
}
