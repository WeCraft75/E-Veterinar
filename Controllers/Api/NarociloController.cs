using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E_Veterinar.Data;
using E_Veterinar.Models;
using E_Veterinar.Filters;

namespace E_Veterinar.Controllers_Api
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKeyAuth]
    public class NarociloController : ControllerBase
    {
        private readonly eveterinarContext _context;

        public NarociloController(eveterinarContext context)
        {
            _context = context;
        }

        // GET: api/Narocilo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Narocilo>>> GetNarocilos()
        {
            return await _context.Narocilos.ToListAsync();
        }

        // GET: api/Narocilo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Narocilo>> GetNarocilo(decimal id)
        {
            var narocilo = await _context.Narocilos.FindAsync(id);

            if (narocilo == null)
            {
                return NotFound();
            }

            return narocilo;
        }

        // PUT: api/Narocilo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNarocilo(decimal id, Narocilo narocilo)
        {
            if (id != narocilo.IdNarocilo)
            {
                return BadRequest();
            }

            _context.Entry(narocilo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NarociloExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Narocilo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Narocilo>> PostNarocilo(Narocilo narocilo)
        {
            _context.Narocilos.Add(narocilo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (NarociloExists(narocilo.IdNarocilo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetNarocilo", new { id = narocilo.IdNarocilo }, narocilo);
        }

        // DELETE: api/Narocilo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNarocilo(decimal id)
        {
            var narocilo = await _context.Narocilos.FindAsync(id);
            if (narocilo == null)
            {
                return NotFound();
            }

            _context.Narocilos.Remove(narocilo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NarociloExists(decimal id)
        {
            return _context.Narocilos.Any(e => e.IdNarocilo == id);
        }
    }
}
