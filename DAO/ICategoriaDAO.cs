using poo2_poject_mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poo2_poject_mvc.DAO
{
    public interface ICategoriaDAO
    {
        List<Categoria> ListarTodos();
        // Adicionando os métodos CRUD completos
        void Salvar(Categoria categoria);
        void Deletar(int id);
        Categoria BuscarPorId(int id);
    }
}
