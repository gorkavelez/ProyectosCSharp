<%@ Page Title="SALIDA COSTURA" Language="C#" MasterPageFile="~/MasterForms/MenuIntranetVerticalBotones.Master" AutoEventWireup="true" CodeBehind="Costura.aspx.cs" Inherits="IntranetINDUSAL.WebForms.Costura" %>
<%@ Register src="../Controles_Personalizados/INIKER_tecladoNumerico.ascx" tagname="INIKER_tecladoNumerico" tagprefix="uc1" %>
<%@ Register src="../Controles_Personalizados/INIKER_surtido.ascx" tagname="INIKER_surtido" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../IntranetINDUSAL.css" rel="Stylesheet" type="text/css" />
    <script src="../IntranetINDUSAL.js" type="text/javascript"></script>    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table style="width:auto;">
        <tr>
            <td style="vertical-align:top;"> 
                <asp:Panel ID="panelTurno" runat="server">
                    <table>
                        <tr>                        
                            <td class="etiqueta">
                                <asp:Label ID="lbTitTurno" runat="server" Text="TURNO:"></asp:Label>                                
                            </td>                            
                            <td>                                            
                                <asp:Label ID="lbTurnoSel" runat="server" Text="" CssClass="codigo"></asp:Label> 
                                <br />
                                <asp:Panel ID="panelSeleccionTurno" runat="server"></asp:Panel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="panelOperario" Enabled="false" runat="server">
                    <table>                    
                        <tr>                    
                            <td class="etiqueta">
                                <asp:Button ID="btOperario" runat="server" Text="OPERARIO" CssClass="boton" 
                                        CommandName="operario" onclick="btDatoTeclado_Click"/>                            
                            </td>
                            <td class="etiqueta">
                                <asp:TextBox ID="txCodOperario" runat="server" AutoPostBack="True" 
                                    CssClass="codigo"></asp:TextBox>
                            </td>
                            <td class="descripcion">                            
                                <asp:Label ID="lbNomOperario" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="panelOperacion" Enabled="false" runat="server">
                </asp:Panel>
                <asp:Panel ID="panelCliente" Enabled="false" runat="server">
                    <table>                    
                        <tr>                    
                            <td class="etiqueta">
                                <asp:Button ID="btCliente" runat="server" Text="CLIENTE" CssClass="boton" 
                                        CommandName="cliente" onclick="btDatoTeclado_Click" />                            
                            </td>
                            <td class="etiqueta">
                                <asp:TextBox ID="txCliente" runat="server" AutoPostBack="True" 
                                    ontextchanged="txCodCliente_TextChanged" CssClass="codigo"></asp:TextBox>
                            </td>
                            <td class="descripcion">                            
                                <asp:Label ID="lbNomCliente" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="panelSurtido" Enabled="false" runat="server">
                    <table>                                                                                 
                        <tr>
                            <td class="etiqueta">
                                <asp:Label ID="lbProdSelec" runat="server" Text="PRODUCTO:"></asp:Label>
                            </td>
                            <td class="etiqueta"></td>
                            <td class="descripcion">
                                <asp:Label ID="lbDescProdSelec" runat="server" CssClass="codigo" Font-Size="X-Large"></asp:Label>
                            </td>
                        </tr>                                        
                    </table>
                    <br />
                    <uc2:INIKER_surtido ID="INIKER_surtidoCliente" runat="server" />
                </asp:Panel>
                <asp:Panel ID="panelCantidad" Enabled="false" runat="server">
                    <table>                    
                        <tr>                    
                            <td class="etiqueta">
                                <asp:Button ID="btCantidad" runat="server" Text="CANTIDAD" CssClass="boton" 
                                        CommandName="cantidad" onclick="btDatoTeclado_Click"/>                            
                            </td>
                            <td class="etiqueta">
                                <asp:TextBox ID="txcantidad" runat="server" AutoPostBack="True" 
                                    CssClass="codigo"></asp:TextBox>
                            </td>                            
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="panelAcciones" Enabled="false" runat="server">
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btRegistrar" runat="server" Text="REGISTRAR" CssClass="boton" 
                                    onclick="btRegistrar_Click" Enabled="false"/>        
                            </td>                            
                            <td>
                                <asp:Button ID="btCancelar" runat="server" Text="CANCELAR" 
                                    CssClass="boton" Enabled="false" />
                            </td>
                        </tr>                        
                    </table>
                </asp:Panel>      
            </td>
            <td style="vertical-align:top; width:300px;">            
                <asp:Panel ID="panelTeclado" runat="server" Enabled="false">
                    <uc1:INIKER_tecladoNumerico ID="INIKER_teclado" runat="server" />            
                </asp:Panel>                
            </td>
        </tr>
    </table>

</asp:Content>
