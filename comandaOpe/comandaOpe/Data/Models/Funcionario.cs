using System.ComponentModel.DataAnnotations.Schema;

namespace comandaOpe.Data.Models
{
    [Table("Funcionario")]
    public class Funcionario :BaseEntitie
    {
        public string Nome { get; set; }
        public string Cargo { get; set; }
  
    }
}
