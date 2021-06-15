using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace comandaOpe.Models
{
    public class ClienteRelatorio
    {
        public string nome_cliente { get; set; }
        public int comanda { get; set; }
        public int quantidade_pedidos { get; set; }
        public double valor_gasto { get; set; }
    }
}
