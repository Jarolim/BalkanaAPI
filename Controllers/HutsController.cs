using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BalkanaAPI.Models;

namespace BalkanaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HutsController : ControllerBase
    {
        private readonly HutsContext _context;

        public HutsController(HutsContext context)
        {
            _context = context;
        }

        // GET: api/Huts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hut>>> GetHutItems()
        {
            return await _context.HutItems.ToListAsync();
        }

        // GET: api/Huts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hut>> GetHut(int id)
        {
            var hut = await _context.HutItems.FindAsync(id);

            if (hut == null)
            {
                return NotFound();
            }

            return hut;
        }

        // PUT: api/Huts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHut(int id, Hut hut)
        {
            if (id != hut.Id)
            {
                return BadRequest();
            }

            _context.Entry(hut).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HutExists(id))
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

        // POST: api/Huts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Hut>> PostHut(Hut hut)
        {
            _context.HutItems.Add(hut);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHut", new { id = hut.Id }, hut);
        }

        // DELETE: api/Huts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Hut>> DeleteHut(int id)
        {
            var hut = await _context.HutItems.FindAsync(id);
            if (hut == null)
            {
                return NotFound();
            }

            _context.HutItems.Remove(hut);
            await _context.SaveChangesAsync();

            return hut;
        }

        private bool HutExists(int id)
        {
            return _context.HutItems.Any(e => e.Id == id);
        }
    }
}
