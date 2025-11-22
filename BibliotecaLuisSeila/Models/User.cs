namespace BibliotecaLuisSeila.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!; // Hasheada
        public bool EmailConfirmed { get; set; } = false;
        public string? EmailConfirmationToken { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public string? PasswordResetToken { get; set; }
        public DateTime? ResetTokenExpiryTime { get; set; }

        // Clave foránea
        public int RoleId { get; set; }

        // Propiedad de navegación
        public virtual Role Role { get; set; } = null!;
    
    }
}