﻿using System.ComponentModel.DataAnnotations.Schema;

namespace comandaOpe.Data.Models
{
    [Table("pedido")]
    public class Pedido:BaseEntitie
    {
        public int id_comanda_pedido { get; set; }
        public string descricao_produto{ get; set; }
        public bool status { get; set; }
        public int quantidade { get; set; }
        public double valor { get; set; }
        public string observacao { get; set; }
        [NotMapped]
        public double total_pagar { get; set; }
    }
}
