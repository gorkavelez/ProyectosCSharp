<%@ Page Title="DESAPRESTADO ROPA" Language="C#" MasterPageFile="~/MasterForms/MenuIntranetVertical.Master" AutoEventWireup="true" CodeBehind="DesaprestadoRopaOLD.aspx.cs" Inherits="IntranetINDUSAL.WebForms.DesaprestadoRopa1" %>
<%@ Register src="../Controles_Personalizados/INIKER_tecladoNumerico.ascx" tagname="INIKER_tecladoNumerico" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../IntranetINDUSAL.css" rel="Stylesheet" type="text/css" />
    <script src="../IntranetINDUSAL.js" type="text/javascript"></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td style="vertical-align:top; width:auto;">
                <table>
                    <tr>                                                                                             
                        <td class="etiqueta">
                            <asp:Label ID="lbTitProducto" runat="server" Text="PRODUCTO:"></asp:Label>
                        </td>
                        <td class="descripcion">
                            <asp:TextBox ID="txProducto" runat="server" CssClass="txIntro" 
                                AutoPostBack="True" ontextchanged="txProducto_TextChanged"></asp:TextBox>
                            <br />
                            <asp:DropDownList ID="ddlItems" runat="server" CssClass="desplegable" 
                                onselectedindexchanged="ddlItems_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>                                    
                    </tr>
                    <tr>                                                                                             
                        <td class="etiqueta">
                            <asp:Label ID="lbTitCantidad" runat="server" Text="CANTIDAD:"></asp:Label>
                        </td>
                        <td class="descripcion">
                            <asp:TextBox ID="txCantidad" runat="server" CssClass="txIntro" Enabled="False" 
                                ontextchanged="txCantidad_TextChanged"></asp:TextBox>
                        </td>                                    
                    </tr>
                </table>        
                <br />                
                <asp:Label ID="lbMensaje" runat="server"></asp:Label>
                <asp:Panel ID="panelAcciones" runat="server">
                    <table>
                        <tr>     
                            <td class="etiquetaBoton">
                                <asp:Button ID="btCancelar" runat="server"  Text="Cancelar" 
                                     onclick="btCancelar_Click" CssClass="boton"/>               
                            </td>
                            <td class="etiquetaBoton">
                                <asp:Button ID="btDesaprestar" runat="server" Text="Desaprestar" 
                                     Enabled="False" onclick="btDesaprestar_Click" CssClass="boton" Visible="false"/>                                                            
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
            <td style="vertical-align:top; width:auto;">
                <asp:Panel ID="PanelTeclado" runat="server" Enabled="false">
                    <uc1:INIKER_tecladoNumerico ID="INIKER_teclado" runat="server" />            
                </asp:Panel>            
            </td>
        </tr>
    </table>

</asp:Content>
