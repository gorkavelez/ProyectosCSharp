<%@ Page Title="" Language="C#" MasterPageFile="~/MasterForms/indusal.master" AutoEventWireup="true" CodeFile="recepcion.aspx.cs" Inherits="recepcion" %>

<%@ Register assembly="Telerik.Web.UI, Version=2009.1.527.35, Culture=neutral, PublicKeyToken=121fae78165ba3d4" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>
    <table style="width: 100%;">
    <tr>
        <td style="width: 528px">
            
            &nbsp;</td>
        <td>
            &nbsp;&nbsp;    
            </td>
    </tr>
    <tr>
        <td style="width: 528px">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 125px; height: 40px;">
                        <asp:Label ID="lbFecha" runat="server" Font-Bold="True" 
                            Font-Names="Calibri" Text="FECHA:"></asp:Label>
                    </td>
                    <td headers="40px" style="height: 40px">
                        <telerik:RadTextBox ID="rtxFecha" Runat="server">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 125px; height: 41px;">
                        <asp:Label ID="lbTransportista" runat="server" Font-Bold="True" 
                            Font-Names="Calibri" Text="TRANSPORTISTA:"></asp:Label>
                    </td>
                    <td style="height: 40px">
                        <telerik:RadComboBox ID="rcbTransportistas" Runat="server" Height="22px" 
                            Width="205px">
                        </telerik:RadComboBox>
                        <asp:ObjectDataSource ID="odsTransportistas" runat="server" 
                            SelectMethod="ReadMultiple" 
                            TypeName="INIKER.Transportistas.ListaTransportistasINDUSAL_Service">
                            <SelectParameters>
                                <asp:Parameter Name="Where" Type="String" />
                                <asp:Parameter Name="pageSize" Type="Int32" />
                                <asp:Parameter Name="numPage" Type="Int32" />
                                <asp:Parameter Name="Sort" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
                <tr>
                    <td style="width: 125px; height: 40px;">
                        <asp:Label ID="lbRuta" runat="server" Font-Bold="True" Font-Names="Calibri" 
                            Text="RUTA:"></asp:Label>
                    </td>
                    <td style="height:40px">
                        <telerik:RadComboBox ID="rcbRutasTransportista" Runat="server" Height="22px" 
                            Width="205px">
                        </telerik:RadComboBox>
                        <asp:ObjectDataSource ID="odsRutasT" runat="server" SelectMethod="ReadMultiple" 
                            TypeName="INIKER.RutasTransporte.ListaRTransporteINDUSAL_Service">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="rcbTransportistas" Name="Where" 
                                    PropertyName="SelectedValue" Type="String" />
                                <asp:Parameter Name="pageSize" Type="Int32" />
                                <asp:Parameter Name="numPage" Type="Int32" />
                                <asp:Parameter Name="Sort" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
                <tr>
                    <td style="width: 125px">
                        <asp:Label ID="lbCliente" runat="server" Font-Bold="True" Font-Names="Calibri" 
                            Text="CLIENTE:"></asp:Label>
                    </td>
                    <td height="40">
                        <telerik:RadComboBox ID="rcbClientes" Runat="server" Height="23px" 
                            Width="204px">
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 125px">
                        <asp:Label ID="lbRecogido" runat="server" Font-Bold="True" Font-Names="Calibri" 
                            Text="RECOGIDO:"></asp:Label>
                    </td>
                    <td height="40" style="vertical-align:middle">
                        <telerik:RadNumericTextBox ID="rNtxRecogido" Runat="server" Height="100%">
                        </telerik:RadNumericTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 125px; height: 40px;">
                        <asp:Label ID="lbEntregado" runat="server" Font-Bold="True" Font-Names="Calibri" 
                            Text="ENTREGADO:"></asp:Label>
                    </td>
                    <td style="height: 40px">                    
                        <telerik:RadNumericTextBox ID="rNtxEntregado" Runat="server" Height="100%">
                        </telerik:RadNumericTextBox>
                        </td>
                </tr>
                <tr>
                    <td style="width: 125px; height: 40px;">
                        <asp:Label ID="lbPeso" runat="server" Font-Bold="True" Font-Names="Calibri" 
                            Text="PESO:"></asp:Label>
                    </td>
                    <td style="height: 40px">
                        <telerik:RadNumericTextBox ID="rNtxPeso" Runat="server" 
                            Culture="Spanish (Spain)" Width="125px">
                        </telerik:RadNumericTextBox>
                        </td>
                </tr>
                <tr>
                    <td style="width: 125px; height: 67px;">
                    </td>
                    <td style="height: 67px">
                        <asp:Button ID="btAceptar" runat="server" Height="100%" 
                            onclick="btAceptar_Click" Text="Añadir línea" Width="45%" 
                            Font-Size="Large" />
                        <asp:Button ID="btCancelar" runat="server" Height="100%" 
                            onclick="btCancelar_Click" Text="Cancelar línea" Width="45%" 
                            Font-Size="Large" />
                    </td>
                </tr>
            </table>
        </td>
        <td style="vertical-align:top;">
            <table style="width:100%;">
                <tr>
                    <td style="height: 295px; vertical-align:top">
                        <telerik:RadGrid ID="rgLineasRecepcion" runat="server">
<MasterTableView>
<RowIndicatorColumn>
<HeaderStyle Width="20px"></HeaderStyle>
</RowIndicatorColumn>

<ExpandCollapseColumn>
<HeaderStyle Width="20px"></HeaderStyle>
</ExpandCollapseColumn>
</MasterTableView>
                        </telerik:RadGrid>
                    </td>
                </tr>
                <tr>
                    <td style="height: 67px">
                        <asp:Button ID="Button1" runat="server" Font-Size="Large" Height="100%" 
                            Text="Registrar Recepción" Width="25%" />
                        <asp:Button ID="Button2" runat="server" Font-Size="Large" Height="100%" 
                            Text="Cancelar" Width="25%" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
</asp:Content>

