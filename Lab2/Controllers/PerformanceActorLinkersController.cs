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
    public class PerformanceActorLinkersController : ControllerBase
    {
        private readonly Performance_ActorContext _context;

        public PerformanceActorLinkersController(Performance_ActorContext context)
        {
            _context = context;
        }

        // GET: api/PerformanceActorLinkers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PerformanceActorLinker>>> GetPerformanceActorLinker()
        {
            return await _context.PerformanceActorLinker.ToListAsync();
        }

        // GET: api/PerformanceActorLinkers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PerformanceActorLinker>> GetPerformanceActorLinker(int id)
        {
            var performanceActorLinker = await _context.PerformanceActorLinker.FindAsync(id);

            if (performanceActorLinker == null)
            {
                return NotFound();
            }

            return performanceActorLinker;
        }

        // PUT: api/PerformanceActorLinkers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerformanceActorLinker(int id, PerformanceActorLinker performanceActorLinker)
        {
            if (id != performanceActorLinker.Id)
            {
                return BadRequest();
            }

            _context.Entry(performanceActorLinker).State = EntityState.Modified;

            if (_context.Actor.Where(b => b.Id == performanceActorLinker.ActorId).ToList().Count() <= 0) return NoContent();
            if (_context.Performance.Where(b => b.Id == performanceActorLinker.PerformanceId).ToList().Count() <= 0) return NoContent();
            var pa = (from s in _context.PerformanceActorLinker
                      where ((s.PerformanceId == performanceActorLinker.PerformanceId) && (s.ActorId == performanceActorLinker.ActorId))
                      select s).ToList();
            if (pa.Count() > 0) return NoContent();

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PerformanceActorLinkerExists(id))
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

        // POST: api/PerformanceActorLinkers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PerformanceActorLinker>> PostPerformanceActorLinker(PerformanceActorLinker performanceActorLinker)
        {
            if (_context.Actor.Where(b => b.Id == performanceActorLinker.ActorId).ToList().Count() <= 0) return NoContent();
            if (_context.Performance.Where(b => b.Id == performanceActorLinker.PerformanceId).ToList().Count() <= 0) return NoContent();
            var pa = (from s in _context.PerformanceActorLinker
                      where ((s.PerformanceId == performanceActorLinker.PerformanceId) && (s.ActorId == performanceActorLinker.ActorId))
                      select s).ToList();
            if (pa.Count() > 0) return NoContent();
            _context.PerformanceActorLinker.Add(performanceActorLinker);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPerformanceActorLinker", new { id = performanceActorLinker.Id }, performanceActorLinker);
        }

        // DELETE: api/PerformanceActorLinkers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PerformanceActorLinker>> DeletePerformanceActorLinker(int id)
        {
            var performanceActorLinker = await _context.PerformanceActorLinker.FindAsync(id);
            if (performanceActorLinker == null)
            {
                return NotFound();
            }

            _context.PerformanceActorLinker.Remove(performanceActorLinker);
            await _context.SaveChangesAsync();

            return performanceActorLinker;
        }

        private bool PerformanceActorLinkerExists(int id)
        {
            return _context.PerformanceActorLinker.Any(e => e.Id == id);
        }
    }
}
