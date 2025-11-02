using MySql.Data.MySqlClient;
using poo2_poject_mvc.Models;
using System.Collections.Generic;

namespace poo2_poject_mvc.DAO
{
    public class ProdutoDAO : IProdutoDAO
    {
        private const string SELECT_BASE =
            "SELECT p.Id, p.Nome, p.Preco, p.IdCategoria, c.Nome AS CategoriaNome " +
            "FROM Produto p INNER JOIN Categoria c ON p.IdCategoria = c.Id";

        private Produto MapearProduto(MySqlDataReader reader)
        {
            return new Produto
            {
                Id = reader.GetInt32("Id"),
                Nome = reader.GetString("Nome"),
                Preco = reader.GetDecimal("Preco"),
                IdCategoria = reader.GetInt32("IdCategoria"),
                Categoria = new Categoria
                {
                    Id = reader.GetInt32("IdCategoria"),
                    Nome = reader.GetString("CategoriaNome")
                }
            };
        }

        public List<Produto> ListarTodos()
        {
            var produtos = new List<Produto>();
            using (var conn = new MySqlConnection(MySqlConfig.GetConnectionString()))
            {
                conn.Open();
                using (var cmd = new MySqlCommand(SELECT_BASE, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        produtos.Add(MapearProduto(reader));
                    }
                }
            }
            return produtos;
        }

        public Produto BuscarPorId(int id)
        {
            string sql = SELECT_BASE + " WHERE p.Id = @Id";
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
                            return MapearProduto(reader);
                        }
                    }
                }
            }
            return null;
        }

        public void Salvar(Produto produto)
        {
            string sql;
            if (produto.Id == 0) // CREATE
            {
                sql = "INSERT INTO Produto (Nome, Preco, IdCategoria) VALUES (@Nome, @Preco, @IdCategoria)";
            }
            else // UPDATE
            {
                sql = "UPDATE Produto SET Nome = @Nome, Preco = @Preco, IdCategoria = @IdCategoria WHERE Id = @Id";
            }

            using (var conn = new MySqlConnection(MySqlConfig.GetConnectionString()))
            {
                conn.Open();
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Nome", produto.Nome);
                    cmd.Parameters.AddWithValue("@Preco", produto.Preco);
                    cmd.Parameters.AddWithValue("@IdCategoria", produto.IdCategoria);

                    if (produto.Id != 0)
                    {
                        cmd.Parameters.AddWithValue("@Id", produto.Id);
                    }

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int id)
        {
            string sql = "DELETE FROM Produto WHERE Id = @Id";
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

        // Implementação do Desafio (Busca por Filtro)
        public List<Produto> BuscarPorNome(string nome)
        {
            var produtos = new List<Produto>();
            string sql = SELECT_BASE + " WHERE p.Nome LIKE @Nome";

            using (var conn = new MySqlConnection(MySqlConfig.GetConnectionString()))
            {
                conn.Open();
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    // Usa '%' para busca parcial
                    cmd.Parameters.AddWithValue("@Nome", "%" + nome + "%");

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            produtos.Add(MapearProduto(reader));
                        }
                    }
                }
            }
            return produtos;
        }
    }
}
