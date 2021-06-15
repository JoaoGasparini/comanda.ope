using comandaOpe.Data;
using comandaOpe.Models.Interface;
using System.Linq;

namespace comandaOpe.Models
{
    public class Aunteticacao:IAutenticacao
    {
        private readonly FuncionarioModel funcionarioModel;
        public Aunteticacao()
        {
            funcionarioModel = new FuncionarioModel();
        }
        public bool ValidarLogin(Usuario usuario)
        {
            try
            {
                var usuarioBanco = funcionarioModel.Listar().Where(user => user.login == usuario.Login && user.senha == usuario.Senha).FirstOrDefault();

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
                var newUserData = new comandaOpe.Data.Models.Funcionario()
                {
                    nome = novoUsuario.Nome,
                    email = novoUsuario.Email,
                    login = novoUsuario.Login,
                    senha = novoUsuario.Senha,
                    cargo = novoUsuario.Cargo
                };

                var retorno = funcionarioModel.Inserir(newUserData);

                return "Registrado com sucesso";
            }
            catch (System.Exception e)
            {
                var newUserError = new comandaOpe.Data.Models.Funcionario() { ErrorMessage = e.InnerException.ToString()};
                return "Error: " + newUserError.ErrorMessage;
            }
        }
    }
}
