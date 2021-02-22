using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace comandaOpe.Data.Models
{
    [Table("Produto")]
    public class Produto : BaseEntitie
    {
        public string Nome { get; set; }
        public DateTime? Validade { get; set; }
        public string Fornecedor { get; set; }
        public string Categoria { get; set; }
        
    }
}
