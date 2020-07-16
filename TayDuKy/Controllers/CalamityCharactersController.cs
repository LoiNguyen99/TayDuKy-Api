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
    public class CalamityCharactersController : ControllerBase
    {
        private readonly MyDbContext _context;

        public CalamityCharactersController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/CalamityCharacters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CalamityCharacter>>> GetCalamityCharacter()
        {
            return await _context.CalamityCharacter.ToListAsync();
        }

        // GET: api/CalamityCharacters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CalamityCharacter>> GetCalamityCharacter(int id)
        {
            var calamityCharacter = await _context.CalamityCharacter.FindAsync(id);

            if (calamityCharacter == null)
            {
                return NotFound();
            }

            return calamityCharacter;
        }

        // PUT: api/CalamityCharacters/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCalamityCharacter(int id, CalamityCharacter calamityCharacter)
        {
            if (id != calamityCharacter.CalamityId)
            {
                return BadRequest();
            }

            _context.Entry(calamityCharacter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalamityCharacterExists(id))
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

        // POST: api/CalamityCharacters
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CalamityCharacter>> PostCalamityCharacter(CalamityCharacter calamityCharacter)
        {
            _context.CalamityCharacter.Add(calamityCharacter);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CalamityCharacterExists(calamityCharacter.CalamityId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCalamityCharacter", new { id = calamityCharacter.CalamityId }, calamityCharacter);
        }

        // DELETE: api/CalamityCharacters/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CalamityCharacter>> DeleteCalamityCharacter(int id)
        {
            var calamityCharacter = await _context.CalamityCharacter.FindAsync(id);
            if (calamityCharacter == null)
            {
                return NotFound();
            }

            _context.CalamityCharacter.Remove(calamityCharacter);
            await _context.SaveChangesAsync();

            return calamityCharacter;
        }

        private bool CalamityCharacterExists(int id)
        {
            return _context.CalamityCharacter.Any(e => e.CalamityId == id);
        }
    }
}
