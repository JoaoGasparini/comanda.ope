using System.ComponentModel.DataAnnotations.Schema;

namespace comandaOpe.Data.Models
{
    [Table("funcionario")]
    public class Funcionario :BaseEntitie
    {
        public string nome { get; set; }
        public string cargo { get; set; }
  
    }
}
