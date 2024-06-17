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
    public class EstadiosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EstadiosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Estadios
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Estadios.ToListAsync());
        }

      
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadio = await _context.Estadios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estadio == null)
            {
                return NotFound();
            }

            return View(estadio);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Localizacao")] Estadio estadio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estadio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estadio);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadio = await _context.Estadios.FindAsync(id);
            if (estadio == null)
            {
                return NotFound();
            }
            return View(estadio);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Localizacao")] Estadio estadio)
        {
            if (id != estadio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estadio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstadioExists(estadio.Id))
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
            return View(estadio);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadio = await _context.Estadios.FindAsync(id);
            if (estadio != null)
            {
                _context.Estadios.Remove(estadio);
            }
            else
            {
                return NotFound();

            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool EstadioExists(int id)
        {
            return _context.Estadios.Any(e => e.Id == id);
        }
    }
}
