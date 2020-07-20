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
    public class CalamityEquipmentsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public CalamityEquipmentsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/CalamityEquipments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CalamityEquipment>>> GetCalamityEquipment()
        {
            return await _context.CalamityEquipment.ToListAsync();
        }

        // GET: api/CalamityEquipments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CalamityEquipment>> GetCalamityEquipment(int id)
        {
            var calamityEquipment = await _context.CalamityEquipment.FindAsync(id);

            if (calamityEquipment == null)
            {
                return NotFound();
            }

            return calamityEquipment;
        }

        // PUT: api/CalamityEquipments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutCalamityEquipment(int id, CalamityEquipment calamityEquipment)
        //{
        //    if (id != calamityEquipment.CalamityId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(calamityEquipment).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CalamityEquipmentExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/CalamityEquipments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult> PostCalamityEquipment(CalamityEquipment calamityEquipment)
        {          
            if (CalamityEquipmentExists(calamityEquipment.CalamityId, calamityEquipment.EquipmentId))
            {
                CalamityEquipment ce = _context.CalamityEquipment.Find(calamityEquipment.CalamityId, calamityEquipment.EquipmentId);
                ce.quantity = ce.quantity + calamityEquipment.quantity;
                _context.Entry(ce).State = EntityState.Modified;
            }
            else
            {
                _context.CalamityEquipment.Add(calamityEquipment);
            }
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/CalamityEquipments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CalamityEquipment>> DeleteCalamityEquipment(int id)
        {
            var calamityEquipment = await _context.CalamityEquipment.FindAsync(id);
            if (calamityEquipment == null)
            {
                return NotFound();
            }

            _context.CalamityEquipment.Remove(calamityEquipment);
            await _context.SaveChangesAsync();

            return calamityEquipment;
        }

        private bool CalamityEquipmentExists(int cid, int eid)
        {
            return _context.CalamityEquipment.Any(e => e.CalamityId == cid && e.EquipmentId == eid);
        }
    }
}
