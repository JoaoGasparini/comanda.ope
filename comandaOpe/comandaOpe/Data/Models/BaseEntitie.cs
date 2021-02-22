using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace comandaOpe.Data.Models
{
    public class BaseEntitie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime Criado { get; set; }
        [NotMapped]
        public string ErrorMessage { get; set; }
        public BaseEntitie()
        {
            Criado = DateTime.Now;
        }
    }
}
