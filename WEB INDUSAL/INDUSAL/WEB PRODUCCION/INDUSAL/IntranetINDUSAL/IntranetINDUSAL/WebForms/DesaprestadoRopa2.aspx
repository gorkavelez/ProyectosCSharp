<%@ Page Title="CONTEO ROPA" Language="C#" MasterPageFile="~/MasterForms/MenuIntranetVertical.Master" AutoEventWireup="true" CodeBehind="DesaprestadoRopa2.aspx.cs" Inherits="IntranetINDUSAL.WebForms.DesaprestadoRopa2" %>
<%@ Register src="../Controles_Personalizados/INIKER_tecladoNumerico.ascx" tagname="INIKER_tecladoNumerico" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../IntranetINDUSAL.css" rel="Stylesheet" type="text/css" />
    <script src="../IntranetINDUSAL.js" type="text/javascript"></script>    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table style="width:100%;">
        <tr>
            <td style="vertical-align:top; width:auto;">                
                <table>                    
                    <tr>                    
                        <td class="etiqueta">
                            <asp:Label ID="lbCliente" runat="server" Text="CLIENTE:"></asp:Label>
                        </td>
                        <td class="etiqueta">
                            <asp:TextBox ID="txCliente" runat="server" AutoPostBack="True" 
                                ontextchanged="txCodCliente_TextChanged" CssClass="codigo"></asp:TextBox>
                        </td>
                        <td class="descripcion">                            
                            <asp:DropDownList ID="ddlClientes" runat="server" 
                                CssClass="desplegable"                                
                                AutoPostBack="True" 
                                onselectedindexchanged="ddlClientes_SelectedIndexChanged">
                            </asp:DropDownList>                 
                        </td>
                    </tr>                                           
                    <tr>
                        <td class="etiqueta">
                            <asp:Label ID="lbProdSelec" runat="server" Text="PRODUCTO:"></asp:Label>
                        </td>
                        <td class="etiqueta"></td>
                        <td class="descripcion">
                            <asp:Label ID="lbDescProdSelec" runat="server" CssClass="txIntro" Font-Size="X-Large"></asp:Label>
                        </td>
                    </tr>                    
                    <tr>
                        <td class="etiqueta">
                            <asp:Label ID="lbCantidad" runat="server" Text="CANTIDAD:"></asp:Label>
                        </td>
                        <td class="etiqueta"></td>
                        <td class="descripcion">
                            <asp:TextBox ID="txCantidad" runat="server" Enabled="False" CssClass="txIntro"></asp:TextBox>
                            <br />
                            <asp:Label ID="lbQtyAlmacen" runat="server" Font-Bold="True" ForeColor="Gray" Font-Size="Small"></asp:Label>                
                        </td>
                    </tr>
                </table>
                <br />                
                <table>
                    <tr>                                                                                 
                        <td class="etiquetaBoton">
                            <asp:Button ID="btRegistrar" runat="server" Text="REGISTRAR" 
                                onclick="btRegistrar_Click" CssClass="boton"/>                            
                        </td>
                        <td class="etiquetaBoton">
                            <asp:Button ID="btCancelar" runat="server" Text="REINICIAR"
                                onclick="btCancelRecep_Click" CssClass="boton" />                            
                        </td>
                        <td class="etiquetaBoton">
                            <asp:Button ID="btAceptar" runat="server" Text="AÑADIR" onclick="btAceptar_Click" CssClass="boton" Visible="false"/>                            
                        </td> 
                    </tr>
                </table>
                <asp:Panel ID="panelClasificacion" runat="server">
                    <asp:Panel ID="pnlTitFamilias" runat="server">
                        <table>
                            <tr>
                                <td class="etiqueta"><asp:Label ID="lbFamilia" runat="server" Text="FAMILIA:" CssClass="textoEtiqueta"></asp:Label></td>
                                <td><asp:Label ID="lbFamSel" runat="server" Text="" CssClass="textoEtiqueta" ForeColor="Blue"></asp:Label></td>
                            </tr>
                        </table>            
                    </asp:Panel>        
                    <asp:Panel ID="pnlFamilias" runat="server" Width="100%">                    
                    </asp:Panel>        
                    
                    <asp:Panel ID="pnlTitSubfam" runat="server">
                        <table>
                            <tr>
                                <td class="etiqueta"><asp:Label ID="lbSubfam" runat="server" Text="SUBFAMILIA:" CssClass="textoEtiqueta"></asp:Label></td>
                                <td><asp:Label ID="lbSubfamSel" runat="server" Text="" CssClass="textoEtiqueta" ForeColor="Blue"></asp:Label></td>
                            </tr>
                        </table>            
                    </asp:Panel>        
                    <asp:Panel ID="pnlSubfamilias" runat="server" Width="100%">
                    </asp:Panel> 
                </asp:Panel>
                <asp:Panel ID="panelSurtido" runat="server">
                    <asp:Panel ID="pnlTitProductos" runat="server">
                        <table>
                            <tr>
                                <td class="etiqueta"><asp:Label ID="lbSurtido" runat="server" Text="SURTIDO:" CssClass="textoEtiqueta"></asp:Label></td>                    
                            </tr>
                        </table>                        
                    </asp:Panel>        
                    <asp:Panel ID="pnlProductos" runat="server" Width="100%">
                    </asp:Panel>       
                </asp:Panel>
            </td>
            <td style="vertical-align:top; width:300px;">            
                <asp:Panel ID="PanelTeclado" runat="server" Enabled="false">
                    <uc1:INIKER_tecladoNumerico ID="INIKER_teclado" runat="server" />            
                </asp:Panel>                
            </td>
        </tr>
    </table>
    <br /> 
    <asp:GridView ID="gridConteo" runat="server" CellPadding="4" 
        ForeColor="#333333" GridLines="None" CssClass="gridLineas"
        onrowdeleting="gridConteo_RowDeleting" 
        onselectedindexchanged="gridConteo_SelectedIndexChanged" 
        onrowdatabound="gridConteo_RowDataBound">
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <Columns>
                <asp:CommandField 
                    ShowDeleteButton="True" CancelText="" DeleteText="Borrar" EditText="" 
                    InsertText="" NewText="" SelectText="" UpdateText="" />
                <asp:CommandField CancelText="" DeleteText="" EditText="" 
                    InsertText="" InsertVisible="False" NewText="" ShowCancelButton="False" 
                    ShowSelectButton="True" UpdateText="" />
            </Columns>
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#999999" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    </asp:GridView>
 
</asp:Content>
