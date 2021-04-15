using System.ComponentModel.DataAnnotations.Schema;

namespace comandaOpe.Data.Models
{
    [Table("comanda_pedido")]
    public class Comanda_Pedido:BaseEntitie
    {
        public int id_comanda_cliente { get; set; }
        public int id_funcionario { get; set; }
        public string forma_pagamento { get; set; }
    }
}
