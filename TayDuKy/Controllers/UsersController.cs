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
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MyDbContext _context;

        public UsersController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet("login")]
        public async Task<ActionResult<User>> Login(String userId, String password)
        {
            User user = await _context.User.Where(u => u.UserId == userId && u.Password == password).FirstOrDefaultAsync();
            user.Password = null;
            return  user;
        }


        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUser(String isDeleted)
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
            return await _context.User.Where(u => u.RoleId == 2 && u.IsDelete == isDeletedBool).ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}/characters")]
        public async Task<ActionResult> GetCharacter(string id)
        {
            if (id == "null")
            {
                var charactere = await _context.Character.Where(c => c.UserId == null).ToListAsync();
                return Ok(charactere);
            }
            var character = await _context.Character.Where(c=> c.UserId == id).ToListAsync();

            if (character == null)
            {
                return NotFound();
            }

            return Ok(character);
        }

        // GET: api/Users/5
        [HttpPut("{id}/characters/{cid}")]
        public async Task<ActionResult> addUserToCharacter(string id, int cid)
        {
            Character character = await _context.Character.Where(c => c.CharacterId == cid).FirstOrDefaultAsync();

            if (character == null)
            {
                return NotFound();
            }

            character.UserId = id;
            _context.Entry(character).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CharacterExists(cid))
                {
                    return NotFound();
                }
                else if (!UserExists(id))
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

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }


        // GET: api/Users/5
        [HttpGet("{id}/calamities")]
        public async Task<ActionResult> GetCalamities(string id)
        {
            var calamity = await _context.CalamityCharacter
                .Include(c => c.Character.User).Where(cc => cc.Character.User.UserId == id).Select(cc => cc.Calamity)
                        .ToListAsync();

            if (calamity == null)
            {
                return NotFound();
            }

            return Ok(calamity);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            User user1 = _context.User.Find(id);
            user.Password = user1.Password;
            _context.Entry(user1).State = EntityState.Detached;

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            user.IsDelete = false;
            _context.User.Add(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(string id)
        {
            User user = _context.User.Find(id);
            if (id != user.UserId)
            {
                return BadRequest();
            }
            user.IsDelete = true;
            _context.Entry(user).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        private bool UserExists(string id)
        {
            return _context.User.Any(e => e.UserId == id);
        }

        private bool CharacterExists(int id)
        {
            return _context.Character.Any(e => e.CharacterId == id);
        }
    }
}
