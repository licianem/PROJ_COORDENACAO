<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="AtualizarComPlanilha.aspx.cs" Inherits="AppControle.Paginas.AtualizarComPlanilha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <legend>Atualizar Dados com Planilha</legend>
    <label>Selecione o Arquivo</label>

    <div class="form-inline">


    <asp:FileUpload ID="FipPlanilha" runat="server" />
    <asp:Button ID="BtnEnviar" OnClick="BtnEnviar_Click" CssClass="btn" runat="server" Text="Enviar" />
    
    <asp:Label ID="UploadStatusLabel" runat="server"></asp:Label>
    </div>

</asp:Content>
