<%@ Page Title="" Language="C#" MasterPageFile="~/MasterForms/MenuIntranetVerticalBotones.Master" AutoEventWireup="true" CodeBehind="Lavadora2.aspx.cs" Inherits="IntranetINDUSAL.WebForms.Lavadora2" %>
<%@ Register src="../Controles_Personalizados/INIKER_tecladoNumerico.ascx" tagname="INIKER_tecladoNumerico" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../IntranetINDUSAL.css" rel="Stylesheet" type="text/css" />
    <script src="../IntranetINDUSAL.js" type="text/javascript"></script>    
    <style type="text/css">
        .style1
        {
            height: 50px;
            width: 210px;
            font-family: Calibri;
            font-size: x-Large;
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <table>
        <tr>
            <td style="vertical-align:top; width:auto;">
                <asp:Panel ID="PanelMaquina" runat="server">
                    <table>                                        
                        <tr>                                                                                             
                            <td class="etiqueta">
                                <asp:Label ID="lbMaquina" runat="server" Text="MAQUINA:"></asp:Label>
                            </td>
                            <td class="descripcion">
                                <asp:DropDownList ID="ddlMaquinas" runat="server"
                                    CssClass="desplegable" AutoPostBack="True" 
                                    onselectedindexchanged="ddlMaquinas_SelectedIndexChanged">
                                </asp:DropDownList>
                                <br />
                            </td>                                    
                        </tr>
                        <tr>                                                                                             
                            <td class="etiqueta">
                                <asp:Label ID="lbTitPrograma" runat="server" Text="PROGRAMA:"></asp:Label>
                            </td>
                            <td class="descripcion">
                                <asp:DropDownList ID="ddlProgramas" runat="server"
                                    CssClass="desplegable" AutoPostBack="True" 
                                    onselectedindexchanged="ddlProgramas_SelectedIndexChanged">
                                </asp:DropDownList>
                                <br />
                            </td>                                    
                        </tr>
                    </table>
                </asp:Panel>
                
                <asp:Panel ID="PanelTunel" runat="server">
                    <table>
                        <tr>                                                                                             
                            <td class="etiqueta">
                                <asp:Label ID="lbTitTunel" runat="server" Text="TUNEL:"></asp:Label>
                            </td>
                            <td class="descripcion">
                                <asp:DropDownList ID="ddlTuneles" runat="server"
                                    CssClass="desplegable" AutoPostBack="True" 
                                    onselectedindexchanged="ddlTuneles_SelectedIndexChanged">
                                </asp:DropDownList>
                                <br />
                            </td>                                    
                        </tr>
                    </table>
                </asp:Panel>
                
                <asp:Panel ID="PanelDatosEmpleado" runat="server">
                    <asp:Panel ID="panelSeleccionTurno" runat="server"></asp:Panel>
                    <table>
                        <tr>                        
                            <td class="etiqueta">
                                <asp:Label ID="lbTitTurno" runat="server" Text="TURNO:"></asp:Label>
                            </td>
                            <td class="etiqueta">
                                <asp:Label ID="lbTurnoSel" runat="server" CssClass="codigo" Text=""></asp:Label>
                            </td>
                            <td>                                            
                                &nbsp;</td>
                        </tr>     
                        <tr>                        
                            <td class="etiqueta">
                                <asp:Button ID="btOperario" runat="server" CommandName="operario" 
                                    CssClass="boton" onclick="btDato_Click" Text="OPERARIO" />
                            </td>
                            <td class="etiqueta">
                                <asp:Label ID="lbNomOperario" runat="server" Text="" Font-Size="medium"></asp:Label>
                                <asp:TextBox ID="txCodOperario" runat="server" AutoPostBack="True" 
                                    CssClass="codigo" ontextchanged="txCodOperario_TextChanged"></asp:TextBox>
                            </td>
                            <td >                                            
                                <asp:Button ID="btSelOperario" runat="server" Text="..." CssClass="boton" 
                                    onclick="btSelOperario_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                
                <asp:Panel ID="panelDatosCliente" runat="server">
                    <table>
                        <tr>                        
                            <td class="etiqueta">
                                <asp:Button ID="btCliente" runat="server" Text="CLIENTE" CssClass="boton" 
                                    CommandName="cliente" onclick="btDato_Click" />
                                
                            </td>
                            <td >
                                <asp:TextBox ID="txCodCliente" runat="server" CssClass="codigo" 
                                    ontextchanged="txCodCliente_TextChanged" AutoPostBack="True"></asp:TextBox>                                
                            </td>
                            <td>                                
                                &nbsp;</td>
                        </tr>
                        <tr>                        
                            <td class="etiqueta">
                                <asp:Label ID="lbTitPedido" runat="server" Text="PEDIDO:"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txNumPedido" runat="server" AutoPostBack="True" 
                                    CssClass="codigo" ontextchanged="txNumPedido_TextChanged"></asp:TextBox>
                            </td>
                            <td>                                                                 
                                &nbsp;</td>
                        </tr>
                    </table>
                </asp:Panel>
                
                <asp:Panel ID="PanelCarro" runat="server">
                    <table>                                        
                        <tr>
                            <td class="etiqueta">
                                <asp:Label ID="lbCarroSaca" runat="server" Text="CARRO/SACA"></asp:Label>
                            </td>     
                            <td class="codigo">&nbsp;</td>                   
                            <td class="descripcion">  
                                <br />                                                                 
                            </td>
                        </tr>                            
                        <tr>
                            <td class="etiqueta">
                                <asp:Button ID="btUnidades" runat="server" Text="PESO" CssClass="boton" 
                                    CommandName="peso" onclick="btDato_Click" />                                        
                            </td>     
                            <td class="style1">
                                <asp:TextBox ID="txPeso" runat="server" 
                                    CssClass="txIntro" Enabled="False" ontextchanged="txPeso_TextChanged"></asp:TextBox>
                            </td>                               
                            <td class="descripcion">&nbsp;</td>                                                                                                                                                            
                        </tr>                        
                    </table>                
                </asp:Panel>
                
                <asp:Panel ID="PanelHoras" runat="server">
                    <table>
                        <tr>                                                                                             
                            <td class="etiqueta">
                                <asp:Button ID="btHoras" runat="server" Text="HR LAVADO" CssClass="boton" 
                                    CommandName="horas" onclick="btDato_Click" />                                
                            </td>
                            <td class="etiqueta">
                                <asp:TextBox ID="txHorasTunel" runat="server" CssClass="codigo" 
                                    Enabled="False"></asp:TextBox> 
                            </td>                                    
                        </tr>
                        <tr>                                                                                             
                            <td class="etiqueta">
                                <asp:Button ID="btKilos" runat="server" Text="KG LAVADOS" CssClass="boton" 
                                    CommandName="kilos" onclick="btDato_Click" />                                
                            </td>
                            <td class="etiqueta">
                                <asp:TextBox ID="txKilosTunel" runat="server" CssClass="codigo" 
                                    Enabled="False"></asp:TextBox> 
                            </td>                                    
                        </tr>
                    </table>
                </asp:Panel>
                
                <asp:Panel ID="panelRegistro" runat="server">
                    <br />
                    <table>
                        <tr>                                              
                            <td class="etiquetaBoton">
                                <asp:Button ID="btRegistrar" runat="server" Text="REGISTRAR" CssClass="boton" 
                                    onclick="btRegistrar_Click" />
                            </td>
                            <td class="etiquetaBoton">
                                <asp:Button ID="btCancelar" runat="server" Text="REINICIAR" CssClass="boton" 
                                    onclick="btCancelar_Click" 
                                    onclientclick="return(ConfirmAction('reiniciar'));" />
                            </td>
                            <td class="etiquetaBoton">
                                <asp:Button ID="btEtiqueta" runat="server" Text="ETIQUETA" CssClass="boton" 
                                    onclick="btEtiqueta_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                
                <asp:Panel ID="PanelPesajes" runat="server">
                    <br />                                          
                    <asp:GridView ID="grdPesajes" runat="server" CellPadding="4" ForeColor="#333333" 
                        GridLines="None" onrowdatabound="grdPesajes_RowDataBound" 
                        onselectedindexchanged="grdPesajes_SelectedIndexChanged" CssClass="gridLineas">
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:CommandField CancelText="" DeleteText="Borrar" EditText="" InsertText="" 
                                NewText="" SelectText="" ShowDeleteButton="True" UpdateText="" />
                            <asp:CommandField CancelText="" DeleteText="" EditText="" InsertText="" 
                                NewText="" ShowSelectButton="True" UpdateText="" />
                        </Columns>
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </asp:Panel>
            </td> 
            <td  style="vertical-align:top; width:300px;">
                <uc1:INIKER_tecladoNumerico ID="INIKER_teclado" runat="server" />
            </td>
        </tr>                
    </table>     

    <input id="hdDatoTeclado" type="hidden" runat="server"/>
</asp:Content>
