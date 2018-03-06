<%@ Page Title="" Language="C#" MasterPageFile="~/MasterForms/MenuIntranetVerticalBotones.Master" AutoEventWireup="true" CodeBehind="RegistroEmpaquetado.aspx.cs" Inherits="IntranetINDUSAL.WebForms.RegistroEmpaquetado" %>
<%@ Register src="../Controles_Personalizados/INIKER_tecladoNumerico.ascx" tagname="INIKER_tecladoNumerico" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">    
    <link href="../IntranetINDUSAL.css" rel="Stylesheet" type="text/css" />
    <script src="../IntranetINDUSAL.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td style="vertical-align:top;">
                <asp:Panel ID="panelDatos" runat="server" CssClass="panelDatos">                
                    <asp:Panel ID="PanelSeleccion" runat="server">
                        <asp:Panel ID="PanelSeleccionCliente" runat="server">
                            <table>    
                                <tr>                        
                                    <td>
                                        <asp:Button ID="btTurnos" runat="server" CommandName="turno" 
                                            CssClass="botonSeleccion" Text="TURNO" onclick="btTurnos_Click" />                                    
                                    </td>
                                    <td>
                                        <asp:Label ID="lbTurnoSel" runat="server" Text="" CssClass="descripcionCodigo"></asp:Label>
                                        <br />
                                        <asp:Panel ID="panelSeleccionTurno" runat="server">                                                       
                                        </asp:Panel>                                                         
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="btOperario" runat="server" Text="OPERARIO" CssClass="botonSeleccion" 
                                            CommandName="operario" CommandArgument="OPERARIO" onclick="btDato_Click"/>                                    
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txCodOperario" runat="server" AutoPostBack="true" 
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
                                            CommandName="cliente" CommandArgument="CLIENTE" onclick="btDato_Click"/>                        
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txCodCliente" runat="server" AutoPostBack="true" 
                                            CssClass="codigoCliente" ontextchanged="txCodCliente_TextChanged"></asp:TextBox>
                                    </td>
                                    <td>       
                                        <asp:Label ID="lbNomCliente" runat="server" Text="" CssClass="descripcionCodigo"></asp:Label>                     
                                    </td>                                    
                                </tr>
                            </table>
                            <table>
                                <tr>                                                                                             
                                    <td>
                                        <asp:Button ID="btPedidos" runat="server" Text="PEDIDO" CssClass="botonSeleccion" 
                                        CommandName="pedido" onclick="btPedidos_Click" />                                    
                                    </td>   
                                    <td>
                                        <asp:TextBox ID="txNumPedido" runat="server" AutoPostBack="true" 
                                            CssClass="numeroPedido" ontextchanged="txNumPedido_TextChanged"></asp:TextBox>
                                        <asp:DropDownList ID="ddlPedidos" runat="server" AutoPostBack="True" 
                                            CssClass="desplegablePedidos" 
                                            onselectedindexchanged="ddlPedidos_SelectedIndexChanged" Visible="false" >                           
                                        </asp:DropDownList>
                                    </td>                                                 
                                </tr>
                            </table>
                        </asp:Panel>        
                    </asp:Panel>
                    <asp:Panel ID="PanelRegistro" runat="server" Visible="false" CssClass="panelDatos">
                        <asp:Panel ID="PanelResumenSeleccion" runat="server" BackColor="ButtonFace">
                            <table>
                                <tr>                                                                                             
                                    <td class="etiquetaSeleccion">
                                        <asp:Label ID="lbTitTurnoSel" runat="server" Text="TURNO:"></asp:Label>
                                    </td>                    
                                    <td class="descripcionSeleccion">                            
                                        <asp:Label ID="lbTurnoSelRes" runat="server" Text=""></asp:Label>
                                    </td>                                    
                                </tr>
                                <tr>                                                                                             
                                    <td class="etiquetaSeleccion">
                                        <asp:Label ID="lbTitEmpleadoSel" runat="server" Text="EMPLEADO:"></asp:Label>
                                    </td>                    
                                    <td class="descripcionSeleccion">                            
                                        <asp:Label ID="lbEmpleadoSel" runat="server" Text=""></asp:Label>
                                    </td>                                    
                                </tr>                    
                                <tr>                                                                                             
                                    <td class="etiquetaSeleccion">
                                        <asp:Label ID="lbTitClienteSel" runat="server" Text="CLIENTE:"></asp:Label>
                                    </td>                    
                                    <td class="descripcionSeleccion">                            
                                        <asp:Label ID="lbClienteSel" runat="server" Text=""></asp:Label>
                                    </td>                                    
                                </tr>
                                <tr>                                                                                             
                                    <td class="etiquetaSeleccion">
                                        <asp:Label ID="lbTitPedidoSel" runat="server" Text="PEDIDO:"></asp:Label>
                                    </td>                    
                                    <td class="descripcionSeleccion">                            
                                        <asp:Label ID="lbPedidoSel" runat="server" Text=""></asp:Label>
                                    </td>                                    
                                </tr>                            
                            </table>
                        </asp:Panel>
                        <asp:Panel ID="PanelDatosRegistro" runat="server">
                            
                            <br />
                            <table style="width:auto;">
                                <tr>
                                    <td style="vertical-align:top; width:auto;">                        
                                        <table>   
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbTitSurtido" runat="server" Text="SURTIDO:" CssClass="etiqueta"></asp:Label>                                        
                                                </td>
                                                <td class="descripcion">                                        
                                                    <asp:Label ID="lbProductoSel" runat="server" Text="" CssClass="codigo"></asp:Label>
                                                    <br />
                                                    <asp:Panel ID="panelSurtido" runat="server">
                                                    </asp:Panel>
                                                </td>
                                            </tr>                         
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbTitTipoCarro" runat="server" Text="TIPO CARRO:" CssClass="etiqueta"></asp:Label>                                        
                                                </td>
                                                <td class="descripcion">                                        
                                                    <asp:DropDownList ID="ddlTiposCarro" runat="server" CssClass="desplegable"
                                                        onselectedindexchanged="ddlTiposCarro_SelectedIndexChanged" 
                                                        AutoPostBack="True">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>                                                            
                                        </table>                               
                                        <br />
                                        <asp:Panel ID="panelDatosPesaje" runat="server" Enabled="false">                                    
                                            <table>                                
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label1" runat="server" Text="NUMERO CARRO" CssClass="etiqueta"></asp:Label>                                        
                                                    </td>                                    
                                                    <td>
                                                        <asp:Button ID="btPeso" runat="server" Text="PESO" CssClass="botonSeleccion" 
                                                            CommandName="peso" />                                        
                                                    </td>                                    
                                                    <td>
                                                        <asp:Button ID="btCarroCompleto" runat="server" Text="COMPLETO" CssClass="botonSeleccion" 
                                                            onclick="btCarroCompleto_Click" CommandName="completo" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btAddLine" runat="server" Text="AÑADIR" CssClass="botonSeleccion" 
                                                            onclick="btAddLine_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btPesoTeclado" runat="server" Text="..." CssClass="botonPeso" 
                                                            CommandName="peso" CommandArgument="PESO" OnClick="btDato_Click" />
                                                    </td>                                     
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txNCarro" runat="server" 
                                                            CssClass="txIntro" Enabled="False" ontextchanged="txNCarro_TextChanged"></asp:TextBox>
                                                    </td>                                    
                                                    <td>                                        
                                                        <asp:TextBox ID="txPeso" runat="server" AutoPostBack="True" 
                                                            CssClass="txIntro" Enabled="False" ontextchanged="txPeso_TextChanged"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txCompleto" runat="server" CssClass="txIntro" 
                                                        Enabled="False"></asp:TextBox>
                                                    </td>
                                                    <td>&nbsp;</td>                                    
                                                </tr>
                                            </table>                         
                                        </asp:Panel>
                                        <br />
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btCambioVista" runat="server" Text="CAMBIAR&#10;VISTA" 
                                                        CssClass="botonSeleccion" onclick="btCambioVista_Click" />
                                                </td>   
                                                <td>
                                                    <asp:Button ID="btVolver" runat="server" Text="VOLVER" CssClass="botonSeleccion" 
                                                        onclick="btVolver_Click" />
                                                </td>                     
                                                <td>
                                                    <asp:Button ID="btRegistrar" runat="server" Text="PEDIDO&#10;COMPLETO" CssClass="botonSeleccion" 
                                                        onclick="btRegistrar_Click" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btCancelar" runat="server" Text="REINICIAR" CssClass="botonSeleccion" 
                                                        onclick="btCancelar_Click" />
                                                </td>                                            
                                            </tr>
                                        </table>
                                    </td>
                                    
                                </tr>
                            </table>            
                        </asp:Panel>
                        <asp:GridView ID="grdPesajes" runat="server" CellPadding="4" 
                            ForeColor="#333333" GridLines="None" 
                            ondatabound="grdPesajes_DataBound" onrowdeleting="grdPesajes_RowDeleting" 
                            onselectedindexchanged="grdPesajes_SelectedIndexChanged"
                            CssClass="gridLineas" onrowdatabound="grdPesajes_RowDataBound" 
                            Visible="False">
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Font-Names="Calibri" 
                                Font-Size="Medium" />
                                <Columns>
                                    <asp:CommandField 
                                        ShowDeleteButton="True" CancelText="" DeleteText="Borrar" EditText="" 
                                        InsertText="" NewText="" SelectText="" UpdateText="" />                    
                                </Columns>
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#0080C0" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#999999" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />        
                        </asp:GridView>
                        <asp:GridView ID="grdCarros" runat="server" CellPadding="4" 
                            ForeColor="#333333" GridLines="None"            
                            CssClass="gridLineas" onrowdatabound="grdCarros_RowDataBound" 
                            onselectedindexchanged="grdCarros_SelectedIndexChanged" >
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Font-Names="Calibri" 
                                Font-Size="Medium" />
                                <Columns>                    
                                    <asp:CommandField CancelText="" DeleteText="" EditText="" 
                                        InsertText="" InsertVisible="False" NewText="" ShowCancelButton="False" 
                                        ShowSelectButton="True" UpdateText="" />
                                </Columns>
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#0080C0" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#999999" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />        
                        </asp:GridView>
                    </asp:Panel>
                </asp:Panel>  
            </td>
            <td style="vertical-align:top;">  
                <asp:Panel ID="panelTeclado" runat="server" Enabled="false" CssClass="panelTeclado">                        
                    <uc2:INIKER_tecladoNumerico ID="INIKER_teclado" runat="server" />                            
                </asp:Panel>                         
            </td>                    
        </tr>
    </table>
    <asp:HiddenField ID="tipoEmpaquetado" runat="server" />
    <asp:HiddenField ID="respuestaConfirm" runat="server" />
    <asp:HiddenField ID="hdfPeso" runat="server" 
        onvaluechanged="hdfPeso_ValueChanged" />
</asp:Content>
