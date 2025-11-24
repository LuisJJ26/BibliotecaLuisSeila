using BibliotecaLuisSeila.Models;

namespace BibliotecaLuisSeila.Services
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken();
        string GeneratePasswordResetToken();

    }
}
