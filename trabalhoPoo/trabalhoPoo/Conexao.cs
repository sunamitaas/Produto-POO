using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trabalhoPoo
{
    public static class Conexao
    {

        static MySqlConnection conexao;

        public static MySqlConnection Abrir()
        {
            try
            {
                string sqlconexao = "server=localhost;" +
                    "port=3306;" +
                    "uid=root;" +
                    "pwd=#Jesus0511;" +
                    "database=produto";
                conexao = new MySqlConnection(sqlconexao);
                conexao.Open();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao conectar com o banco de dados!" + ex.Message);
            }
            return conexao;
        }
        public static void Fechar()
        {
            conexao.Close();
        }
    }
}
