using System.ComponentModel.DataAnnotations.Schema;

namespace comandaOpe.Data.Models
{
    [Table("cozinha")]
    public class Cozinha:BaseEntitie
    {
        public int id_pedido { get; set; }
        public bool status { get; set; }
    }
}
