using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using E_Veterinar.Data;
using E_Veterinar.Models;

namespace E_Veterinar.Controllers_Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TerminController : ControllerBase
    {
        private readonly eveterinarContext _context;

        public TerminController(eveterinarContext context)
        {
            _context = context;
        }

        // GET: api/Termin
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Termin>>> GetTermins()
        {
            return await _context.Termins.ToListAsync();
        }

        // GET: api/Termin/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Termin>> GetTermin(decimal id)
        {
            var termin = await _context.Termins.FindAsync(id);

            if (termin == null)
            {
                return NotFound();
            }

            return termin;
        }

        // PUT: api/Termin/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTermin(decimal id, Termin termin)
        {
            if (id != termin.IdVeterinar)
            {
                return BadRequest();
            }

            _context.Entry(termin).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TerminExists(id))
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

        // POST: api/Termin
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Termin>> PostTermin(Termin termin)
        {
            _context.Termins.Add(termin);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TerminExists(termin.IdVeterinar))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTermin", new { id = termin.IdVeterinar }, termin);
        }

        // DELETE: api/Termin/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTermin(decimal id)
        {
            var termin = await _context.Termins.FindAsync(id);
            if (termin == null)
            {
                return NotFound();
            }

            _context.Termins.Remove(termin);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TerminExists(decimal id)
        {
            return _context.Termins.Any(e => e.IdVeterinar == id);
        }
    }
}
