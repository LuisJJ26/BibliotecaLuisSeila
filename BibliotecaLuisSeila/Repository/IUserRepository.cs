using BibliotecaLuisSeila.Models;

namespace BibliotecaLuisSeila.Repository
{
    public interface IUserRepository
    {
        Task<User?> GetUserByUserName(string userName);
        Task<User?> GetUserByEmail(string email);
        Task<User> AddAsync(User user);
        bool ValidatePassword(User user, string password);
        Task SaveAsync();
        Task<User?> GetUserByRefreshTokenAsync(string refreshToken);
    }
}