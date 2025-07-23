using System.Collections.Generic;

namespace RegistroEstudiantesApi.Models
{
    public class Materia
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Creditos { get; set; }
        public ICollection<EstudianteMateria> EstudianteMaterias { get; set; }
        public ICollection<ProfesorMateria> ProfesorMaterias { get; set; }
    }
} 