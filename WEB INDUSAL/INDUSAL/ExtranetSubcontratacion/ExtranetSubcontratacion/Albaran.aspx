<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Albaran.aspx.cs" Inherits="ExtranetSubcontratacion.Albaran" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="Albaran.css" rel="stylesheet" type="text/css" media="screen" />
    <link href="printAlbaran.css" rel="stylesheet" type="text/css" media="print" />    
    <title></title>    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="panelCabecera" runat="server">
            <table>
                <tr>
                    <td>
                        <asp:Panel ID="panelDatosINDUSAL" runat="server" Width="90mm">
                            <table>
                                <tr><td><asp:Image ID="logoIndusal" runat="server" ImageUrl="~/Imagenes/palomo.BMP"/></td></tr>
                                <tr><td><asp:Label ID="lbDatosIndusal1" runat="server" CssClass="encabezado_emisor_bold"></asp:Label></td></tr>
                                <tr><td><asp:Label ID="lbDatosIndusal2" runat="server" CssClass="encabezado_emisor"></asp:Label></td></tr>
                                <tr><td><asp:Label ID="lbDatosIndusal3" runat="server" CssClass="encabezado_emisor"></asp:Label></td></tr>
                                <tr><td><asp:Label ID="lbDatosIndusal4" runat="server" CssClass="encabezado_emisor"></asp:Label></td></tr>
                                <tr><td><asp:Label ID="lbDatosIndusal5" runat="server" CssClass="encabezado_emisor"></asp:Label></td></tr>
                                <tr><td><asp:Label ID="lbDatosIndusal6" runat="server" CssClass="encabezado_emisor"></asp:Label></td></tr>
                                <tr><td><asp:Label ID="lbDatosIndusal7" runat="server" CssClass="encabezado_emisor"></asp:Label></td></tr>
                                <tr><td><asp:Label ID="lbDatosIndusal8" runat="server" CssClass="encabezado_emisor"></asp:Label></td></tr>
                            </table>
                        </asp:Panel>                        
                    </td>
                    <td>
                        <asp:Panel ID="panelDatosProveedor" runat="server" Width="90mm">
                            <table>
                                <tr><td><asp:Label ID="lbTitAlbaran" runat="server" Text="Albarán" CssClass="encabezado_emisor_grande"></asp:Label></td></tr>
                                <tr><td><hr /></td></tr>
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td style="width:20mm;"><asp:Label ID="lbTitNAlbaran" runat="server" CssClass="encabezado_emisor_bold" Text="Nº albarán:"></asp:Label></td>
                                                <td><asp:Label ID="lbDatosAlbaran1" runat="server" CssClass="encabezado_emisor_bold"></asp:Label></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr><td><hr /></td></tr>
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td style="width:20mm;"><asp:Label ID="lbTitFecha" runat="server" CssClass="encabezado_emisor_bold" Text="Fecha:"></asp:Label></td>
                                                <td><asp:Label ID="lbDatosAlbaran2" runat="server" CssClass="encabezado_emisor"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="width:20mm;"><asp:Label ID="lbTitSuNumero" runat="server" CssClass="encabezado_emisor_bold" Text="Su número:"></asp:Label></td>
                                                <td><asp:Label ID="lbDatosAlbaran3" runat="server" CssClass="encabezado_emisor"></asp:Label></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>                                
                                <tr><td><hr class="separador" /></td></tr>
                                <tr><td><asp:Label ID="lbDatosAlbaran4" runat="server" CssClass="encabezado_emisor_bold"></asp:Label></td></tr>                                
                                <tr><td><asp:Label ID="lbDatosAlbaran5" runat="server" CssClass="encabezado_emisor"></asp:Label></td></tr>
                                <tr><td><asp:Label ID="lbDatosAlbaran6" runat="server" CssClass="encabezado_emisor"></asp:Label></td></tr>
                                <tr><td><asp:Label ID="lbDatosAlbaran7" runat="server" CssClass="encabezado_emisor"></asp:Label></td></tr>
                                <tr><td><asp:Label ID="lbDatosAlbaran8" runat="server" CssClass="encabezado_emisor"></asp:Label></td></tr>
                                <tr><td><asp:Button ID="btImprimir" runat="server" Text="IMPRIMIR" 
                                            CssClass="boton" OnClientClick="javascript:window.print();"/>
                                    </td></tr>
                            </table>
                        </asp:Panel>
                    </td>                    
                </tr>                
            </table>
        </asp:Panel>
        <br />
        <asp:Panel ID="panelLineas" runat="server" Width="180mm">
            <table>
                <tr>
                    <td style="width:100px; text-align:left;">
                        <asp:Label ID="lbEncabezadoGrid1" runat="server" Text="Código" CssClass="encabezado_emisor_bold"></asp:Label>
                    </td>
                    <td style="width:450px; text-align:left;">
                        <asp:Label ID="lbEncabezadoGrid2" runat="server" Text="Descripción" CssClass="encabezado_emisor_bold"></asp:Label>
                    </td>
                    <td style="width:70px; text-align:right;">
                        <asp:Label ID="lbEncabezadoGrid3" runat="server" Text="Cant." CssClass="encabezado_emisor_bold"></asp:Label>
                    </td>
                    <td style="width:100px; text-align:center;">
                        <asp:Label ID="lbEncabezadoGrid4" runat="server" Text="UM" CssClass="encabezado_emisor_bold"></asp:Label>
                    </td>
                </tr>
            </table>
            <hr />
            <asp:GridView ID="gridLineas" runat="server" GridLines="None" 
                CssClass="lineas" onrowdatabound="gridLineas_RowDataBound" 
                ShowHeader="False">                
            </asp:GridView>
        </asp:Panel>
        <asp:Panel ID="panelPie" runat="server">
        
        </asp:Panel>
    </div>
    </form>
</body>
</html>
