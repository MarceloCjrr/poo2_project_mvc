using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poo2_poject_mvc.DAO
{
    public static class MySqlConfig
    {
        // AJUSTE A STRING DE CONEXÃO CONFORME SEU AMBIENTE!
        private const string ConnectionString =
            "Server=127.0.0.1;Port=3306;Database=poo2_projeto_mvc;User Id=root;Password=SUA_SENHA_AQUI;";

        public static string GetConnectionString()
        {
            return ConnectionString;
        }
    }
}
