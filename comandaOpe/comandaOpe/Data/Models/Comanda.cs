using System.ComponentModel.DataAnnotations.Schema;

namespace comandaOpe.Data.Models
{
    [Table("comanda")]
    public class Comanda:BaseEntitie
    {
        public int numero_comanda { get; set; }
        public bool status { get; set; }
    
    }
}
