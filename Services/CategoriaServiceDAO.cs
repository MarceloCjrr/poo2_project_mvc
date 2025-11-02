using poo2_poject_mvc.DAO;
using poo2_poject_mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poo2_poject_mvc.Services
{
    public class CategoriaServiceDAO : ICategoriaService
    {
        // Esta classe recebe e usa o DAO, aplicando regras de negócio se necessário
        private readonly ICategoriaDAO _categoriaDAO;

        public CategoriaServiceDAO(ICategoriaDAO categoriaDAO)
        {
            _categoriaDAO = categoriaDAO;
        }

        public List<Categoria> ListarTodos() => _categoriaDAO.ListarTodos();

        public void Salvar(Categoria categoria)
        {
            // Exemplo de regra de negócio: garantir que o nome da categoria não seja vazio.
            if (string.IsNullOrWhiteSpace(categoria.Nome))
            {
                throw new System.Exception("O nome da categoria não pode ser vazio.");
            }
            _categoriaDAO.Salvar(categoria);
        }

        public void Deletar(int id) => _categoriaDAO.Deletar(id);
    }
}
