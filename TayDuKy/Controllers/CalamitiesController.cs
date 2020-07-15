using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TayDuKy;
using TayDuKy.Models;

namespace TayDuKy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalamitiesController : ControllerBase
    {
        private readonly MyDbContext _context;

        public CalamitiesController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Calamities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Calamity>>> GetCalamity()
        {
            return await _context.Calamity.ToListAsync();
        }

        // GET: api/Calamities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Calamity>> GetCalamity(int id)
        {
            var calamity = await _context.Calamity.FindAsync(id);

            if (calamity == null)
            {
                return NotFound();
            }

            return calamity;
        }

        // PUT: api/Calamities/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCalamity(int id, Calamity calamity)
        {
            if (id != calamity.CalamityId)
            {
                return BadRequest();
            }

            _context.Entry(calamity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalamityExists(id))
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

        // POST: api/Calamities
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Calamity>> PostCalamity(Calamity calamity)
        {
            _context.Calamity.Add(calamity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCalamity", new { id = calamity.CalamityId }, calamity);
        }

        // DELETE: api/Calamities/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Calamity>> DeleteCalamity(int id)
        {
            var calamity = await _context.Calamity.FindAsync(id);
            if (calamity == null)
            {
                return NotFound();
            }

            _context.Calamity.Remove(calamity);
            await _context.SaveChangesAsync();

            return calamity;
        }

        private bool CalamityExists(int id)
        {
            return _context.Calamity.Any(e => e.CalamityId == id);
        }
    }
}
