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
    public class VeterinarController : ControllerBase
    {
        private readonly eveterinarContext _context;

        public VeterinarController(eveterinarContext context)
        {
            _context = context;
        }

        // GET: api/Veterinar
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Veterinar>>> GetVeterinars()
        {
            return await _context.Veterinars.ToListAsync();
        }

        // GET: api/Veterinar/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Veterinar>> GetVeterinar(decimal id)
        {
            var veterinar = await _context.Veterinars.FindAsync(id);

            if (veterinar == null)
            {
                return NotFound();
            }

            return veterinar;
        }

        // PUT: api/Veterinar/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVeterinar(decimal id, Veterinar veterinar)
        {
            if (id != veterinar.IdVeterinar)
            {
                return BadRequest();
            }

            _context.Entry(veterinar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VeterinarExists(id))
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

        // POST: api/Veterinar
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Veterinar>> PostVeterinar(Veterinar veterinar)
        {
            _context.Veterinars.Add(veterinar);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (VeterinarExists(veterinar.IdVeterinar))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetVeterinar", new { id = veterinar.IdVeterinar }, veterinar);
        }

        // DELETE: api/Veterinar/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVeterinar(decimal id)
        {
            var veterinar = await _context.Veterinars.FindAsync(id);
            if (veterinar == null)
            {
                return NotFound();
            }

            _context.Veterinars.Remove(veterinar);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VeterinarExists(decimal id)
        {
            return _context.Veterinars.Any(e => e.IdVeterinar == id);
        }
    }
}
