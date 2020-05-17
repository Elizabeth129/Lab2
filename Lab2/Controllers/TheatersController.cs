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
    public class TheatersController : ControllerBase
    {
        private readonly Performance_ActorContext _context;

        public TheatersController(Performance_ActorContext context)
        {
            _context = context;
        }

        // GET: api/Theaters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Theater>>> GetTheater()
        {
            return await _context.Theater.ToListAsync();
        }

        // GET: api/Theaters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Theater>> GetTheater(int id)
        {
            var theater = await _context.Theater.FindAsync(id);

            if (theater == null)
            {
                return NotFound();
            }

            return theater;
        }

        // PUT: api/Theaters/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTheater(int id, Theater theater)
        {
            if (id != theater.Id)
            {
                return BadRequest();
            }

            _context.Entry(theater).State = EntityState.Modified;

            var periodFrom = new DateTime(1800, 1, 1, 0, 0, 0);
            var periodTo = new DateTime(2020, 1, 1, 0, 0, 0);
            if ((theater.DateOfStartWork < periodFrom) || (theater.DateOfStartWork > periodTo)) return NoContent();
            var r = (from d in _context.Director
                     where (d.Id == theater.DirectorId)
                     select d.Id).ToList();
            if (r.Count() <= 0) return NoContent();
            var a = (from w in _context.Theater
                     where (w.Address == theater.Address)
                     select w).ToList();
            if (a.Count() > 0) return NoContent();

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TheaterExists(id))
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

        // POST: api/Theaters
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Theater>> PostTheater(Theater theater)
        {
            var periodFrom = new DateTime(1800, 1, 1, 0, 0, 0);
            var periodTo = new DateTime(2020, 1, 1, 0, 0, 0);
            if ((theater.DateOfStartWork < periodFrom) || (theater.DateOfStartWork > periodTo)) return NoContent();
            var r = (from d in _context.Director
                     where (d.Id == theater.DirectorId)
                     select d.Id).ToList();
            if (r.Count() <= 0) return NoContent();
            var a = (from w in _context.Theater
                     where (w.Address == theater.Address)
                     select w).ToList();
            if (a.Count() > 0) return NoContent();
            _context.Theater.Add(theater);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTheater", new { id = theater.Id }, theater);
        }

        // DELETE: api/Theaters/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Theater>> DeleteTheater(int id)
        {
            var theater = await _context.Theater.FindAsync(id);
            if (theater == null)
            {
                return NotFound();
            }

            _context.Theater.Remove(theater);
            await _context.SaveChangesAsync();

            return theater;
        }

        private bool TheaterExists(int id)
        {
            return _context.Theater.Any(e => e.Id == id);
        }
    }
}
