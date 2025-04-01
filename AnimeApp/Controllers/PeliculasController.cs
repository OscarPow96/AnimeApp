using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AnimeApp.Data;
using AnimeApp.Models;

namespace AnimeApp.Controllers
{
    public class PeliculasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PeliculasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pelicula
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Peliculas.Include(p => p.Director).Include(p => p.Genero).Include(p => p.Pais);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Pelicula/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas
                .Include(p => p.Director)
                .Include(p => p.Genero)
                .Include(p => p.Pais)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pelicula == null)
            {
                return NotFound();
            }

            return View(pelicula);
        }

        // GET: Pelicula/Create
        public IActionResult Create()
        {
            ViewData["DirectorId"] = new SelectList(_context.Directores, "Id", "Apellido");
            ViewData["GeneroId"] = new SelectList(_context.Generos, "Id", "Nombre");
            ViewData["PaisId"] = new SelectList(_context.Paises, "Id", "Nombre");
            return View();
        }

        // POST: Pelicula/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Resena,ImagenPortadaUrl,CodigoTrailer,GeneroId,PaisId,DirectorId")] Pelicula pelicula)
        {
            //if (ModelState.IsValid)
            //{
                _context.Add(pelicula);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            ViewData["DirectorId"] = new SelectList(_context.Directores, "Id", "Apellido", pelicula.DirectorId);
            ViewData["GeneroId"] = new SelectList(_context.Generos, "Id", "Nombre", pelicula.GeneroId);
            ViewData["PaisId"] = new SelectList(_context.Paises, "Id", "Nombre", pelicula.PaisId);
            return View(pelicula);
        }

        // GET: Pelicula/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas.FindAsync(id);
            if (pelicula == null)
            {
                return NotFound();
            }
            ViewData["DirectorId"] = new SelectList(_context.Directores, "Id", "Apellido", pelicula.DirectorId);
            ViewData["GeneroId"] = new SelectList(_context.Generos, "Id", "Nombre", pelicula.GeneroId);
            ViewData["PaisId"] = new SelectList(_context.Paises, "Id", "Nombre", pelicula.PaisId);
            return View(pelicula);
        }

        // POST: Pelicula/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Resena,ImagenPortadaUrl,CodigoTrailer,GeneroId,PaisId,DirectorId")] Pelicula pelicula)
        {
            if (id != pelicula.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(pelicula);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeliculaExists(pelicula.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            //}
            ViewData["DirectorId"] = new SelectList(_context.Directores, "Id", "Apellido", pelicula.DirectorId);
            ViewData["GeneroId"] = new SelectList(_context.Generos, "Id", "Nombre", pelicula.GeneroId);
            ViewData["PaisId"] = new SelectList(_context.Paises, "Id", "Nombre", pelicula.PaisId);
            return View(pelicula);
        }

        // GET: Pelicula/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas
                .Include(p => p.Director)
                .Include(p => p.Genero)
                .Include(p => p.Pais)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pelicula == null)
            {
                return NotFound();
            }

            return View(pelicula);
        }

        // POST: Pelicula/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pelicula = await _context.Peliculas.FindAsync(id);
            if (pelicula != null)
            {
                _context.Peliculas.Remove(pelicula);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PeliculaExists(int id)
        {
            return _context.Peliculas.Any(e => e.Id == id);
        }
    }
}
