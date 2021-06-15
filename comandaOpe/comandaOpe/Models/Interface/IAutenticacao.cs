namespace comandaOpe.Models.Interface
{
    public interface IAutenticacao
    {
        bool ValidarLogin(Usuario usuario);
        string RegistrarUsuario(Usuario usuario);
    }
}
