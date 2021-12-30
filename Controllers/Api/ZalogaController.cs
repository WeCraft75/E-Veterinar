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
    public class ZalogaController : ControllerBase
    {
        private readonly eveterinarContext _context;

        public ZalogaController(eveterinarContext context)
        {
            _context = context;
        }

        // GET: api/Zaloga
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Zaloga>>> GetZalogas()
        {
            return await _context.Zalogas.ToListAsync();
        }

        // GET: api/Zaloga/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Zaloga>> GetZaloga(decimal id)
        {
            var zaloga = await _context.Zalogas.FindAsync(id);

            if (zaloga == null)
            {
                return NotFound();
            }

            return zaloga;
        }

        // PUT: api/Zaloga/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutZaloga(decimal id, Zaloga zaloga)
        {
            if (id != zaloga.IdIzdelek)
            {
                return BadRequest();
            }

            _context.Entry(zaloga).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZalogaExists(id))
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

        // POST: api/Zaloga
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Zaloga>> PostZaloga(Zaloga zaloga)
        {
            _context.Zalogas.Add(zaloga);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ZalogaExists(zaloga.IdIzdelek))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetZaloga", new { id = zaloga.IdIzdelek }, zaloga);
        }

        // DELETE: api/Zaloga/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteZaloga(decimal id)
        {
            var zaloga = await _context.Zalogas.FindAsync(id);
            if (zaloga == null)
            {
                return NotFound();
            }

            _context.Zalogas.Remove(zaloga);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ZalogaExists(decimal id)
        {
            return _context.Zalogas.Any(e => e.IdIzdelek == id);
        }
    }
}
