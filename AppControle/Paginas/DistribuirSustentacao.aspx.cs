using AppControle.Classes.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppControle.Paginas
{
    public partial class DistribuirSustentacao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            CarregarComboSistemas();
        }

        protected void BtnPesquisar_Click(object sender, EventArgs e)
        {
            OdsPrazos.DataBind();
        }

        protected void BtnAlocarPessoas_Click(object sender, ImageClickEventArgs e)
        {
            var parametros = ((ImageButton)sender).CommandArgument;
            txtDemanda.Text = parametros.Split('#')[0];
            txtSistema.Text = parametros.Split('#')[1];

            CarregarComboPessoas();

            pnlAlocar.Visible = true;
            pnlConsulta.Visible = false;
        }

        protected void CarregarComboSistemas()
        {
            ddlSistema.Items.Clear();
            using (var dao = new DAOOasis())
            {
                var lista = dao.ListarSistemas();

                foreach (var item in lista)
                {
                    ddlSistema.Items.Add(new ListItem { Text = item, Value = item });
                }
            }
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
                    if(item.Tipo == 1)
                        ddlAnalistas.Items.Add(new ListItem { Text = item.Nome, Value = item.IdPessoa.ToString() });
                    if (item.Tipo == 2)
                        ddlRequisitos.Items.Add(new ListItem { Text = item.Nome, Value = item.IdPessoa.ToString() });
                }
            }
        }

        public void VoltarTelaConsulta()
        {
            pnlAlocar.Visible = false;
            pnlConsulta.Visible = true;
        }

        protected void BtnSalvar_Click(object sender, EventArgs e)
        {
            using (var dao = new DAOOasis())
            {
                dao.AlocarResponsavel(txtDemanda.Text, Convert.ToInt32(ddlRequisitos.SelectedValue), Convert.ToInt32(ddlAnalistas.SelectedValue));
            }

            OdsPrazos.DataBind();
            GridView1.DataBind();
            VoltarTelaConsulta();
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            VoltarTelaConsulta();
        }
    }
}