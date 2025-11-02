using poo2_poject_mvc.DAO;
using poo2_poject_mvc.Models;
using System;

namespace poo2_poject_mvc.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUsuarioDAO _usuarioDAO;

        public LoginService(IUsuarioDAO usuarioDAO)
        {
            _usuarioDAO = usuarioDAO;
        }

        public bool Autenticar(string username, string password)
        {
            // ... (o método Autenticar permanece o mesmo) ...
            var usuario = _usuarioDAO.BuscarPorUsername(username);

            if (usuario != null && usuario.PasswordHash == password)
            {
                return true;
            }

            return false;
        }

        // NOVO: Lógica de Cadastro
        public void CadastrarNovoUsuario(Usuario usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Username) || usuario.PasswordHash.Length < 5)
            {
                throw new Exception("O nome de usuário e senha devem ser válidos. Senha precisa ter no mínimo 5 caracteres.");
            }

            // Regra de Negócio: Não permite cadastrar um usuário já existente
            if (_usuarioDAO.BuscarPorUsername(usuario.Username) != null)
            {
                throw new Exception($"O usuário '{usuario.Username}' já existe no sistema.");
            }

            // Se tudo estiver OK, salva no banco
            _usuarioDAO.Salvar(usuario);
        }
    }
}
