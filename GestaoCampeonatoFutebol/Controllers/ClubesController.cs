using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestaoCampeonatoFutebol.Data;
using GestaoCampeonatoFutebol.Models;
using Microsoft.AspNetCore.Authorization;

namespace GestaoCampeonatoFutebol.Controllers
{
    public class ClubesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClubesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Clubes.Include(c => c.Estadio);
            return View(await applicationDbContext.ToListAsync());
        }

      
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clube = await _context.Clubes
                .Include(c => c.Estadio)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clube == null)
            {
                return NotFound();
            }

            return View(clube);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["EstadioId"] = new SelectList(_context.Estadios, "Id", "Nome");
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Presidente,EstadioId")] Clube clube)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clube);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EstadioId"] = new SelectList(_context.Estadios, "Id", "Nome", clube.EstadioId);
            return View(clube);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clube = await _context.Clubes.FindAsync(id);
            if (clube == null)
            {
                return NotFound();
            }
            ViewData["EstadioId"] = new SelectList(_context.Estadios, "Id", "Nome", clube.EstadioId);
            return View(clube);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Presidente,EstadioId")] Clube clube)
        {
            if (id != clube.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clube);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClubeExists(clube.Id))
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
            ViewData["EstadioId"] = new SelectList(_context.Estadios, "Id", "Nome", clube.EstadioId);
            
            return View(clube);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clube = await _context.Clubes.FindAsync(id);
            if (clube != null)
            {
                _context.Clubes.Remove(clube);
            }
            else
            {
                return NotFound();
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool ClubeExists(int id)
        {
            return _context.Clubes.Any(e => e.Id == id);
        }
    }
}
