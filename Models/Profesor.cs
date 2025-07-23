using System.Collections.Generic;

namespace RegistroEstudiantesApi.Models
{
    public class Profesor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public ICollection<ProfesorMateria> ProfesorMaterias { get; set; }
    }
} 