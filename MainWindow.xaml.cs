using poo2_poject_mvc.Controllers;
using poo2_poject_mvc.DAO;
using poo2_poject_mvc.Models;
using poo2_poject_mvc.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;

namespace poo2_poject_mvc
{
    // A View interage APENAS com o Controller.
    public partial class MainWindow : Window
    {
        private ProdutoController _produtoController;
        private CategoriaController _categoriaController; // Opcional, mas útil para o CRUD de categorias

        public MainWindow()
        {
            InitializeComponent();

            // --- INICIALIZAÇÃO DA ARQUITETURA (Injeção de Dependência) ---
            // 1. Instanciar DAOs
            ICategoriaDAO categoriaDao = new CategoriaDAO(); // ESTA LINHA JÁ EXISTIA
            IProdutoDAO produtoDao = new ProdutoDAO();

            // 2. Instanciar Services (usando as implementações DAO)
            ICategoriaService categoriaService = new CategoriaServiceDAO(categoriaDao); // NOVO
            IProdutoService produtoService = new ProdutoServiceDAO(produtoDao);

            // 3. Instanciar Controllers
            _produtoController = new ProdutoController(produtoService, categoriaDao); // O Produto Controller já está OK.
            _categoriaController = new CategoriaController(categoriaService); // NOVO

            // Carregar dados iniciais
            CarregarTudo();
        }

        private void CarregarTudo()
        {
            try
            {
                // Carrega o ComboBox de Categorias
                CmbCategoria.ItemsSource = _produtoController.CarregarCategorias();

                // Carrega a DataGrid de Produtos
                DataGridProdutos.ItemsSource = _produtoController.CarregarProdutos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados. Verifique sua conexão com o MySQL e a string em MySqlConfig.cs. Detalhe: " + ex.Message, "Erro Crítico", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }

        // --- MÉTODOS CRUD (Ações da View) ---

        private void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Produto produto = new Produto();

                // 1. Mapeamento da View para o Model
                produto.Id = int.Parse(TxtId.Text);
                produto.Nome = TxtNome.Text;

                if (!decimal.TryParse(TxtPreco.Text, NumberStyles.Currency, CultureInfo.CurrentCulture, out decimal preco))
                {
                    MessageBox.Show("Preço inválido.", "Erro de Validação");
                    return;
                }
                produto.Preco = preco;

                if (CmbCategoria.SelectedValue == null)
                {
                    MessageBox.Show("Selecione uma categoria.", "Erro de Validação");
                    return;
                }
                produto.IdCategoria = (int)CmbCategoria.SelectedValue;

                // 2. Ação via Controller
                _produtoController.SalvarProduto(produto);

                MessageBox.Show("Produto salvo com sucesso!", "Sucesso");

                // 3. Atualização da View
                CarregarTudo();
                BtnLimpar_Click(null, null); // Limpa o formulário
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar o produto: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnDeletar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Tem certeza que deseja DELETAR este produto?", "Confirmação", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    int id = int.Parse(TxtId.Text);
                    _produtoController.DeletarProduto(id);
                    MessageBox.Show("Produto deletado com sucesso!", "Sucesso");
                    CarregarTudo();
                    BtnLimpar_Click(null, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao deletar: Verifique se não há outros dados relacionados. Detalhe: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnLimpar_Click(object sender, RoutedEventArgs e)
        {
            TxtId.Text = "0";
            TxtNome.Text = "";
            TxtPreco.Text = "";
            CmbCategoria.SelectedIndex = -1; // Limpa a seleção
            BtnDeletar.IsEnabled = false;
        }

        // --- MÉTODOS DE INTERAÇÃO COM A GRID ---

        private void DataGridProdutos_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (DataGridProdutos.SelectedItem is Produto produtoSelecionado)
            {
                // Preenche o formulário com os dados do produto selecionado
                TxtId.Text = produtoSelecionado.Id.ToString();
                TxtNome.Text = produtoSelecionado.Nome;
                TxtPreco.Text = produtoSelecionado.Preco.ToString(CultureInfo.CurrentCulture);
                CmbCategoria.SelectedValue = produtoSelecionado.IdCategoria;

                BtnDeletar.IsEnabled = true;
            }
            else
            {
                BtnLimpar_Click(null, null);
            }
        }

        // --- DESAFIO DE PESQUISA (Busca por Nome) ---

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            string termo = TxtFiltro.Text;
            DataGridProdutos.ItemsSource = _produtoController.FiltrarProdutos(termo);
        }

        private void TxtFiltro_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            // Opcional: Aciona a busca ao pressionar Enter
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                BtnBuscar_Click(sender, null);
            }
        }

        private void BtnGerarPdf_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Pega a lista atual de produtos exibida na DataGrid (pode ser a lista filtrada)
                var produtos = DataGridProdutos.ItemsSource as List<Produto>;

                if (produtos != null && produtos.Count > 0)
                {
                    PdfGenerator.GerarRelatorioProdutos(produtos);
                    MessageBox.Show("Relatório PDF gerado com sucesso no diretório do projeto!", "Sucesso");
                }
                else
                {
                    MessageBox.Show("Não há produtos para gerar o relatório.", "Aviso");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Falha ao gerar o PDF. Detalhe: " + ex.Message, "Erro Crítico");
            }
        }
    }
}