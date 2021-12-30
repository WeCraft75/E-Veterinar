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
    public class PostumController : ControllerBase
    {
        private readonly eveterinarContext _context;

        public PostumController(eveterinarContext context)
        {
            _context = context;
        }

        // GET: api/Postum
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Postum>>> GetPosta()
        {
            return await _context.Posta.ToListAsync();
        }

        // GET: api/Postum/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Postum>> GetPostum(decimal id)
        {
            var postum = await _context.Posta.FindAsync(id);

            if (postum == null)
            {
                return NotFound();
            }

            return postum;
        }

        // PUT: api/Postum/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPostum(decimal id, Postum postum)
        {
            if (id != postum.Stevilka)
            {
                return BadRequest();
            }

            _context.Entry(postum).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostumExists(id))
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

        // POST: api/Postum
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Postum>> PostPostum(Postum postum)
        {
            _context.Posta.Add(postum);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PostumExists(postum.Stevilka))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPostum", new { id = postum.Stevilka }, postum);
        }

        // DELETE: api/Postum/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePostum(decimal id)
        {
            var postum = await _context.Posta.FindAsync(id);
            if (postum == null)
            {
                return NotFound();
            }

            _context.Posta.Remove(postum);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PostumExists(decimal id)
        {
            return _context.Posta.Any(e => e.Stevilka == id);
        }
    }
}
