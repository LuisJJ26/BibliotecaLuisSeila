namespace BibliotecaLuisSeila.Models.Settings
{
    public class JwtSettings
    {
        public string Key { get; set; } = null!;
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public int TokenValidityInMinutes { get; set; } = 15;
    }
}