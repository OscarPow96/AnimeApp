﻿using System;
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
    public class DirectoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DirectoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Director
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Directores.Include(d => d.Pais);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Director/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var director = await _context.Directores
                .Include(d => d.Pais)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (director == null)
            {
                return NotFound();
            }

            return View(director);
        }

        // GET: Director/Create
        public IActionResult Create()
        {
            ViewData["PaisId"] = new SelectList(_context.Paises, "Id", "Nombre");
            return View();
        }

        // POST: Director/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido,PaisId")] Director director)
        {
            //if (ModelState.IsValid)
            //{
                _context.Add(director);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            ViewData["PaisId"] = new SelectList(_context.Paises, "Id", "Nombre", director.PaisId);
            return View(director);
        }

        // GET: Director/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var director = await _context.Directores.FindAsync(id);
            if (director == null)
            {
                return NotFound();
            }
            ViewData["PaisId"] = new SelectList(_context.Paises, "Id", "Nombre", director.PaisId);
            return View(director);
        }

        // POST: Director/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellido,PaisId")] Director director)
        {
            if (id != director.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(director);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DirectorExists(director.Id))
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
            ViewData["PaisId"] = new SelectList(_context.Paises, "Id", "Nombre", director.PaisId);
            return View(director);
        }

        // GET: Director/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var director = await _context.Directores
                .Include(d => d.Pais)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (director == null)
            {
                return NotFound();
            }

            return View(director);
        }

        // POST: Director/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var director = await _context.Directores.FindAsync(id);
            if (director != null)
            {
                _context.Directores.Remove(director);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DirectorExists(int id)
        {
            return _context.Directores.Any(e => e.Id == id);
        }
    }
}
