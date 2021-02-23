using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace comandaOpe.Data.Models
{
    public class BaseEntitie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime criado { get; set; }
        [NotMapped]
        public string ErrorMessage { get; set; }
        public BaseEntitie()
        {
            criado = DateTime.Now;
        }
    }
}
