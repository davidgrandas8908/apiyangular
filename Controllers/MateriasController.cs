using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistroEstudiantesApi.Data;
using RegistroEstudiantesApi.Models;

namespace RegistroEstudiantesApi.Controllers
{
    /// <summary>
    /// Controlador para la gestión de materias.
    /// Permite realizar operaciones CRUD sobre las materias.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class MateriasController : ControllerBase
    {
        private readonly AppDbContext _context;
        public MateriasController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene la lista de todas las materias registradas.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Materia>>> GetMaterias()
        {
            return await _context.Materias.ToListAsync();
        }

        /// <summary>
        /// Obtiene una materia por su identificador.
        /// </summary>
        /// <param name="id">Identificador de la materia</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<Materia>> GetMateria(int id)
        {
            var materia = await _context.Materias.FindAsync(id);
            if (materia == null)
                return NotFound();
            return materia;
        }

        /// <summary>
        /// Registra una nueva materia.
        /// </summary>
        /// <param name="materia">Datos de la materia a registrar</param>
        [HttpPost]
        public async Task<ActionResult<Materia>> PostMateria(Materia materia)
        {
            _context.Materias.Add(materia);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMateria), new { id = materia.Id }, materia);
        }

        /// <summary>
        /// Actualiza los datos de una materia existente.
        /// </summary>
        /// <param name="id">Identificador de la materia</param>
        /// <param name="materia">Datos actualizados de la materia</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMateria(int id, Materia materia)
        {
            if (id != materia.Id)
                return BadRequest();
            _context.Entry(materia).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Materias.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }
            return NoContent();
        }

        /// <summary>
        /// Elimina una materia por su identificador.
        /// </summary>
        /// <param name="id">Identificador de la materia</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMateria(int id)
        {
            var materia = await _context.Materias.FindAsync(id);
            if (materia == null)
                return NotFound();
            _context.Materias.Remove(materia);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
} 