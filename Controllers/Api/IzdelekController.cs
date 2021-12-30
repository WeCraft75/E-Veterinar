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
    public class IzdelekController : ControllerBase
    {
        private readonly eveterinarContext _context;

        public IzdelekController(eveterinarContext context)
        {
            _context = context;
        }

        // GET: api/Izdelek
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Izdelek>>> GetIzdeleks()
        {
            return await _context.Izdeleks.ToListAsync();
        }

        // GET: api/Izdelek/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Izdelek>> GetIzdelek(decimal id)
        {
            var izdelek = await _context.Izdeleks.FindAsync(id);

            if (izdelek == null)
            {
                return NotFound();
            }

            return izdelek;
        }

        // PUT: api/Izdelek/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIzdelek(decimal id, Izdelek izdelek)
        {
            if (id != izdelek.IdIzdelek)
            {
                return BadRequest();
            }

            _context.Entry(izdelek).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IzdelekExists(id))
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

        // POST: api/Izdelek
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Izdelek>> PostIzdelek(Izdelek izdelek)
        {
            _context.Izdeleks.Add(izdelek);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (IzdelekExists(izdelek.IdIzdelek))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetIzdelek", new { id = izdelek.IdIzdelek }, izdelek);
        }

        // DELETE: api/Izdelek/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIzdelek(decimal id)
        {
            var izdelek = await _context.Izdeleks.FindAsync(id);
            if (izdelek == null)
            {
                return NotFound();
            }

            _context.Izdeleks.Remove(izdelek);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IzdelekExists(decimal id)
        {
            return _context.Izdeleks.Any(e => e.IdIzdelek == id);
        }
    }
}
