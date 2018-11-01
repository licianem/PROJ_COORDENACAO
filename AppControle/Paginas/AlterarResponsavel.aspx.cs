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
    public partial class AlterarResponsavel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {

        }

        protected void CarregarComboPessoas()
        {
            using (var dao = new DAOProfissional())
            {
                var lista = dao.ListarProfissionais();

                ddlAnalistas.Items.Clear();
                ddlRequisitos.Items.Clear();
                ddlAnalistas.Items.Add((new ListItem { Text = "NÃO HÁ", Value = "0" }));

                foreach (var item in lista)
                {
                    if (item.Tipo == 1)
                        ddlAnalistas.Items.Add(new ListItem { Text = item.Nome, Value = item.IdPessoa.ToString() });
                    if (item.Tipo == 2)
                        ddlRequisitos.Items.Add(new ListItem { Text = item.Nome, Value = item.IdPessoa.ToString() });
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
                    CarregarComboPessoas();
                    if (demanda.ID_REQUISITOS > 0 && demanda.ID_ANALISTA > 0)
                    {
                        ddlRequisitos.SelectedValue = demanda.ID_REQUISITOS.ToString();
                        ddlAnalistas.SelectedValue = demanda.ID_ANALISTA.ToString();
                    }
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
        }

        protected void BtnSalvar_Click(object sender, EventArgs e)
        {
          
            if (!ValidarDemanda())
                return;

            using (var dao = new DAOOasis())
            {
                dao.EditarResponsavel(txtDemanda.Text, Convert.ToInt32(ddlRequisitos.SelectedValue), Convert.ToInt32(ddlAnalistas.SelectedValue));
            }

            LimparCampos();

            lbMensagem.ForeColor = System.Drawing.Color.Green;
            lbMensagem.Text = "Providência para " + txtDemanda.Text + " incluída com sucesso!";

        }

        protected void txtDemanda_TextChanged(object sender, EventArgs e)
        {
            ValidarDemanda();
        }

      
  
    }
}