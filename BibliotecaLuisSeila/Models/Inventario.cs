namespace BibliotecaLuisSeila.Models
{
    public class Inventario
    {
        public int Id { get; set; }                      // Primary Key

        // Relación con libros
        public int LibroId { get; set; }
        public Libros Libro { get; set; }

        public int CantidadTotal { get; set; }
        public int CantidadDisponible { get; set; }
        public int CantidadPrestada { get; set; }

        public DateTime ÚltimaActualizacion { get; set; } = DateTime.Now;
    }
}
