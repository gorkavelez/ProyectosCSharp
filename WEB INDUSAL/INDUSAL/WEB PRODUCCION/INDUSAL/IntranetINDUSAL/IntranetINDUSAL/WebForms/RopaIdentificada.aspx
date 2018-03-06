<%@ Page Title="UNIFORMIDAD" Language="C#" MasterPageFile="~/MasterForms/MenuIntranetVerticalBotones.Master" AutoEventWireup="true" CodeBehind="RopaIdentificada.aspx.cs" Inherits="IntranetINDUSAL.WebForms.RopaIdentificada" %>
<%@ Register src="../Controles_Personalizados/INIKER_tecladoNumerico.ascx" tagname="INIKER_tecladoNumerico" tagprefix="uc1" %>
<%@ Register src="../Controles_Personalizados/INIKER_surtido.ascx" tagname="INIKER_surtido" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../IntranetINDUSAL.css" rel="Stylesheet" type="text/css" />
    <script src="../IntranetINDUSAL.js" type="text/javascript"></script>    
    <style type="text/css">
        .style1
        {
            width: 39px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table>
        <tr>
            <td style="vertical-align:top;"> 
                <asp:Panel ID="panelDatos" runat="server" CssClass="panelDatos">
                
                    <asp:Panel ID="panelIntroDatos" runat="server">
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btTurnos" runat="server" Text="TURNO" CssClass="botonSeleccion" 
                                        CommandName="turno" onclick="btTurnos_Click" />                                
                                </td>
                                <td>
                                    <asp:Label ID="lbTurnoSel" runat="server" Text="" CssClass="descripcionCodigo"></asp:Label>
                                    <br />
                                    <asp:Panel ID="panelTurnos" runat="server" Visible="False" CssClass="panelTurnos">
                                    </asp:Panel>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        
                        <table>     
                            <tr>                        
                                <td>
                                    <asp:Button ID="btOperario" runat="server" CommandName="empleado" 
                                        CssClass="botonSeleccion" onclick="btDato_Click" Text="OPERARIO" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txCodOperario" runat="server" AutoPostBack="True" 
                                        CssClass="codigoCliente" ontextchanged="txCodOperario_TextChanged"></asp:TextBox>
                                </td>
                                <td>                   
                                    <asp:Label ID="lbNomOperario" runat="server" Text="" CssClass="descripcionCodigo"></asp:Label>                         
                                </td>
                            </tr>
                        </table>
                        
                        <table>                    
                            <tr>                    
                                <td>
                                    <asp:Button ID="btCliente" runat="server" Text="CLIENTE" CssClass="botonSeleccion" 
                                            CommandName="cliente" onclick="btDato_Click" />                            
                                </td>
                                <td>
                                    <asp:TextBox ID="txCodCliente" runat="server" AutoPostBack="True" 
                                        ontextchanged="txCodCliente_TextChanged" CssClass="codigoCliente"></asp:TextBox>
                                </td>
                                <td>                            
                                    <asp:Label ID="lbNomCliente" runat="server" Text="" CssClass="descripcionCodigo"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="panelPedido" runat="server" Visible="true">                        
                            <table>
                                <tr>                        
                                    <td>
                                        <asp:Button ID="btPedidos" runat="server" Text="PEDIDO" CssClass="botonSeleccion" 
                                            CommandName="pedido" onclick="btPedidos_Click" />                                
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txNumPedido" runat="server" AutoPostBack="True" 
                                            CssClass="numeroPedido" ontextchanged="txNumPedido_TextChanged"></asp:TextBox>
                                        <asp:DropDownList ID="ddlPedidos" runat="server" CssClass="desplegablePedidos" 
                                            onselectedindexchanged="ddlPedidos_SelectedIndexChanged" 
                                            AutoPostBack="True" Visible="false">
                                        </asp:DropDownList>
                                    </td>                                                        
                                </tr>
                            </table>
                        </asp:Panel>
                        <table>
                            <tr>                    
                                <td>
                                    <asp:Label ID="lbTitNSerie" runat="server" Text="Nº SERIE:" CssClass="etiqueta"></asp:Label>                            
                                </td>
                                <td class="style1"></td>
                                <td>
                                    <asp:TextBox ID="txNSerie" runat="server" AutoPostBack="True" 
                                        CssClass="numeroPedido" ontextchanged="txNSerie_TextChanged"></asp:TextBox>
                                </td>                                
                            </tr>
                        </table>                        
                    </asp:Panel>   
                                                           
                    <asp:Panel ID="panelAcciones" runat="server">
                        <br />
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btRegistrar" runat="server" Text="REGISTRAR" CssClass="botonSeleccion" 
                                        onclick="btRegistrar_Click" Enabled="false"/>        
                                </td>                                
                                <td>
                                    <asp:Button ID="btCancelar" runat="server" Text="CANCELAR" 
                                        CssClass="botonSeleccion" onclick="btCancelar_Click" Enabled="false" />
                                </td>
                                <td>
                                    <asp:Button ID="btLimpiar" runat="server" Text="LIMPIAR" 
                                        CssClass="botonSeleccion" onclick="btLimpiar_Click" Enabled="false" />
                                </td>
                            </tr>                        
                        </table>
                    </asp:Panel>
                    
                    <asp:Panel ID="panelNumerosSerie" runat="server">
                        <br /> 
                        <table>
                            <tr>
                                <td style="vertical-align:top;">
                                    <asp:GridView ID="gridNumerosSerie" runat="server" CellPadding="4" 
                                        ForeColor="#333333" GridLines="None" CssClass="gridLineas"
                                        onrowdeleting="gridNumerosSerie_RowDeleting" 
                                        onrowdatabound="gridNumerosSerie_RowDataBound">
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                            <Columns>
                                                <asp:CommandField 
                                                    ShowDeleteButton="True" CancelText="" DeleteText="Borrar" EditText="" 
                                                    InsertText="" NewText="" SelectText="" UpdateText="" />                                    
                                            </Columns>
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <EditRowStyle BackColor="#999999" />
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    </asp:GridView>
                                </td>
                                <td style="vertical-align:top;">
                                    <asp:GridView ID="gridProductos" runat="server" CellPadding="4" 
                                        ForeColor="#333333" GridLines="None" CssClass="gridLineas">
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />                                
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <EditRowStyle BackColor="#999999" />
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    </asp:GridView>        
                                </td>
                            </tr>
                        </table>                        
                    </asp:Panel>
                    
                    <asp:Panel ID="panelProductos" runat="server">
                        <br /> 
                        
                    </asp:Panel>
                
                </asp:Panel>
            </td>
            <td style="vertical-align:top;">            
                <asp:Panel ID="panelTeclado" runat="server" Enabled="false" CssClass="panelTeclado">
                    <uc1:INIKER_tecladoNumerico ID="INIKER_teclado" runat="server" />            
                </asp:Panel>                
            </td>
        </tr>
    </table>
    
    <asp:HiddenField ID="tipoConteo" runat="server" />
</asp:Content>
