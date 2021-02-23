using System.ComponentModel.DataAnnotations.Schema;

namespace comandaOpe.Data.Models
{
    [Table("estoque")]
    public class Estoque:BaseEntitie
    {
        public int id_produto { get; set; }
        public int? quantidade { get; set; }
        
    }
}
