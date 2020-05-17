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
    public class PerformancesController : ControllerBase
    {
        private readonly Performance_ActorContext _context;

        public PerformancesController(Performance_ActorContext context)
        {
            _context = context;
        }

        // GET: api/Performances
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Performance>>> GetPerformance()
        {
            return await _context.Performance.ToListAsync();
        }

        // GET: api/Performances/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Performance>> GetPerformance(int id)
        {
            var performance = await _context.Performance.FindAsync(id);

            if (performance == null)
            {
                return NotFound();
            }

            return performance;
        }

        // PUT: api/Performances/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerformance(int id, Performance performance)
        {
            if (id != performance.Id)
            {
                return BadRequest();
            }

            _context.Entry(performance).State = EntityState.Modified;

            var t = (from w in _context.TypeOfPerformanceCollection
                     where (w.Id == performance.TypeOfPerformanceId)
                     select w).ToList();
            if (t.Count() <= 0) return NoContent();
            var a = (from w in _context.Performance
                     where (w.Name == performance.Name)
                     select w).ToList();
            if (a.Count() > 0) return NoContent();


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PerformanceExists(id))
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

        // POST: api/Performances
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Performance>> PostPerformance(Performance performance)
        {
            var t = (from w in _context.TypeOfPerformanceCollection
                     where (w.Id == performance.TypeOfPerformanceId)
                     select w).ToList();
            if (t.Count() <= 0) return NoContent();
            var a = (from w in _context.Performance
                     where (w.Name == performance.Name)
                     select w).ToList();
            if (a.Count() > 0) return NoContent();

            _context.Performance.Add(performance);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPerformance", new { id = performance.Id }, performance);
        }

        // DELETE: api/Performances/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Performance>> DeletePerformance(int id)
        {
            var performance = await _context.Performance.FindAsync(id);
            if (performance == null)
            {
                return NotFound();
            }

            _context.Performance.Remove(performance);
            await _context.SaveChangesAsync();

            return performance;
        }

        private bool PerformanceExists(int id)
        {
            return _context.Performance.Any(e => e.Id == id);
        }
    }
}
