using System.ComponentModel.DataAnnotations.Schema;

namespace comandaOpe.Data.Models
{
    [Table("Pedido")]
    public class Pedido:BaseEntitie
    {
        public int Id_Comanda { get; set; }
        public int Id_Funcionario { get; set; }
		public int Id_Produto { get; set; }
        public bool Status { get; set; }
        public int Quantidade { get; set; }
        public float Valor { get; set; }
        public string Observacao { get; set; }

        
	}
}
