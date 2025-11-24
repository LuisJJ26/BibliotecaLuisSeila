using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BibliotecaLuisSeila.Data;
using BibliotecaLuisSeila.Models;
using BibliotecaLuisSeila.Dto;

namespace BibliotecaLuisSeila.Controllers
{
    public class LibrosController : Controller
    {
        private readonly AppDbContext _context;

        public LibrosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Libros
        public async Task<IActionResult> Index()
        {
            var libros = await _context.Libros
                .Include(l => l.Inventario)
                .ToListAsync();
            return View(libros);
        }

        // GET: Libros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros
                .Include(l => l.Inventario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // GET: Libros/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Libros/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LibroDto libroDto)
        {
            if (ModelState.IsValid)
            {
                // Crear el libro
                var libro = new Libros
                {
                    Titulo = libroDto.Titulo,
                    Autor = libroDto.Autor,
                    Editorial = libroDto.Editorial,
                    Categoria = libroDto.Categoria,
                    AñoPublicacion = libroDto.AñoPublicacion,
                    ISBN = libroDto.ISBN,
                    Descripcion = libroDto.Descripcion
                };

                _context.Add(libro);
                await _context.SaveChangesAsync();

                // Crear el inventario asociado
                var inventario = new Inventario
                {
                    LibroId = libro.Id,
                    CantidadTotal = libroDto.CantidadTotal,
                    CantidadDisponible = libroDto.CantidadTotal,
                    CantidadPrestada = 0,
                    ÚltimaActualizacion = DateTime.Now
                };

                _context.Add(inventario);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(libroDto);
        }

        // GET: Libros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros
                .Include(l => l.Inventario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (libro == null)
            {
                return NotFound();
            }
            return View(libro);
        }

        // POST: Libros/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Libros libro)
        {
            if (id != libro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(libro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibroExists(libro.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(libro);
        }

        // GET: Libros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros
                .Include(l => l.Inventario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // POST: Libros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var libro = await _context.Libros.FindAsync(id);
            if (libro != null)
            {
                // Eliminar también el inventario asociado
                var inventario = await _context.Inventario.FirstOrDefaultAsync(i => i.LibroId == id);
                if (inventario != null)
                {
                    _context.Inventario.Remove(inventario);
                }
                _context.Libros.Remove(libro);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LibroExists(int id)
        {
            return _context.Libros.Any(e => e.Id == id);
        }
    }
}