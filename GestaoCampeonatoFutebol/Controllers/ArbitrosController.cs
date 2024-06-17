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
    public class ArbitrosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArbitrosController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Arbitros.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var arbitro = await _context.Arbitros
                .FirstOrDefaultAsync(m => m.Id == id);
            if (arbitro == null)
            {
                return NotFound();
            }

            return View(arbitro);
        }

        
        [Authorize(Roles = "Admin" )]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Idade")] Arbitro arbitro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(arbitro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(arbitro);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var arbitro = await _context.Arbitros.FindAsync(id);
            if (arbitro == null)
            {
                return NotFound();
            }
            return View(arbitro);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Idade")] Arbitro arbitro)
        {
            if (id != arbitro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(arbitro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArbitroExists(arbitro.Id))
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
            return View(arbitro);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var arbitro = await _context.Arbitros.FindAsync(id);
            if (arbitro != null)
            {
                _context.Arbitros.Remove(arbitro);
            }
            else
            {
                return NotFound();
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        
        private bool ArbitroExists(int id)
        {
            return _context.Arbitros.Any(e => e.Id == id);
        }
    }
}
