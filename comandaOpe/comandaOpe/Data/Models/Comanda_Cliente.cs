using System.ComponentModel.DataAnnotations.Schema;

namespace comandaOpe.Data.Models
{
    [Table("comanda_cliente")]
    public class Comanda_Cliente : BaseEntitie
    {
        public int id_comanda { get; set; }
        public int id_cliente { get; set; }
        public bool status { get; set; }
 
	}
}
