using comandaOpe.Data;
using comandaOpe.Models.Interface;
using System.Linq;

namespace comandaOpe.Models
{
    public class Aunteticacao:IAutenticacao
    {
        private readonly UsuarioModel usuarioModel;
        public Aunteticacao()
        {
            usuarioModel = new UsuarioModel();
        }
        public bool ValidarLogin(Usuario usuario)
        {
            try
            {
                var usuarioBanco = usuarioModel.Listar().Where(user => user.login == usuario.Login && user.senha == usuario.Senha).FirstOrDefault();

                if (usuarioBanco != null) return true;
                else return false;
            }
            catch (System.Exception e)
            {
                var error = e.Message;
                return false;
            }
            
        }
        public string RegistrarUsuario(Usuario novoUsuario)
        {
            try
            {
                var newUserData = new comandaOpe.Data.Models.Usuario()
                {
                    nome = novoUsuario.Nome,
                    email = novoUsuario.Email,
                    login = novoUsuario.Login,
                    senha = novoUsuario.Senha
                };

                var retorno = usuarioModel.Inserir(newUserData);

                return "Registrado com sucesso";
            }
            catch (System.Exception e)
            {
                var newUserError = new comandaOpe.Data.Models.Usuario() { ErrorMessage = e.InnerException.ToString()};
                return "Error: " + newUserError.ErrorMessage;
            }
        }
    }
}
