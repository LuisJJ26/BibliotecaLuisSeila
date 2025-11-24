using BibliotecaLuisSeila.Data;
using BibliotecaLuisSeila.Models;

namespace BibliotecaLuisSeila.Repository
{
    public class UsuarioRepository(AppDbContext context) : IUserRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<User> AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _context.Users.FindAsync(email);
        }

        public async Task<User?> GetUserByRefreshTokenAsync(string refreshToken)
        {
            return await _context.Users.FindAsync(refreshToken);
        }

        public async Task<User?> GetUserByUserName(string userName)
        {
            return await _context.Users.FindAsync(userName);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

 

        public bool ValidatePassword(User user, string password)
        {
            return user.Password == password;
        }
    }
}
