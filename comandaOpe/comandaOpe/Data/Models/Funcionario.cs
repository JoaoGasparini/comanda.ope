﻿using System.ComponentModel.DataAnnotations.Schema;

namespace comandaOpe.Data.Models
{
    [Table("funcionario")]
    public class Funcionario :BaseEntitie
    {
        public string email { get; set; } 
        public string login { get; set; }
        public string senha { get; set; }
        public string nome { get; set; }
        public string cargo { get; set; }
  
    }
}
