<%@ Page Title="RECEPCION ROPA" Language="C#" MasterPageFile="~/MasterForms/MenuIntranetVerticalBotones.Master" AutoEventWireup="true" CodeBehind="RecepcionRopa.aspx.cs" Inherits="IntranetINDUSAL.WebForms.RecepcionRopa" %>
<%@ Register src="../Controles_Personalizados/INIKER_tecladoNumerico.ascx" tagname="INIKER_tecladoNumerico" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../IntranetINDUSAL.css" rel="Stylesheet" type="text/css" />
    <script src="../IntranetINDUSAL.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">    
    <table style="width:100%;">
        <tr>
            <td style="vertical-align:top;">                
                <asp:Panel ID="panelDatos" runat="server" CssClass="panelDatos">                
                    <table>                    
                        <tr>
                            <td>
                                <asp:Label ID="lbTitTransp" runat="server" Text="TRANSP.:" CssClass="etiqueta"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlTransportistas" runat="server" AutoPostBack="True" 
                                    CssClass="desplegable" 
                                    onselectedindexchanged="ddlTransportistas_SelectedIndexChanged">
                                </asp:DropDownList>                
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbTitRuta" runat="server" Text="RUTA:" CssClass="etiqueta"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlRutasTransportista" runat="server" AutoPostBack="True" 
                                    CssClass="desplegable"
                                    onselectedindexchanged="ddlRutasTransportista_SelectedIndexChanged">
                                </asp:DropDownList>                
                            </td>
                        </tr>                
                        <tr>
                            <td>
                                <asp:Label ID="lbtitCliente" runat="server" Text="CLIENTE:" CssClass="etiqueta"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlClientesRuta" runat="server" 
                                    CssClass="desplegable"
                                    onselectedindexchanged="ddlClientesRuta_SelectedIndexChanged" 
                                    AutoPostBack="True">
                                </asp:DropDownList>               
                            </td>
                        </tr>
                    </table>                
                    <br />
                    <table>
                        <tr>
                            <td>                                
                                <asp:Button ID="btRecogido" runat="server" Text="RECOGIDO" CssClass="botonRecepcion" 
                                    CommandName="recogido" CommandArgument="RECOGIDO" onclick="btDato_Click"/>
                            </td>
                            <td>                                
                                <asp:Button ID="btEntregado" runat="server" Text="ENTREGADO" CssClass="botonRecepcion" 
                                    CommandName="entregado" CommandArgument="ENTREGADO" onclick="btDato_Click"/>
                            </td>
                            <td>                                
                                <asp:Button ID="btVacios" runat="server" Text="VACIOS" CssClass="botonRecepcion" 
                                    CommandName="vacios" CommandArgument="VACIOS" onclick="btDato_Click"/>
                            </td>
                            <td>                                
                                <asp:Button ID="btPeso" runat="server" Text="PESO" CssClass="botonRecepcion" 
                                    CommandName="peso" CommandArgument="PESO"  />
                            </td>  
                            <td>                                
                                <asp:Button ID="btEtiquetas" runat="server" Text="ETIQUETAS" CssClass="botonRecepcion" 
                                    CommandName="etiquetas" onclick="btDato_Click"/>
                            </td>            
                            <td>
                                <asp:Button ID="btPesoTeclado" runat="server" Text="..." CssClass="botonPeso" 
                                    CommandName="peso" CommandArgument="PESO" onclick="btDato_Click"  />
                            </td>          
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txRecogido" runat="server" CssClass="txIntro" Enabled="false"></asp:TextBox>
                            </td>                        
                            <td>
                                <asp:TextBox ID="txEntregado" runat="server" CssClass="txIntro" Enabled="false"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txVacios" runat="server" CssClass="txIntro" Enabled="false"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txPeso" runat="server" CssClass="txIntro" Enabled="false"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txEtiquetas" runat="server" CssClass="txIntro" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table>                    
                        <tr>
                            <td>
                                <asp:Label ID="lbTitIncidencias" runat="server" Text="INCIDENCIA:" CssClass="etiqueta"></asp:Label>                                                            
                            </td>
                            <td class="descripcion">
                                <asp:DropDownList ID="ddlIncidencias" runat="server" 
                                    CssClass="desplegable"
                                    AutoPostBack="true" 
                                    onselectedindexchanged="ddlIncidencias_SelectedIndexChanged" >
                                </asp:DropDownList>
                            </td>
                        </tr>                    
                    </table>
                    <br />
                    <asp:Panel ID="panelAcciones" runat="server" Enabled="false">                
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btRegConteo" runat="server" Text="CIRCUITO&#10;CON CONTEO" 
                                        CssClass="boton" CommandArgument="Conteo" onclick="Registrar_Click"/>                            
                                </td>                                                          
                                <td>
                                    <asp:Button ID="btRegSinConteo" runat="server" Text="CIRCUITO&#10;SIN CONTEO" 
                                        CssClass="boton" CommandArgument="SinConteo" onclick="Registrar_Click" />                            
                                </td>
                                <td>
                                    <asp:Button ID="btRegTercerCircuito" runat="server" Text="TERCER&#10;CIRCUITO" 
                                        CssClass="boton" CommandArgument="TercerCircuito" onclick="Registrar_Click" />                            
                                </td>
                                <td>
                                    <asp:Button ID="btCancelRecep" runat="server" Text="REINICIAR" 
                                        onclick="btCancelRecep_Click" CssClass="boton" />                            
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <br />
                    <asp:GridView ID="grdDatosReg" runat="server" CellPadding="4" ForeColor="#333333" 
                        GridLines="None" CssClass="gridLineasReadOnly" Width="160mm">
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>                                
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
            <td style="vertical-align:top;">            
                <asp:Panel ID="PanelTeclado" runat="server" Enabled="false" CssClass="panelTeclado">
                    <uc1:INIKER_tecladoNumerico ID="INIKER_teclado" runat="server" />            
                </asp:Panel>                
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdfPeso" runat="server" 
        onvaluechanged="hdfPeso_ValueChanged" />
</asp:Content>
