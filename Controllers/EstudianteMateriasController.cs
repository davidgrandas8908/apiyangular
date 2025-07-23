using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistroEstudiantesApi.Data;
using RegistroEstudiantesApi.Models;

namespace RegistroEstudiantesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstudianteMateriasController : ControllerBase
    {
        private readonly AppDbContext _context;
        public EstudianteMateriasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstudianteMateria>>> GetEstudianteMaterias()
        {
            return await _context.EstudianteMaterias
                .Include(em => em.Estudiante)
                .Include(em => em.Materia)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EstudianteMateria>> GetEstudianteMateria(int id)
        {
            var em = await _context.EstudianteMaterias
                .Include(x => x.Estudiante)
                .Include(x => x.Materia)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (em == null)
                return NotFound();
            return em;
        }

        [HttpPost]
        public async Task<ActionResult<EstudianteMateria>> PostEstudianteMateria(EstudianteMateria em)
        {
            _context.EstudianteMaterias.Add(em);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEstudianteMateria), new { id = em.Id }, em);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstudianteMateria(int id, EstudianteMateria em)
        {
            if (id != em.Id)
                return BadRequest();
            _context.Entry(em).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.EstudianteMaterias.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstudianteMateria(int id)
        {
            var em = await _context.EstudianteMaterias.FindAsync(id);
            if (em == null)
                return NotFound();
            _context.EstudianteMaterias.Remove(em);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
} 