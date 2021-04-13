using System.ComponentModel.DataAnnotations.Schema;

namespace comandaOpe.Data.Models
{
    [Table("comanda")]
    public class Comanda:BaseEntitie
    {
        public int id_cliente { get; set; }
        public int id_funcionario { get; set; }
        public int id_pedido { get; set; }
        public int numero_comanda { get; set; }
        public bool status { get; set; }
        public string forma_pagamento { get; set; }
    
    }
}
