using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistroEstudiantesApi.Data;
using RegistroEstudiantesApi.Models;

namespace RegistroEstudiantesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfesorMateriasController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProfesorMateriasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfesorMateria>>> GetProfesorMaterias()
        {
            return await _context.ProfesorMaterias
                .Include(pm => pm.Profesor)
                .Include(pm => pm.Materia)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProfesorMateria>> GetProfesorMateria(int id)
        {
            var pm = await _context.ProfesorMaterias
                .Include(x => x.Profesor)
                .Include(x => x.Materia)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (pm == null)
                return NotFound();
            return pm;
        }

        [HttpPost]
        public async Task<ActionResult<ProfesorMateria>> PostProfesorMateria(ProfesorMateria pm)
        {
            _context.ProfesorMaterias.Add(pm);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProfesorMateria), new { id = pm.Id }, pm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfesorMateria(int id, ProfesorMateria pm)
        {
            if (id != pm.Id)
                return BadRequest();
            _context.Entry(pm).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.ProfesorMaterias.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfesorMateria(int id)
        {
            var pm = await _context.ProfesorMaterias.FindAsync(id);
            if (pm == null)
                return NotFound();
            _context.ProfesorMaterias.Remove(pm);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
} 