using AppControle.Classes.DAO;
using AppControle.Classes.TO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppControle.Paginas
{
    public partial class Providencias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {

        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            pnlAdd.Visible = true;
            pnlConsulta.Visible = false;
            BtnAdd.Visible = false;
            BtnSalvar.Visible = true;
            lbMensagem.Text = string.Empty;
            CarregarComboPessoas();
        }

        protected void MontarTelaConsulta()
        {
            pnlAdd.Visible = false;
            pnlConsulta.Visible = true;
            BtnAdd.Visible = true;
            BtnSalvar.Visible = false;

        }

        protected void CarregarComboPessoas()
        {
            using (var dao = new DAOProfissional())
            {
                var lista = dao.ListarProfissionais();

                ddlResponsavel.Items.Clear();
                ddlResponsavel.Items.Add(new ListItem { Text = "N/A", Value = "" });
                foreach (var item in lista)
                {
                    ddlResponsavel.Items.Add(new ListItem { Text =item.Nome, Value = item.IdPessoa.ToString() });
                }
            }
        }

        protected bool ValidarDemanda()
        {
            bool valido = false;
            using (var dao = new DAOOasis())
            {
                 var demanda =  dao.ConsultarPorIdDemanda(txtDemanda.Text, null);
                if (demanda != null)
                {
                    valido = true;
                    txtSistema.Text = demanda.DE_SISTEMA;
                }
            }

            if (!valido)
            {
                lbMensagem.ForeColor = System.Drawing.Color.Red;
                lbMensagem.Text = txtDemanda.Text + " não é uma demanda válida!";
                txtDemanda.Text = string.Empty;
            }

            return valido;
           
        }



        protected void LimparCampos()
        {
            txtDemanda.Text = string.Empty;
            lbMensagem.Text = string.Empty;
            txtPrazoLimite.Text = string.Empty;
            TxtObservacoes.Text = string.Empty;
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            MontarTelaConsulta();
        }

        protected void BtnSalvar_Click(object sender, EventArgs e)
        {
            var to = new TOProvidencia();
            if (!ValidarDemanda())
                return;

            to.ID_DEMANDA = txtDemanda.Text;
            to.ST_CPD = chkCPD.Checked;
            to.DE_OBSERVACOES = TxtObservacoes.Text;
            if (!string.IsNullOrEmpty(txtPrazoLimite.Text))
                to.DT_LIMITE = Convert.ToDateTime(txtPrazoLimite.Text);
            if (ddlResponsavel.SelectedValue != "")
                to.ID_PESSOA_RESPONSAVEL = Convert.ToInt32(ddlResponsavel.SelectedValue);

            using (var dao = new DAOProvidencia())
            {
                dao.IncluirProvidencia(to);
            }

            LimparCampos();
            MontarTelaConsulta();

            lbMensagem.ForeColor = System.Drawing.Color.Green;
            lbMensagem.Text = "Providência para " + txtDemanda.Text + " incluída com sucesso!";

            OdsProvidencia.DataBind();
            GridView1.DataBind();
        }

        protected void txtDemanda_TextChanged(object sender, EventArgs e)
        {
            ValidarDemanda();
        }

        protected void BtnEditar_Click1(object sender, ImageClickEventArgs e)
        {

        }

        protected void BtnResolver_Click(object sender, ImageClickEventArgs e)
        {
            var id = Convert.ToInt32(((ImageButton)sender).CommandArgument);

            using (var dao = new DAOProvidencia())
            {
                dao.AlterarResolvido(id);
            }

            OdsProvidencia.DataBind();
            GridView1.DataBind();

        }

        protected void BtnExcluir_Click(object sender, ImageClickEventArgs e)
        {
            var id = Convert.ToInt32(((ImageButton)sender).CommandArgument);

            using (var dao = new DAOProvidencia())
            {
                dao.ExcluirProvidencia(id);
            }

            GridView1.DataBind();
            OdsProvidencia.DataBind();
        }
    }
}