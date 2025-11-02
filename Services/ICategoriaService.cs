using poo2_poject_mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poo2_poject_mvc.Services
{
    public interface ICategoriaService
    {
        // Métodos básicos para a Categoria
        List<Categoria> ListarTodos();
        void Salvar(Categoria categoria);
        void Deletar(int id);
    }
}
