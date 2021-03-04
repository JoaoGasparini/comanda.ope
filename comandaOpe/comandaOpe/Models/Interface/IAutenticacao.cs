namespace comandaOpe.Models.Interface
{
    public interface IAutenticacao
    {
        //string RegistrarUsuario(Usuario usuario);
        bool ValidarLogin(Usuario usuario);
        string RegistrarUsuario(Usuario usuario);
    }
}
