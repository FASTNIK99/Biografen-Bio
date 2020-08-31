﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biografen.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Biografen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministratorController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public AdministratorController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/administrator
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Administrator>>> GetAdministrators()
        {
            return await _context.administrators.ToListAsync();
        }

        // GET: api/administrator/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Administrator>> GetAdministrator(int id)
        {
            var admin = await _context.administrators.FindAsync(id);

            if (admin == null)
            {
                return NotFound();
            }

            return admin;
        }

        // PUT: api/administrator/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdministrator(int id, Administrator administrator)
        {
            if (id != administrator.administratorId)
            {
                return BadRequest();
            }

            _context.Entry(administrator).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdministratorExists(id))
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

        // POST: api/administrator
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Administrator>> PostAdministrator(Administrator administrator)
        {
            _context.administrators.Add(administrator);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdministrator", new { id = administrator.administratorId }, administrator);
        }

        // DELETE: api/administrator/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Administrator>> DeleteAdministrator(int id)
        {
            var administrator = await _context.administrators.FindAsync(id);
            if (administrator == null)
            {
                return NotFound();
            }

            _context.administrators.Remove(administrator);
            await _context.SaveChangesAsync();

            return administrator;
        }

        private bool AdministratorExists(int id)
        {
            return _context.administrators.Any(e => e.administratorId == id);
        }
    }
}
