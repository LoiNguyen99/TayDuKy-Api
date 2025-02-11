﻿using System;
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
        public async Task<ActionResult<IEnumerable<Calamity>>> GetCalamity(String isDeleted)
        {
            bool isDeletedBool = false;
            if (isDeleted.ToLower() == "true")
            {
                isDeletedBool = true;
            }
            else if (isDeleted.ToLower() == "false")
            {
                isDeletedBool = false;
            }
            return await _context.Calamity.Where(c => c.IsDelete == isDeletedBool)
                .Include(c => c.CalamityCharacters)
                    .ThenInclude(cc => cc.Character.User)
                .Include(c => c.CalamityEquipment)
                    .ThenInclude(ce => ce.Equipment)
                .ToListAsync();
        }

        // GET: api/Calamities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Calamity>> GetCalamity(int id)
        {
            Calamity calamity = await _context.Calamity
                .Include(c => c.CalamityCharacters)
                    .ThenInclude(cc => cc.Character.User)
                .Include(c => c.CalamityEquipment)
                    .ThenInclude(ce => ce.Equipment)
                .Where(c => c.CalamityId == id).FirstOrDefaultAsync();

            if (calamity == null)
            {
                return NotFound();
            }

            return calamity;
        }


        [HttpGet("{id}/characters")]
        public async Task<ActionResult<Calamity>> GetCharacter(int id, string isExcept)

        {
            if (isExcept == null && isExcept != "true")
            {
                List<Character> characters = await _context.CalamityCharacter.Include(c => c.Character.User).Where(c => c.CalamityId == id).Select(c => c.Character).ToListAsync();
                return Ok(characters);
            }

            else if (isExcept.ToLower() == "true")
            {

                List<CalamityCharacter> calamityCharacters = await _context.CalamityCharacter.Include(c => c.Character).Where(c => c.CalamityId == id).ToListAsync();
                List<Character> characters = await _context.Character.Where(c => c.IsDelete == false && c.UserId != null).Include(c => c.User).ToListAsync();

                foreach (CalamityCharacter Ccharacter in calamityCharacters)
                    characters.Remove(Ccharacter.Character);

                return Ok(characters);
            }
            else
            {
                return NotFound();
            }

        }

        //[HttpGet("{id}/equipments")]
        //public async Task<ActionResult<Calamity>> GetEquipment(int id, string isExcept)
        //{
        //    if (isExcept == null && isExcept != "true")
        //    {
        //        List<Equipment> equipments = await _context.CalamityEquipment.Include(c => c.Equipment).Where(c => c.CalamityId == id).Select(c => c.Equipment).ToListAsync();
        //        return Ok(equipments);
        //    }

        //    else if (isExcept.ToLower() == "true")
        //    {
        //        List<Equipment> equipments = await _context.Equipment.Where(c => c.IsDelete == false && c.Quantity > 0).ToListAsync();
        //        return Ok(equipments);
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }

        //}


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
            calamity.IsDelete = true;
            _context.Entry(calamity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}/characters/{chId}")]
        public async Task<ActionResult<Calamity>> DeleteCharacter(int id, int chId)
        {
            var calamityChaharacter = await _context.CalamityCharacter.Where( c => c.CalamityId == id && c.CharacterId == chId).FirstOrDefaultAsync();
            if (calamityChaharacter == null)
            {
                return NotFound();
            }
            _context.CalamityCharacter.Remove(calamityChaharacter);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}/equipments/{eId}")]
        public async Task<ActionResult<Calamity>> DeleteEq(int id, int eId)
        {
            var calamityEquipment = await _context.CalamityEquipment.Where(c => c.CalamityId == id && c.EquipmentId == eId).FirstOrDefaultAsync();
            if (calamityEquipment == null)
            {
                return NotFound();
            }
            _context.CalamityEquipment.Remove(calamityEquipment);

            int quantity = calamityEquipment.quantity;
            Equipment equipment = _context.Equipment.Find(eId);
            equipment.Quantity = equipment.Quantity + quantity;
            _context.Entry(equipment).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool CalamityExists(int id)
        {
            return _context.Calamity.Any(e => e.CalamityId == id);
        }
    }
}
