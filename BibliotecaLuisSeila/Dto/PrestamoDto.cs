namespace BibliotecaLuisSeila.Dto
{
    public class PrestamoDto
    {
        public int EstudianteId { get; set; }
        public int LibroId { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string Observaciones { get; set; }
    }
}