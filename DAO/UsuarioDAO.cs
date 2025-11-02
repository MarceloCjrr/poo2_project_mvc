using MySql.Data.MySqlClient;
using poo2_poject_mvc.Models;
using System;

namespace poo2_poject_mvc.DAO
{
    public class UsuarioDAO : IUsuarioDAO
    {
        public Usuario BuscarPorUsername(string username)
        {
            string sql = "SELECT Id, Username, PasswordHash, NomeCompleto FROM Usuario WHERE Username = @Username";
            using (var conn = new MySqlConnection(MySqlConfig.GetConnectionString()))
            {
                conn.Open();
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Usuario
                            {
                                Id = reader.GetInt32("Id"),
                                Username = reader.GetString("Username"),
                                PasswordHash = reader.GetString("PasswordHash"),
                                NomeCompleto = reader.GetString("NomeCompleto")
                            };
                        }
                    }
                }
            }
            return null;
        }
        public void Salvar(Usuario usuario)
        {
            // O ID deve ser 0 para INSERT (autoincrement)
            if (usuario.Id != 0)
            {
                throw new InvalidOperationException("Não é possível atualizar usuário nesta implementação. Apenas novos cadastros são permitidos.");
            }

            string sql = "INSERT INTO Usuario (Username, PasswordHash, NomeCompleto) VALUES (@Username, @PasswordHash, @NomeCompleto)";

            using (var conn = new MySqlConnection(MySqlConfig.GetConnectionString()))
            {
                conn.Open();
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", usuario.Username);
                    cmd.Parameters.AddWithValue("@PasswordHash", usuario.PasswordHash);
                    cmd.Parameters.AddWithValue("@NomeCompleto", usuario.NomeCompleto ?? (usuario.Username + " User"));

                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}

