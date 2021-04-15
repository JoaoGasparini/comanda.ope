using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace comandaOpe.Data.Models
{
    [Table("produto")]
    public class Produto : BaseEntitie
    {
        public string descricao { get; set; }
        public double preco { get; set; }
        public string categoria { get; set; }
        
    }
}
