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
    public class JogosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JogosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Jogos
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Jogos.Include(j => j.EquipaOne).Include(j => j.EquipaTwo);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Jogos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jogo = await _context.Jogos
                .Include(j => j.EquipaOne)
                .Include(j => j.EquipaTwo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jogo == null)
            {
                return NotFound();
            }

            return View(jogo);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["EquipaOneId"] = new SelectList(_context.Equipas, "Id", "Nome");
            ViewData["EquipaTwoId"] = new SelectList(_context.Equipas, "Id", "Nome");
            List<string> types = new List<string>() { "Agendado", "Progresso", "Equipa 1 ganhou", "Equipa 2 ganhou", "Empate", "Cancelado" };
            ViewData["Types"] = new SelectList(types, "Agendado");
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DataHora,EquipaOneId,EquipaTwoId,Resultado")] Jogo jogo)
        {

            if (jogo.DataHora < DateTime.Now)
            {
                ModelState.AddModelError("DataHora", "Não é possivel criar um jogo para uma data já ultrapassada");
            }

            if(jogo.EquipaOneId == jogo.EquipaTwoId)
            {
                ModelState.AddModelError("DataHora", "Não é possivel criar um jogo com duas equipas sendo a mesma");

            }
           


            if (ModelState.IsValid)
            {
                _context.Add(jogo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EquipaOneId"] = new SelectList(_context.Equipas, "Id", "Nome", jogo.EquipaOneId);
            ViewData["EquipaTwoId"] = new SelectList(_context.Equipas, "Id", "Nome", jogo.EquipaTwoId);
            List<string> types = new List<string>() { "Agendado", "Progresso", "Equipa 1 ganhou", "Equipa 2 ganhou", "Empate", "Cancelado" };
            ViewData["Types"] = new SelectList(types, "Agendado");
            return View(jogo);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {

            
            if (id == null)
            {
                return NotFound();
            }

            var jogo = await _context.Jogos.FindAsync(id);
            if (jogo == null)
            {
                return NotFound();
            }
            ViewData["EquipaOneId"] = new SelectList(_context.Equipas, "Id", "Nome", jogo.EquipaOneId);
            ViewData["EquipaTwoId"] = new SelectList(_context.Equipas, "Id", "Nome", jogo.EquipaTwoId);
            List<string> types = new List<string>() { "Agendado", "Progresso", "Equipa 1 ganhou", "Equipa 2 ganhou", "Empate","Cancelado" };
            ViewData["Types"] = new SelectList(types, jogo.Resultado);
            return View(jogo);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DataHora,EquipaOneId,EquipaTwoId,Resultado")] Jogo jogo)
        {
            List<string> types = new List<string>() { "Agendado", "Progresso", "Equipa 1 ganhou", "Equipa 2 ganhou", "Empate", "Cancelado" };

            if (id != jogo.Id)
            {
                return NotFound();
            }

            if(jogo.EquipaOneId == jogo.EquipaTwoId)
            {
                ModelState.AddModelError("EquipaTwoId", "Não pode ser a mesma equipa contra a mesma equipa");
            }

            if (!types.Contains(jogo.Resultado))
            {
                ModelState.AddModelError("Resultado", "O resultado não é válido");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jogo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JogoExists(jogo.Id))
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
            ViewData["EquipaOneId"] = new SelectList(_context.Equipas, "Id", "Nome", jogo.EquipaOneId);
            ViewData["EquipaTwoId"] = new SelectList(_context.Equipas, "Id", "Nome", jogo.EquipaTwoId);
            ViewData["Types"] = new SelectList(types, "Agendado");
            return View(jogo);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jogo = await _context.Jogos.FindAsync(id);
            if (jogo != null)
            {
                _context.Jogos.Remove(jogo);
            }
            else
            {
                return NotFound();
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool JogoExists(int id)
        {
            return _context.Jogos.Any(e => e.Id == id);
        }
    }
}
