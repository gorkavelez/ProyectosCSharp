<%@ Page Title="" Language="C#" MasterPageFile="~/MasterForms/MenuIntranetVertical.Master" AutoEventWireup="true" CodeBehind="Empaquetado.aspx.cs" Inherits="IntranetINDUSAL.WebForms.Empaquetado" %>
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
                            <asp:Label ID="lbCliente" runat="server" Text="CLIENTE:"></asp:Label>
                        </td>
                        <td class="etiqueta">
                            <asp:TextBox ID="txCodCliente" runat="server" AutoPostBack="true" CssClass="codigo"></asp:TextBox>
                        </td>
                        <td class="descripcion">                            
                            <asp:DropDownList ID="ddlClientes" runat="server" DataTextField="Name" DataValueField="No" 
                                Font-Size="Large" Height="30px" Width="100%" AutoPostBack="True" 
                                onselectedindexchanged="ddlClientes_SelectedIndexChanged">
                            </asp:DropDownList>                            
                        </td>                                    
                    </tr>
                </table>                                 
                <asp:Panel ID="pnlCodsFact" runat="server">
                
                </asp:Panel>
            </td>
            <td style="vertical-align:top; width:auto;">            
                <uc1:INIKER_tecladoNumerico ID="INIKER_teclado" runat="server" />            
            </td>
        </tr>                
    </table>     
    
    </asp:Content>
