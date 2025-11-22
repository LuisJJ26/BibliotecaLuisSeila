namespace BibliotecaLuisSeila.Models
{
    public class Prestamos
    {
        public int Id { get; set; }                          // Primary Key

        // Estudiante
        public int EstudianteId { get; set; }
        public Estudiantes Estudiante { get; set; }

        // Libro
        public int LibroId { get; set; }
        public Libros Libro { get; set; }

        public DateTime FechaPrestamo { get; set; } = DateTime.Now;
        public DateTime FechaVencimiento { get; set; }       // Fecha límite
        public DateTime? FechaDevolucion { get; set; }       // Nulo hasta que se devuelve

        public bool EstaDevuelto { get; set; }               // True/false

        public string Observaciones { get; set; }
    }
}
