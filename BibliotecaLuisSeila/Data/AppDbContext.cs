using Microsoft.EntityFrameworkCore;
using BibliotecaLuisSeila.Models;

namespace BibliotecaLuisSeila.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // EXISTENTES
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        // NUEVOS MODELOS DE LA BIBLIOTECA
        public DbSet<Estudiantes> Estudiantes { get; set; }
        public DbSet<Libros> Libros { get; set; }
        public DbSet<Inventario> Inventario { get; set; }
        public DbSet<Prestamos> Prestamos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed Roles
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "Usuario" }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
