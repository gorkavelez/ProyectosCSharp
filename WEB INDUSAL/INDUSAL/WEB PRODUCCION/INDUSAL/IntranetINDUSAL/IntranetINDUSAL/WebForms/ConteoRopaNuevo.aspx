<%@ Page Title="CONTEO ROPA" Language="C#" MasterPageFile="~/MasterForms/MenuIntranetVerticalBotones.Master" AutoEventWireup="true" CodeBehind="ConteoRopaNuevo.aspx.cs" Inherits="IntranetINDUSAL.WebForms.ConteoRopaNuevo" %>
<%@ Register src="../Controles_Personalizados/INIKER_tecladoNumerico.ascx" tagname="INIKER_tecladoNumerico" tagprefix="uc1" %>
<%@ Register src="../Controles_Personalizados/INIKER_surtido.ascx" tagname="INIKER_surtido" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../IntranetINDUSAL.css" rel="Stylesheet" type="text/css" />
    <script src="../IntranetINDUSAL.js" type="text/javascript"></script>    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table>
        <tr>
            <td style="vertical-align:top;"> 
                <asp:Panel ID="panelDatos" runat="server" CssClass="panelDatos">
                    <asp:Panel ID="panelEmpleado" runat="server">
                        <table>
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
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="panelClienteConteo" runat="server" Enabled="false">
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
                    </asp:Panel>  
                    <asp:Panel ID="panelClienteDesaprestado" runat="server">
                        <table>                    
                            <tr>                    
                                <td>
                                    <asp:Label ID="lbTitCliente" runat="server" Text="CLIENTE:" CssClass="etiqueta"></asp:Label>                                
                                </td>
                                <td>
                                    <asp:Label ID="lbCodCliente" runat="server" Text="" CssClass="codigoCliente"></asp:Label>
                                </td>
                                <td>                            
                                    <asp:Label ID="lbDescCliente" runat="server" Text="" CssClass="descripcionCodigo"></asp:Label>                 
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>             
                    <table>                                                                                 
                        <tr>
                            <td>
                                <asp:Label ID="lbProdSelec" runat="server" Text="PRODUCTO:" CssClass="etiqueta"></asp:Label>
                            </td>                        
                            <td>
                                <asp:Label ID="lbDescProdSelec" runat="server" CssClass="descripcionCodigo"></asp:Label>
                            </td>
                        </tr>                                        
                    </table>
                    <br />                                                
                    <asp:Panel ID="panelSurtido" runat="server" Visible="false" CssClass="panelSurtido">
                        <uc2:INIKER_surtido ID="INIKER_surtidoCliente" runat="server" />                          
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
                                    <asp:Button ID="btReiniciar" runat="server" Text="REINICIAR" 
                                        CssClass="botonSeleccion" onclick="btCancelRecep_Click" Enabled="false" />
                                </td>
                                <td>
                                    <asp:Button ID="btCancelar" runat="server" Text="CANCELAR" 
                                        CssClass="botonSeleccion" onclick="btCancelar_Click" Enabled="false" />
                                </td>
                            </tr>                        
                        </table>
                    </asp:Panel>
                
                </asp:Panel>
            </td>
            <td style="vertical-align:top;">            
                <asp:Panel ID="PanelTeclado" runat="server" Enabled="false" CssClass="panelTeclado">
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
    <asp:HiddenField ID="tipoConteo" runat="server" />
</asp:Content>
