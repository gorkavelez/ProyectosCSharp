<%@ Page Title="" Language="C#" MasterPageFile="~/MasterForms/MenuIntranetVerticalBotones.Master"
    AutoEventWireup="true" CodeBehind="Etiquetas.aspx.cs" Inherits="IntranetINDUSAL.WebForms.Etiquetas" %>

<%@ Register Src="../Controles_Personalizados/INIKER_tecladoNumerico.ascx" TagName="INIKER_tecladoNumerico"
    TagPrefix="uc1" %>
<%@ Register Src="../Controles_Personalizados/INIKER_surtido.ascx" TagName="INIKER_surtido"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../IntranetINDUSAL.css" rel="Stylesheet" type="text/css" />
    <script src="../IntranetINDUSAL.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td id="tdDatos" style="vertical-align: top;">
                <asp:Panel ID="panelCliente" runat="server">                    
                    <table>
                        <tr id="trCliente">
                            <td class="etiqueta">
                                <asp:Button ID="btCliente" runat="server" Text="CLIENTE" CssClass="boton" 
                                    CommandName="cliente" onclick="btDato_Click"/>
                            </td>
                            <td class="etiqueta">
                                <asp:TextBox ID="txCodCliente" runat="server" AutoPostBack="True" OnTextChanged="txCodCliente_TextChanged"
                                    CssClass="codigo"></asp:TextBox>
                            </td>
                            <td class="descripcion">
                                <asp:Label ID="lbNomCliente" runat="server" Text="" CssClass="descripcionCodigo"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="panelPedido" runat="server">
                    <table>
                        <tr id="trPedidoCliente">
                            <td class="etiqueta">
                                <asp:Button ID="btPedido" runat="server" Text="PEDIDO" CssClass="boton" 
                                    onclick="btPedidos_Click" /> 
                            </td>
                            <td class="etiqueta">
                                <asp:TextBox ID="txNumPedido" runat="server" AutoPostBack="True"
                                    CssClass="numeroPedido" ontextchanged="txNumPedido_TextChanged"></asp:TextBox>
                                <asp:DropDownList ID="ddlPedidos" runat="server" CssClass="desplegablePedidos" 
                                        onselectedindexchanged="ddlPedidos_SelectedIndexChanged" 
                                        AutoPostBack="True" Visible="false">
                                    </asp:DropDownList>
                            </td>
                            <td class="descripcion">
                                &nbsp;</td>
                        </tr>
                    </table>  
                </asp:Panel> 
                <asp:Panel ID="panelProducto" runat="server">
                    <table>
                        <tr id="trProducto">
                            <td class="etiqueta">
                                <asp:Button ID="btProducto" runat="server" Text="PRODUCTO" CssClass="boton" 
                                    CommandName="producto" onclick="btProducto_Click"/>
                            </td>
                            <td class="etiqueta">
                                <asp:TextBox ID="txCodProducto" runat="server" AutoPostBack="false"
                                    CssClass="codigo" Enabled="false"></asp:TextBox>
                            </td>
                            <td class="descripcion">
                                <asp:Label ID="lbDescProducto" runat="server" Text="" CssClass="descripcionCodigo"></asp:Label>
                            </td>
                        </tr>                   
                    </table>
                </asp:Panel>
                <asp:Panel ID="panelSurtido" runat="server" Visible="false" CssClass="panelSurtido">
                    <br />
                    <uc2:INIKER_surtido ID="INIKER_surtidoCliente" runat="server" />
                </asp:Panel>
                
                <asp:Panel ID="panelCarro" runat="server">
                    <table>
                        <tr id="trNumeroCarro">
                            <td class="etiqueta">
                                <asp:Button ID="btNCarro" runat="server" Text="Nº CARRO" CssClass="boton" 
                                    CommandName="nCarro" onclick="btDato_Click"/>
                            </td>
                            <td>
                                <asp:TextBox ID="txNCarro" runat="server" AutoPostBack="True"
                                    CssClass="codigo"></asp:TextBox>
                            </td>                            
                        </tr>                   
                    </table>
                </asp:Panel>
                <asp:Panel ID="panelCopias" runat="server">
                    <table>
                        <tr id="trNumeroCopias">
                            <td class="etiqueta">
                                <asp:Button ID="btNCopias" runat="server" Text="Nº COPIAS" CssClass="boton" 
                                    CommandName="nCopias" onclick="btDato_Click"/>
                            </td>
                            <td>
                                <asp:TextBox ID="txNCopias" runat="server" AutoPostBack="True"
                                    CssClass="codigo"></asp:TextBox>
                            </td>                            
                        </tr>                   
                    </table>
                </asp:Panel>
            </td>
            <td id="tdTeclado" style="vertical-align:top; width:300px;">
                <asp:Panel ID="panelTeclado" runat="server" Enabled="false">
                    <uc1:INIKER_tecladoNumerico ID="INIKER_teclado" runat="server" />
                </asp:Panel>
            </td>
        </tr>
    </table>    
    <asp:HiddenField ID="datoTeclado" runat="server" />
    <asp:HiddenField ID="tipoEtiqueta" runat="server" />
</asp:Content>
