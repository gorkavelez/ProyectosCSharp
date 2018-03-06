<%@ Page Title="" Language="C#" MasterPageFile="~/MasterForms/MenuIntranetVertical.Master" AutoEventWireup="true" CodeBehind="RegistroExpedicion.aspx.cs" Inherits="IntranetINDUSAL.WebForms.RegistroExpedicion" %>
<%@ Register src="../Controles_Personalizados/INIKER_tecladoNumerico.ascx" tagname="INIKER_tecladoNumerico" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">    
    <link href="../IntranetINDUSAL.css" rel="Stylesheet" type="text/css" />
    <script src="../IntranetINDUSAL.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width:100%;">
        <tr>
            <td>
                <table style="width:100%;">
                    <tr>
                        <td style="vertical-align:top; width:auto;">
                            <table>
                                <tr>                        
                                    <td class="etiqueta">
                                        <asp:Label ID="lbCliente" runat="server" Text="CLIENTE:"></asp:Label></td>
                                    <td class="descripcion">
                                        <asp:Label ID="lbNomCliente" runat="server" Font-Size="Large"></asp:Label>
                                    </td>
                                </tr>
                                <tr> 
                                    <td class="etiqueta">
                                        <asp:Label ID="lbCalandra" runat="server" Text="PEDIDO:"></asp:Label></td>
                                    <td class="descripcion">
                                        <asp:Label ID="lbCalandraSel" runat="server" Font-Size="Large"></asp:Label>
                                    </td>                                                                                          
                                </tr>
                                <tr>
                                    <td class="etiqueta">
                                        <asp:Label ID="lbTitCodProd" runat="server" Text="PRODUCTO:"></asp:Label>
                                    </td>
                                    <td class="descripcion">
                                        <asp:Label ID="lbCodProd" runat="server" Font-Size="Medium"></asp:Label><br />
                                        <asp:Label ID="lbDescProd" runat="server" Font-Size="Large"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <asp:Panel ID="panelTurnos" runat="server">
                            </asp:Panel>
                            <br />
                            <table>
                                <tr>                        
                                    <td class="etiqueta">
                                        <asp:Label ID="lbTitTurno" runat="server" Text="TURNO:"></asp:Label>
                                    </td>
                                    <td class="descripcion">                                            
                                        <asp:Label ID="lbTurnoSel" runat="server" Text="" CssClass="txIntro"></asp:Label> 
                                    </td>
                                </tr>
                                <tr>
                                    <td class="etiqueta">
                                        <asp:Label ID="lbOperario" runat="server" Text="OPERARIO"></asp:Label>                                        
                                    </td>
                                    <td class="descripcion">                                        
                                        <asp:TextBox ID="txOperario" runat="server" 
                                            CssClass="txIntro" Enabled="False" ontextchanged="txOperario_TextChanged"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="etiqueta">
                                        <asp:Label ID="Label2" runat="server" Text="CODIGO CARRO"></asp:Label>                                        
                                    </td>
                                    <td class="descripcion">                                        
                                        <asp:DropDownList ID="ddlTiposCarro" runat="server" CssClass="txIntro"
                                            onselectedindexchanged="ddlTiposCarro_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>                                                            
                            </table>                               
                            <table>                                
                                <tr>
                                    <td class="etiquetaBoton">
                                        <asp:Label ID="Label1" runat="server" Text="NUMERO CARRO"></asp:Label>                                        
                                    </td>                                    
                                    <td class="etiquetaBoton">
                                        <asp:Button ID="btUnidades" runat="server" Text="PESO" CssClass="boton" 
                                            onclick="btDato_Click" CommandName="peso" />                                        
                                    </td>                                    
                                    <td class="etiquetaBoton">
                                        <asp:Button ID="btCarroCompleto" runat="server" Text="COMPLETO" CssClass="boton" 
                                            onclick="btCarroCompleto_Click" CommandName="completo" />
                                    </td>
                                    <td class="etiquetaBoton">
                                        <asp:Button ID="btAddLine" runat="server" Text="AÑADIR" CssClass="boton" 
                                            onclick="btAddLine_Click" />
                                    </td>                                     
                                </tr>
                                <tr>
                                    <td class="etiquetaIntro">
                                        <asp:TextBox ID="txNCarro" runat="server" 
                                            CssClass="txIntro" Enabled="False" ontextchanged="txNCarro_TextChanged"></asp:TextBox>
                                    </td>                                    
                                    <td class="etiquetaIntro">                                        
                                        <asp:TextBox ID="txPeso" runat="server" AutoPostBack="True" 
                                            CssClass="txIntro" Enabled="False" ontextchanged="txPeso_TextChanged"></asp:TextBox>
                                    </td>
                                    <td class="etiquetaIntro">
                                        <asp:TextBox ID="txCompleto" runat="server" CssClass="txIntro" 
                                        Enabled="False"></asp:TextBox>
                                    </td>
                                    <td>&nbsp;</td>                                    
                                </tr>
                            </table>                         
                            <br />
                            <table>
                                <tr>
                                    <td class="etiquetaBoton">
                                        <asp:Button ID="btVolver" runat="server" Text="VOLVER" CssClass="boton" 
                                            onclick="btVolver_Click" />
                                    </td>                                                                       
                                    <td class="etiquetaBoton">
                                        <asp:Button ID="btRegistrar" runat="server" Text="REGISTRAR" CssClass="boton" 
                                            onclick="btRegistrar_Click" />
                                    </td>
                                    <td class="etiquetaBoton">
                                        <asp:Button ID="btCancelar" runat="server" Text="REINICIAR" CssClass="boton" 
                                            onclick="btCancelar_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="vertical-align:top; width:auto;">                            
                            <uc2:INIKER_tecladoNumerico ID="INIKER_teclado" runat="server" />                            
                        </td>                    
                    </tr>
                </table>                
            </td>
        </tr>
        <tr>
            <td>
                <input id="hdDatoTeclado" type="hidden" runat="server"/>
            </td>
        </tr>
    </table>
             
    <asp:GridView ID="gridConteo" runat="server" CellPadding="4" 
        ForeColor="#333333" GridLines="None" Height="100%" Width="1020px" 
        ondatabound="gridConteo_DataBound" onrowdeleting="gridConteo_RowDeleting" 
        onselectedindexchanged="gridConteo_SelectedIndexChanged">
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Font-Names="Calibri" 
            Font-Size="Medium" />
            <Columns>
                <asp:CommandField 
                    ShowDeleteButton="True" CancelText="" DeleteText="Del" EditText="" 
                    InsertText="" NewText="" SelectText="" UpdateText="" />
                <asp:CommandField CancelText="" DeleteText="" EditText="" 
                    InsertText="" InsertVisible="False" NewText="" ShowCancelButton="False" 
                    ShowSelectButton="True" UpdateText="" SelectText="Sel" />
            </Columns>
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#0080C0" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#999999" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />        
    </asp:GridView> 
    <br />
    
    <asp:HiddenField ID="hfCodAlmacen" runat="server" />
    <asp:HiddenField ID="hfCantPaquete" runat="server" />
    <asp:HiddenField ID="hfCodProducto" runat="server" />
    <asp:ObjectDataSource ID="odsCalandras" runat="server" 
        SelectMethod="ReadMultiple" TypeName="INIKER.WorkCenter.WorkCenterList_Service">
        <SelectParameters>
            <asp:Parameter DefaultValue="CALAN" Name="Where" Type="String" />
            <asp:Parameter Name="pageSize" Type="Int32" />
            <asp:Parameter Name="numPage" Type="Int32" />
            <asp:Parameter Name="Sort" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>             
    <input id="hddDatosSinRegistrar" type="hidden" runat="server" />
</asp:Content>
