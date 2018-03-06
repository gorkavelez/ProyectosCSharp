<%@ Page Title="" Language="C#" MasterPageFile="~/MasterForms/menu.Master" AutoEventWireup="true" CodeBehind="RechazadoOxido.aspx.cs" Inherits="IntranetINDUSAL.WebForms.RechazadoOxido" %>
<%@ Register assembly="Telerik.Web.UI, Version=2009.1.527.35, Culture=neutral, PublicKeyToken=121fae78165ba3d4" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style2
        {
            width: 130px;
        }
        .style3
        {
            height: 6px;
            width: 130px;
        }
        .style4
        {
            height: 41px;
            width: 130px;
        }
        .style5
        {
            height: 83px;
            width: 130px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 100%;">
    <tr>
        <td class="style2">
                &nbsp;</td>
        <td style="width: 124px">
                <asp:Label ID="lbCliente" runat="server" Font-Bold="True" Font-Names="Calibri" 
                    Text="CLIENTE:"></asp:Label>
            </td>
        <td style="width: 669px">
            <telerik:RadTextBox ID="rtxCliente" Runat="server">
            </telerik:RadTextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style2">
                &nbsp;</td>
        <td style="width: 124px">
                <asp:Label ID="lbSubfamilia" runat="server" Font-Bold="True" Font-Names="Calibri" 
                    Text="SUBFAMILIA:"></asp:Label>
            </td>
        <td style="width: 669px">
            <telerik:RadTextBox ID="rtxSubfamilia" Runat="server">
            </telerik:RadTextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style2">
                &nbsp;</td>
        <td style="width: 124px">
                <asp:Label ID="lbNSerie" runat="server" Font-Bold="True" Font-Names="Calibri" 
                    Text="Nº SERIE:"></asp:Label>
            </td>
        <td style="width: 669px">
            <telerik:RadTextBox ID="rtxNSerie" Runat="server">
            </telerik:RadTextBox>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style3">
                &nbsp;</td>
        <td style="height: 6px; width: 124px">
                <asp:Label ID="lbTipoRechazo0" runat="server" Font-Bold="True" Font-Names="Calibri" 
                    Text="CANTIDAD:"></asp:Label>
        </td>
        <td style="height: 6px; width: 669px;">
            <telerik:RadNumericTextBox ID="rNtxCantidad" Runat="server">
                <NumberFormat DecimalDigits="0" />
            </telerik:RadNumericTextBox>
        </td>
        <td style="height: 6px">
            &nbsp;</td>
    </tr>
    <tr style="vertical-align:bottom">
        <td class="style4">
                &nbsp;</td>
        <td style="height: 41px; width: 124px">
                <asp:Label ID="lbTipoSalida" runat="server" Font-Bold="True" Font-Names="Calibri" 
                    Text="TIPO SALIDA:"></asp:Label>
        </td>
        <td style="height: 41px; width: 669px;">
            </td>
        <td style="height: 41px">
            </td>
    </tr>
    <tr>
        <td class="style5">
            &nbsp;</td>
        <td style="height: 83px; width: 124px">
        </td>
        <td style="height: 83px; width: 669px;">
            <asp:Button ID="btAlmacen" runat="server" Font-Bold="True" Font-Names="Calibri" 
                Height="70px" Text="A ALMACEN" Width="110px" />
            &nbsp;&nbsp;
            <asp:Button ID="btCostura" runat="server" Font-Bold="True" Font-Names="Calibri" 
                Height="70px" Text="A COSTURA" Width="110px" />
            &nbsp; &nbsp;<asp:Button ID="btCliente" runat="server" Font-Bold="True" Font-Names="Calibri" 
                Height="70px" Text="A CLIENTE" Width="110px" />
            &nbsp;&nbsp;
            <asp:Button ID="btCancelar" runat="server" Font-Bold="True" 
                Font-Names="Calibri" Height="70px" Text="CANCELAR" Width="110px" />
        </td>
        <td style="height: 83px">
            </td>
    </tr>
</table>
</asp:Content>
