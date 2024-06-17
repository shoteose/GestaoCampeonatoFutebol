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
    public class EquipasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EquipasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Equipas
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Equipas.Include(e => e.Clube);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Equipas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipa = await _context.Equipas
                .Include(e => e.Clube)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (equipa == null)
            {
                return NotFound();
            }

            return View(equipa);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["ClubeId"] = new SelectList(_context.Clubes, "Id", "Nome");
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,ClubeId")] Equipa equipa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(equipa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClubeId"] = new SelectList(_context.Clubes, "Id", "Nome", equipa.ClubeId);
            return View(equipa);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipa = await _context.Equipas.FindAsync(id);
            if (equipa == null)
            {
                return NotFound();
            }
            ViewData["ClubeId"] = new SelectList(_context.Clubes, "Id", "Nome", equipa.ClubeId);
            return View(equipa);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,ClubeId")] Equipa equipa)
        {
            if (id != equipa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(equipa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipaExists(equipa.Id))
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
            ViewData["ClubeId"] = new SelectList(_context.Clubes, "Id", "Nome", equipa.ClubeId);
            return View(equipa);
            
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipa = await _context.Equipas.FindAsync(id);
            if (equipa != null)
            {
                _context.Equipas.Remove(equipa);
            }
            else
            {
                return NotFound();

            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool EquipaExists(int id)
        {
            return _context.Equipas.Any(e => e.Id == id);
        }
    }
}
