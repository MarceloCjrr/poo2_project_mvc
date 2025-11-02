using poo2_poject_mvc.Models;
using System.Collections.Generic;

namespace poo2_poject_mvc.Services
{
    public interface IProdutoService
    {
        List<Produto> ListarTodos();
        void Salvar(Produto produto);
        void Deletar(int id);
        List<Produto> BuscarPorNome(string nome);
    }
}
