<%@ Page Title="" Language="C#" MasterPageFile="~/MasterForms/MenuIntranetVerticalBotones.Master" AutoEventWireup="true" CodeBehind="SalidaCostura.aspx.cs" Inherits="IntranetINDUSAL.WebForms.SalidaCostura" %>
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
                <asp:Panel ID="panelDatos" runat="server" CssClass="panelDatos">                
                    <asp:Panel ID="panelIntroduccion" runat="server" CssClass="panelSurtido">   
                        <asp:Panel ID="PanelDatosEmpleado" runat="server">                    
                            <table>     
                                <tr>                    
                                    <td>
                                        <asp:Button ID="btOperario" runat="server" Text="OPERARIO" CssClass="botonSeleccion" 
                                                CommandName="operario" onclick="btDatoTeclado_Click"/>                            
                                    </td>
                                    <td class="etiqueta">
                                        <asp:TextBox ID="txCodOperario" runat="server" AutoPostBack="True" 
                                             CssClass="codigoCliente" ontextchanged="txCodOperario_TextChanged"></asp:TextBox>
                                    </td>
                                    <td class="descripcion">                            
                                        <asp:Label ID="lbNomOperario" runat="server" Text="" CssClass="descripcionCodigo"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>                        
                                    <td>
                                        <asp:Button ID="btTurnos" runat="server" CommandName="turno" 
                                            CssClass="botonSeleccion" Text="TURNO" onclick="btTurnos_Click" />
                                        
                                    </td>                            
                                    <td>                                            
                                        <asp:Label ID="lbTurnoSel" runat="server" Text="" CssClass="descripcionCodigo"></asp:Label> 
                                        <br />
                                        <asp:Panel ID="panelSeleccionTurno" runat="server" Visible="False"></asp:Panel>
                                    </td>
                                </tr>
                            </table>                        
                        </asp:Panel>                                
                        <asp:Panel ID="panelCliente" runat="server">
                            <table>                    
                                <tr>                    
                                    <td class="etiqueta">
                                        <asp:Button ID="btCliente" runat="server" Text="CLIENTE" CssClass="botonSeleccion" 
                                                CommandName="cliente" onclick="btDatoTeclado_Click" />                            
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txCodCliente" runat="server" CssClass="codigoProducto" 
                                            AutoPostBack="True" ontextchanged="txCodCliente_TextChanged"></asp:TextBox>
                                    </td>
                                    <td class="descripcion">                            
                                        <asp:Label ID="lbNomCliente" runat="server" Text="" CssClass="descripcionCodigo"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>    
                        <asp:Panel ID="panelProducto" runat="server">
                            <table>                                                                                 
                                <tr>
                                    <td>
                                        <asp:Button ID="btProducto" runat="server" Text="PRODUCTO" CssClass="botonSeleccion" 
                                            onclick="btProducto_Click" />
                                        
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txCodProducto" runat="server"
                                            CssClass="codigoProducto" ontextchanged="txCodProducto_TextChanged"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbNomProducto" runat="server" CssClass="descripcionCodigo"></asp:Label>
                                    </td>
                                </tr>   
                                <tr>                    
                                    <td>
                                        <asp:Button ID="btCantidad" runat="server" Text="CANTIDAD" CssClass="botonSeleccion" 
                                                CommandName="cantidad" onclick="btDatoTeclado_Click"/>                            
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txCantidad" runat="server" AutoPostBack="True" 
                                            CssClass="codigoCliente"></asp:TextBox>
                                    </td>
                                </tr>                                      
                            </table>
                        </asp:Panel>                                    
                        <asp:Panel ID="panelSurtido" Visible="false" runat="server" CssClass="panelSurtido">                                                            
                            <br />
                            <uc2:INIKER_surtido ID="INIKER_surtidoCliente" runat="server" />
                        </asp:Panel>
                        <br />
                        <asp:Panel ID="panelSeleccionTrapo" runat="server" Visible="false"></asp:Panel>                                       
                        <br />
                        <asp:Panel ID="panelBotonesOperacion" runat="server">
                            <asp:Label ID="lbOperacionSel" runat="server" Text="" CssClass="codigo"></asp:Label>
                            <br />
                            <asp:Button ID="btCostura" runat="server" Text="ENTRADA&#10;A&#10;COSTURA" CssClass="botonCostura" 
                            CommandName="entrada_costura" onclick="btOperacion_Click" Enabled="False" />
                            <asp:Button ID="btEntradaOxido" runat="server" Text="ENTRADA&#10;DESDE&#10;OXIDO/GRASA" CssClass="botonCostura" 
                            CommandName="oxido_costura" onclick="btOperacion_Click" Enabled="False" />   
                            <asp:Button ID="btAumentoCostura" runat="server" Text="AUMENTO&#10;COSTURA&#10; " 
                            CssClass="botonCostura" CommandName="aumento_costura" 
                                onclick="btOperacion_Click" Enabled="False"/>
                            <asp:Button ID="btTrapos" runat="server" Text="KILOS&#10;TRAPOS&#10; " 
                            CssClass="botonCostura" CommandName="trapos" onclick="btOperacion_Click" 
                                Enabled="False"/>                        
                            <asp:Button ID="btConfeccion" runat="server" Text="CONFECCION&#10; &#10; " 
                            CssClass="botonCostura" CommandName="confeccion" onclick="btOperacion_Click" 
                                Enabled="False"/>
                            <br />                                                                                   
                            <asp:Button ID="btTraspasoCliente" runat="server" 
                                Text="SALIDA&#10;COSTURA&#10;CLIENTE" CssClass="botonCostura" 
                            CommandName="traspaso_cliente" onclick="btOperacion_Click" Enabled="False"/>
                            <asp:Button ID="btTraspasoSucia" runat="server" 
                                Text="SALIDA&#10;COSTURA&#10;SAL" CssClass="botonCostura" 
                            CommandName="traspaso_sucia" onclick="btOperacion_Click" Enabled="False"/>
                            <asp:Button ID="btBaja" runat="server" Text="BAJA&#10;COSTURA&#10; " CssClass="botonCostura" 
                            CommandName="baja_costura" onclick="btOperacion_Click" Enabled="False"/>
                        </asp:Panel>
                        
                    </asp:Panel>
                    <asp:Panel ID="panelAcciones" runat="server" Visible="false">
                        <asp:Panel ID="panelResumenSeleccion" BackColor="ButtonFace" runat="server">
                            <table id="datosSel">
                                <tr id="turnoSel">
                                    <td class="etiquetaSeleccion">
                                        <asp:Label ID="lbTitTurnoSel" runat="server" Text="TURNO:"></asp:Label>
                                    </td>
                                    <td class="descripcionSeleccion">
                                        <asp:Label ID="lbTurnoResumen" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="operarioSel">
                                    <td class="etiquetaSeleccion">
                                        <asp:Label ID="lbTitOperarioSel" runat="server" Text="OPERARIO:"></asp:Label>
                                    </td>
                                    <td class="descripcionSeleccion">
                                        <asp:Label ID="lbOperarioResumen" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="operacionSel">
                                    <td class="etiquetaSeleccion">
                                        <asp:Label ID="lbTitOperacionSel" runat="server" Text="MOVIMIENTO:"></asp:Label>
                                    </td>
                                    <td class="descripcionSeleccion">
                                        <asp:Label ID="lbOperacionResumen" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="clienteSel">
                                    <td class="etiquetaSeleccion">
                                        <asp:Label ID="lbTitClienteSel" runat="server" Text="CLIENTE:"></asp:Label>
                                    </td>
                                    <td class="descripcionSeleccion">
                                        <asp:Label ID="lbClienteResumen" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="productoSel">
                                    <td class="etiquetaSeleccion">
                                        <asp:Label ID="lbTitProductoSel" runat="server" Text="PRODUCTO:"></asp:Label>
                                    </td>
                                    <td class="descripcionSeleccion">
                                        <asp:Label ID="lbProductoResumen" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="cantidadSel">
                                    <td class="etiquetaSeleccion">
                                        <asp:Label ID="lbTitCantidadSel" runat="server" Text="CANTIDAD:"></asp:Label>
                                    </td>
                                    <td class="descripcionSeleccion">
                                        <asp:Label ID="lbCantidadResumen" runat="server"></asp:Label>
                                    </td>
                                </tr>                            
                            </table>
                        </asp:Panel>
                        <br />
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btRegistrar" runat="server" Text="REGISTRAR" CssClass="boton" 
                                        onclick="btRegistrar_Click"/>        
                                </td>                            
                                <td>
                                    <asp:Button ID="btCancelar" runat="server" Text="CANCELAR" 
                                        CssClass="boton" onclick="btCancelar_Click"/>
                                </td>                            
                            </tr>                        
                        </table>
                    </asp:Panel>      
                </asp:Panel>           
            </td>
            <td style="vertical-align:top;">            
                <asp:Panel ID="panelTeclado" runat="server" Enabled="false" CssClass="panelTeclado" >
                    <uc1:INIKER_tecladoNumerico ID="INIKER_teclado" runat="server" />            
                </asp:Panel>                
            </td>
        </tr>
    </table>

</asp:Content>
