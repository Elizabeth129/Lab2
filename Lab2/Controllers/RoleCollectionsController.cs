using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab2.Models;

namespace Lab2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleCollectionsController : ControllerBase
    {
        private readonly Performance_ActorContext _context;

        public RoleCollectionsController(Performance_ActorContext context)
        {
            _context = context;
        }

        // GET: api/RoleCollections
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleCollection>>> GetRoleCollection()
        {
            return await _context.RoleCollection.ToListAsync();
        }

        // GET: api/RoleCollections/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleCollection>> GetRoleCollection(int id)
        {
            var roleCollection = await _context.RoleCollection.FindAsync(id);

            if (roleCollection == null)
            {
                return NotFound();
            }

            return roleCollection;
        }

        // PUT: api/RoleCollections/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoleCollection(int id, RoleCollection roleCollection)
        {
            if (id != roleCollection.Id)
            {
                return BadRequest();
            }

            _context.Entry(roleCollection).State = EntityState.Modified;

            var a = (from w in _context.RoleCollection
                     where (w.RoleName == roleCollection.RoleName)
                     select w).ToList();
            if (a.Count() > 0) return NoContent();


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleCollectionExists(id))
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

        // POST: api/RoleCollections
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<RoleCollection>> PostRoleCollection(RoleCollection roleCollection)
        {
            
            var a = (from w in _context.RoleCollection
                     where (w.RoleName == roleCollection.RoleName)
                     select w).ToList();
            if (a.Count() > 0) return NoContent();


            _context.RoleCollection.Add(roleCollection);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoleCollection", new { id = roleCollection.Id }, roleCollection);
        }

        // DELETE: api/RoleCollections/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RoleCollection>> DeleteRoleCollection(int id)
        {
            var roleCollection = await _context.RoleCollection.FindAsync(id);
            if (roleCollection == null)
            {
                return NotFound();
            }

            _context.RoleCollection.Remove(roleCollection);
            await _context.SaveChangesAsync();

            return roleCollection;
        }

        private bool RoleCollectionExists(int id)
        {
            return _context.RoleCollection.Any(e => e.Id == id);
        }
    }
}
