using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoCampeonatoFutebol.Models
{
    public class Jogo
    {
        [Key]
        public int Id { get; set; }
        public DateTime DataHora { get; set; }

        [Required]
        [ForeignKey("EquipaOne")]
        public int EquipaOneId { get; set; }


        [Required]
        [ForeignKey("EquipaTwo")]
        public int EquipaTwoId { get; set; }

        public Equipa? EquipaOne { get; set; }
        public Equipa? EquipaTwo { get; set; }

        public string? Resultado { get; set; }
    }
}
