using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class InscripcionesController : Controller
    {
        private readonly PlataformaCursosContext _context;

        public InscripcionesController(PlataformaCursosContext context)
        {
            _context = context;
        }

        // GET: Inscripciones
        public async Task<IActionResult> Index()
        {
            var plataformaCursosContext = _context.Inscripciones.Include(i => i.IdCursoNavigation).Include(i => i.IdUsuarioNavigation);
            return View(await plataformaCursosContext.ToListAsync());
        }

        // GET: Inscripciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inscripcione = await _context.Inscripciones
                .Include(i => i.IdCursoNavigation)
                .Include(i => i.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inscripcione == null)
            {
                return NotFound();
            }

            return View(inscripcione);
        }

        // GET: Inscripciones/Create
        public IActionResult Create()
        {
            ViewData["IdCurso"] = new SelectList(_context.Cursos, "Id", "Titulo");
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "Id", "Id");
            return View();
        }

        // POST: Inscripciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdUsuario,IdCurso,FechaInscripcion,Estado")] Inscripcione inscripcione)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(inscripcione);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "Pagoes");
            }
            ViewData["IdCurso"] = new SelectList(_context.Cursos, "Id", "Id", inscripcione.IdCurso);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "Id", "Id", inscripcione.IdUsuario);
            return View(inscripcione);
        }

        // GET: Inscripciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inscripcione = await _context.Inscripciones.FindAsync(id);
            if (inscripcione == null)
            {
                return NotFound();
            }
            ViewData["IdCurso"] = new SelectList(_context.Cursos, "Id", "Id", inscripcione.IdCurso);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "Id", "Id", inscripcione.IdUsuario);
            return View(inscripcione);
        }

        // POST: Inscripciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdUsuario,IdCurso,FechaInscripcion,Estado")] Inscripcione inscripcione)
        {
            if (id != inscripcione.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(inscripcione);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InscripcioneExists(inscripcione.Id))
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
            ViewData["IdCurso"] = new SelectList(_context.Cursos, "Id", "Id", inscripcione.IdCurso);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "Id", "Id", inscripcione.IdUsuario);
            return View(inscripcione);
        }

        // GET: Inscripciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inscripcione = await _context.Inscripciones
                .Include(i => i.IdCursoNavigation)
                .Include(i => i.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inscripcione == null)
            {
                return NotFound();
            }

            return View(inscripcione);
        }

        // POST: Inscripciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inscripcione = await _context.Inscripciones.FindAsync(id);
            if (inscripcione != null)
            {
                _context.Inscripciones.Remove(inscripcione);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InscripcioneExists(int id)
        {
            return _context.Inscripciones.Any(e => e.Id == id);
        }
    }
}
