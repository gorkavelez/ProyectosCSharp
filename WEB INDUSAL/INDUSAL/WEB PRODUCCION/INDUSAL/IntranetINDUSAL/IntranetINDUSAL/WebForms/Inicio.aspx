<%@ Page Title="INDUSAL" Language="C#" MasterPageFile="~/MasterForms/MenuIntranetVerticalBotones.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="IntranetINDUSAL.WebForms.Inicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../IntranetINDUSAL.css" rel="Stylesheet" type="text/css" />
    <script src="../IntranetINDUSAL.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="panelIdentificacion" runat="server">
        <table>                                        
            <tr>                                                                                             
                <td class="etiqueta">
                    <asp:Label ID="lbEmpresa" runat="server" Text="EMPRESA:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlEmpresas" runat="server"
                        CssClass="desplegable" AutoPostBack="True" 
                        onselectedindexchanged="ddlEmpresas_SelectedIndexChanged">
                    </asp:DropDownList> 
                    <asp:DropDownList ID="ddlEtiquetas" visible="false" runat="server">
                    </asp:DropDownList>                               
                </td>                                    
            </tr>                
        </table>
    </asp:Panel>
    
</asp:Content>
