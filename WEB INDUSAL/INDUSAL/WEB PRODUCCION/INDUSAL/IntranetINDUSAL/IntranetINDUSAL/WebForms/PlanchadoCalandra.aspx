<%@ Page Title="" Language="C#" MasterPageFile="~/MasterForms/MenuIntranet.Master" AutoEventWireup="true" CodeBehind="PlanchadoCalandra.aspx.cs" Inherits="IntranetINDUSAL.WebForms.PlanchadoCalandra" %>
<%@ Register src="../Controles_Personalizados/INIKER_ButtonDG.ascx" tagname="INIKER_ButtonDG" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .espacioIzda
        {
        	height: 40px;
            width: 130px;            
        }
        .etiqueta
        {
            height: 40px;
            width: 130px;
            font-family:Calibri;
            font-size:large;
            font-weight:bold;                                    
        }
        .codigo
        {
            height: 40px;
            width: 130px;
        }
        .descripcion
        {
        	height: 40px;
            width: 300px;            
        }  
        .lineas
        {
        	width: 50%;
        	vertical-align: top;
        }          
        
        .style2
        {
            width: 572px;
        }
        
        .style4
        {
            width: 130px;
        }
        
        .style6
        {
            height: 40px;
            width: 460px;
        }
        .style8
        {
            width: 241px;
        }
        
        .style9
        {
            height: 40px;
            width: 216px;
            font-family: Calibri;
            font-size: large;
            font-weight: bold;
        }
        .style11
        {
            height: 40px;
            width: 460px;
            font-family: Calibri;
            font-size: large;
            font-weight: bold;
        }
        .style12
        {
            width: 460px;
        }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width:100%;">
        <tr>
            <td>
                <table>
                    <tr>
                        <td class="espacioIzda">
                            <asp:HiddenField ID="hfCodAlmacen" runat="server" />
                        </td>
                        <td class="etiqueta">
                            <asp:Label ID="lbOperario" runat="server" Text="OPERARIO:"></asp:Label></td>
                        <td class="etiqueta">
                            <asp:TextBox ID="txCodOperario" runat="server" AutoPostBack="True" 
                                ontextchanged="txCodCliente_TextChanged" Width="109px"></asp:TextBox></td>                        
                        <td  class="style11">
                            &nbsp;</td>                        
                    </tr>                    
                    <tr>     
                        <td class="espacioIzda">&nbsp;</td>                   
                        <td class="etiqueta">
                            <asp:Label ID="lbCliente" runat="server" Text="CLIENTE:"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txCodCliente" runat="server" AutoPostBack="True" 
                                ontextchanged="txCodCliente_TextChanged" Width="109px"></asp:TextBox></td>
                        <td class="style6">
                            <asp:Label ID="lbNomCliente" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="espacioIzda">
                            <asp:HiddenField ID="hfCantPaquete" runat="server" />
                        </td>
                        <td class="etiqueta">
                            <asp:Label ID="lbCalandra" runat="server" Text="CALANDRA:"></asp:Label></td>
                        <td>
                            <asp:DropDownList ID="ddlCalandras" runat="server" DataSourceID="odsCalandras" 
                                DataTextField="Name" DataValueField="No" AutoPostBack="True" Height="16px" 
                                onselectedindexchanged="ddlCalandras_SelectedIndexChanged" Width="107px">
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="odsCalandras" runat="server" 
                                    SelectMethod="ReadMultiple" TypeName="INIKER.WorkCenter.WorkCenterList_Service">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="CALAN" Name="Where" Type="String" />
                                        <asp:Parameter Name="pageSize" Type="Int32" />
                                        <asp:Parameter Name="numPage" Type="Int32" />
                                        <asp:Parameter Name="Sort" Type="String" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                        </td>
                        <td class="style12">
                            <asp:Label ID="lbCalandraSel" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="espacioIzda">
                            <asp:HiddenField ID="hfCodProducto" runat="server" /></td>
                        <td class="etiqueta">
                            <asp:Label ID="lbProdSelec" runat="server" Text="PRODUCTO:"></asp:Label></td>
                        <td></td>
                        <td class="style12">
                            <asp:Label ID="lbDescProdSelec" runat="server"></asp:Label></td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td class="espacioIzda"></td>
                        <td class="etiqueta"><asp:Label ID="lbPaq" runat="server" Text="PAQUETES"></asp:Label></td>
                        <td class="etiqueta"><asp:Label ID="lbEtiPaq" runat="server" Text="ETIQ. PAQ."></asp:Label></td>
                        <td class="etiqueta"><asp:Label ID="lbUds" runat="server" Text="UNIDADES"></asp:Label></td>
                        <td class="etiqueta"><asp:Label ID="lbEtiUds" runat="server" Text="ETIQ. UDS."></asp:Label></td>                        
                        <td class="etiqueta"><asp:Label ID="lbCantPaq" runat="server" Text="UDS. PAQUETE"></asp:Label></td>
                        <td class="etiqueta"><asp:Label ID="lbCantAlm" runat="server" Text="UDS. ALMACEN"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="espacioIzda"></td>
                        <td class="style4"><asp:TextBox ID="txPaq" runat="server" Enabled="False" 
                                Width="110px" AutoPostBack="True" ontextchanged="txPaq_TextChanged"></asp:TextBox></td>
                        <td class="style4"><asp:TextBox ID="txEtiqPaq" runat="server" Enabled="False" Width="110px"></asp:TextBox></td>
                        <td class="style4"><asp:TextBox ID="txUnidades" runat="server" Enabled="False" 
                                Width="110px" AutoPostBack="True" ontextchanged="txUnidades_TextChanged"></asp:TextBox></td>
                        <td class="style4"><asp:TextBox ID="txEtiqUds" runat="server" Enabled="False" Width="110px"></asp:TextBox></td>
                        <td class="style4"><asp:Label ID="lbUdsPaq" runat="server" Text=""></asp:Label></td>
                        <td class="style4"><asp:Label ID="lbQtyAlmacen" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="espacioIzda"></td>
                        <td>
                            <asp:Button ID="btAddCount" runat="server" onclick="btAddCount_Click" 
                                Text="OK" />
                            <asp:Button ID="btCancel" runat="server" onclick="btCancel_Click" 
                                Text="Cancel" />
                            <asp:Button ID="btRegistrar" runat="server" onclick="btRegistrar_Click" 
                                Text="Registrar" />
                        </td>
                    </tr>
                </table>
            </td>            
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td class="style2">
                            <uc1:INIKER_ButtonDG ID="INIKER_gridSurtido" runat="server" />
                        </td>
                        <td class="lineas">
                            <asp:GridView ID="gridConteo" runat="server" CellPadding="4" 
                                ForeColor="#333333" GridLines="None" Height="100%" Width="100%" 
                                onrowdeleting="gridConteo_RowDeleting" 
                                onselectedindexchanged="gridConteo_SelectedIndexChanged">
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
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
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#999999" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            </asp:GridView>
                        </td>                        
                    </tr>
                </table>
            </td>            
        </tr>        
    </table>
</asp:Content>
