<%@ Page Title="" Language="C#" MasterPageFile="~/MasterForms/MenuIntranetVerticalBotones.Master"
    AutoEventWireup="true" CodeBehind="PlegadoNuevo.aspx.cs" Inherits="IntranetINDUSAL.WebForms.PlegadoNuevo" %>
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
                <asp:Panel ID="panelDatos" runat="server" CssClass="panelDatos">                
                    <asp:Panel ID="panelSeleccion" runat="server">                    
                        <table>
                            <tr id="trCliente">
                                <td>
                                    <asp:Button ID="btCliente" runat="server" Text="CLIENTE" CssClass="botonSeleccion" CommandName="cliente"
                                        OnClick="btCliente_Click" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txCodCliente" runat="server" AutoPostBack="True" OnTextChanged="txCodCliente_TextChanged"
                                        CssClass="codigoProducto"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lbNomCliente" runat="server" Text="" CssClass="descripcionCodigo"></asp:Label>
                                </td>
                            </tr>
                            <tr id="trPedidoCliente">
                                <td>
                                    <asp:Button ID="btPedidos" runat="server" Text="PEDIDO" CssClass="botonSeleccion" 
                                        CommandName="pedido" onclick="btPedidos_Click" />                                
                                </td>
                                <td>
                                    <asp:TextBox ID="txNumPedido" runat="server" AutoPostBack="True"
                                        CssClass="numeroPedido" ontextchanged="txNumPedido_TextChanged"></asp:TextBox>
                                    <asp:DropDownList ID="ddlPedidos" runat="server" CssClass="desplegablePedidos" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlPedidos_SelectedIndexChanged" Visible="false">
                                    </asp:DropDownList>                                    
                                </td>                                
                            </tr>                        
                            <tr id="trTurnoSel">
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
                            <tr id="trEmpleado">
                                <td>
                                    <asp:Button ID="btOperario" runat="server" Text="OPERARIO" CssClass="botonSeleccion" 
                                        CommandName="operario" onclick="btDato_Click"/>
                                </td>
                                <td>
                                    <asp:TextBox ID="txCodOperario" runat="server" AutoPostBack="True"
                                        CssClass="codigoCliente" ontextchanged="txCodOperario_TextChanged"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lbNomOperario" runat="server" Text="" CssClass="descripcionCodigo"></asp:Label>                                
                                    <br />
                                </td>
                            </tr>
                            <tr id="trMaquina">
                                <td>
                                    <asp:Label ID="lbCalandra" runat="server" Text="MAQUINA:" CssClass="etiqueta"></asp:Label>
                                </td>
                                <td>                                
                                    <asp:DropDownList ID="ddlMaquinas" runat="server" AutoPostBack="True" 
                                        CssClass="desplegableCarros" 
                                        OnSelectedIndexChanged="ddlMaquinas_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <br />
                                </td>
                            </tr>
                        </table>                    
                    </asp:Panel>
                    <asp:Panel ID="panelDatosSeleccionados" BackColor="ButtonFace" runat="server">
                        <table id="datosSel">
                            <tr id="clienteSel">
                                <td class="etiquetaSeleccion">
                                    <asp:Label ID="lbTitClienteSel" runat="server" Text="CLIENTE:"></asp:Label>
                                </td>
                                <td class="descripcionSeleccion">
                                    <asp:Label ID="lbClienteSel" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr id="pedidoSel">
                                <td class="etiquetaSeleccion">
                                    <asp:Label ID="lbTitPedidoSel" runat="server" Text="PEDIDO:"></asp:Label>
                                </td>
                                <td class="descripcionSeleccion">
                                    <asp:Label ID="lbpedidoSel" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr id="maquinaSel">
                                <td class="etiquetaSeleccion">
                                    <asp:Label ID="lbTitMaquinaSel" runat="server" Text="MAQUINA:"></asp:Label>
                                </td>
                                <td class="descripcionSeleccion">
                                    <asp:Label ID="lbMaquinaSel" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr id="ProductoSel">
                                <td class="etiquetaSeleccion">
                                    <asp:Label ID="lbTitProductoSel" runat="server" Text="PRODUCTO:"></asp:Label>
                                </td>
                                <td class="descripcionSeleccion">
                                    <asp:Label ID="lbProductoSel" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr id="cantPaqueteProductoSel">
                                <td class="etiquetaSeleccion">
                                    <asp:Label ID="lbTitCantPaq" runat="server" Text="UDS. PAQUETE:"></asp:Label>
                                </td>
                                <td class="descripcionSeleccion">
                                    <asp:Label ID="lbCantPaq" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <br />
                    <asp:Panel ID="panelSurtido" runat="server" Visible="false" CssClass="panelSurtido">
                        <uc2:INIKER_surtido ID="INIKER_surtidoCliente" runat="server" />
                        <br />
                        <asp:Button ID="btVolverInicio" CommandName="inicio" runat="server" Text="VOLVER" CssClass="boton" OnClick="btVolver_Click" />
                    </asp:Panel>
                    <asp:Panel ID="panelRegistro" runat="server" Visible="false">
                        <br />
                        <table id="introQtys">
                            <tr id="botonesIntroCant">
                                <td>
                                    <asp:Button ID="btAddPaquete" runat="server" Text="PAQUETES" CssClass="botonSeleccion" OnClick="btDato_Click"
                                        CommandName="addPaq" />
                                </td>
                                <td>
                                    <asp:Button ID="btUnidades" runat="server" Text="UNIDADES" CssClass="botonSeleccion" OnClick="btDato_Click"
                                        CommandName="uds" />
                                </td>
                                <td>
                                    <asp:Label ID="lbTitUdsTotal" runat="server" Text="UDS. TOTALES" CssClass="etiqueta"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbTitUdsContadas" runat="server" Text="UDS.&#10;CONTADAS" 
                                        ForeColor="Red" CssClass="etiqueta"></asp:Label>
                                </td>
                            </tr>
                            <tr id="cantidades">
                                <td>
                                    <asp:TextBox ID="txPaq" runat="server" CssClass="txIntro"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txUnidades" runat="server" AutoPostBack="True" CssClass="txIntro"
                                        Enabled="False"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txUdsTotal" runat="server" ReadOnly="true" CssClass="txIntro"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txUdsRegistradas" runat="server" ReadOnly="true" 
                                        CssClass="txIntro" ForeColor="Red"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table id="acciones">
                            <tr>                            
                                <td>
                                    <asp:Button ID="btVolver" CommandName="surtido" runat="server" Text="VOLVER" CssClass="boton" OnClick="btVolver_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btRegistrar" runat="server" Text="REGISTRAR" CssClass="boton" OnClick="btRegistrar_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btCancelar" runat="server" Text="REINICIAR" CssClass="boton" 
                                        OnClick="btCancelar_Click" Visible="False" />
                                </td>
                            </tr>
                            <tr>                            
                                <td>
                                    <asp:Button ID="btOxido" runat="server" Text="A&#10;OXIDO/GRASA" 
                                        CssClass="boton" CommandName="oxido" onclick="btDato_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btEtiqueta" runat="server" Text="ETIQUETAS" CssClass="boton" 
                                        onclick="btDato_Click" CommandName="etiqueta" />
                                </td>
                                <td>
                                    <asp:Button ID="btEtiquetaCliente" runat="server" Text="ETIQUETA&#10;CLIENTE" CssClass="boton" 
                                        onclick="btDato_Click" CommandName="etiquetaCliente" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </asp:Panel>
            </td>
            <td id="tdTeclado" style="vertical-align:top;">
                <asp:Panel ID="panelTeclado" runat="server" Enabled="false" CssClass="panelTeclado">
                    <uc1:INIKER_tecladoNumerico ID="INIKER_teclado" runat="server" />
                </asp:Panel>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="tipoPlegado" runat="server" />
    <asp:HiddenField ID="mostrarPopUp" runat="server" />
    </asp:Content>
