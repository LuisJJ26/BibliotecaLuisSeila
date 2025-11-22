namespace BibliotecaLuisSeila.Models
{
    public class Libros
    {
        public int Id { get; set; }                     // Primary Key
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Editorial { get; set; }
        public string Categoria { get; set; }
        public int AñoPublicacion { get; set; }
        public string ISBN { get; set; }                // identificador único
        public string Descripcion { get; set; }

        // Relación con Inventario (1 libro tiene 1 inventario)
        public Inventario Inventario { get; set; }

        // Relación: 1 libro puede tener muchos préstamos
        public ICollection<Prestamos> Prestamos { get; set; }
    }

}
