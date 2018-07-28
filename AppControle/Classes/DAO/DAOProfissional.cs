using AppControle.Classes.TO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppControle.Classes.DAO
{
    public class DAOProfissional : IDisposable
    {
        private DBConnect db;

        public DAOProfissional()
        {
            db = new DBConnect();
           
        }

        public List<TOPessoa> ListarProfissionais()
        {
            string query = "select * from tb_pessoa where st_status = 1 order by no_pessoa";

            var lista = new List<TOPessoa>();

           var connection =  db.OpenConnection();

            //Create Command
            MySqlCommand cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                var to = new TOPessoa();

                to.IdPessoa = Convert.ToInt32(dataReader["ID_PESSOA"]);
                to.Nome = dataReader["NO_PESSOA"].ToString();
                to.Tipo = Convert.ToInt32(dataReader["NU_TIPO"]);
                lista.Add(to);

            }

            return lista;

        }

        public void Dispose()
        {
            db.CloseConnection();
        }
    }
}