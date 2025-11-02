using poo2_poject_mvc.Models;

namespace poo2_poject_mvc.Services
{
    public interface ILoginService
    {
        // Retorna true se a autenticação for bem-sucedida
        bool Autenticar(string username, string password);
        void CadastrarNovoUsuario(Usuario usuario);
    }
}
