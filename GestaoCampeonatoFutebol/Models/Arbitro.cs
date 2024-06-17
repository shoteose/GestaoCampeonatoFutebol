using System.ComponentModel.DataAnnotations;

namespace GestaoCampeonatoFutebol.Models
{
    public class Arbitro
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set;}
    }
}
