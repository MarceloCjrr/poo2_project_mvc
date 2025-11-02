using poo2_poject_mvc.DAO;
using poo2_poject_mvc.Models;
using System.Collections.Generic;

namespace poo2_poject_mvc.Services
{
    // A implementação final que usa o MySQL via DAO
    public class ProdutoServiceDAO : IProdutoService
    {
        private readonly IProdutoDAO _produtoDAO;

        public ProdutoServiceDAO(IProdutoDAO produtoDAO)
        {
            _produtoDAO = produtoDAO;
        }

        public List<Produto> ListarTodos() => _produtoDAO.ListarTodos();

        public void Salvar(Produto produto)
        {
            // Adicione regras de negócio/validação aqui, se necessário
            if (produto.Preco <= 0)
            {
                throw new System.Exception("O preço deve ser maior que zero.");
            }
            _produtoDAO.Salvar(produto);
        }

        public void Deletar(int id) => _produtoDAO.Deletar(id);
        public List<Produto> BuscarPorNome(string nome) => _produtoDAO.BuscarPorNome(nome);
    }
}
