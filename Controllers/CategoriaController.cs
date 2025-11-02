using poo2_poject_mvc.Models;
using poo2_poject_mvc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poo2_poject_mvc.Controllers
{
    public class CategoriaController
    {
        private readonly ICategoriaService _categoriaService;

        // O Controller depende do Service Layer
        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        public List<Categoria> CarregarCategorias()
        {
            return _categoriaService.ListarTodos();
        }

        public void SalvarCategoria(Categoria categoria)
        {
            _categoriaService.Salvar(categoria);
        }

        public void DeletarCategoria(int id)
        {
            _categoriaService.Deletar(id);
        }
    }
}
