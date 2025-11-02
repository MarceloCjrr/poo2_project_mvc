using poo2_poject_mvc.DAO;
using poo2_poject_mvc.Models;
using poo2_poject_mvc.Services;
using System;
using System.Windows;
using System.Windows.Input;

namespace poo2_poject_mvc
{
    public partial class CadastrarUsuario : Window
    {
        private readonly ILoginService _loginService;

        public CadastrarUsuario()
        {
            InitializeComponent();

            // Inicializa o Service de Login (dependência)
            IUsuarioDAO usuarioDAO = new UsuarioDAO();
            _loginService = new LoginService(usuarioDAO);
        }

        private void BtnCadastrar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Usuario novoUsuario = new Usuario
                {
                    Username = TxtNewUsername.Text,
                    PasswordHash = TxtNewPassword.Password, // PasswordBox usa .Password
                    NomeCompleto = TxtNomeCompleto.Text // Pode ser null ou vazio
                };

                _loginService.CadastrarNovoUsuario(novoUsuario);

                MessageBox.Show($"Usuário '{novoUsuario.Username}' cadastrado com sucesso! Use-o para logar.", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close(); // Fecha a janela após o sucesso
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar usuário: " + ex.Message, "Erro de Cadastro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void TxtNewPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BtnCadastrar_Click(sender, null);
            }
        }
    }
}
