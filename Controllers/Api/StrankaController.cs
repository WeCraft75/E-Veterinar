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
    public class StrankaController : ControllerBase
    {
        private readonly eveterinarContext _context;

        public StrankaController(eveterinarContext context)
        {
            _context = context;
        }

        // GET: api/Stranka
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stranka>>> GetStrankas()
        {
            return await _context.Strankas.ToListAsync();
        }

        // GET: api/Stranka/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Stranka>> GetStranka(decimal id)
        {
            var stranka = await _context.Strankas.FindAsync(id);

            if (stranka == null)
            {
                return NotFound();
            }

            return stranka;
        }

        // PUT: api/Stranka/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStranka(decimal id, Stranka stranka)
        {
            if (id != stranka.IdStranka)
            {
                return BadRequest();
            }

            _context.Entry(stranka).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StrankaExists(id))
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

        // POST: api/Stranka
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Stranka>> PostStranka(Stranka stranka)
        {
            _context.Strankas.Add(stranka);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StrankaExists(stranka.IdStranka))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetStranka", new { id = stranka.IdStranka }, stranka);
        }

        // DELETE: api/Stranka/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStranka(decimal id)
        {
            var stranka = await _context.Strankas.FindAsync(id);
            if (stranka == null)
            {
                return NotFound();
            }

            _context.Strankas.Remove(stranka);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StrankaExists(decimal id)
        {
            return _context.Strankas.Any(e => e.IdStranka == id);
        }
    }
}
