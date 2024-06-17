using GestaoCampeonatoFutebol.Data;
using GestaoCampeonatoFutebol.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GestaoCampeonatoFutebol.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            HomeIndexViewModel model = new HomeIndexViewModel()
            {
               //Não têm os includes para n estar a buscar dados duas vezes a DB
               //Pois aqui já pegamos em todos os dados que lá estão
                Arbitros = _context.Arbitros.ToList(),
                Clubes = _context.Clubes.ToList(),
                Equipas = _context.Equipas.ToList(),
                Estadios = _context.Estadios.ToList(),
                Jogadores = _context.Jogadores.ToList(),
                Jogos = _context.Jogos.ToList(),
            };


            //Assim populamos todos os dados para a View
            foreach (var clube in model.Clubes)
            {
                clube.Estadio = model.Estadios.Find(e => e.Id == clube.EstadioId);
            }

            foreach (var equipa in model.Equipas)
            {
                equipa.Clube = model.Clubes.Find(c => c.Id == equipa.ClubeId);
            }

            foreach (var jogador in model.Jogadores)
            {
                jogador.Equipa = model.Equipas.Find(e => e.Id == jogador.EquipaId);
            }

            foreach (var jogo in model.Jogos)
            {
                jogo.EquipaOne = model.Equipas.Find(e => e.Id == jogo.EquipaOneId);
                jogo.EquipaTwo = model.Equipas.Find(e => e.Id == jogo.EquipaTwoId);

            }

            return View(model);
        }


        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> AdminDashboard()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
