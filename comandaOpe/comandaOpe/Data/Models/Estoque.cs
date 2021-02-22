using System.ComponentModel.DataAnnotations.Schema;

namespace comandaOpe.Data.Models
{
    [Table("Estoque")]
    public class Estoque:BaseEntitie
    {
        public int Id_Produto { get; set; }
        public int? Quantidade { get; set; }
        
    }
}
