using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GestaoCampeonatoFutebol.Models
{
    public class Perfil
    {
        [Key]
        public int Id { get; set; }

        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }

        public string? UserId { get; set; }


        public IdentityUser? User { get; set; }

    }
}
