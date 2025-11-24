namespace BibliotecaLuisSeila.Dto
{
    public class LibroDto
    {
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Editorial { get; set; }
        public string Categoria { get; set; }
        public int AñoPublicacion { get; set; }
        public string ISBN { get; set; }
        public string Descripcion { get; set; }
        public int CantidadTotal { get; set; }
    }
}