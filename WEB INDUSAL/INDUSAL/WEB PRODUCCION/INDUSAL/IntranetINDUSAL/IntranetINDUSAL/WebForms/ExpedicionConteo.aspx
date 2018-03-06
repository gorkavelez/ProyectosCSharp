<%@ Page Title="" Language="C#" MasterPageFile="~/MasterForms/MenuIntranetVertical.Master" AutoEventWireup="true" CodeBehind="ExpedicionConteo.aspx.cs" Inherits="IntranetINDUSAL.WebForms.ExpedicionConteo" %>
<%@ Register src="../Controles_Personalizados/INIKER_tecladoNumerico.ascx" tagname="INIKER_tecladoNumerico" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../IntranetINDUSAL.css" rel="Stylesheet" type="text/css" />
    <script src="../IntranetINDUSAL.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td style="vertical-align:top; width:auto;">
                <table>
                    <tr>                        
                        <td class="etiqueta">
                            <asp:Label ID="lbRuta" runat="server" Text="RUTA:"></asp:Label>
                        </td>
                        <td class="descripcion">
                            <asp:DropDownList ID="ddlRutas" runat="server" AutoPostBack="True" 
                                Font-Size="Large" Height="30px" Width="100%" 
                                onselectedindexchanged="ddlRutas_SelectedIndexChanged">
                            </asp:DropDownList>                
                            <br />
                            <asp:Label ID="lbDescRuta" runat="server" Font-Size="Small"></asp:Label>
                        </td>
                    </tr>
                    <tr>                                                                                             
                        <td class="etiqueta">
                            <asp:Label ID="lbCliente" runat="server" Text="CLIENTE:"></asp:Label>
                        </td>
                        <td class="descripcion">
                            <asp:TextBox ID="txCliente" runat="server" Font-Size="X-Large" Height="30px" 
                                Width="100%" ontextchanged="txCliente_TextChanged" AutoPostBack="True"></asp:TextBox>
                            <asp:DropDownList ID="ddlClientesRuta" runat="server" DataTextField="Name" DataValueField="No" 
                                Font-Size="Large" Height="30px" Width="100%" AutoPostBack="True" 
                                onselectedindexchanged="ddlClientesRuta_SelectedIndexChanged">
                            </asp:DropDownList>
                            <br />
                        </td>                                    
                    </tr>
                </table>                 
                <br />  
                <asp:Panel ID="pnlLineasPedido" runat="server">
                    <asp:Label ID="lbPedido" runat="server" Text="Datos del pedido en curso"></asp:Label>
                    <asp:GridView ID="grdPedido" runat="server" CellPadding="4" ForeColor="#333333" 
                        GridLines="None" ondatabound="grdPedido_DataBound" 
                        onrowdeleting="grdPedido_RowDeleting" 
                        onselectedindexchanged="grdPedido_SelectedIndexChanged">
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:CommandField CancelText="" DeleteText="Del" EditText="" InsertText="" 
                                NewText="" SelectText="" ShowDeleteButton="True" UpdateText="" />
                            <asp:CommandField CancelText="" DeleteText="" EditText="" InsertText="" 
                                NewText="" SelectText="Sel" ShowSelectButton="True" UpdateText="" />
                        </Columns>
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                    <asp:Button ID="btRegistrar" runat="server" Text="REGISTRAR DATOS PEDIDO" 
                        Height="40px" Width="100%" onclick="btRegistrar_Click" />
                </asp:Panel>
                
                <asp:Panel ID="pnlCodsFact" runat="server">
                
                </asp:Panel>
            </td>
            <td style="vertical-align:top; width:auto;">            
                <uc1:INIKER_tecladoNumerico ID="INIKER_teclado" runat="server" />            
            </td>
        </tr>                
    </table>     
    
    </asp:Content>
