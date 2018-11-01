<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="AlterarResponsavel.aspx.cs" Inherits="AppControle.Paginas.AlterarResponsavel" %>

   
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
      <!-- Bootstrap core CSS -->
    <link href="../css/shop-item.css" rel="stylesheet">

    <!-- Custom styles for this template -->
    <link href="../vendor/bootstrap/css/bootstrap.css" rel="stylesheet">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
          
    
     <legend>Alterar Responsável</legend>
              <asp:Label ID="lbMensagem" runat="server"></asp:Label>
        <div class="row">
                <div class="form-group col-md-4">
                    <label for="campo1">Nº Demanda</label>
                        <asp:TextBox AutoPostBack="true" OnTextChanged="txtDemanda_TextChanged" MaxLength="10" runat="server" ID="txtDemanda" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group col-md-4">
                    <label for="campo1">Sistema</label>
                        <asp:TextBox Enabled="false" runat="server" ID="txtSistema" CssClass="form-control"></asp:TextBox>
                </div>
            <br />
                <div class="form-group col-md-4">
                    <label for="campo1">Requisitos:</label>
                        <asp:DropDownList  CssClass="form-control dropPadrao"  runat="server" ID="ddlRequisitos" />
                </div>
               <div class="form-group col-md-4">
                    <label for="campo1">Analista:</label>
                        <asp:DropDownList  CssClass="form-control dropPadrao"  runat="server" ID="ddlAnalistas" />
                </div>
                <div class="form-group col-md-12">
                    <label for="campo1">Descrição</label>
                        <asp:TextBox Columns="50" Rows="4" TextMode="MultiLine" ID="TxtObservacoes" CssClass="form-text" runat="server"></asp:TextBox>
                </div>
            </div>
                
            <asp:Button Text="Salvar" runat="server" class="btn btn-success" ID="BtnSalvar" OnClick="BtnSalvar_Click" />
             
            

</asp:Content>
