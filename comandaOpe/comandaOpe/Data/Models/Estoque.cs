using System.ComponentModel.DataAnnotations.Schema;

namespace comandaOpe.Data.Models
{
    [Table("estoque")]
    public class Estoque:BaseEntitie
    {
        public string produto { get; set; }
        public string fornecedor { get; set; }
        public int? quantidade { get; set; }
        public string categoria { get; set; }

    }
}
