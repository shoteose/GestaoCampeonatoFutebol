using System.ComponentModel.DataAnnotations;

namespace GestaoCampeonatoFutebol.Models
{
    public class Estadio
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Localizacao { get; set; }
    }
}
