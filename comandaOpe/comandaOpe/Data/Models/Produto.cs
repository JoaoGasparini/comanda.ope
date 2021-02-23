using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace comandaOpe.Data.Models
{
    [Table("produto")]
    public class Produto : BaseEntitie
    {
        public string nome { get; set; }
        public DateTime? validade { get; set; }
        public string fornecedor { get; set; }
        public string categoria { get; set; }
        
    }
}
