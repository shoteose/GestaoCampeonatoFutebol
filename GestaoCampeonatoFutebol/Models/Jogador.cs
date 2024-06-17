using System.ComponentModel.DataAnnotations;

namespace GestaoCampeonatoFutebol.Models
{
    public class Jogador
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "É obrigatorio")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "É obrigatorio")]
        public int Idade {  get; set; }

        public int EquipaId { get; set; }
        public Equipa? Equipa { get; set; }
    }
}
