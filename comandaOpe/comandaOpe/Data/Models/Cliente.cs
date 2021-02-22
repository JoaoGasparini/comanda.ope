using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace comandaOpe.Data.Models
{
    [Table("Cliente")]
    public class Cliente: BaseEntitie
    {
        public string Cpf { get; set; }
        public string Nome { get; set; }
  
  
    }
}
