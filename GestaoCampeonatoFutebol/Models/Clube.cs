using System.ComponentModel.DataAnnotations;

namespace GestaoCampeonatoFutebol.Models
{
    public class Clube
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Presidente { get; set; }

        public int EstadioId { get; set; }
        public Estadio? Estadio { get; set; }
    }
}
