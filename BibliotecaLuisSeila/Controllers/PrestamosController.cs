using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BibliotecaLuisSeila.Data;
using BibliotecaLuisSeila.Models;
using BibliotecaLuisSeila.Dto;

namespace BibliotecaLuisSeila.Controllers
{
    public class PrestamosController : Controller
    {
        private readonly AppDbContext _context;

        public PrestamosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Prestamos
        public async Task<IActionResult> Index()
        {
            var prestamos = await _context.Prestamos
                .Include(p => p.Estudiante)
                .Include(p => p.Libro)
                .ThenInclude(l => l.Inventario)
                .OrderByDescending(p => p.FechaPrestamo)
                .ToListAsync();
            return View(prestamos);
        }

        // GET: Prestamos/Create
        public async Task<IActionResult> Create()
        {
            await CargarListasDesplegables();
            return View();
        }

        // POST: Prestamos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PrestamoDto prestamoDto)
        {
            if (ModelState.IsValid)
            {
                // Verificar disponibilidad
                var inventario = await _context.Inventario
                    .FirstOrDefaultAsync(i => i.LibroId == prestamoDto.LibroId);

                if (inventario == null || inventario.CantidadDisponible <= 0)
                {
                    ModelState.AddModelError("", "El libro no está disponible para préstamo");
                    await CargarListasDesplegables();
                    return View(prestamoDto);
                }

                // Verificar que el estudiante existe
                var estudiante = await _context.Estudiantes.FindAsync(prestamoDto.EstudianteId);
                if (estudiante == null)
                {
                    ModelState.AddModelError("", "El estudiante seleccionado no existe");
                    await CargarListasDesplegables();
                    return View(prestamoDto);
                }

                // Crear préstamo
                var prestamo = new Prestamos
                {
                    EstudianteId = prestamoDto.EstudianteId,
                    LibroId = prestamoDto.LibroId,
                    FechaPrestamo = DateTime.Now,
                    FechaVencimiento = prestamoDto.FechaVencimiento,
                    FechaDevolucion = null,
                    EstaDevuelto = false,
                    Observaciones = prestamoDto.Observaciones
                };

                // Actualizar inventario
                inventario.CantidadDisponible--;
                inventario.CantidadPrestada++;
                inventario.ÚltimaActualizacion = DateTime.Now;

                _context.Add(prestamo);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Préstamo registrado exitosamente";
                return RedirectToAction(nameof(Index));
            }

            await CargarListasDesplegables();
            return View(prestamoDto);
        }

        // POST: Prestamos/Devolver/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Devolver(int id)
        {
            var prestamo = await _context.Prestamos
                .Include(p => p.Libro)
                .ThenInclude(l => l.Inventario)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (prestamo == null)
            {
                return NotFound();
            }

            if (!prestamo.EstaDevuelto)
            {
                prestamo.EstaDevuelto = true;
                prestamo.FechaDevolucion = DateTime.Now;

                // Actualizar inventario
                var inventario = prestamo.Libro.Inventario;
                if (inventario != null)
                {
                    inventario.CantidadDisponible++;
                    inventario.CantidadPrestada--;
                    inventario.ÚltimaActualizacion = DateTime.Now;
                }

                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Libro devuelto exitosamente";
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Prestamos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prestamo = await _context.Prestamos
                .Include(p => p.Estudiante)
                .Include(p => p.Libro)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prestamo == null)
            {
                return NotFound();
            }

            return View(prestamo);
        }

        // POST: Prestamos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prestamo = await _context.Prestamos
                .Include(p => p.Libro)
                .ThenInclude(l => l.Inventario)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (prestamo != null)
            {
                // Si el préstamo no estaba devuelto, actualizar el inventario
                if (!prestamo.EstaDevuelto && prestamo.Libro?.Inventario != null)
                {
                    var inventario = prestamo.Libro.Inventario;
                    inventario.CantidadDisponible++;
                    inventario.CantidadPrestada--;
                    inventario.ÚltimaActualizacion = DateTime.Now;
                }

                _context.Prestamos.Remove(prestamo);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Préstamo eliminado exitosamente";
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Prestamos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prestamo = await _context.Prestamos
                .Include(p => p.Estudiante)
                .Include(p => p.Libro)
                .ThenInclude(l => l.Inventario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prestamo == null)
            {
                return NotFound();
            }

            return View(prestamo);
        }

        private bool PrestamoExists(int id)
        {
            return _context.Prestamos.Any(e => e.Id == id);
        }

        // Método auxiliar para cargar las listas desplegables
        private async Task CargarListasDesplegables()
        {
            // Cargar estudiantes como SelectListItem
            var estudiantes = await _context.Estudiantes
                .OrderBy(e => e.Nombre)
                .ThenBy(e => e.Apellido)
                .ToListAsync();

            ViewBag.Estudiantes = estudiantes.Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = $"{e.Nombre} {e.Apellido} - {e.Carnet}"
            }).ToList();

            // Cargar libros disponibles como SelectListItem
            var libros = await _context.Libros
                .Include(l => l.Inventario)
                .Where(l => l.Inventario != null && l.Inventario.CantidadDisponible > 0)
                .OrderBy(l => l.Titulo)
                .ToListAsync();

            ViewBag.Libros = libros.Select(l => new SelectListItem
            {
                Value = l.Id.ToString(),
                Text = $"{l.Titulo} - {l.Autor} (Disponibles: {l.Inventario?.CantidadDisponible})"
            }).ToList();
        }
    }
}