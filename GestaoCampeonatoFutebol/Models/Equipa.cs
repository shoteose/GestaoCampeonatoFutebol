using System.ComponentModel.DataAnnotations;

namespace GestaoCampeonatoFutebol.Models
{
    public class Equipa
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public int ClubeId { get; set; }
        public Clube? Clube { get; set; }

    }
}
