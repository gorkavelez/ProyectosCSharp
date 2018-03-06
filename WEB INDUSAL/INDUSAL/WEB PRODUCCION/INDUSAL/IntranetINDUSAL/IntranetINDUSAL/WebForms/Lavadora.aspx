<%@ Page Title="" Language="C#" MasterPageFile="~/MasterForms/MenuIntranetVerticalBotones.Master" AutoEventWireup="true" CodeBehind="Lavadora.aspx.cs" Inherits="IntranetINDUSAL.WebForms.Lavadora" %>
<%@ Register src="../Controles_Personalizados/INIKER_tecladoNumerico.ascx" tagname="INIKER_tecladoNumerico" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../IntranetINDUSAL.css" rel="Stylesheet" type="text/css" />
    <script src="../IntranetINDUSAL.js" type="text/javascript"></script>    
    <style type="text/css">
        .style1
        {
            width: 20px;
        }
        .style2
        {
            width: 6px;
        }
        .style3
        {
            width: 38px;
        }
    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <table>
        <tr>
            <td>
                <asp:Panel ID="panelDatos" runat="server" CssClass="panelDatos">
                
                    <asp:Panel ID="PanelMaquina" runat="server">
                        <table>                                        
                            <tr>                                                                                             
                                <td>
                                    <asp:Label ID="lbMaquina" runat="server" Text="MAQUINA:" CssClass="etiqueta"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlMaquinas" runat="server"
                                        CssClass="desplegable" AutoPostBack="True" 
                                        onselectedindexchanged="ddlMaquinas_SelectedIndexChanged">
                                    </asp:DropDownList>                                
                                </td>                                    
                            </tr>                        
                            <tr>                                                                                             
                                <td>
                                    <asp:Label ID="lbTitPrograma" runat="server" Text="PROGRAMA:" CssClass="etiqueta"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlProgramas" runat="server"
                                        CssClass="desplegable" AutoPostBack="True" 
                                        onselectedindexchanged="ddlProgramas_SelectedIndexChanged">
                                    </asp:DropDownList>                                
                                </td>                                    
                            </tr>
                        </table>
                    </asp:Panel>
                    
                    <asp:Panel ID="PanelTunel" runat="server">
                        <table>
                            <tr>                                                                                             
                                <td>
                                    <asp:Label ID="lbTitTunel" runat="server" Text="TUNEL:" CssClass="etiqueta"></asp:Label>
                                </td>
                                <td class="style3"></td>
                                <td>
                                    <asp:DropDownList ID="ddlTuneles" runat="server"
                                        CssClass="desplegable" AutoPostBack="True" 
                                        onselectedindexchanged="ddlTuneles_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <br />
                                </td>                                    
                            </tr>
                        </table>
                    </asp:Panel>
                    
                    <asp:Panel ID="panelPesoMax" runat="server">                
                        <table>
                            <tr>                                                                                             
                                <td>
                                    <asp:Label ID="lbTitPesoMaximo" runat="server" Text="PESO MAX.: " CssClass="etiqueta"></asp:Label>
                                </td>
                                <td class="style2"></td>
                                <td> 
                                    <asp:Label ID="lbPesoMaximo" runat="server" Text="" CssClass="descripcionCodigo"></asp:Label>                                
                                </td>                                    
                            </tr>
                        </table>
                    </asp:Panel>
                    
                    <asp:Panel ID="PanelDatosEmpleado" runat="server">                    
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
                        <table>     
                            <tr>                        
                                <td>
                                    <asp:Button ID="btOperario" runat="server" CommandName="operario" 
                                        CssClass="botonSeleccion" CommandArgument="OPERARIO" onclick="btDato_Click" Text="OPERARIO" />
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
                    </asp:Panel>
                    
                    <asp:Panel ID="panelDatosCliente" runat="server">
                        <table>
                            <tr>                        
                                <td>
                                    <asp:Button ID="btCliente" runat="server" Text="CLIENTE" CssClass="botonSeleccion" 
                                        CommandName="cliente" CommandArgument="CLIENTE" onclick="btDato_Click" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txCodCliente" runat="server" CssClass="codigoCliente" 
                                        ontextchanged="txCodCliente_TextChanged" AutoPostBack="True"></asp:TextBox>                                
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
                    
                    <asp:Panel ID="PanelCarro" runat="server">
                        <table>                                        
                            <tr>
                                <td>
                                    <asp:Label ID="lbCarroSaca" runat="server" Text="CAR./SACA:" CssClass="etiqueta"></asp:Label>
                                </td>
                                <td class="style1"></td>     
                                <td>  
                                    <asp:DropDownList ID="ddlTiposCarro" runat="server"
                                        CssClass="desplegableCarros" AutoPostBack="True" 
                                        onselectedindexchanged="ddlTiposCarro_SelectedIndexChanged">
                                    </asp:DropDownList>                                                                                                
                                </td>
                                <td>
                                    <asp:Button ID="btPeso" runat="server" Text="PESO" CssClass="botonSeleccion" 
                                        CommandName="peso" />                                        
                                </td>     
                                <td>
                                    <asp:TextBox ID="txPeso" runat="server" 
                                        CssClass="txIntro" Enabled="False" ontextchanged="txPeso_TextChanged"></asp:TextBox>
                                    <asp:Button ID="btPesoTeclado" runat="server" Text="..." CssClass="botonPeso" 
                                        CommandName="peso" CommandArgument="PESO" onclick="btDato_Click" />
                                </td>
                            </tr>
                        </table>                                  
                    </asp:Panel>
                    
                    <asp:Panel ID="PanelHoras" runat="server">
                        <table>
                            <tr>                                                                                             
                                <td>
                                    <asp:Button ID="btHoras" runat="server" Text="HR LAVADO" CssClass="botonSeleccion" 
                                        CommandName="horas" CommandArgument="HR LAVADO" onclick="btDato_Click" />                                
                                </td>
                                &nbsp;
                                <td>
                                    <asp:TextBox ID="txHorasTunel" runat="server" CssClass="codigoCliente" 
                                        Enabled="False"></asp:TextBox> 
                                </td>                                    
                            </tr>
                            <tr>                                                                                             
                                <td>
                                    <asp:Button ID="btKilos" runat="server" Text="KG LAVADOS" CssClass="botonSeleccion" 
                                        CommandName="kilos" CommandArgument="KG LAVADOS" onclick="btDato_Click" />                                
                                </td>
                                <td>
                                    <asp:TextBox ID="txKilosTunel" runat="server" CssClass="codigoCliente" 
                                        Enabled="False"></asp:TextBox> 
                                </td>                                    
                            </tr>
                        </table>
                    </asp:Panel>
                    
                    <asp:Panel ID="panelRegistro" runat="server">
                        <table>
                            <tr>                                              
                                <td>
                                    <asp:Button ID="btRegistrar" runat="server" Text="REGISTRAR" CssClass="boton" 
                                        onclick="btRegistrar_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btCancelar" runat="server" Text="REINICIAR" CssClass="boton" 
                                        onclick="btCancelar_Click" 
                                        onclientclick="return(ConfirmAction('reiniciar'));" />
                                </td>
                                <td>
                                    <asp:Button ID="btEtiqueta" runat="server" Text="ETIQUETA" CssClass="boton" 
                                        onclick="btEtiqueta_Click" Visible="false"/>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                
                </asp:Panel>
            </td> 
            <td>
                <asp:Panel ID="panelTeclado" runat="server" Enabled="false" CssClass="panelTeclado">
                    <uc1:INIKER_tecladoNumerico ID="INIKER_teclado" runat="server" />
                </asp:Panel>                
            </td>
        </tr>                
    </table>     
    
    <asp:Panel ID="PanelPesajes" runat="server">
        <br />                                          
        <asp:GridView ID="grdPesajes" runat="server" CellPadding="4" ForeColor="#333333" 
            GridLines="None" onrowdatabound="grdPesajes_RowDataBound" 
            onselectedindexchanged="grdPesajes_SelectedIndexChanged" 
            CssClass="gridLineas" onrowdeleting="grdPesajes_RowDeleting">
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
                <asp:CommandField CancelText="" DeleteText="Borrar" EditText="" InsertText="" 
                    NewText="" SelectText="" ShowDeleteButton="True" UpdateText="" ButtonType="Button" />                
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
    </asp:Panel>
    <input id="hdDatoTeclado" type="hidden" runat="server"/>
    <asp:HiddenField ID="tipoLavado" runat="server" />
    <asp:HiddenField ID="hdCliente" runat="server" />
    <asp:HiddenField ID="hdPedido" runat="server" />
    <asp:HiddenField ID="hdfPeso" runat="server" 
        onvaluechanged="hdfPeso_ValueChanged" />
</asp:Content>
