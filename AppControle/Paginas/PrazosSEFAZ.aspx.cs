using AppControle.Classes.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Configuration;

namespace AppControle.Paginas
{
    public partial class PrazosSEFAZ : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarComboPessoas();
                CarregarGrids();

                try
                {
                    using (var bd = new DAOOasis())
                    {
                        string ultima = bd.ConsultarUltimaAtualizacao();
                        lbData.Text = string.Format("(Atualizado em {0})", ultima);
                    }
                }
                catch 
                {

                }
            }
        }

        protected void CarregarGrids()
        {
            using (var bd = new DAOOasis())
            {
                var dados = bd.RelatorioPrazosObjeto(Convert.ToInt32(ddlRequisitos.SelectedValue), Convert.ToInt32(ddlAnalistas.SelectedValue), DropDownList1.SelectedValue);

                GridPropostas.DataSource = dados.Where(x => x.TemProposta && x.TemPropostaEnviada && !x.TemAceiteProposta).OrderBy(x => Convert.ToDateTime(x.PRAZO_PROPOSTA)).ToList();
                GridPropostas.DataBind();

                GridExecucao.DataSource = dados.Where(x => x.TemAceiteProposta && !x.TemPendenciaExecucaoCast  && !x.TemAceitesExecucao).OrderBy(x => Convert.ToDateTime(x.PRAZO_PARCELA)).ToList();
                GridExecucao.DataBind();

                GridFecharParcela.DataSource = dados.Where(x => x.TemPendenciaParcerParcelaSEF).OrderBy(x => Convert.ToDateTime(x.PRAZO_PARCELA)).ToList();
                GridFecharParcela.DataBind();

                GridTodosProjetos.DataSource = dados;
                GridTodosProjetos.DataBind();
            }
        }

        protected System.Drawing.Color DefinirCorPrazo(string dtLimite)
        {
            DateTime CdtPrazo;
            if (DateTime.TryParse(dtLimite, out CdtPrazo))
            {

                if (DateTime.Now >= CdtPrazo)
                    return System.Drawing.Color.Red;

                if (DateTime.Now.AddDays(10) >= CdtPrazo)
                    return System.Drawing.Color.Orange;

            }

            return System.Drawing.Color.Black;
        }


        protected System.Drawing.Color DefinirCorEnvioProposta(string dtEnvio, string dtPrazo, string deTipo, string deTipoDemanda)
        {
            try
            {


                DateTime CdtEnvio;
                DateTime CdtPrazo;
                DateTime.TryParse(dtPrazo, out CdtPrazo);

                if (deTipo == "Sustentação" && deTipoDemanda != "Corretiva")
                    return System.Drawing.Color.Black;

                if (dtEnvio == "Pendente")
                {
                    if (DateTime.Now >= CdtPrazo)
                        return System.Drawing.Color.Red;

                    if (DateTime.Now.AddDays(5) >= CdtPrazo)
                        return System.Drawing.Color.Gold;
                }
                else
                {
                    if (DateTime.TryParse(dtPrazo, out CdtPrazo))
                    {
                        return System.Drawing.Color.Green;

                    }
                }

                return System.Drawing.Color.Black;

            }
            catch (Exception)
            {

                return System.Drawing.Color.Black;
            }

            
        }

        protected System.Drawing.Color DefinirCorPrazoParcela(string dtPrazo, string dtAceiteProducao, string parecerSefProposta)
        {
            try
            {
                if(parecerSefProposta == "Pendente")
                    return System.Drawing.Color.Black;

                DateTime CdPrazo;
                DateTime CdAceiteProducao;
                DateTime.TryParse(dtPrazo, out CdPrazo);


                if (dtAceiteProducao == "Pendente")
                {
                    if (DateTime.Now >= CdPrazo)
                        return System.Drawing.Color.Red;

                    if (DateTime.Now.AddDays(5) >= CdPrazo)
                        return System.Drawing.Color.Gold;
                }
                else
                {
                    if (DateTime.TryParse(dtAceiteProducao, out CdAceiteProducao))
                    {
                        return System.Drawing.Color.Green;
                    }
                }

                return System.Drawing.Color.Black;

            }
            catch (Exception)
            {

                return System.Drawing.Color.Black;
            }


        }

        protected string TratarData(string dt)
        {
            DateTime dateValue;
            if (DateTime.TryParse(dt, out dateValue))
                return dateValue.ToShortDateString();
            else
                return dt;

        }

        protected string TratarValor(string vl)
        {
            return Convert.ToDecimal(vl).ToString("0.##");
        }

        protected void CarregarComboPessoas()
        {
            using (var dao = new DAOProfissional())
            {
                var lista = dao.ListarProfissionais();

                ddlAnalistas.Items.Clear();
                ddlRequisitos.Items.Clear();
                ddlAnalistas.Items.Add((new ListItem { Text = "Todos", Value = "0" }));
                ddlRequisitos.Items.Add((new ListItem { Text = "Todos", Value = "0" }));

                foreach (var item in lista)
                {
                    if (item.Tipo == 1)
                        ddlAnalistas.Items.Add(new ListItem { Text = item.Nome, Value = item.IdPessoa.ToString() });
                    if (item.Tipo == 2)
                        ddlRequisitos.Items.Add(new ListItem { Text = item.Nome, Value = item.IdPessoa.ToString() });
                }
            }
        }

        protected void BtnPesquisar_Click(object sender, EventArgs e)
        {
            CarregarGrids();
        }

        protected void ExportToExcel(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Prazos.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages

                GridTodosProjetos.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in GridTodosProjetos.HeaderRow.Cells)
                {
                    cell.BackColor = GridTodosProjetos.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in GridTodosProjetos.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = GridTodosProjetos.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = GridTodosProjetos.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                GridTodosProjetos.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void rdbColunas_SelectedIndexChanged(object sender, EventArgs e)
        {
            int value = Convert.ToInt32(rdbColunas.SelectedValue);
            HabilitaTodasColunas();
            switch (value)
            {
                case 1:
                    GridTodosProjetos.Columns[10].Visible = false;
                    GridTodosProjetos.Columns[11].Visible = false;
                    GridTodosProjetos.Columns[12].Visible = false;
                    GridTodosProjetos.Columns[13].Visible = false;
                    GridTodosProjetos.Columns[14].Visible = false;
                    break;
                case 2:
                    GridTodosProjetos.Columns[6].Visible = false;
                    GridTodosProjetos.Columns[7].Visible = false;
                    GridTodosProjetos.Columns[8].Visible = false;
                    GridTodosProjetos.Columns[9].Visible = false;
                    GridTodosProjetos.Columns[15].Visible = false;
                    break;
            }
        }

        protected void HabilitaTodasColunas()
        {
            GridTodosProjetos.Columns[6].Visible = true;
            GridTodosProjetos.Columns[7].Visible = true;
            GridTodosProjetos.Columns[8].Visible = true;
            GridTodosProjetos.Columns[9].Visible = true;
            GridTodosProjetos.Columns[14].Visible = true;
            GridTodosProjetos.Columns[10].Visible = true;
            GridTodosProjetos.Columns[11].Visible = true;
            GridTodosProjetos.Columns[12].Visible = true;
            GridTodosProjetos.Columns[13].Visible = true;
            GridTodosProjetos.Columns[14].Visible = true;
        }
    }
}