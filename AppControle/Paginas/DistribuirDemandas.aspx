<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="DistribuirDemandas.aspx.cs" Inherits="AppControle.Paginas.DistribuirDemandas" %>
   <asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
     
       </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <legend>Distribuir Demandas</legend>
    
    <asp:Panel ID="pnlAlocar" Visible="false" runat="server" Style="margin-top: 10px; margin-bottom:10px;">
        
                <div class="row">
                      <div class="form-group col-md-4">
                           <label for="campo1">Nº Demanda:</label>
                               <asp:TextBox Enabled="false" runat="server" ID="txtDemanda" CssClass="form-control"></asp:TextBox>
                      </div>
                     <div class="form-group col-md-4">
                           <label for="campo1">Sistema:</label>
                               <asp:TextBox Enabled="false" runat="server" ID="txtSistema" CssClass="form-control"></asp:TextBox>
                      </div>
                    <br />
                    </div>
                <div class="row">
                       <div class="form-group col-md-4">
                           <label for="campo1">Requisito:</label>
                               <asp:DropDownList CssClass="form-control dropPadrao" runat="server" ID="ddlRequisitos" />
                        </div>
                       <div class="form-group col-md-4">
                           <label for="campo1">Analista:</label>
                               <asp:DropDownList CssClass="form-control dropPadrao" runat="server" ID="ddlAnalistas" />
                        </div>
                  </div>

                 <asp:Button Text="Voltar" runat="server" class="btn" ID="BtnCancelar" OnClick="BtnCancelar_Click" />
                 <asp:Button Text="Salvar" runat="server" class="btn btn-success" ID="BtnSalvar" OnClick="BtnSalvar_Click" />
                <br />

    </asp:Panel>

     <asp:Panel ID="pnlConsulta" runat="server" Style="margin-top: 10px; margin-bottom:10px;">
         <fieldset class="">
         <div class="row">
         <div class="form-group col-md-4">
            <label for="campo1">Sistema:</label>
            <asp:DropDownList CssClass="form-control dropPadrao" runat="server" ID="ddlSistema" >
            </asp:DropDownList>
            </div>
        </div>
         </fieldset>
          <div class="row">
         <div style="float:right; margin:10px;">
            <asp:Button Text="Pesquisar" runat="server" class="btn btn-success" ID="BtnPesquisar" OnClick="BtnPesquisar_Click" />
        </div>
              </div>
             
         <div class="divGrid">
        
            <asp:GridView CssClass="GridResultado"  ID="GridView1" runat="server"
                DataSourceID="OdsPrazos" Style="align:center" AutoGenerateColumns="False" SkinID="SkinGrdSemPaginacaoComLinhas"
                >
                 <Columns>
                    <asp:BoundField DataField="ID_DEMANDA" HeaderText="Demanda" SortExpression="ID_DEMANDA" />
                    <asp:BoundField DataField="DE_SISTEMA" HeaderText="Sistema" SortExpression="DE_SISTEMA" />
                    <asp:BoundField DataField="DE_SITUACAO_DEMANDA" HeaderText="Status" SortExpression="DE_SITUACAO_DEMANDA" />
                    <asp:BoundField DataField="DT_AUTORIZACAO" HeaderText="Dt. Autorização" DataFormatString="{0:dd/MM/yyyy}" SortExpression="PRAZO_PROPOSTA_SEF" />
                    <asp:BoundField DataField="DE_ASSUNTO_SOLICITACAO" HeaderText="Descrição" SortExpression="PRAZO_PROPOSTA_SEF" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton runat="server" CommandArgument='<%# Eval("ID_DEMANDA") + "#" + Eval("DE_SISTEMA") %>' ImageUrl="~/Images/bt_editar.png" ToolTip="Alocar Pessoas" ID="BtnAlocarPessoas" OnClick="BtnAlocarPessoas_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <asp:ObjectDataSource ID="OdsPrazos" runat="server"
            SelectMethod="DemandasSemAlocacao"
            TypeName="AppControle.Classes.DAO.DAOOasis" >
            <SelectParameters>
                <asp:ControlParameter ControlID="ddlSistema" Name="sistema" PropertyName="SelectedValue" DefaultValue="" Type="String" />
                <asp:Parameter Name="sustentacao" Type="Boolean"  DefaultValue="False"  />
            </SelectParameters>
        </asp:ObjectDataSource>
 
       </asp:Panel>

</asp:Content>
