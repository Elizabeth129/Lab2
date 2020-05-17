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
    public class TypeOfPerformanceCollectionsController : ControllerBase
    {
        private readonly Performance_ActorContext _context;

        public TypeOfPerformanceCollectionsController(Performance_ActorContext context)
        {
            _context = context;
        }

        // GET: api/TypeOfPerformanceCollections
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeOfPerformanceCollection>>> GetTypeOfPerformanceCollection()
        {
            return await _context.TypeOfPerformanceCollection.ToListAsync();
        }

        // GET: api/TypeOfPerformanceCollections/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TypeOfPerformanceCollection>> GetTypeOfPerformanceCollection(int id)
        {
            var typeOfPerformanceCollection = await _context.TypeOfPerformanceCollection.FindAsync(id);

            if (typeOfPerformanceCollection == null)
            {
                return NotFound();
            }

            return typeOfPerformanceCollection;
        }

        // PUT: api/TypeOfPerformanceCollections/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTypeOfPerformanceCollection(int id, TypeOfPerformanceCollection typeOfPerformanceCollection)
        {
            if (id != typeOfPerformanceCollection.Id)
            {
                return BadRequest();
            }

            _context.Entry(typeOfPerformanceCollection).State = EntityState.Modified;

            var a = (from w in _context.TypeOfPerformanceCollection
                     where (w.TypeOfPerformanceName == typeOfPerformanceCollection.TypeOfPerformanceName)
                     select w).ToList();
            if (a.Count() > 0) return NoContent();

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeOfPerformanceCollectionExists(id))
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

        // POST: api/TypeOfPerformanceCollections
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TypeOfPerformanceCollection>> PostTypeOfPerformanceCollection(TypeOfPerformanceCollection typeOfPerformanceCollection)
        {
            var a = (from w in _context.TypeOfPerformanceCollection
                     where (w.TypeOfPerformanceName == typeOfPerformanceCollection.TypeOfPerformanceName)
                     select w).ToList();
            if (a.Count() > 0) return NoContent();

            _context.TypeOfPerformanceCollection.Add(typeOfPerformanceCollection);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTypeOfPerformanceCollection", new { id = typeOfPerformanceCollection.Id }, typeOfPerformanceCollection);
        }

        // DELETE: api/TypeOfPerformanceCollections/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TypeOfPerformanceCollection>> DeleteTypeOfPerformanceCollection(int id)
        {
            var typeOfPerformanceCollection = await _context.TypeOfPerformanceCollection.FindAsync(id);
            if (typeOfPerformanceCollection == null)
            {
                return NotFound();
            }

            _context.TypeOfPerformanceCollection.Remove(typeOfPerformanceCollection);
            await _context.SaveChangesAsync();

            return typeOfPerformanceCollection;
        }

        private bool TypeOfPerformanceCollectionExists(int id)
        {
            return _context.TypeOfPerformanceCollection.Any(e => e.Id == id);
        }
    }
}
