<%@ Page Title=":: RECEPCION DE ROPA :: " Language="C#" MasterPageFile="~/MasterForms/MenuIntranet.Master" AutoEventWireup="true" CodeBehind="RecepcionRopaOLD.aspx.cs" Inherits="IntranetINDUSAL.RecepcionRopa" %>
<%@ Register assembly="Telerik.Web.UI, Version=2009.1.527.35, Culture=neutral, PublicKeyToken=121fae78165ba3d4" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style2
        {
            width: 416px;
        }
        .FilaBotones
        {
        	height: 50px;        	
        	width: 25%;
        }
        .InputText
        {
        	height:30px;
        	width: 30px;
        	text-align: right;
        	vertical-align:middle;
        	font-size:large;
        	font-weight:bold;        	
        }
        .HeaderCols1
        {
        	width:30px;
        }        
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <table>
            <tr>                
                <td class="style2"><asp:Label ID="Label2" runat="server" Font-Bold="True" 
                            Font-Names="Calibri" Text="TRANSPORTISTA:"></asp:Label></td>
                <td class="style2"><asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Calibri" 
                            Text="RUTA:"></asp:Label></td>
                <td class="style2"><asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Calibri" 
                            Text="CLIENTE:"></asp:Label></td>
                <td class="HeaderCols1"><asp:Label ID="Label1" runat="server" Font-Bold="True" 
                            Font-Names="Calibri" Text="FECHA:"></asp:Label></td>                            
                <td class="HeaderCols1"><asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="Calibri" 
                            Text="RECOGIDO:"></asp:Label></td>
                <td class="HeaderCols1"><asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="Calibri" 
                            Text="ENTREGADO:"></asp:Label></td>
                <td class="HeaderCols1"><asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Names="Calibri" 
                            Text="PESO:"></asp:Label></td>
            </tr>
            <tr>                
                <td>
                    <asp:DropDownList ID="ddlTransportistas" runat="server" AutoPostBack="True" 
                        DataSourceID="odsTransportistas" DataTextField="Name" DataValueField="Code" 
                        Font-Size="Large" Height="30px" Width="100%">
                    </asp:DropDownList>
                        <asp:ObjectDataSource ID="odsTransportistas" runat="server" 
                            SelectMethod="ReadMultiple" 
                            TypeName="INIKER.Transportistas.ListaTransportistasINDUSAL_Service">
                            <SelectParameters>
                                <asp:Parameter Name="Where" Type="String" />
                                <asp:Parameter Name="pageSize" Type="Int32" />
                                <asp:Parameter Name="numPage" Type="Int32" />
                                <asp:Parameter Name="Sort" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource></td>
                <td>
                    <asp:DropDownList ID="ddlRutasTransportista" runat="server" AutoPostBack="True" 
                        DataSourceID="odsRutasTte" DataTextField="Nombre_ruta" 
                        DataValueField="Cod_ruta" Font-Size="Large" Height="30px" Width="100%">
                    </asp:DropDownList>
                        <asp:ObjectDataSource ID="odsRutasTte" runat="server" 
                            SelectMethod="ReadMultiple" 
                            TypeName="INIKER.RutasTransporte.ListaRTransporteINDUSAL_Service">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="ddlTransportistas" DefaultValue="##" 
                                    Name="Where" PropertyName="SelectedValue" Type="String" />
                                <asp:Parameter Name="pageSize" Type="Int32" />
                                <asp:Parameter Name="numPage" Type="Int32" />
                                <asp:Parameter Name="Sort" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource></td>
                <td>
                    <asp:DropDownList ID="ddlClientesRuta" runat="server" 
                        DataSourceID="odsClientesRuta" DataTextField="Name" DataValueField="No" 
                        Font-Size="Large" Height="30px" Width="100%">
                    </asp:DropDownList>
                        <asp:ObjectDataSource ID="odsClientesRuta" runat="server" 
                            SelectMethod="ReadMultiple" 
                            TypeName="INIKER.Cliente.ListaClientesINDUSAL_Service">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="ddlRutasTransportista" DefaultValue="##" 
                                    Name="Where" PropertyName="SelectedValue" Type="String" />
                                <asp:Parameter Name="pageSize" Type="Int32" />
                                <asp:Parameter Name="numPage" Type="Int32" />
                                <asp:Parameter Name="Sort" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource></td>
                <td>
                    <telerik:RadTextBox ID="rtxFecha" Runat="server" CssClass="InputText" 
                        ReadOnly="True">
                    </telerik:RadTextBox>
                </td>                        
                <td><telerik:RadNumericTextBox ID="rNtxRecogido" Runat="server" Font-Bold="True" 
                        Font-Size="Large" Height="30px" CssClass="InputText">
                            <NumberFormat DecimalDigits="0" />
                        </telerik:RadNumericTextBox></td>
                <td><telerik:RadNumericTextBox ID="rNtxEntregado" Runat="server" Font-Bold="True" 
                        Font-Size="Large" Height="30px" CssClass="InputText">
                            <NumberFormat DecimalDigits="0" />
                        </telerik:RadNumericTextBox></td>
                <td><telerik:RadNumericTextBox ID="rNtxPeso" Runat="server" Font-Bold="True" 
                        Font-Size="Large" Height="30px" CssClass="InputText">
                        </telerik:RadNumericTextBox></td>
            </tr>
        </table>
    </tr>
    <tr><td>&nbsp;</td></tr>
    <tr>
        <td style="width:100%;">
            <table style="width: 100%;">
                <tr>
                    <td class="FilaBotones"><asp:Button ID="btAceptar" runat="server" Height="100%" 
                            Text="Añadir línea" Width="100%" 
                                Font-Size="Large" onclick="btAceptar_Click" /></td>
                    <td class="FilaBotones"><asp:Button ID="btCancelar" runat="server" Height="100%" 
                            Text="Cancelar línea" Width="100%" 
                                Font-Size="Large" onclick="btCancelar_Click" /></td>
                    <td class="FilaBotones"><asp:Button ID="btRegRecepcion" runat="server" 
                            Font-Size="Large" Height="100%" 
                                Text="Registrar Recepción" Width="100%" Enabled="False" 
                            onclick="btRegRecepcion_Click" /></td>
                    <td class="FilaBotones"><asp:Button ID="btCancelRecep" runat="server" 
                            Font-Size="Large" Height="100%" 
                                Text="Cancelar Recepción" Width="100%" Enabled="False" 
                            onclick="btCancelRecep_Click" /></td>                
                </tr>            
            </table>
        </td>
    </tr>
    <tr><td>&nbsp;</td></tr>
    <tr>
        <td>
        <asp:ObjectDataSource ID="odsLineasRecepcion" runat="server" 
        SelectMethod="GetLines" TypeName="IntranetINDUSAL.cRecepcion">
    </asp:ObjectDataSource>
        <telerik:RadGrid ID="rgLinRecepcion" runat="server" GridLines="None" 
        AllowAutomaticDeletes="True" AutoGenerateDeleteColumn="True" 
        ondeletecommand="rgLinRecepcion_DeleteCommand">
                        </telerik:RadGrid></td>
    </tr>
    </table>

</asp:Content>
