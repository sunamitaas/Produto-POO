using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trabalhoPoo.Models;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;

namespace trabalhoPoo.DAO
{
    // CRUD
    // PADRÃO DAO (DATA ACCESS OBJECT)

    internal class ProdutoDAO
    {
        public void Insert(Produto produto)
        {
            try
            {

                string insert = "INSERT INTO produto (name_produto, descricao_produto, precovenda_produto, id_categoria_fk) " +
                 "VALUES (@name_produto, @descricao_produto, @precovenda_produto, @id_categoria_fk)";

                using (MySqlCommand comando = new MySqlCommand(insert, Conexao.Abrir()))
                {
                    comando.Parameters.AddWithValue("@name_produto", produto.name_produto);
                    comando.Parameters.AddWithValue("@descricao_produto", produto.descricao_produto);
                    comando.Parameters.AddWithValue("@precovenda_produto", produto.precovenda_produto);
                    comando.Parameters.AddWithValue("@id_categoria_fk", produto.id_categoria_fk.id_categoria);
                    comando.ExecuteNonQuery();
                }
                Console.WriteLine("Produto cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao cadastrar o produto! {ex.Message}");
            }
            finally
            {
                Conexao.Fechar();
            }
        }

        public void Delete(int id_produto)
        {
            try
            {
                string delete = "DELETE FROM produto WHERE id_produto = @id_produto";

                using (MySqlConnection conectar = Conexao.Abrir())
                using (MySqlCommand comando = new MySqlCommand(delete, conectar))
                {
                    comando.Parameters.AddWithValue("@id_produto", id_produto);
                    comando.ExecuteNonQuery();

                }

                Console.WriteLine("Produto excluído com sucesso!");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao excluir o Produto! {ex.Message}");
            }
            finally
            {
                Conexao.Fechar();
            }
        }

        public List<Produto> List()
        {
            List<Produto> produtoList = new List<Produto>();
            try
            {

                string sql = "SELECT p.id_produto, p.name_produto, p.descricao_produto, p.precovenda_produto, " +
                     "c.id_categoria, c.name_categoria FROM produto AS p " +
                     "INNER JOIN categoria AS c ON p.id_categoria_fk = c.id_categoria " +
                     "ORDER BY p.id_produto";

                using (MySqlConnection conectar = Conexao.Abrir())
                using (MySqlCommand comando = new MySqlCommand(sql, conectar))

                using (MySqlDataReader ler = comando.ExecuteReader())
                {
                    while (ler.Read())
                    {
                        Produto prod = new Produto
                        {
                            id_produto = ler.GetInt32("id_produto"),
                            name_produto = ler.GetString("name_produto"),
                            descricao_produto = ler.GetString("descricao_produto"),
                            precovenda_produto = ler.GetDecimal("precovenda_produto"),
                            id_categoria_fk = new Categoria
                            {
                                id_categoria = ler.GetInt32("id_categoria"),
                                name_categoria = ler.GetString("name_categoria")
                            }


                        };
                        produtoList.Add(prod);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao listar os produtos! {ex.Message}");
            }
            finally
            {
                Conexao.Fechar();
            }
            return produtoList;
        }

        public bool Update(Produto produto)
        {
            try
            {
                string update = "UPDATE produto SET name_produto = @name_produto, descricao_produto = @descricao_produto, " +
                "precovenda_produto = @precovenda_produto, id_categoria_fk = @id_categoria_fk WHERE id_produto = @id_produto";

                using (MySqlConnection conectar = Conexao.Abrir())
                using (MySqlCommand comando = new MySqlCommand(update, conectar))
                {

                    comando.Parameters.AddWithValue("@name_produto", produto.name_produto);
                    comando.Parameters.AddWithValue("@descricao_produto", produto.descricao_produto);
                    comando.Parameters.AddWithValue("@precovenda_produto", produto.precovenda_produto);
                    comando.Parameters.AddWithValue("@id_categoria_fk", produto.id_categoria_fk.id_categoria);
                    comando.Parameters.AddWithValue("@id_produto", produto.id_produto);

                    int rowsAffected = comando.ExecuteNonQuery();
                    return rowsAffected > 0;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar o produto! {ex.Message}");
                Console.WriteLine($"Detalhes do erro: {ex.StackTrace}");
                throw new Exception($"Erro ao atualizar o produto! {ex.Message}");
            }
            finally
            {
                Conexao.Fechar();
            }

        }

    }
}
