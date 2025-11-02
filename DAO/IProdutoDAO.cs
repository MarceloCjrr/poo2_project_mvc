using poo2_poject_mvc.Models;
using System.Collections.Generic;

namespace poo2_poject_mvc.DAO
{
    public interface IProdutoDAO
    {
        List<Produto> ListarTodos();
        Produto BuscarPorId(int id);
        void Salvar(Produto produto);
        void Deletar(int id);
        // Desafio de Pesquisa: Busca por Filtro
        List<Produto> BuscarPorNome(string nome);
    }
}
