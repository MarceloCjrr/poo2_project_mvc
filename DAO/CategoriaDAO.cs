using MySql.Data.MySqlClient;
using poo2_poject_mvc.Models;
using System.Collections.Generic;

namespace poo2_poject_mvc.DAO
{
    public class CategoriaDAO : ICategoriaDAO
    {
        // Método de Mapeamento (útil para evitar repetição)
        private Categoria MapearCategoria(MySqlDataReader reader)
        {
            return new Categoria
            {
                Id = reader.GetInt32("Id"),
                Nome = reader.GetString("Nome")
            };
        }

        public List<Categoria> ListarTodos()
        {
            var categorias = new List<Categoria>();
            string sql = "SELECT Id, Nome FROM Categoria ORDER BY Nome";

            using (var conn = new MySqlConnection(MySqlConfig.GetConnectionString()))
            {
                conn.Open();
                using (var cmd = new MySqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categorias.Add(MapearCategoria(reader));
                    }
                }
            }
            return categorias;
        }

        public Categoria BuscarPorId(int id)
        {
            string sql = "SELECT Id, Nome FROM Categoria WHERE Id = @Id";
            using (var conn = new MySqlConnection(MySqlConfig.GetConnectionString()))
            {
                conn.Open();
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapearCategoria(reader);
                        }
                    }
                }
            }
            return null;
        }

        public void Salvar(Categoria categoria)
        {
            string sql;
            if (categoria.Id == 0) // CREATE
            {
                sql = "INSERT INTO Categoria (Nome) VALUES (@Nome)";
            }
            else // UPDATE
            {
                sql = "UPDATE Categoria SET Nome = @Nome WHERE Id = @Id";
            }

            using (var conn = new MySqlConnection(MySqlConfig.GetConnectionString()))
            {
                conn.Open();
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Nome", categoria.Nome);

                    if (categoria.Id != 0)
                    {
                        cmd.Parameters.AddWithValue("@Id", categoria.Id);
                    }

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int id)
        {
            string sql = "DELETE FROM Categoria WHERE Id = @Id";
            using (var conn = new MySqlConnection(MySqlConfig.GetConnectionString()))
            {
                conn.Open();
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
