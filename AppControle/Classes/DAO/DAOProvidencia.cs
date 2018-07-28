using AppControle.Classes.TO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppControle.Classes.DAO
{
    public class DAOProvidencia : IDisposable
    {
        private DBConnect db;

        public DAOProvidencia()
        {
            db = new DBConnect();

        }

        public void IncluirProvidencia(TOProvidencia item)
        {
            var connection = db.OpenConnection();

            MySqlCommand myCommand = connection.CreateCommand();
            myCommand.Connection = connection;

            #region Parametros
            myCommand.Parameters.AddWithValue("@ID_PROVIDENCIA", item.ID_PROVIDENCIA);
            myCommand.Parameters.AddWithValue("@ID_DEMANDA", item.ID_DEMANDA.ToString());
            myCommand.Parameters.AddWithValue("@DE_OBSERVACOES", item.DE_OBSERVACOES);
            myCommand.Parameters.AddWithValue("@DT_INCLUSAO", DateTime.Now);
            if (item.DT_LIMITE.HasValue)
                myCommand.Parameters.AddWithValue("@DT_LIMITE", item.DT_LIMITE);
            else
                myCommand.Parameters.AddWithValue("@DT_LIMITE", DBNull.Value);

            if (item.ID_PESSOA_RESPONSAVEL.HasValue)
                myCommand.Parameters.AddWithValue("@ID_PESSOA_RESPONSAVEL", item.ID_PESSOA_RESPONSAVEL);
            else
                myCommand.Parameters.AddWithValue("@ID_PESSOA_RESPONSAVEL", DBNull.Value);

            myCommand.Parameters.AddWithValue("@ST_CPD", item.ST_CPD);
            #endregion


            var sql = @"INSERT INTO tb_providencia
                        (
                        ID_DEMANDA,
                        DE_OBSERVACOES,
                        DT_INCLUSAO,
                        DT_LIMITE,
                        ID_PESSOA_RESPONSAVEL,
                        ST_CPD)
                        VALUES
                        (
                        @ID_DEMANDA,
                        @DE_OBSERVACOES,
                        @DT_INCLUSAO,
                        @DT_LIMITE,
                        @ID_PESSOA_RESPONSAVEL,
                        @ST_CPD
                        )";

            myCommand.CommandText = sql;
            myCommand.ExecuteNonQuery();
        }

        public TOProvidencia AlterarProvidencia(TOProvidencia to)
        {
            return to;
        }

        public void ExcluirProvidencia(int idProvidencia)
        {
            var connection = db.OpenConnection();

            MySqlCommand myCommand = connection.CreateCommand();
            MySqlTransaction myTrans;
            myCommand.Connection = connection;
            myCommand.Parameters.Clear();
            var sql = @"DELETE FROM tb_providencia WHERE ID_PROVIDENCIA = " + idProvidencia;
            myCommand.CommandText = sql;
            myCommand.ExecuteNonQuery();
        }

        internal void AlterarResolvido(int idProvidencia)
        {
            var connection = db.OpenConnection();

            MySqlCommand myCommand = connection.CreateCommand();
            MySqlTransaction myTrans;
            myCommand.Connection = connection;
            myCommand.Parameters.Clear();
            var sql = @"UPDATE tb_providencia SET ST_RESOLVIDO = 1 WHERE ID_PROVIDENCIA  = " + idProvidencia;
            myCommand.CommandText = sql;
            myCommand.ExecuteNonQuery();
        }

        public List<TOProvidencia> ListarProvidencias()
        {
            string query = "select * from tb_providencia where ST_RESOLVIDO = 0";
            var connection = db.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader(); ;
            var lista = new List<TOProvidencia>();
            try
            {
                
              

                while (dataReader.Read())
                {
                    var to = new TOProvidencia();

                    to.DE_OBSERVACOES = dataReader["DE_OBSERVACOES"].ToString();
                    to.DT_INCLUSAO = Convert.ToDateTime(dataReader["DT_INCLUSAO"].ToString());
                    if(!String.IsNullOrEmpty(dataReader["DT_LIMITE"].ToString()))
                         to.DT_LIMITE = Convert.ToDateTime(dataReader["DT_LIMITE"]);
                    to.ID_DEMANDA = dataReader["ID_DEMANDA"].ToString();
                  //  to.ID_PESSOA_RESPONSAVEL = Convert.ToInt32(dataReader["ID_PESSOA_RESPONSAVEL"]);
                    to.ID_PROVIDENCIA = Convert.ToInt32(dataReader["ID_PROVIDENCIA"]);
                    to.ST_CPD = Convert.ToBoolean(dataReader["ST_CPD"]);
                    to.ST_RESOLVIDO = Convert.ToBoolean(dataReader["ST_RESOLVIDO"]);
                    lista.Add(to);

                }
            }
            finally
            {
                dataReader.Close();
            }

            return lista;

        }

        public void Dispose()
        {
            db.CloseConnection();
        }

     
    }
}