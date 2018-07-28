using AppControle.Classes.TO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace AppControle.Classes.DAO
{
    public class DAOOasis : IDisposable
    {
        private DBConnect db;

        public DAOOasis()
        {
            db = new DBConnect();

        }

        public List<TORelatorioPrazo> RelatorioPrazosObjeto(int idRequisito, int idAnalista, string sustentacao)
        {
            return RelatorioPrazosTO(idRequisito, idAnalista, sustentacao);
        }

        private List<TORelatorioPrazo> RelatorioPrazosTO(int idRequisito, int idAnalista, string sustentacao)
        {
            var connection = db.OpenConnection();

            #region sql
            string query = @"SELECT 
                          O.ID_DEMANDA, 
                          O.DE_SISTEMA,	
 	                        PE_R.NO_PESSOA AS REQUISITO,
                            O.DE_PARECER_PARCELA,
	                        PE_T.NO_PESSOA AS ANALISTA,
                          CASE WHEN O.ST_SUSTENTACAO = 1 THEN 'Sustentação'
	                        else 'Desenvolvimento' end AS TIPO,
	                        O.DE_TIPO_DEMANDA,
	                        O.DE_FECHAMENTO_PROPOSTA AS ENVIO_PROPOSTA,
	                        O.DE_ACEITE_PROPOSTA AS ACEITE_PROPOSTA,                       
                            O.DE_PARECER_PROPOSTA AS PARECER_SEF_PROPOSTA,
                            O.DE_ASSUNTO_SOLICITACAO,
                             CONVERT(O.DT_PRAZO_LIMITE_PROPOSTA, char) AS PRAZO_PROPOSTA,
	                       
                            O.DE_FECHAMENTO_HOMOLOGACAO AS ENVIO_HOMOL,
	                        O.DE_ACEITE_HOMOLOGACAO AS ACEITE_HOMOL,
	                        O.DE_FECHAMENTO_PRODUCAO AS ENVIO_PROD,
	                        O.DE_ACEITE_PRODUCAO AS ACEITE_PROD,
                           CONVERT(O.DT_PRAZO_LIMITE_PARCELA, char) AS PRAZO_PARCELA,
	                       -- O.DT_PRAZO_LIMITE_PARCELA AS PRAZO_PARCELA,
	                        O.DE_FECHAMENTO_PARCELA AS PARCELA_FECHADA,
                            O.VL_CONTAGEM_ESTIMADA,                          
                            O.VL_CONTAGEM_DETALHADA,
                            PV.DE_OBSERVACOES AS DESC_PROVIDENCIA
                        FROM tb_prazo P
                        INNER JOIN tb_oasis O ON O.ID_DEMANDA = P.ID_DEMANDA
                        INNER JOIN tb_pessoa PE_R ON P.ID_PESSOA_REQUISITO = PE_R.ID_PESSOA
                        INNER JOIN tb_pessoa PE_T ON P.ID_PESSOA_TECNICO = PE_T.ID_PESSOA 
                        LEFT JOIN tb_providencia PV ON PV.ID_DEMANDA = P.ID_DEMANDA AND PV.ST_RESOLVIDO = 0
                        WHERE  P.ST_ENCERRADA = 0 ";

            if (idRequisito > 0)
                query += " AND P.ID_PESSOA_REQUISITO = " + idRequisito;

            if (idAnalista > 0)
                query += " AND P.ID_PESSOA_TECNICO = " + idAnalista;

            if (!string.IsNullOrEmpty(sustentacao))
                query += " AND O.ST_SUSTENTACAO = " + sustentacao;

            query += " ORDER BY ST_SUSTENTACAO , DE_SISTEMA, ID_DEMANDA, REQUISITO";
            #endregion

            MySqlCommand cmd = new MySqlCommand(query, connection);
            var dataReader = cmd.ExecuteReader();
            var lista = new List<TORelatorioPrazo>();


            try
            {
                while (dataReader.Read())
                {
                    var to = new TORelatorioPrazo();

                    #region Preenche Campos
                    to.ID_DEMANDA = dataReader["ID_DEMANDA"].ToString();
                    to.DE_SISTEMA = dataReader["DE_SISTEMA"].ToString();
                    to.REQUISITO = dataReader["REQUISITO"].ToString();
                    to.ANALISTA = dataReader["ANALISTA"].ToString();
                    to.TIPO = dataReader["TIPO"].ToString();
                    to.DE_TIPO_DEMANDA = dataReader["DE_TIPO_DEMANDA"].ToString();
                    to.ENVIO_PROPOSTA = dataReader["ENVIO_PROPOSTA"].ToString();
                    to.ACEITE_PROPOSTA = dataReader["ACEITE_PROPOSTA"].ToString();
                    to.PARECER_SEF_PROPOSTA = dataReader["PARECER_SEF_PROPOSTA"].ToString();
                    to.DE_ASSUNTO_SOLICITACAO = dataReader["DE_ASSUNTO_SOLICITACAO"].ToString();
                    to.PRAZO_PROPOSTA = dataReader["PRAZO_PROPOSTA"].ToString();
                    to.ENVIO_HOMOL = dataReader["ENVIO_HOMOL"].ToString();
                    to.ACEITE_HOMOL = dataReader["ACEITE_HOMOL"].ToString();
                    to.ENVIO_PROD = dataReader["ENVIO_PROD"].ToString();
                    to.ACEITE_PROD = dataReader["ACEITE_PROD"].ToString();
                    to.PRAZO_PARCELA = dataReader["PRAZO_PARCELA"].ToString();
                    to.PARCELA_FECHADA = dataReader["PARCELA_FECHADA"].ToString();
                    to.VL_CONTAGEM_ESTIMADA = Convert.ToDecimal(dataReader["VL_CONTAGEM_ESTIMADA"]);
                    to.VL_CONTAGEM_DETALHADA = Convert.ToDecimal(dataReader["VL_CONTAGEM_DETALHADA"]);
                    to.DESC_PROVIDENCIA = dataReader["DESC_PROVIDENCIA"].ToString();
                    to.DE_PARECER_PARCELA = dataReader["DE_PARECER_PARCELA"].ToString();

                    #endregion

                    lista.Add(to);
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                dataReader.Close();
            }

            return lista;
        }

        public DataTable RelatorioPrazos(int idRequisito, int idAnalista, string sustentacao)
        {
           var connection = db.OpenConnection();

            #region sql
            string query = @"SELECT 
                          O.ID_DEMANDA, 
                          O.DE_SISTEMA,	
 	                        PE_R.NO_PESSOA AS REQUISITO,
	                        PE_T.NO_PESSOA AS ANALISTA,
                          CASE WHEN O.ST_SUSTENTACAO = 1 THEN 'Sustentação'
	                        else 'Desenvolvimento' end AS TIPO,
	                        O.DE_TIPO_DEMANDA,
	                        O.DE_FECHAMENTO_PROPOSTA AS ENVIO_PROPOSTA,
	                        O.DE_ACEITE_PROPOSTA AS ACEITE_PROPOSTA,                       
                            O.DE_PARECER_PROPOSTA AS PARECER_SEF_PROPOSTA,
                            O.DE_ASSUNTO_SOLICITACAO,
                             CONVERT(O.DT_PRAZO_LIMITE_PROPOSTA, char) AS PRAZO_PROPOSTA,
	                       
                            O.DE_FECHAMENTO_HOMOLOGACAO AS ENVIO_HOMOL,
	                        O.DE_ACEITE_HOMOLOGACAO AS ACEITE_HOMOL,
	                        O.DE_FECHAMENTO_PRODUCAO AS ENVIO_PROD,
	                        O.DE_ACEITE_PRODUCAO AS ACEITE_PROD,
                           CONVERT(O.DT_PRAZO_LIMITE_PARCELA, char) AS PRAZO_PARCELA,
	                       -- O.DT_PRAZO_LIMITE_PARCELA AS PRAZO_PARCELA,
	                        O.DE_FECHAMENTO_PARCELA AS PARCELA_FECHADA,
                            O.VL_CONTAGEM_ESTIMADA,                          
                            O.VL_CONTAGEM_DETALHADA,
                            PV.DE_OBSERVACOES AS DESC_PROVIDENCIA
                        FROM tb_prazo P
                        INNER JOIN tb_oasis O ON O.ID_DEMANDA = P.ID_DEMANDA
                        INNER JOIN tb_pessoa PE_R ON P.ID_PESSOA_REQUISITO = PE_R.ID_PESSOA
                        INNER JOIN tb_pessoa PE_T ON P.ID_PESSOA_TECNICO = PE_T.ID_PESSOA 
                        LEFT JOIN tb_providencia PV ON PV.ID_DEMANDA = P.ID_DEMANDA AND PV.ST_RESOLVIDO = 0
                        WHERE  P.ST_ENCERRADA = 0 ";

            if (idRequisito > 0)
                query += " AND P.ID_PESSOA_REQUISITO = " + idRequisito;

            if (idAnalista > 0)
                    query += " AND P.ID_PESSOA_TECNICO = " + idAnalista;

            if (!string.IsNullOrEmpty(sustentacao))
                query += " AND O.ST_SUSTENTACAO = " + sustentacao;



            query += " ORDER BY ST_SUSTENTACAO , DE_SISTEMA, ID_DEMANDA, REQUISITO";
            #endregion

            MySqlCommand cmd = new MySqlCommand(query, connection);
            var dataReader = cmd.ExecuteReader();
            var data = new System.Data.DataTable();

            try
            {
                while (!dataReader.IsClosed)
                {
                    data.Load(dataReader);
                }

            }
            catch(Exception ex)
            {

            }
            finally
            {
                dataReader.Close();
            }

            return data;
        }

        public string IncluirPlanilha(List<TOOasis> lista)
        {
            var connection = db.OpenConnection();

            MySqlCommand myCommand = connection.CreateCommand();
            MySqlTransaction myTrans;
            // Start a local transaction
            myTrans = connection.BeginTransaction();
            // Must assign both transaction object and connection
            // to Command object for a pending local transaction
            myCommand.Connection = connection;
            myCommand.Transaction = myTrans;

            try
            {
                int countInseridos = 0;
                int countAtualizados = 0;

                foreach (var item in lista)
                {
                    var existe = ConsultarPorIdDemanda(item.ID_DEMANDA, connection);

                    myCommand.Parameters.Clear();

                    #region Parameters

                    if (item.DT_ULTIMA_ATUALIZACAO.HasValue)
                        myCommand.Parameters.AddWithValue("@DT_ULTIMA_ATUALIZACAO", item.DT_ULTIMA_ATUALIZACAO.Value);
                    else
                        myCommand.Parameters.AddWithValue("@DT_ULTIMA_ATUALIZACAO", DBNull.Value);

                    if (item.DT_AUTORIZACAO.HasValue)
                        myCommand.Parameters.AddWithValue("@DT_AUTORIZACAO", item.DT_AUTORIZACAO.Value);
                    else
                        myCommand.Parameters.AddWithValue("@DT_AUTORIZACAO", DBNull.Value);

                    if (item.DT_PRAZO_LIMITE_PROPOSTA.HasValue)
                        myCommand.Parameters.AddWithValue("@DT_PRAZO_LIMITE_PROPOSTA", item.DT_PRAZO_LIMITE_PROPOSTA.Value);
                    else
                        myCommand.Parameters.AddWithValue("@DT_PRAZO_LIMITE_PROPOSTA", DBNull.Value);



                    myCommand.Parameters.AddWithValue("@ID_DEMANDA", item.ID_DEMANDA);
                    myCommand.Parameters.AddWithValue("@DE_DEMANDA", item.DE_DEMANDA);
                    if (item.NU_PARCELA == "")
                        myCommand.Parameters.AddWithValue("@NU_PARCELA", DBNull.Value);
                    else
                        myCommand.Parameters.AddWithValue("@NU_PARCELA", item.NU_PARCELA);

                    myCommand.Parameters.AddWithValue("@DE_SISTEMA", item.DE_SISTEMA);

                    myCommand.Parameters.AddWithValue("@DE_TIPO_DEMANDA", item.DE_TIPO_DEMANDA);
                    myCommand.Parameters.AddWithValue("@ST_SUSTENTACAO", item.ST_SUSTENTACAO.ToString());
                    myCommand.Parameters.AddWithValue("@DE_PRIORIDADE", item.DE_PRIORIDADE);
                    myCommand.Parameters.AddWithValue("@DE_ASSUNTO_SOLICITACAO", item.DE_ASSUNTO_SOLICITACAO);

                    myCommand.Parameters.AddWithValue("@DE_GESTOR_TECNICO", item.DE_GESTOR_TECNICO);
                    myCommand.Parameters.AddWithValue("@DE_GESTOR_OPERACIONAl", item.DE_GESTOR_OPERACIONAl);
                    myCommand.Parameters.AddWithValue("@VL_TOTAL_PF_PROPOSTA", item.VL_TOTAL_PF_PROPOSTA.ToString().Replace(",","."));
                    myCommand.Parameters.AddWithValue("@VL_CONTAGEM_ESTIMADA", item.VL_CONTAGEM_ESTIMADA.ToString().Replace(",", "."));

                    myCommand.Parameters.AddWithValue("@VL_CONTAGEM_DETALHADA", item.VL_CONTAGEM_DETALHADA.ToString().Replace(",", "."));
                    myCommand.Parameters.AddWithValue("@VL_HORAS", item.VL_HORAS.ToString().Replace(",", "."));
                    myCommand.Parameters.AddWithValue("@VL_PF", item.VL_PF.ToString().Replace(",", "."));
                    myCommand.Parameters.AddWithValue("@DE_INM", item.DE_INM);

                    myCommand.Parameters.AddWithValue("@DE_CONTRATO", item.DE_CONTRATO);
                    myCommand.Parameters.AddWithValue("@DE_SITUACAO_DEMANDA", item.DE_SITUACAO_DEMANDA);
                    myCommand.Parameters.AddWithValue("@DE_SITUACAO_PARCELA", item.DE_SITUACAO_PARCELA);

                    myCommand.Parameters.AddWithValue("@DE_ACEITE_PROPOSTA", item.DE_ACEITE_PROPOSTA);
                    myCommand.Parameters.AddWithValue("@DE_FECHAMENTO_PROPOSTA", item.DE_FECHAMENTO_PROPOSTA);

                    myCommand.Parameters.AddWithValue("@DE_PARECER_PROPOSTA", item.DE_PARECER_PROPOSTA);

                    if (item.DT_AUTORIZACAO_PARCELA.HasValue)
                        myCommand.Parameters.AddWithValue("@DT_AUTORIZACAO_PARCELA", item.DT_AUTORIZACAO_PARCELA);
                    else
                        myCommand.Parameters.AddWithValue("@DT_AUTORIZACAO_PARCELA", DBNull.Value);

                    if (item.DT_PRAZO_LIMITE_PARCELA.HasValue)
                        myCommand.Parameters.AddWithValue("@DT_PRAZO_LIMITE_PARCELA", item.DT_PRAZO_LIMITE_PARCELA);
                    else
                        myCommand.Parameters.AddWithValue("@DT_PRAZO_LIMITE_PARCELA", DBNull.Value);

                    myCommand.Parameters.AddWithValue("@DE_FECHAMENTO_PARCELA", item.DE_FECHAMENTO_PARCELA);
                    myCommand.Parameters.AddWithValue("@DE_PARECER_PARCELA", item.DE_PARECER_PARCELA);
                    myCommand.Parameters.AddWithValue("@DE_FECHAMENTO_HOMOLOGACAO", item.DE_FECHAMENTO_HOMOLOGACAO);
                    myCommand.Parameters.AddWithValue("@DE_ACEITE_HOMOLOGACAO", item.DE_ACEITE_HOMOLOGACAO);
                    myCommand.Parameters.AddWithValue("@DE_FECHAMENTO_PRODUCAO", item.DE_FECHAMENTO_PRODUCAO);
                    myCommand.Parameters.AddWithValue("@DE_ACEITE_PRODUCAO", item.DE_ACEITE_PRODUCAO);
                    myCommand.Parameters.AddWithValue("@DE_NUMERO_DA_FATURA", item.DE_NUMERO_DA_FATURA);
                    myCommand.Parameters.AddWithValue("@ANO_DA_FATURA", item.ANO_DA_FATURA);
                    myCommand.Parameters.AddWithValue("@ST_GARANTIA", item.ST_GARANTIA);
                    myCommand.Parameters.AddWithValue("@DE_PROFISSIONAL", item.DE_PROFISSIONAL);
                    myCommand.Parameters.AddWithValue("@DT_IMPORTACAO_PLANILHA", DateTime.Now);


                    #endregion


                    try
                    {
                        if (existe == null)
                        {
                            var sql = @"INSERT INTO TB_OASIS
                                    (ID_DEMANDA, DE_DEMANDA, NU_PARCELA, DE_SISTEMA,
                                    DE_TIPO_DEMANDA, ST_SUSTENTACAO, DE_PRIORIDADE, DE_ASSUNTO_SOLICITACAO,
                                    DE_GESTOR_TECNICO, DE_GESTOR_OPERACIONAl, VL_TOTAL_PF_PROPOSTA, VL_CONTAGEM_ESTIMADA,
                                    VL_CONTAGEM_DETALHADA, VL_HORAS, VL_PF, DE_INM,
                                    DE_CONTRATO, DE_SITUACAO_DEMANDA, DE_SITUACAO_PARCELA, 
                                    DT_ULTIMA_ATUALIZACAO, DT_AUTORIZACAO, DT_PRAZO_LIMITE_PROPOSTA,
                                    DE_FECHAMENTO_PROPOSTA, DE_ACEITE_PROPOSTA, DE_PARECER_PROPOSTA, DT_IMPORTACAO_PLANILHA)
                                    VALUES
                                    (
                                        @ID_DEMANDA, @DE_DEMANDA, @NU_PARCELA, @DE_SISTEMA,
                                        @DE_TIPO_DEMANDA, @ST_SUSTENTACAO, @DE_PRIORIDADE, @DE_ASSUNTO_SOLICITACAO,
                                        @DE_GESTOR_TECNICO, @DE_GESTOR_OPERACIONAl, @VL_TOTAL_PF_PROPOSTA, @VL_CONTAGEM_ESTIMADA,
                                        @VL_CONTAGEM_DETALHADA, @VL_HORAS, @VL_PF, @DE_INM,
                                        @DE_CONTRATO, @DE_SITUACAO_DEMANDA, @DE_SITUACAO_PARCELA, 
                                        @DT_ULTIMA_ATUALIZACAO, @DT_AUTORIZACAO, @DT_PRAZO_LIMITE_PROPOSTA,
                                        @DE_FECHAMENTO_PROPOSTA, @DE_ACEITE_PROPOSTA, @DE_PARECER_PROPOSTA, @DT_IMPORTACAO_PLANILHA
                                     )";
                            myCommand.CommandText = sql;
                            myCommand.ExecuteNonQuery();

                            countInseridos++;
                        }
                        else
                        {
                            var sql = @"UPDATE TB_OASIS
                                         SET
                                            DE_DEMANDA =  @DE_DEMANDA,
                                            NU_PARCELA =  @NU_PARCELA, 
                                            DE_SISTEMA = @DE_SISTEMA,
                                            DE_TIPO_DEMANDA = @DE_TIPO_DEMANDA, 
                                            ST_SUSTENTACAO = @ST_SUSTENTACAO, 
                                            DE_PRIORIDADE = @DE_PRIORIDADE,
                                            DE_ASSUNTO_SOLICITACAO = @DE_ASSUNTO_SOLICITACAO,
                                            DE_GESTOR_TECNICO = @DE_GESTOR_TECNICO,
                                            DE_GESTOR_OPERACIONAl = @DE_GESTOR_OPERACIONAl,
                                            VL_TOTAL_PF_PROPOSTA = @VL_TOTAL_PF_PROPOSTA,
                                            VL_CONTAGEM_ESTIMADA = @VL_CONTAGEM_ESTIMADA,
                                            VL_CONTAGEM_DETALHADA = @VL_CONTAGEM_DETALHADA,
                                            VL_HORAS = @VL_HORAS,
                                            VL_PF = @VL_PF,
                                            DE_INM = @DE_INM,
                                            DE_CONTRATO = @DE_CONTRATO,
                                            DE_SITUACAO_DEMANDA = @DE_SITUACAO_DEMANDA,
                                            DE_SITUACAO_PARCELA = @DE_SITUACAO_PARCELA,
                                            DT_ULTIMA_ATUALIZACAO = @DT_ULTIMA_ATUALIZACAO,
                                            DT_AUTORIZACAO = @DT_AUTORIZACAO,
                                            DT_PRAZO_LIMITE_PROPOSTA =  @DT_PRAZO_LIMITE_PROPOSTA,
                                            DE_FECHAMENTO_PROPOSTA = @DE_FECHAMENTO_PROPOSTA,
                                            DE_ACEITE_PROPOSTA	= @DE_ACEITE_PROPOSTA,
                                            DT_AUTORIZACAO_PARCELA	= @DT_AUTORIZACAO_PARCELA,
                                            DT_PRAZO_LIMITE_PARCELA	= @DT_PRAZO_LIMITE_PARCELA,
                                            DE_FECHAMENTO_PARCELA	= @DE_FECHAMENTO_PARCELA,
                                            DE_PARECER_PARCELA	=	@DE_PARECER_PARCELA,
                                            DE_FECHAMENTO_HOMOLOGACAO	=	@DE_FECHAMENTO_HOMOLOGACAO,
                                            DE_ACEITE_HOMOLOGACAO	=	@DE_ACEITE_HOMOLOGACAO,
                                            DE_FECHAMENTO_PRODUCAO	=	@DE_FECHAMENTO_PRODUCAO,
                                            DE_ACEITE_PRODUCAO	=	@DE_ACEITE_PRODUCAO,
                                            DE_NUMERO_DA_FATURA	=	@DE_NUMERO_DA_FATURA,
                                            ANO_DA_FATURA	=	@ANO_DA_FATURA,
                                            ST_GARANTIA	=	@ST_GARANTIA,
                                            DE_PROFISSIONAL	=	@DE_PROFISSIONAL,
                                            DE_PARECER_PROPOSTA = @DE_PARECER_PROPOSTA,
                                            DT_IMPORTACAO_PLANILHA = @DT_IMPORTACAO_PLANILHA
                                          WHERE ID_DEMANDA = @ID_DEMANDA
                                    ";



                            myCommand.CommandText = sql;
                            myCommand.ExecuteNonQuery();

                            countAtualizados++;
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }

                myTrans.Commit();
                return String.Format("Incluídos {0} registros. Atualizados {1}.", countInseridos, countAtualizados);
            }
            catch (Exception ex)
            {
                
                myTrans.Rollback();
                return ex.Message;
            }
            

        }

        public void AlocarResponsavel(string idDemanda, int idRequisito, int idAnalista)
        {
            var connection = db.OpenConnection();

            MySqlCommand myCommand = connection.CreateCommand();
            MySqlTransaction myTrans;
            myCommand.Connection = connection;

            myCommand.Parameters.Clear();

            #region Parameters

            myCommand.Parameters.AddWithValue("@ID_DEMANDA", idDemanda);
            myCommand.Parameters.AddWithValue("@ID_REQUISITO", idRequisito);
            myCommand.Parameters.AddWithValue("@ID_ANALISTA", idAnalista);
            #endregion



            var sql = @"CALL 
                        SP_DISTRIBUIR_DEMANDA(@ID_DEMANDA, @ID_REQUISITO, @ID_ANALISTA) ";
            myCommand.CommandText = sql;
            myCommand.ExecuteNonQuery();

            try
            {
                EnviarEmail(idDemanda);
            }
            catch (Exception ex)
            {
                
            }

        
        }

        public void EnviarEmail(string idDemanda)
        {
            //var to = ConsultarPorIdDemanda(idDemanda, null);
            //var basicCredential = new NetworkCredential("lsmorato", "Liciane16");
            //var mail = new MailMessage();
            //mail.To.Add(new MailAddress("lsmorato@fazenda.df.gov.br"));
            //mail.From = new MailAddress("lsmorato@fazenda.df.gov.br");

            //mail.Subject = "Nova demanda OASIS distribuída! " + idDemanda;
            //mail.IsBodyHtml =true;
            //mail.Body = string.Format(@"Uma demanda foi distribuída para você. <br />
            //                            Número: {0} <br />
            //                            Sistema: {1} <br />                                      
            //                            Desrição: {2} ", to.ID_DEMANDA, to.DE_SISTEMA, to.DE_ASSUNTO_SOLICITACAO);
            //using (var smtp = new SmtpClient("correio.fazenda.net",25))
            //{
            //    smtp.Credentials = basicCredential;
            //    smtp.Send(mail);
            //}
        }

        public TOOasis ConsultarPorIdDemanda(String numDemanda, MySqlConnection connection)
        {
            if(connection == null)
                connection = db.OpenConnection();

            string query = "select * from tb_oasis WHERE ID_DEMANDA = '" + numDemanda + "'";

            var lista = new List<TOOasis>();

           // var connection = db.OpenConnection();

            //Create Command
            MySqlCommand cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();

            TOOasis to = null;

            try
            {
                if (dataReader.Read())
                {
                    to = new TOOasis();

                    #region Preenche Campos
                    to.ID_DEMANDA = dataReader["ID_DEMANDA"].ToString();
                    to.NU_PARCELA = dataReader["NU_PARCELA"].ToString();
                    to.DE_SISTEMA = dataReader["DE_SISTEMA"].ToString();
                    //to.ST_GARANTIA = dataReader["ST_GARANTIA"].ToString() == ""? 0 : Convert.ToInt32(dataReader["ST_GARANTIA"].ToString());
                    //to.ST_SUSTENTACAO = dataReader["ST_SUSTENTACAO"].ToString() == "" ? 0 : Convert.ToInt32(dataReader["ST_SUSTENTACAO"].ToString());
                    //to.VL_CONTAGEM_DETALHADA = Convert.ToDecimal(dataReader["VL_CONTAGEM_DETALHADA"].ToString());
                    //to.VL_CONTAGEM_ESTIMADA = Convert.ToDecimal(dataReader["VL_CONTAGEM_ESTIMADA"].ToString());
                    //to.VL_HORAS = Convert.ToDecimal(dataReader["VL_HORAS"].ToString());
                    //to.VL_PF = Convert.ToDecimal(dataReader["VL_PF"].ToString());
                    //to.VL_TOTAL_PF_PROPOSTA = Convert.ToDecimal(dataReader["VL_TOTAL_PF_PROPOSTA"].ToString());
                    //to.ANO_DA_FATURA = dataReader["ANO_DA_FATURA"].ToString();
                    //to.DE_ACEITE_HOMOLOGACAO = dataReader["DE_ACEITE_HOMOLOGACAO"].ToString();
                    //to.DE_ACEITE_PRODUCAO = dataReader["DE_ACEITE_PRODUCAO"].ToString();
                    //to.DE_ACEITE_PROPOSTA = dataReader["DE_ACEITE_PROPOSTA"].ToString();
                    //to.DE_ASSUNTO_SOLICITACAO = dataReader["DE_ASSUNTO_SOLICITACAO"].ToString();
                    //to.DE_CONTRATO = dataReader["DE_CONTRATO"].ToString();
                    #endregion

                }

            }
            finally
            {
                dataReader.Close();
            }
        

            return to;

        }

        public List<TOOasis> ListarDemandas()
        {
            string query = "select * from tb_oasis";

            var lista = new List<TOOasis>();

            var connection = db.OpenConnection();

            //Create Command
            MySqlCommand cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                var to = new TOOasis();

                #region Preenche Campos
                to.ID_DEMANDA = dataReader["ID_DEMANDA"].ToString();
                to.NU_PARCELA = dataReader["NU_PARCELA"].ToString();
                to.ST_GARANTIA = Convert.ToInt32(dataReader["ST_GARANTIA"].ToString());
                to.ST_SUSTENTACAO = Convert.ToInt32(dataReader["ST_SUSTENTACAO"].ToString());
                to.VL_CONTAGEM_DETALHADA = Convert.ToDecimal(dataReader["VL_CONTAGEM_DETALHADA"].ToString());
                to.VL_CONTAGEM_ESTIMADA = Convert.ToDecimal(dataReader["VL_CONTAGEM_ESTIMADA"].ToString());
                to.VL_HORAS = Convert.ToDecimal(dataReader["VL_HORAS"].ToString());
                to.VL_PF = Convert.ToDecimal(dataReader["VL_PF"].ToString());
                to.VL_TOTAL_PF_PROPOSTA = Convert.ToDecimal(dataReader["VL_TOTAL_PF_PROPOSTA"].ToString());
                to.ANO_DA_FATURA = dataReader["ANO_DA_FATURA"].ToString();
                to.DE_ACEITE_HOMOLOGACAO = dataReader["DE_ACEITE_HOMOLOGACAO"].ToString();
                to.DE_ACEITE_PRODUCAO = dataReader["DE_ACEITE_PRODUCAO"].ToString();
                to.DE_ACEITE_PROPOSTA = dataReader["DE_ACEITE_PROPOSTA"].ToString();
                to.DE_ASSUNTO_SOLICITACAO = dataReader["DE_ASSUNTO_SOLICITACAO"].ToString();
                to.DE_CONTRATO = dataReader["DE_CONTRATO"].ToString(); 
                #endregion

                lista.Add(to);

            }

            return lista;

        }

        public List<string> ListarSistemas()
        {
            string query = @"select distinct(DE_SISTEMA) AS SISTEMA from tb_oasis 
                            ORDER BY 1";

            var lista = new List<string>();

            var connection = db.OpenConnection();

            //Create Command
            MySqlCommand cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
               lista.Add(dataReader["SISTEMA"].ToString());
            }
            dataReader.Close();
            return lista;

        }

        public string ConsultarUltimaAtualizacao()
        {
            string query = @"select max(DT_IMPORTACAO_PLANILHA) as max from tb_oasis";

            var connection = db.OpenConnection();

            //Create Command
            MySqlCommand cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();

            string retorno = "";

            if (dataReader.Read())
            {
                retorno = dataReader["max"].ToString();
            }
            dataReader.Close();
            return retorno;

        }

        public List<TOOasis> DemandasSemAlocacao(string sistema, bool sustentacao)
        {
            string query = @"SELECT * 
                                FROM TB_OASIS O
                                WHERE(SELECT COUNT(*) FROM TB_PRAZO P WHERE O.ID_DEMANDA = P.ID_DEMANDA) = 0
                                AND ST_SUSTENTACAO = "+ sustentacao.GetHashCode() + @" AND ID_DEMANDA <> '?'
                                AND DE_SITUACAO_DEMANDA NOT IN ('Solicitação Registrada', 'Solicitação Rejeitada', 'Solicitação Devolvida p/ Ajuste', 'Solicitação Ajustada')";

            if (sistema!= null && sistema.Trim() != "")
                query += " AND DE_SISTEMA = '" + sistema +"'";

            //Retira demandas que estão sendo atendidas pelo Mário
            query += @" AND
                         (UPPER(DE_SISTEMA) LIKE '%SIGGO%'  
                            OR
                            UPPER(DE_SISTEMA) IN (SELECT UPPER(DE_SISTEMA) FROM tb_sistemas_atendidos)
                          )
                        AND ID_DEMANDA NOT LIKE '115/2018%'";

            query +="  ORDER BY DT_AUTORIZACAO DESC, ID_DEMANDA";

            var lista = new List<TOOasis>();

            var connection = db.OpenConnection();

            //Create Command
            MySqlCommand cmd = new MySqlCommand(query, connection);
            //Create a data reader and Execute the command
            MySqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                var to = new TOOasis();

                #region Preenche Campos
                to.ID_DEMANDA = dataReader["ID_DEMANDA"].ToString();
                to.NU_PARCELA = dataReader["NU_PARCELA"].ToString();
                to.DE_SISTEMA = dataReader["DE_SISTEMA"].ToString();
                to.DE_SITUACAO_DEMANDA = dataReader["DE_SITUACAO_DEMANDA"].ToString();
                to.DE_ASSUNTO_SOLICITACAO = dataReader["DE_ASSUNTO_SOLICITACAO"].ToString();
                if (dataReader["DT_AUTORIZACAO"].ToString() != "")
                    to.DT_AUTORIZACAO = Convert.ToDateTime(dataReader["DT_AUTORIZACAO"]);
                if (dataReader["ST_GARANTIA"].ToString() != "")
                    to.ST_GARANTIA = Convert.ToInt32(dataReader["ST_GARANTIA"].ToString());
                to.ST_SUSTENTACAO = Convert.ToInt32(dataReader["ST_SUSTENTACAO"].ToString());
                to.VL_CONTAGEM_DETALHADA = Convert.ToDecimal(dataReader["VL_CONTAGEM_DETALHADA"].ToString());
                to.VL_CONTAGEM_ESTIMADA = Convert.ToDecimal(dataReader["VL_CONTAGEM_ESTIMADA"].ToString());
                to.VL_HORAS = Convert.ToDecimal(dataReader["VL_HORAS"].ToString());
                to.VL_PF = Convert.ToDecimal(dataReader["VL_PF"].ToString());
                to.VL_TOTAL_PF_PROPOSTA = Convert.ToDecimal(dataReader["VL_TOTAL_PF_PROPOSTA"].ToString());
                to.ANO_DA_FATURA = dataReader["ANO_DA_FATURA"].ToString();
                to.DE_ACEITE_HOMOLOGACAO = dataReader["DE_ACEITE_HOMOLOGACAO"].ToString();
                to.DE_ACEITE_PRODUCAO = dataReader["DE_ACEITE_PRODUCAO"].ToString();
                to.DE_ACEITE_PROPOSTA = dataReader["DE_ACEITE_PROPOSTA"].ToString();
                to.DE_ASSUNTO_SOLICITACAO = dataReader["DE_ASSUNTO_SOLICITACAO"].ToString();
                to.DE_CONTRATO = dataReader["DE_CONTRATO"].ToString();
                #endregion

                lista.Add(to);

            }
            dataReader.Close();
            return lista;

        }

        public void Dispose()
        {
            db.CloseConnection();
        }
    }
}