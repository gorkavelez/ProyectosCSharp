<%@ Page Title="Página indice" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Indice.aspx.cs" Inherits="Formulario_web.Indice" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Bucar mapa
    </h2>
    <div id="Contenedor" class="main">    
        <table id="tprincipal">
            <tr>
                <td><asp:Label ID="lblOrigen" Text="Origen" runat="server"></asp:Label></td>
                <td><asp:TextBox ID="txtOrigen" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="LblDestino" Text="Destino" runat="server"></asp:Label></td>
                <td><asp:TextBox ID="txtDestino" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <table id="subtable">
                    <tr>
                    <td><asp:Button ID="btnBuscar" Text="Buscar" runat="server" onclick="btnBuscar_Click" /></td>
                    </tr>
                </table>
            </tr>
        </table>    
        <p></p>

    <h1>Resultados</h1>
    <asp:GridView ID="grvDatos" runat="server">
    
    </asp:GridView>
    <asp:Label ID="lblResumen" runat="server"></asp:Label>
    </div>
</asp:Content>