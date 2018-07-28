<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="Prazos.aspx.cs" Inherits="AppControle.Paginas.Prazos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <legend>Prazos de Projetos <span style="font-size:10px !important;"> <asp:Label ID="lbData" runat="server" ></asp:Label></span></legend>
    
       <div class="row">
            <div class="form-group col-md-4">
                <label for="campo1">Requisito:</label>
                    <asp:DropDownList CssClass="form-control dropPadrao" runat="server" ID="ddlRequisitos" />
            </div>
            <div class="form-group col-md-4">
                <label for="campo1">Analista:</label>
                    <asp:DropDownList CssClass="form-control dropPadrao" runat="server" ID="ddlAnalistas" />
            </div>
            <div class="form-group col-md-2">
                <label for="campo1">Tipo:</label>
                    <asp:DropDownList CssClass="form-control dropPadrao" runat="server" ID="DropDownList1">
                        <asp:ListItem Text="Todos" Value=""></asp:ListItem>
                        <asp:ListItem Text="Sustentação" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Projeto" Value="0"></asp:ListItem>
                    </asp:DropDownList>
            </div>
        </div>

     <asp:Panel ID="pnlConsulta" runat="server" Style="margin-top: 10px; margin-bottom:10px;">
       


         <div style="float:right; margin:10px;">
             <asp:RadioButtonList style="float: left;margin-right:10px;display:none" ID="rdbColunas" AutoPostBack="true" OnSelectedIndexChanged="rdbColunas_SelectedIndexChanged" runat="server" RepeatDirection="Horizontal">
                 <asp:ListItem Text="Tudo" Value="0" Selected="True"></asp:ListItem>
                 <asp:ListItem Text="Proposta"  Value="1"></asp:ListItem>
                 <asp:ListItem Text="Execução"  Value="2"></asp:ListItem>
             </asp:RadioButtonList>
             <asp:Button ID="btnExport" runat="server" class="btn btn-info" Text="Exportar" OnClick = "ExportToExcel" />
             <asp:Button Text="Pesquisar" runat="server" class="btn btn-success" ID="BtnPesquisar" OnClick="BtnPesquisar_Click" />
             </div>
        <div class="divGrid" style="overflow-x: scroll; max-height=500px; font-size:10px !important;">
         
          <b> Propostas:   </b>     
         <asp:GridView CssClass="GridResultado" ID="GridPropostas" runat="server"
            SkinID="SkinGrdSemPaginacaoComLinhas" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateField HeaderText="Observação">   
                           <ItemTemplate>
                               <asp:Image ImageUrl="~/Images/bt_important.png" Height="20px" Width="20px" ToolTip='<%# Eval("DESC_PROVIDENCIA").ToString() %>' Visible='<%# Eval("DESC_PROVIDENCIA").ToString() != String.Empty  %>' runat="server" />
                           </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ID_DEMANDA" HeaderText="Demanda" />
                    <asp:TemplateField HeaderText="Descrição">   
                         <ItemTemplate>
                             <asp:Label ID="lbID" Text='<%# Eval("DE_ASSUNTO_SOLICITACAO") %>' runat="server"></asp:Label>
                         </ItemTemplate>
                    </asp:TemplateField>
                     <asp:BoundField DataField="DE_SISTEMA" HeaderText="Sistema" />
                    
                     <asp:BoundField DataField="REQUISITO" HeaderText="Requisitos" />
                     <asp:BoundField DataField="ANALISTA" HeaderText="Analista" />
                      <asp:BoundField DataField="Sustentacao" HeaderText="Sustentação" />
                     <asp:BoundField DataField="DE_TIPO_DEMANDA" HeaderText="SubTipo" />
                     <asp:TemplateField HeaderText="Prazo Proposta" >
                     <ItemTemplate>
                          <asp:Label runat="server" ID="PRAZO_PROPOSTA" ForeColor='<%# DefinirCorPrazo(Eval("PRAZO_PROPOSTA").ToString()) %>' Text='<%# TratarData(Eval("PRAZO_PROPOSTA").ToString()) %>' ></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                </Columns>
             </asp:GridView>
            <br />
         <b> Execução:     </b> 
         <asp:GridView CssClass="GridResultado" ID="GridExecucao" runat="server"
            SkinID="SkinGrdSemPaginacaoComLinhas" AutoGenerateColumns="False">
                <Columns>
                      <asp:TemplateField HeaderText="Observação">   
                           <ItemTemplate>
                               <asp:Image ImageUrl="~/Images/bt_important.png" Height="20px" Width="20px" ToolTip='<%# Eval("DESC_PROVIDENCIA").ToString() %>' Visible='<%# Eval("DESC_PROVIDENCIA").ToString() != String.Empty  %>' runat="server" />
                           </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ID_DEMANDA" HeaderText="Demanda" />
                    <asp:TemplateField HeaderText="Descrição">   
                         <ItemTemplate>
                             <asp:Label ID="lbID" Text='<%# Eval("DE_ASSUNTO_SOLICITACAO") %>' runat="server"></asp:Label>
                         </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="DE_SISTEMA" HeaderText="Sistema" />
                      <asp:BoundField DataField="Sustentacao" HeaderText="Sustentação" />
                     <asp:BoundField DataField="DE_TIPO_DEMANDA" HeaderText="SubTipo" />
                      <asp:TemplateField HeaderText="Envio Homol (CAST)" >
                     <ItemTemplate>
                          <asp:Label runat="server" ID="ENVIO_HOMOL" Text='<%# TratarData(Eval("ENVIO_HOMOL").ToString()) %>' ></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Aceite Homol (SEF)" >
                     <ItemTemplate>
                          <asp:Label runat="server" ID="ACEITE_HOMOL" Text='<%# TratarData(Eval("ACEITE_HOMOL").ToString()) %>' ></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>

                    <asp:TemplateField HeaderText="Envio Prod (CAST)" >
                     <ItemTemplate>
                          <asp:Label runat="server" ID="ENVIO_PROD" Text='<%# TratarData(Eval("ENVIO_PROD").ToString()) %>' ></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                    <asp:TemplateField HeaderText="Aceite Prod (SEF)" >
                     <ItemTemplate>
                          <asp:Label runat="server" ID="ACEITE_PROD" Text='<%# TratarData(Eval("ACEITE_PROD").ToString()) %>' ></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                    <asp:TemplateField HeaderText="Prazo Limite Parcela" >
                     <ItemTemplate>
                          <asp:Label runat="server" ID="PRAZO_PARCELA" Text='<%# TratarData(Eval("PRAZO_PARCELA").ToString()) %>' ForeColor='<%# DefinirCorPrazo(Eval("PRAZO_PARCELA").ToString()) %>' ></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                </Columns>
             </asp:GridView>
                <br />
         
           <b> Fechamento de Parcela:     </b> 
         <asp:GridView CssClass="GridResultado" ID="GridFecharParcela" runat="server"
            SkinID="SkinGrdSemPaginacaoComLinhas" AutoGenerateColumns="False">
                <Columns>
                      <asp:TemplateField HeaderText="Observação">   
                           <ItemTemplate>
                               <asp:Image ImageUrl="~/Images/bt_important.png" Height="20px" Width="20px" ToolTip='<%# Eval("DESC_PROVIDENCIA").ToString() %>' Visible='<%# Eval("DESC_PROVIDENCIA").ToString() != String.Empty  %>' runat="server" />
                           </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ID_DEMANDA" HeaderText="Demanda" />
                    <asp:TemplateField HeaderText="Descrição">   
                         <ItemTemplate>
                             <asp:Label ID="lbID" Text='<%# Eval("DE_ASSUNTO_SOLICITACAO") %>' runat="server"></asp:Label>
                         </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="DE_SISTEMA" HeaderText="Sistema" />
                      <asp:BoundField DataField="REQUISITO" HeaderText="Requisitos" />
                       <asp:BoundField DataField="Sustentacao" HeaderText="Sustentação" />
                     <asp:BoundField DataField="DE_TIPO_DEMANDA" HeaderText="SubTipo" />
                  <asp:TemplateField HeaderText="Fechamento (CAST)" >
                     <ItemTemplate>
                          <asp:Label runat="server" ID="PARCELA_FECHADA" Text='<%# TratarData(Eval("PARCELA_FECHADA").ToString()) %>' ></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                      <asp:BoundField DataField="DE_PARECER_PARCELA" HeaderText="Parerecer (SEF)" />
                    <asp:TemplateField HeaderText="Prazo Limite Parcela" >
                     <ItemTemplate>
                          <asp:Label runat="server" ID="PRAZO_PARCELA" Text='<%# TratarData(Eval("PRAZO_PARCELA").ToString()) %>' ForeColor='<%# DefinirCorPrazo(Eval("PRAZO_PARCELA").ToString()) %>' ></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                      <asp:BoundField DataField="VL_CONTAGEM_ESTIMADA" DataFormatString="{0:n2}" HeaderText="Estimada" />
                 <asp:BoundField DataField="VL_CONTAGEM_DETALHADA" DataFormatString="{0:n2}" HeaderText="Detalhada" />
                </Columns>
             </asp:GridView>
                <br />
                
         <asp:GridView CssClass="GridResultado" ID="GridTodosProjetos"  runat="server"    style="display:none"
            SkinID="SkinGrdSemPaginacaoComLinhas" AutoGenerateColumns="False">
             <Columns>
                 <asp:BoundField DataField="ID_DEMANDA" HeaderText="Id" />
                 <asp:BoundField DataField="DE_ASSUNTO_SOLICITACAO" HeaderText="Assunto" />
                 <asp:BoundField DataField="DE_SISTEMA" HeaderText="Sistema" />
                 <asp:BoundField DataField="REQUISITO" HeaderText="Requisitos" />
                 <asp:BoundField DataField="ANALISTA" HeaderText="Analista" />
                 <asp:BoundField DataField="TIPO" HeaderText="Tipo" />
                 <asp:BoundField DataField="DE_TIPO_DEMANDA" HeaderText="SubTipo" />
                 
                 <asp:TemplateField HeaderText="Envio Proposta" >
                     <ItemTemplate>
                          <asp:Label runat="server" ForeColor='<%# DefinirCorEnvioProposta(Eval("ENVIO_PROPOSTA").ToString(),Eval("PRAZO_PROPOSTA").ToString(),Eval("TIPO").ToString(),Eval("DE_TIPO_DEMANDA").ToString() ) %>' ID="ENVIO_PROPOSTA" Text='<%# TratarData(Eval("ENVIO_PROPOSTA").ToString()) %>' ></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Prazo Proposta" >
                     <ItemTemplate>
                          <asp:Label runat="server" ID="PRAZO_PROPOSTA" Text='<%# Eval("TIPO").ToString() =="Sustentação" && (Eval("DE_TIPO_DEMANDA").ToString() == "Corretiva" || Eval("DE_TIPO_DEMANDA").ToString() == "Corretiva Crítica" ) ? "N/A" : TratarData(Eval("PRAZO_PROPOSTA").ToString()) %>' ></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Aceite Proposta" >
                     <ItemTemplate>
                          <asp:Label runat="server" ID="ACEITE_PROPOSTA" Text='<%# Eval("TIPO").ToString() =="Sustentação" && (Eval("DE_TIPO_DEMANDA").ToString() == "Corretiva"|| Eval("DE_TIPO_DEMANDA").ToString() == "Corretiva Crítica" )  ? "N/A" : TratarData(Eval("ACEITE_PROPOSTA").ToString()) %>' ></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Parecer SEF" >
                     <ItemTemplate>
                          <asp:Label runat="server" ID="PARECER_SEF_PROPOSTA" Text='<%# Eval("TIPO").ToString() =="Sustentação" && (Eval("DE_TIPO_DEMANDA").ToString() == "Corretiva"|| Eval("DE_TIPO_DEMANDA").ToString() == "Corretiva Crítica" )  ? "N/A" :  TratarData(Eval("PARECER_SEF_PROPOSTA").ToString()) %>' ></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Envio Homol (CAST)" >
                     <ItemTemplate>
                          <asp:Label runat="server" ID="ENVIO_HOMOL" Text='<%# TratarData(Eval("ENVIO_HOMOL").ToString()) %>' ></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Aceite Homol (SEF)" >
                     <ItemTemplate>
                          <asp:Label runat="server" ID="ACEITE_HOMOL" Text='<%# TratarData(Eval("ACEITE_HOMOL").ToString()) %>' ></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>

                    <asp:TemplateField HeaderText="Envio Prod (CAST)" >
                     <ItemTemplate>
                          <asp:Label runat="server" ID="ENVIO_PROD" Text='<%# TratarData(Eval("ENVIO_PROD").ToString()) %>' ></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                    <asp:TemplateField HeaderText="Aceite Prod (SEF)" >
                     <ItemTemplate>
                          <asp:Label runat="server" ID="ACEITE_PROD" Text='<%# TratarData(Eval("ACEITE_PROD").ToString()) %>' ></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                    <asp:TemplateField HeaderText="Prazo Limite Parcela" >
                     <ItemTemplate>
                          <asp:Label runat="server" ID="PRAZO_PARCELA" Text='<%# TratarData(Eval("PRAZO_PARCELA").ToString()) %>' ForeColor='<%# DefinirCorPrazoParcela(Eval("PRAZO_PARCELA").ToString(), Eval("ACEITE_PROD").ToString(), Eval("PARECER_SEF_PROPOSTA").ToString()) %>' ></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>

                 <asp:TemplateField HeaderText="Fechamento Parcela (CAST)" >
                     <ItemTemplate>
                          <asp:Label runat="server" ID="PARCELA_FECHADA" Text='<%# TratarData(Eval("PARCELA_FECHADA").ToString()) %>' ></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>

                   <asp:TemplateField HeaderText="Estimada" >
                     <ItemTemplate>
                          <asp:Label runat="server" ID="VL_CONTAGEM_ESTIMADA" Text='<%# TratarValor(Eval("VL_CONTAGEM_ESTIMADA").ToString()) %>' ></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Detalhada" >
                     <ItemTemplate>
                          <asp:Label runat="server" ID="VL_CONTAGEM_DETALHADA" Text='<%# TratarValor(Eval("VL_CONTAGEM_DETALHADA").ToString()) %>' ></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
             </Columns>
        </asp:GridView>
        
                </div>
 
       </asp:Panel>

</asp:Content>
