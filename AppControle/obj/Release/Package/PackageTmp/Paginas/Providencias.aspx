<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="Providencias.aspx.cs" Inherits="AppControle.Paginas.Providencias" %>

   
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
      <!-- Bootstrap core CSS -->
    <link href="../css/shop-item.css" rel="stylesheet">

    <!-- Custom styles for this template -->
    <link href="../vendor/bootstrap/css/bootstrap.css" rel="stylesheet">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
          
    
     <legend>Providências</legend>
              <asp:Label ID="lbMensagem" runat="server"></asp:Label>
        <div>
            <asp:Button Text="Adicionar" Style="margin-bottom: 10px;" runat="server" class="btn btn-primary" ID="BtnAdd" OnClick="BtnAdd_Click" />
                  <br />
            <asp:Panel ID="pnlAdd" Style="margin:10px" runat="server" Visible="false">
               

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
                           <label for="campo1">Responsável pela Solução</label>
                               <asp:DropDownList  CssClass="form-control dropPadrao"  runat="server" ID="ddlResponsavel" />
                        </div>
                       <div class="form-group col-md-12">
                           <label for="campo1">Descrição</label>
                               <asp:TextBox Columns="50" Rows="4" TextMode="MultiLine" ID="TxtObservacoes" CssClass="form-text" runat="server"></asp:TextBox>
                        </div>
                    <br />
                        <div class="form-group col-md-4">
                           <label for="campo1">Prazo Limite</label>
                               <asp:TextBox MaxLength="10" runat="server" ID="txtPrazoLimite" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group col-md-2">
                           <label for="campo1">Encaminhar ao CPD</label>
                               <asp:CheckBox runat="server" ID="chkCPD" />
                        </div>
                  </div>

                 <asp:Button Text="Voltar" runat="server" class="btn" ID="BtnCancelar" OnClick="BtnCancelar_Click" />
                 <asp:Button Text="Salvar" runat="server" class="btn btn-success" ID="BtnSalvar" OnClick="BtnSalvar_Click" />
                <br />
               </asp:Panel>
    

       <asp:Panel ID="pnlConsulta" runat="server">

           <div class="divGrid">
        <asp:GridView CssClass="GridResultado" ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            DataSourceID="OdsProvidencia" >
            <Columns>
                <asp:BoundField DataField="ID_DEMANDA" HeaderText="Demanda" SortExpression="ID_DEMANDA" />
                <asp:BoundField DataField="DE_OBSERVACOES" HeaderText="Observações" SortExpression="DE_OBSERVACOES" />
                <asp:BoundField DataField="DT_INCLUSAO" HeaderText="Inclusão" SortExpression="DT_INCLUSAO" />
                <asp:BoundField DataField="DT_LIMITE" HeaderText="Atendimento" SortExpression="DT_LIMITE" />
                <asp:BoundField DataField="ID_PESSOA_RESPONSAVEL" HeaderText="Responsável" SortExpression="ID_PESSOA_RESPONSAVEL" />
                <asp:CheckBoxField DataField="ST_CPD" HeaderText="CPD?" SortExpression="ST_CPD" />
                 <asp:TemplateField ItemStyle-Width="50px">
                        <ItemTemplate>
                            <asp:ImageButton runat="server" CommandArgument='<%# Eval("ID_PROVIDENCIA") %>' ImageUrl="~/Images/btn_check.png" ToolTip="Resolver" ID="BtnResolver" OnClick="BtnResolver_Click" />
                               <asp:ImageButton runat="server" CommandArgument='<%# Eval("ID_PROVIDENCIA") %>' ImageUrl="~/Images/btn_delete.png" ToolTip="Excluir" ID="BtnExcluir" OnClick="BtnExcluir_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>
                 </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="OdsProvidencia" runat="server" DataObjectTypeName="AppControle.Classes.TO.TOProvidencia"
            SelectMethod="ListarProvidencias"
            TypeName="AppControle.Classes.DAO.DAOProvidencia" >
            <DeleteParameters>
                <asp:Parameter Name="idProvidencia" Type="Int32" />
            </DeleteParameters>
        </asp:ObjectDataSource>
        
           </div>
 
       </asp:Panel>

        </div>
</asp:Content>
