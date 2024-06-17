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
    public class JogadoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JogadoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Jogadores
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Jogadores.Include(j => j.Equipa);
            return View(await applicationDbContext.ToListAsync());
        }

       
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jogador = await _context.Jogadores
                .Include(j => j.Equipa)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jogador == null)
            {
                return NotFound();
            }

            return View(jogador);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["EquipaId"] = new SelectList(_context.Equipas, "Id", "Nome");
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Idade,EquipaId")] Jogador jogador)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jogador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EquipaId"] = new SelectList(_context.Equipas, "Id", "Nome", jogador.EquipaId);
            return View(jogador);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jogador = await _context.Jogadores.FindAsync(id);
            if (jogador == null)
            {
                return NotFound();
            }
            ViewData["EquipaId"] = new SelectList(_context.Equipas, "Id", "Nome", jogador.EquipaId);
            return View(jogador);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Idade,EquipaId")] Jogador jogador)
        {
            if (id != jogador.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jogador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JogadorExists(jogador.Id))
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
            ViewData["EquipaId"] = new SelectList(_context.Equipas, "Id", "Nome", jogador.EquipaId);
            return View(jogador);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jogador = await _context.Jogadores.FindAsync(id);
            if (jogador != null)
            {
                _context.Jogadores.Remove(jogador);
            }
            else
            {
                return NotFound();

            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    

        private bool JogadorExists(int id)
        {
            return _context.Jogadores.Any(e => e.Id == id);
        }
    }
}
