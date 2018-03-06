<%@ Page Title="" Language="C#" MasterPageFile="~/MasterForms/MenuIntranetVertical.Master" AutoEventWireup="true" CodeBehind="Plegado.aspx.cs" Inherits="IntranetINDUSAL.WebForms.Plegado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../IntranetINDUSAL.css" rel="Stylesheet" type="text/css" />
    <script src="../IntranetINDUSAL.js" type="text/javascript"></script>        
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="panelTurnos" runat="server">
    </asp:Panel>
    <table>
        <tr>
            <td class="etiqueta">
                <asp:Label ID="lbCliente" runat="server" Text="CLIENTE:"></asp:Label>
            </td>
            <td class="etiqueta">
                <asp:TextBox ID="txCodCliente" runat="server" AutoPostBack="True" 
                    ontextchanged="txCodCliente_TextChanged" CssClass="codigo"></asp:TextBox>
            </td>
            <td class="descripcion">                
                <asp:DropDownList ID="ddlClientes" runat="server" AutoPostBack="true" 
                    CssClass="desplegable" 
                    onselectedindexchanged="ddlClientes_SelectedIndexChanged">
                </asp:DropDownList>
            </td>                        
        </tr>
        <tr>                                                                     
            <td class="etiqueta">
                <asp:Label ID="lbCalandra" runat="server" Text="MAQUINA:"></asp:Label>
            </td>
            <td class="etiqueta">&nbsp;</td>
            <td class="descripcion">
                <asp:DropDownList ID="ddlMaquinas" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="ddlMaquinas_SelectedIndexChanged" CssClass="desplegable" 
                    ondatabound="ddlMaquinas_DataBound">
                </asp:DropDownList><br />
            </td> 
        </tr>
        <tr>                        
            <td class="etiqueta">
                <asp:Label ID="lbTitTurno" runat="server" Text="TURNO:"></asp:Label>
            </td>
            <td class="etiqueta">&nbsp;</td>
            <td class="descripcion">                                            
                <asp:Label ID="lbTurnoSel" runat="server" Text="" CssClass="txIntro"></asp:Label> 
            </td>
        </tr>  
        <tr>                        
            <td class="etiqueta">
                <asp:Label ID="lbOperario" runat="server" Text="OPERARIO:"></asp:Label>
            </td>
            <td class="etiqueta">&nbsp;</td>
            <td class="descripcion">            
                <asp:DropDownList ID="ddlEmpleados" runat="server" CssClass="desplegable" 
                    onselectedindexchanged="ddlEmpleados_SelectedIndexChanged">
                </asp:DropDownList>
                <br />
            </td>
        </tr>
        <tr>
            <td class="etiqueta">
                <asp:Label ID="lbPedido" runat="server" Text="PEDIDO:"></asp:Label>
            </td>
            <td class="etiqueta">&nbsp;</td>
            <td class="descripcion">                
                <asp:TextBox ID="txPedido" runat="server" AutoPostBack="true" CssClass="codigo" 
                    ontextchanged="txPedido_TextChanged"></asp:TextBox>                
            </td>                        
        </tr>        
    </table>                 
    <br />  
    <asp:GridView ID="grdPedidos" runat="server" CellPadding="4" 
        ForeColor="#333333" GridLines="None" CssClass="gridLineas" 
        onselectedindexchanged="grdPedidos_SelectedIndexChanged">
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <Columns>
            <asp:CommandField CancelText="" DeleteText="" EditText="" InsertText="" 
                NewText="" ShowSelectButton="True" UpdateText="" />
        </Columns>
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#999999" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    </asp:GridView>     
    <br />
    <asp:Panel ID="pnlClteSAL" runat="server">
        <asp:Panel ID="pnlTitFamilias" runat="server">
            <table>
                <tr>
                    <td class="etiqueta"><asp:Label ID="lbFamilia" runat="server" Text="FAMILIA:" CssClass="textoEtiqueta"></asp:Label></td>
                    <td><asp:Label ID="lbFamSel" runat="server" Text="" CssClass="textoEtiqueta" ForeColor="Blue"></asp:Label></td>
                </tr>
            </table>            
        </asp:Panel>        
        <asp:Panel ID="pnlFamilias" runat="server" Width="1020">                    
        </asp:Panel>        
        
        <asp:Panel ID="pnlTitSubfam" runat="server">
            <table>
                <tr>
                    <td class="etiqueta"><asp:Label ID="lbSubfam" runat="server" Text="SUBFAMILIA:" CssClass="textoEtiqueta"></asp:Label></td>
                    <td><asp:Label ID="lbSubfamSel" runat="server" Text="" CssClass="textoEtiqueta" ForeColor="Blue"></asp:Label></td>
                </tr>
            </table>            
        </asp:Panel>        
        <asp:Panel ID="pnlSubfamilias" runat="server" Width="1020">
        </asp:Panel> 
        
        <asp:Panel ID="pnlTitProductos" runat="server">
            <table>
                <tr>
                    <td class="etiqueta"><asp:Label ID="lbSurtido" runat="server" Text="SURTIDO:" CssClass="textoEtiqueta"></asp:Label></td>                    
                </tr>
            </table>                        
        </asp:Panel>        
        <asp:Panel ID="pnlProductos" runat="server" Width="1020">
        </asp:Panel>       
    </asp:Panel>
    
    <asp:HiddenField ID="hfCodAlmacen" runat="server" />
    <asp:HiddenField ID="hfCantPaquete" runat="server" />
    <asp:HiddenField ID="hfCodProducto" runat="server" />
    <input id="hdCodOperario" type="hidden" runat="server"/>
    <input id="hdCodMaquina" type="hidden" runat="server" />
    <input id="hdCodCliente" type="hidden" runat="server" />
</asp:Content>
