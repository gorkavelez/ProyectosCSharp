<%@ Page Title="" Language="C#" MasterPageFile="~/MasterForms/menu.Master" AutoEventWireup="true" CodeBehind="PlanchadoForma.aspx.cs" Inherits="IntranetINDUSAL.WebForms.PlanchadoForma" %>
<%@ Register assembly="Telerik.Web.UI, Version=2009.1.527.35, Culture=neutral, PublicKeyToken=121fae78165ba3d4" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style2
        {
            width: 130px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 100%;">
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td class="style2">
                            &nbsp;</td>
                        <td style="width: 101px">
                <asp:Label ID="lbOperario" runat="server" Font-Bold="True" Font-Names="Calibri" 
                    Text="OPERARIO:"></asp:Label>
                        </td>
                        <td style="width: 157px">
                            <telerik:RadTextBox ID="rtxCodOperario" Runat="server">
                            </telerik:RadTextBox>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="rtxNomOperario" Runat="server" ReadOnly="True" 
                                Width="300px">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            &nbsp;</td>
                        <td style="width: 101px">
                <asp:Label ID="lbFecha" runat="server" Font-Bold="True" Font-Names="Calibri" 
                    Text="FECHA:"></asp:Label>
                        </td>
                        <td style="width: 157px">
                            <telerik:RadTextBox ID="rtxFecha" Runat="server">
                            </telerik:RadTextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style2">
                            &nbsp;</td>
                        <td style="width: 101px">
                <asp:Label ID="lbHora" runat="server" Font-Bold="True" Font-Names="Calibri" 
                    Text="HORA:"></asp:Label>
                        </td>
                        <td style="width: 157px">
                            <telerik:RadTextBox ID="rtxHora" Runat="server">
                            </telerik:RadTextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style2">
                            &nbsp;</td>
                        <td style="width: 101px">
                <asp:Label ID="lbCliente" runat="server" Font-Bold="True" Font-Names="Calibri" 
                    Text="CLIENTE:"></asp:Label>
                        </td>
                        <td style="width: 157px">
                            <telerik:RadTextBox ID="rtxCodCliente" Runat="server">
                            </telerik:RadTextBox>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="rtxNomCliente" Runat="server" ReadOnly="True" 
                                Width="300px">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            &nbsp;</td>
                        <td style="width: 101px">
                <asp:Label ID="lbDetalle" runat="server" Font-Bold="True" Font-Names="Calibri" 
                    Text="DETALLE:"></asp:Label>
                        </td>
                        <td style="width: 157px">
                            &nbsp;</td>
                        <td>
                            <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="Red" 
                                Text="NOTA: Tener en cuenta que es ropa identificada."></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>
