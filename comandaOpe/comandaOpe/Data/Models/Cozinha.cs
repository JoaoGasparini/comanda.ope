using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace comandaOpe.Data.Models
{
    [Table("Cozinha")]
    public class Cozinha:BaseEntitie
    {
        public int Id_Pedido { get; set; }
        public bool status { get; set; }
    }
}
