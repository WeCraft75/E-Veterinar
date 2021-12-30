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
    public class StoritevController : ControllerBase
    {
        private readonly eveterinarContext _context;

        public StoritevController(eveterinarContext context)
        {
            _context = context;
        }

        // GET: api/Storitev
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Storitev>>> GetStoritevs()
        {
            return await _context.Storitevs.ToListAsync();
        }

        // GET: api/Storitev/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Storitev>> GetStoritev(decimal id)
        {
            var storitev = await _context.Storitevs.FindAsync(id);

            if (storitev == null)
            {
                return NotFound();
            }

            return storitev;
        }

        // PUT: api/Storitev/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStoritev(decimal id, Storitev storitev)
        {
            if (id != storitev.IdStoritev)
            {
                return BadRequest();
            }

            _context.Entry(storitev).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoritevExists(id))
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

        // POST: api/Storitev
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Storitev>> PostStoritev(Storitev storitev)
        {
            _context.Storitevs.Add(storitev);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StoritevExists(storitev.IdStoritev))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetStoritev", new { id = storitev.IdStoritev }, storitev);
        }

        // DELETE: api/Storitev/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStoritev(decimal id)
        {
            var storitev = await _context.Storitevs.FindAsync(id);
            if (storitev == null)
            {
                return NotFound();
            }

            _context.Storitevs.Remove(storitev);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StoritevExists(decimal id)
        {
            return _context.Storitevs.Any(e => e.IdStoritev == id);
        }
    }
}
