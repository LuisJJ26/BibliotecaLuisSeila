namespace BibliotecaLuisSeila.Models
{
    public class Estudiantes
    {
        public int Id { get; set; }                  // Primary Key
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Carnet { get; set; }           // Código único del estudiante
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Carrera { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        // Relación: Un estudiante puede tener muchos préstamos
        public ICollection<Prestamos> Prestamos { get; set; }
    }

}
