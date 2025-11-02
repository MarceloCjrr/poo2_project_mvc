using poo2_poject_mvc.DAO;
using poo2_poject_mvc.Services;
using System.Windows;

namespace poo2_poject_mvc
{
    /// <summary>
    /// Lógica interna para LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly ILoginService _loginService;

        public LoginWindow()
        {
            InitializeComponent();

            // Inicialização Simples da Arquitetura de Login
            IUsuarioDAO usuarioDAO = new UsuarioDAO();
            _loginService = new LoginService(usuarioDAO);

            // Dica: Foca automaticamente no campo de senha
            TxtPassword.Focus();
        }

        private void RealizarLogin()
        {
            string username = TxtUsername.Text;
            string password = TxtPassword.Password; // Use .Password para PasswordBox

            if (_loginService.Autenticar(username, password))
            {
                // Sucesso: Abre a janela principal
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();

                // Fecha a janela de Login
                this.Close();
            }
            else
            {
                MessageBox.Show("Usuário ou senha inválidos.", "Falha na Autenticação", MessageBoxButton.OK, MessageBoxImage.Error);
                TxtPassword.Clear();
                TxtPassword.Focus();
            }
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            RealizarLogin();
        }

        // Opcional: Permite logar ao pressionar Enter na caixa de senha
        private void TxtPassword_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                RealizarLogin();
            }
        }

        private void BtnAbrirCadastro_Click(object sender, RoutedEventArgs e)
        {
            CadastrarUsuario cadastroWindow = new CadastrarUsuario();
            cadastroWindow.ShowDialog(); // Usa ShowDialog para bloquear a janela principal até o cadastro ser concluído
        }
    }
}
