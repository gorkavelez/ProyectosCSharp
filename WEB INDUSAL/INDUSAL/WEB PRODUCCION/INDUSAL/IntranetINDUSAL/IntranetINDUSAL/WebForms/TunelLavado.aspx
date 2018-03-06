<%@ Page Title="" Language="C#" MasterPageFile="~/MasterForms/menu.Master" AutoEventWireup="true" CodeBehind="TunelLavado.aspx.cs" Inherits="IntranetINDUSAL.WebForms.TunelLavado" %>
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
                <asp:Label ID="lbLavadora" runat="server" Font-Bold="True" Font-Names="Calibri" 
                    Text="TUNEL:"></asp:Label>
                        </td>
                        <td style="width: 157px">
                            <telerik:RadComboBox ID="rcbTuneles" Runat="server" DataSourceID="odsTuneles" 
                                DataTextField="Name" DataValueField="No">
                            </telerik:RadComboBox>
                        </td>
                        <td>
                            <asp:ObjectDataSource ID="odsTuneles" runat="server" 
                                SelectMethod="ReadMultiple" TypeName="INIKER.WorkCenter.WorkCenterList_Service">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="TUNEL" Name="Where" Type="String" />
                                    <asp:Parameter Name="pageSize" Type="Int32" />
                                    <asp:Parameter Name="numPage" Type="Int32" />
                                    <asp:Parameter Name="Sort" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            &nbsp;</td>
                        <td style="width: 101px">
                <asp:Label ID="lbPrograma" runat="server" Font-Bold="True" Font-Names="Calibri" 
                    Text="HORAS:"></asp:Label>
                        </td>
                        <td style="width: 157px">
                            <telerik:RadNumericTextBox ID="rNtxHoras" Runat="server">
                            </telerik:RadNumericTextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style2">
                            &nbsp;</td>
                        <td style="width: 101px">
                <asp:Label ID="lbPesadas" runat="server" Font-Bold="True" Font-Names="Calibri" 
                    Text="KILOS:"></asp:Label>
                        </td>
                        <td style="width: 157px">
                            <telerik:RadNumericTextBox ID="rNtxKilos" Runat="server">
                            </telerik:RadNumericTextBox>
                        </td>
                        <td>
                            &nbsp;</td>
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
