using GestaoCampeonatoFutebol.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GestaoCampeonatoFutebol.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Clube>? Clubes { get; set; }
        public DbSet<Jogo>? Jogos { get; set; }
        public DbSet<Equipa>? Equipas { get; set; }
        public DbSet<Arbitro>? Arbitros { get; set; }
        public DbSet<Estadio>? Estadios { get; set; }
        public DbSet<Jogador>? Jogadores { get; set; }
        public DbSet<Perfil>? Perfis { get; set; }

    }
}
