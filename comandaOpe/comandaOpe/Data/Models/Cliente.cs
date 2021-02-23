using System.ComponentModel.DataAnnotations.Schema;

namespace comandaOpe.Data.Models
{
    [Table("cliente")]
    public class Cliente: BaseEntitie
    {
        public string cpf { get; set; }
        public string nome { get; set; }
  
  
    }
}
