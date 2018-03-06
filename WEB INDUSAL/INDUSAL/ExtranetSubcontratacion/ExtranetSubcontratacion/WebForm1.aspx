<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="ExtranetSubcontratacion._WebForm1" %>


<%@ Register assembly="Telerik.Web.UI, Version=2009.1.527.20, Culture=neutral, PublicKeyToken=121fae78165ba3d4" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <link href="ExtranetSubcontr.css" rel="stylesheet" type="text/css" />
    <script src="ScriptsSubcontratacion.js" type="text/javascript"></script>
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 75px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lbTitProveedor" runat="server" Text="USUARIO:" CssClass="texto_titulo"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lbProveedor" runat="server" Text="" CssClass="texto_azul"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lbNombreProv" runat="server" Text="" CssClass="texto_azul"></asp:Label>
                </td>
                <td>
                    <asp:Button ID="btDesconectar" runat="server" Text="Desconectar" 
                        onclick="btDesconectar_Click" />
                </td>                
            </tr>
        </table>
        <br />        
        <asp:Panel ID="panelFiltros" runat="server">
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lbLavanderia" runat="server" Text="LAVANDERIA:" CssClass="texto_titulo"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="ddlLavanderias" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="ddlLavanderias_SelectedIndexChanged1">
                        </telerik:RadComboBox>                        
                    </td>
                    <td></td>
                    <td>
                        <asp:Label ID="lbCliente" runat="server" Text="CLIENTE:" CssClass="texto_titulo"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="ddlClientes" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="ddlClientes_SelectedIndexChanged">
                        </telerik:RadComboBox>                        
                    </td>                    
                    <td></td>                                                                     
                    <td>
                        <asp:LinkButton ID="lkbtDatos" runat="server" onclick="lkbtDatos_Click" 
                            CssClass="texto_link" Visible="false">Buscar pedidos</asp:LinkButton>                        
                    </td>
                    <td></td>
                    <td>
                        <asp:LinkButton ID="btNuevoPedido" runat="server" onclick="btNuevoPedido_Click" 
                            CssClass="texto_link" Visible="false">Nuevo pedido</asp:LinkButton>
                    </td>
                    <td class="style1"></td>
                    <td>
                        <asp:LinkButton ID="btBuscarAlbaranes" runat="server"
                            CssClass="texto_link" Visible="false" onclick="btBuscarAlbaranes_Click">Buscar albaranes</asp:LinkButton>
                    </td> 
                </tr>            
            </table>
        </asp:Panel>    
        <br />
        <asp:Panel ID="panelCalendar" runat="server" Visible="false">
            <asp:HiddenField ID="hfFechaRegistro" runat="server" />
            <telerik:RadCalendar ID="Calendario" runat="server" Font-Names="Arial, Verdana, Tahoma"
                ForeColor="Black" Style="border-color: #ececec" 
                onselectionchanged="Calendario_SelectionChanged" AutoPostBack="True" 
                EnableMultiSelect="False">
            </telerik:RadCalendar> 
        </asp:Panel>         
        <asp:Panel ID="panelSeleccionPedido" runat="server" Visible="false">
            <asp:Label ID="lbPedidosAbiertos" runat="server" Text="PEDIDOS ABIERTOS:" CssClass="texto_titulo"></asp:Label>
            <br />
            <telerik:RadGrid ID="gridPedidosAbiertos" runat="server" GridLines="None" 
            onselectedindexchanged="gridPedidosAbiertos_SelectedIndexChanged">            
                <MasterTableView>
                    <RowIndicatorColumn>
                    <HeaderStyle Width="20px"></HeaderStyle>
                    </RowIndicatorColumn>

                    <ExpandCollapseColumn>
                    <HeaderStyle Width="20px"></HeaderStyle>
                    </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridButtonColumn CommandName="Select" Text="Seleccionar" 
                                UniqueName="column">
                            </telerik:GridButtonColumn>
                        </Columns>
                </MasterTableView>
            </telerik:RadGrid>    
        </asp:Panel>
        
        <asp:Panel ID="panelSeleccionAlbaran" runat="server" Visible="false">
            <asp:Label ID="lbAlbaranes" runat="server" Text="ALBARANES:" CssClass="texto_titulo"></asp:Label>
            <br />
            <telerik:RadGrid ID="gridAlbaranes" runat="server" GridLines="None" 
                ondeletecommand="gridAlbaranes_DeleteCommand" 
                onselectedindexchanged="gridAlbaranes_SelectedIndexChanged" >            
                <MasterTableView>
                    <RowIndicatorColumn>
                    <HeaderStyle Width="20px"></HeaderStyle>
                    </RowIndicatorColumn>

                    <ExpandCollapseColumn>
                    <HeaderStyle Width="20px"></HeaderStyle>
                    </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridButtonColumn CommandName="Select" Text="Ver líneas" 
                                UniqueName="column">
                            </telerik:GridButtonColumn>
                            <telerik:GridButtonColumn CommandName="Delete" Text="Borrar" 
                                UniqueName="column">
                            </telerik:GridButtonColumn>
                        </Columns>
                </MasterTableView>
            </telerik:RadGrid>    
        </asp:Panel>        
        <br />        
        <asp:Panel ID="panelPedidoSeleccionado" runat="server" Visible="false" 
            BackColor="#F2F2F2">
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lbTitPedido" runat="server" Text="PEDIDO:" CssClass="texto_titulo"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lbNumPedido" runat="server" Text="" CssClass="texto_azul"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbTitFecha" runat="server" Text="FECHA CREACION:" CssClass="texto_titulo"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lbFechaPedido" runat="server" Text="" CssClass="texto_azul"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbTitFechaRegistro" runat="server" Text="FECHA REGISTRO:" CssClass="texto_titulo"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lbFechaRegistro" runat="server" Text="" CssClass="texto_azul"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbTitCteFinal" runat="server" Text="CLIENTE FINAL:" CssClass="texto_titulo"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lbCteFinal" runat="server" Text="" CssClass="texto_azul"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />            
            <table>
                <tr>                    
                    <td>
                        <asp:LinkButton ID="btEliminarPedido" runat="server" 
                            onclick="btEliminarPedido_Click" CssClass="texto_link">Eliminar pedido</asp:LinkButton>
                    </td>
                    <td></td>
                    <td>
                        <asp:LinkButton ID="btConfirmarPedido" runat="server" 
                            onclick="btConfirmarPedido_Click" CssClass="texto_link">Confirmar pedido</asp:LinkButton>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hfCodCliente" runat="server" />
        </asp:Panel>                  
        <br />
        <asp:Panel ID="panelLineasPedido" runat="server" Visible="false">        
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lbLineasPedido" runat="server" Text="Líneas de pedido:" CssClass="texto_titulo"></asp:Label>
                    </td>
                    <td></td>
                    <td>
                        <asp:LinkButton ID="btNuevaLinea" runat="server" onclick="btNuevaLinea_Click" CssClass="texto_link">Nueva línea</asp:LinkButton>
                    </td>
                </tr>
            </table>
            <br />              
            <asp:Panel ID="panelSeleccionSurtido" runat="server" Visible="false">            
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lbFamilia" runat="server" Text="FAMILIA:" CssClass="texto_titulo"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbSubfamilias" runat="server" Text="SUBFAMILIA:" CssClass="texto_titulo"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbProductos" runat="server" Text="PRODUCTO:" CssClass="texto_titulo"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbCantidad" runat="server" Text="CANTIDAD:" CssClass="texto_titulo"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadComboBox ID="ddlFamilias" runat="server" AutoPostBack="True" 
                                onselectedindexchanged="ddlFamilias_SelectedIndexChanged" 
                                CssClass="desplegable">
                            </telerik:RadComboBox>                            
                        </td>                    
                        <td>
                            <telerik:RadComboBox ID="ddlSubfamilias" runat="server" AutoPostBack="True" 
                                onselectedindexchanged="ddlSubfamilias_SelectedIndexChanged">
                            </telerik:RadComboBox>                            
                        </td>                                   
                        <td>
                            <telerik:radcombobox ID="ddlProductos" runat="server" AutoPostBack="True" 
                                onselectedindexchanged="ddlProductos_SelectedIndexChanged">
                            </telerik:RadComboBox>                            
                        </td>
                        <td>
                            <telerik:radnumerictextbox ID="txCantidad" runat="server">
                            </telerik:RadNumericTextBox>                            
                        </td>
                        <td></td>
                        <td>
                            <asp:LinkButton ID="btAceptar" runat="server" onclick="btAceptar_Click" CssClass="texto_link">Aceptar</asp:LinkButton>        
                        </td>
                        <td></td>
                        <td>
                            <asp:LinkButton ID="btCancelar" runat="server" CssClass="texto_link" 
                                onclick="btCancelar_Click">Cancelar</asp:LinkButton>        
                        </td>
                    </tr>
                </table>
            </asp:Panel>               
            <br />
            <telerik:RadGrid ID="gridLineasPedido" runat="server" GridLines="None" 
                ondeletecommand="gridLineasPedido_DeleteCommand">
                <MasterTableView>
                    <RowIndicatorColumn>
                        <HeaderStyle Width="20px" />
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn>
                        <HeaderStyle Width="20px" />
                    </ExpandCollapseColumn>
                    <Columns>
                        <telerik:GridButtonColumn CommandName="Delete" Text="Borrar" 
                            UniqueName="column">
                        </telerik:GridButtonColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </asp:Panel> 
        
        <asp:Panel ID="panelLineasAlbaran" runat="server" Visible="false">
            <table>
                <tr>
                    <td><asp:Label ID="lbNumAlbaran" runat="server" Text="" CssClass="texto_titulo"></asp:Label></td>
                    <td></td>
                    <td>
                        <asp:LinkButton ID="btImprimir" runat="server" CssClass="texto_link" 
                            OnClientClick="javascript:ventanaSecundaria('albaran.aspx');">Imprimir</asp:LinkButton></td>
                </tr>                
            </table>            
            <br />
            <telerik:RadGrid ID="gridLineasAlbaran" runat="server" GridLines="None" >
                <MasterTableView>
                    <RowIndicatorColumn>
                        <HeaderStyle Width="20px" />
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn>
                        <HeaderStyle Width="20px" />
                    </ExpandCollapseColumn>                    
                </MasterTableView>
            </telerik:RadGrid>
        </asp:Panel>     
        
    </div>
    </form>
</body>
</html>
