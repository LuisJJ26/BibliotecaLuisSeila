using BibliotecaLuisSeila.Data;
using BibliotecaLuisSeila.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaLuisSeila.Repository
{
    public class RolesRepository(AppDbContext context) : IRolesRepository
    {
        private readonly AppDbContext _context = context;


        public async Task ActualizarAsync(Role role)
        {
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
        }

        public async Task<Role> AgregarAsync(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task EliminarAsync(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role != null)
            {
                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Role?> ObtenerPorIdAsync(int id)
        {
            return await _context.Roles.FindAsync(id);
        }

        public async Task<Role?> ObtenerPorNombreAsync(string nombre)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Name == nombre); //FirstOrDefaultAsync
        }

        public async Task<IEnumerable<Role>> ObtenerTodosAsync()
        {
            return await _context.Roles.ToListAsync();
        }
    }
}