using AppControle.Classes.DAO;
using AppControle.Classes.TO;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppControle.Paginas
{
    public partial class AtualizarComPlanilha : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnEnviar_Click(object sender, EventArgs e)
        {
            if (FipPlanilha.HasFile && Path.GetExtension(FipPlanilha.FileName) == ".xlsx")
            {
                using (var excel = new ExcelPackage(FipPlanilha.PostedFile.InputStream))
                {
                    var tbl = new DataTable();
                    var ws = excel.Workbook.Worksheets.First();
                    var hasHeader = true;  // adjust accordingly
                                           // add DataColumns to DataTable
                    foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                        tbl.Columns.Add(hasHeader ? firstRowCell.Text
                            : String.Format("Column {0}", firstRowCell.Start.Column));

                    // add DataRows to DataTable
                    int startRow = hasHeader ? 2 : 1;
                    for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                    {
                        var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                        DataRow row = tbl.NewRow();
                        foreach (var cell in wsRow)
                        {
                            //if(cell.Address.StartsWith("U") || cell.Address.StartsWith("V") || cell.Address.StartsWith("W") || cell.Address.StartsWith("AA") || cell.Address.StartsWith("AB"))
                            //    row[cell.Start.Column - 1] = cell.Value;
                            //else
                            //    row[cell.Start.Column - 1] = cell.Text;

                           row[cell.Start.Column - 1] = cell.Value;
                        }
                        tbl.Rows.Add(row);
                    }
                    var msg = String.Format("DataTable criada com sucesso a partir de arquivo Excel. Colunas-contagem:{0} Linhas-contagem:{1}",
                                            tbl.Columns.Count, tbl.Rows.Count);

                    var demandas = new List<TOOasis>();
                    foreach (DataRow dr in tbl.Rows)
                    {
                        if (dr[0].ToString() == "")
                            continue;

                        var to = new TOOasis();
                        to.ID_DEMANDA = dr[0].ToString();
                        to.DE_DEMANDA = dr[1].ToString() + "/" + dr[2].ToString();
                        to.NU_PARCELA = dr[3].ToString();
                        to.DE_SISTEMA = dr[4].ToString();
                        to.DE_TIPO_DEMANDA = dr[5].ToString();
                        to.ST_SUSTENTACAO = dr[6].ToString().ToUpper().Trim() == "SIM"? 1 : 0;
                        to.DE_PRIORIDADE = dr[7].ToString();
                        to.DE_ASSUNTO_SOLICITACAO = dr[8].ToString();
                        to.DE_GESTOR_TECNICO = dr[9].ToString();
                        to.DE_GESTOR_OPERACIONAl = dr[10].ToString();
                        to.VL_TOTAL_PF_PROPOSTA = dr[11].ToString() == "" ? 0 : Convert.ToDecimal(dr[11]);
                        to.VL_CONTAGEM_ESTIMADA = dr[12].ToString() == "" ? 0 : Convert.ToDecimal(dr[12]);
                        to.VL_CONTAGEM_DETALHADA = dr[13].ToString() == "" ? 0 : Convert.ToDecimal(dr[13]);
                        to.VL_HORAS = dr[14].ToString() == "" ? 0 : Convert.ToDecimal(dr[14]);
                        to.VL_PF = dr[15].ToString() == "" ? 0 : Convert.ToDecimal(dr[15]);
                        to.DE_INM = dr[16].ToString();
                        to.DE_CONTRATO = dr[17].ToString();


                        DateTime? dataNula =null;

                        to.DE_SITUACAO_DEMANDA = dr[18].ToString();
                        to.DE_SITUACAO_PARCELA = dr[19].ToString();
                        to.DT_ULTIMA_ATUALIZACAO = !String.IsNullOrEmpty(dr[20].ToString()) ? Convert.ToDateTime(dr[20].ToString()) : dataNula;
                        to.DT_AUTORIZACAO = !String.IsNullOrEmpty(dr[21].ToString()) ? Convert.ToDateTime(dr[21].ToString()) : dataNula;
                        to.DT_PRAZO_LIMITE_PROPOSTA = !String.IsNullOrEmpty(dr[22].ToString()) ? Convert.ToDateTime(dr[22].ToString()) : dataNula;
                        to.DE_FECHAMENTO_PROPOSTA = dr[23].ToString();
                        to.DE_ACEITE_PROPOSTA = dr[24].ToString();
                        to.DE_PARECER_PROPOSTA = dr[25].ToString();
                        to.DT_AUTORIZACAO_PARCELA = !String.IsNullOrEmpty(dr[26].ToString()) ? Convert.ToDateTime(dr[26].ToString()) : dataNula;
                        to.DT_PRAZO_LIMITE_PARCELA = (!String.IsNullOrEmpty(dr[27].ToString()) && dr[27].ToString() != "Pendente" )? Convert.ToDateTime(dr[27].ToString()) : dataNula;
                        to.DE_FECHAMENTO_PARCELA = dr[28].ToString();
                        to.DE_PARECER_PARCELA = dr[29].ToString();
                        to.DE_FECHAMENTO_HOMOLOGACAO = dr[30].ToString();

                        to.DE_ACEITE_HOMOLOGACAO = dr[31].ToString();
                        to.DE_FECHAMENTO_PRODUCAO = dr[32].ToString();
                        to.DE_ACEITE_PRODUCAO = dr[33].ToString();
                        to.DE_NUMERO_DA_FATURA = dr[34].ToString();
                        to.ANO_DA_FATURA = dr[35].ToString();
                        to.ST_GARANTIA = dr[36].ToString().ToUpper().Trim() == "SIM"? 1 : 0;
                        to.DE_PROFISSIONAL = dr[37].ToString();

                        if (to.DT_AUTORIZACAO.HasValue)
                        {
                            //Tratamento
                            if (to.DE_FECHAMENTO_HOMOLOGACAO.ToUpper().Trim() == "PENDENTE" && to.DE_ACEITE_PRODUCAO.ToUpper().Trim() == "PENDENTE")
                                to.DE_SITUACAO_PARCELA = "Parcela Iniciada";

                            if (to.DE_FECHAMENTO_HOMOLOGACAO.ToUpper().Trim() != "PENDENTE" && !String.IsNullOrEmpty(to.DE_FECHAMENTO_HOMOLOGACAO) && to.DE_ACEITE_PRODUCAO.ToUpper().Trim() == "PENDENTE")
                                to.DE_SITUACAO_PARCELA = "Homologação Aprovada";

                            if (to.DE_ACEITE_HOMOLOGACAO.ToUpper().Trim() != "PENDENTE" && !String.IsNullOrEmpty(to.DE_FECHAMENTO_HOMOLOGACAO) && to.DE_ACEITE_PRODUCAO.ToUpper().Trim() != "PENDENTE" && !String.IsNullOrEmpty(to.DE_FECHAMENTO_PRODUCAO))
                                to.DE_SITUACAO_PARCELA = "Produção Aprovada";
                        }
                        demandas.Add(to);

                    }
                    string msg2 = "";
                    using (var bd = new DAOOasis())
                    {
                        msg2 = bd.IncluirPlanilha(demandas);
                    }

                    UploadStatusLabel.Text = msg + "<br />" + msg2;
                }
            }
            else
            {
                UploadStatusLabel.Text = "You did not specify a file to upload.";
            }
        }
    }
}