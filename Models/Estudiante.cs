using System.Collections.Generic;

namespace RegistroEstudiantesApi.Models
{
    public class Estudiante
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public ICollection<EstudianteMateria> EstudianteMaterias { get; set; }
    }
} 