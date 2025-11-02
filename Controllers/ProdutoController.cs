using poo2_poject_mvc.DAO;
using poo2_poject_mvc.Models;
using poo2_poject_mvc.Services;
using System.Collections.Generic;

namespace poo2_poject_mvc.Controllers
{
    public class ProdutoController
    {
        private readonly IProdutoService _produtoService;
        // Precisamos do service de categoria para carregar o ComboBox
        private readonly ICategoriaDAO _categoriaDAO;

        // Injeção de Dependência no Construtor
        public ProdutoController(IProdutoService produtoService, ICategoriaDAO categoriaDAO)
        {
            _produtoService = produtoService;
            _categoriaDAO = categoriaDAO;
        }

        public List<Produto> CarregarProdutos()
        {
            return _produtoService.ListarTodos();
        }

        public List<Categoria> CarregarCategorias()
        {
            return _categoriaDAO.ListarTodos();
        }

        public void SalvarProduto(Produto produto)
        {
            _produtoService.Salvar(produto);
        }

        public void DeletarProduto(int id)
        {
            _produtoService.Deletar(id);
        }

        // Método para o Desafio de Pesquisa
        public List<Produto> FiltrarProdutos(string nome)
        {
            return _produtoService.BuscarPorNome(nome);
        }
    }
}