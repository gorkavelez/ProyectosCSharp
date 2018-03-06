<%@ Page Title="" Language="C#" MasterPageFile="~/MasterForms/MenuIntranetVerticalBotones.Master" AutoEventWireup="true" CodeBehind="Rechazo.aspx.cs" Inherits="IntranetINDUSAL.WebForms.Rechazo" %>
<%@ Register src="../Controles_Personalizados/INIKER_tecladoNumerico.ascx" tagname="INIKER_tecladoNumerico" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../IntranetINDUSAL.css" rel="Stylesheet" type="text/css" />
    <script src="../IntranetINDUSAL.js" type="text/javascript"></script>    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table>
        <tr>
            <td style="vertical-align:top;">
                <asp:Panel ID="panelDatos" runat="server" CssClass="panelDatos">
                    <table>                    
                        <tr>                        
                            <td class="etiqueta">
                                <asp:Label ID="lbTitTurno" runat="server" Text="TURNO:"></asp:Label>
                            </td>                            
                            <td>                                            
                                <asp:Label ID="lbTurnoSel" runat="server" Text="" CssClass="codigo"></asp:Label> 
                                <br />
                                <asp:Panel ID="panelTurnos" runat="server">
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btOperario" runat="server" Text="OPERARIO" CssClass="botonSeleccion" 
                                    CommandName="operario" CommandArgument="OPERARIO" onclick="btDato_Click"/>
                            </td>
                            <td style="vertical-align:middle">
                                <asp:TextBox ID="txCodOperario" runat="server" AutoPostBack="True"
                                    CssClass="codigoCliente" ontextchanged="txCodOperario_TextChanged"></asp:TextBox>
                                <asp:Label ID="lbNomOperario" runat="server" Text="" CssClass="descripcionCodigo"></asp:Label>
                            </td>                            
                        </tr>
                        <tr>                        
                            <td class="etiqueta">
                                <asp:Label ID="lbTitCarro" runat="server" Text="CARRO/SACA:"></asp:Label>
                            </td>                            
                            <td>                                            
                                <asp:DropDownList ID="ddlTiposCarro" runat="server" 
                                    CssClass="desplegablePedidos" AutoPostBack="True" 
                                    onselectedindexchanged="ddlTiposCarro_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btPeso" runat="server" Text="PESO" CssClass="botonSeleccion" CommandName="peso" />
                            </td>
                            <td>
                                <asp:TextBox ID="txPeso" runat="server" AutoPostBack="True" 
                                    CssClass="txIntro" Enabled="False" ontextchanged="txPeso_TextChanged"></asp:TextBox>
                                <asp:Button ID="btPesoTeclado" runat="server" Text="..." CssClass="botonPeso" 
                                                            CommandName="kilos" CommandArgument="KILOS" OnClick="btDato_Click" />
                            </td>
                        </tr>                                                                
                    </table>         
                </asp:Panel>                                
            </td>
            <td  style="vertical-align:top;">            
                <asp:Panel ID="panelTeclado" runat="server" Enabled="false" CssClass="panelTeclado">
                    <uc1:INIKER_tecladoNumerico ID="INIKER_teclado" runat="server" />            
                </asp:Panel>
                
            </td>
        </tr>                
    </table>     

    <asp:HiddenField ID="pesoCarro" runat="server" />
    <asp:HiddenField ID="hdfPeso" runat="server" 
        onvaluechanged="hdfPeso_ValueChanged" />
</asp:Content>
