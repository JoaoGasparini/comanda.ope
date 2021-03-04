using System.ComponentModel.DataAnnotations;

namespace comandaOpe.Models
{
    public class Usuario
    {
        [Display(Name = "Login")]
        [Required(ErrorMessage = "Informe o nome do usuario", AllowEmptyStrings = false)]
        public string Login { get; set; }

        [Required(ErrorMessage = "Informe a senha do usuario", AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string Senha { get; set; }
        [Required]
        [Display(Name = "Nome do usuário :")]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "Email do usuário :")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
