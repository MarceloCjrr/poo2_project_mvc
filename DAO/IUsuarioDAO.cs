using poo2_poject_mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poo2_poject_mvc.DAO
{
    public interface IUsuarioDAO
    {
        Usuario BuscarPorUsername(string username);
        // NOVO: Método para cadastro (Salvar/Inserir)
        void Salvar(Usuario usuario);
    }
}
