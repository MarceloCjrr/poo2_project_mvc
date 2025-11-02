using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poo2_poject_mvc.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }

        // Chave Estrangeira
        public int IdCategoria { get; set; }

        // Propriedade de Navegação (Para exibir o nome na grid)
        public Categoria Categoria { get; set; }
    }
}
