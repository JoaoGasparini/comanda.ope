using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace comandaOpe.Data.Models
{
    [Table("Comanda")]
    public class Comanda:BaseEntitie
    {
        public int Id_Cliente { get; set; }
        public int id_Funcionario { get; set; }
        public int Id_Pedido { get; set; }
        public bool Status { get; set; }
        public string Forma_Pagamento { get; set; }
    
    }
}
