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
    public class EvidencaController : ControllerBase
    {
        private readonly eveterinarContext _context;

        public EvidencaController(eveterinarContext context)
        {
            _context = context;
        }

        // GET: api/Evidenca
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Evidenca>>> GetEvidencas()
        {
            return await _context.Evidencas.ToListAsync();
        }

        // GET: api/Evidenca/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Evidenca>> GetEvidenca(decimal id)
        {
            var evidenca = await _context.Evidencas.FindAsync(id);

            if (evidenca == null)
            {
                return NotFound();
            }

            return evidenca;
        }

        // PUT: api/Evidenca/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvidenca(decimal id, Evidenca evidenca)
        {
            if (id != evidenca.IdEvidence)
            {
                return BadRequest();
            }

            _context.Entry(evidenca).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EvidencaExists(id))
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

        // POST: api/Evidenca
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Evidenca>> PostEvidenca(Evidenca evidenca)
        {
            _context.Evidencas.Add(evidenca);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EvidencaExists(evidenca.IdEvidence))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEvidenca", new { id = evidenca.IdEvidence }, evidenca);
        }

        // DELETE: api/Evidenca/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvidenca(decimal id)
        {
            var evidenca = await _context.Evidencas.FindAsync(id);
            if (evidenca == null)
            {
                return NotFound();
            }

            _context.Evidencas.Remove(evidenca);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EvidencaExists(decimal id)
        {
            return _context.Evidencas.Any(e => e.IdEvidence == id);
        }
    }
}
