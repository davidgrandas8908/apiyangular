using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistroEstudiantesApi.Data;
using RegistroEstudiantesApi.Models;

namespace RegistroEstudiantesApi.Controllers
{
    /// <summary>
    /// Controlador para la gestión de estudiantes.
    /// Permite realizar operaciones CRUD sobre los estudiantes.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class EstudiantesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public EstudiantesController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene la lista de todos los estudiantes registrados.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estudiante>>> GetEstudiantes()
        {
            return await _context.Estudiantes.ToListAsync();
        }

        /// <summary>
        /// Obtiene un estudiante por su identificador.
        /// </summary>
        /// <param name="id">Identificador del estudiante</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<Estudiante>> GetEstudiante(int id)
        {
            var estudiante = await _context.Estudiantes.FindAsync(id);
            if (estudiante == null)
                return NotFound();
            return estudiante;
        }

        /// <summary>
        /// Registra un nuevo estudiante.
        /// </summary>
        /// <param name="estudiante">Datos del estudiante a registrar</param>
        [HttpPost]
        public async Task<ActionResult<Estudiante>> PostEstudiante(Estudiante estudiante)
        {
            _context.Estudiantes.Add(estudiante);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEstudiante), new { id = estudiante.Id }, estudiante);
        }

        /// <summary>
        /// Actualiza los datos de un estudiante existente.
        /// </summary>
        /// <param name="id">Identificador del estudiante</param>
        /// <param name="estudiante">Datos actualizados del estudiante</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstudiante(int id, Estudiante estudiante)
        {
            if (id != estudiante.Id)
                return BadRequest();
            _context.Entry(estudiante).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Estudiantes.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }
            return NoContent();
        }

        /// <summary>
        /// Elimina un estudiante por su identificador.
        /// </summary>
        /// <param name="id">Identificador del estudiante</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstudiante(int id)
        {
            var estudiante = await _context.Estudiantes.FindAsync(id);
            if (estudiante == null)
                return NotFound();
            _context.Estudiantes.Remove(estudiante);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
} 