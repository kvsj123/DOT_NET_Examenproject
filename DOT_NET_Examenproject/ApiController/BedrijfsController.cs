using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DOT_NET_Examenproject.Data;
using DOT_NET_Examenproject.Models;
using Microsoft.AspNetCore.Authorization;

namespace DOT_NET_Examenproject.ApiController
{
    [Route("api/[controller]")]
    [ApiController]
    public class BedrijfsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BedrijfsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Bedrijfs
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bedrijf>>> GetBedrijf()
        {
            return await _context.Bedrijf.ToListAsync();
        }

        // GET: api/Bedrijfs/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Bedrijf>> GetBedrijf(int id)
        {
            var bedrijf = await _context.Bedrijf.FindAsync(id);

            if (bedrijf == null)
            {
                return NotFound();
            }

            return bedrijf;
        }

        // PUT: api/Bedrijfs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBedrijf(int id, Bedrijf bedrijf)
        {
            if (id != bedrijf.BedrijfId)
            {
                return BadRequest();
            }

            _context.Entry(bedrijf).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BedrijfExists(id))
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

        // POST: api/Bedrijfs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Bedrijf>> PostBedrijf(Bedrijf bedrijf)
        {
            _context.Bedrijf.Add(bedrijf);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBedrijf", new { id = bedrijf.BedrijfId }, bedrijf);
        }

        // DELETE: api/Bedrijfs/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBedrijf(int id)
        {
            var bedrijf = await _context.Bedrijf.FindAsync(id);
            if (bedrijf == null)
            {
                return NotFound();
            }

            _context.Bedrijf.Remove(bedrijf);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BedrijfExists(int id)
        {
            return _context.Bedrijf.Any(e => e.BedrijfId == id);
        }
    }
}
