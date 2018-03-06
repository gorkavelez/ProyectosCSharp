<%@ Page Title="" Language="C#" MasterPageFile="~/MasterForms/MenuIntranet.Master" AutoEventWireup="true" CodeBehind="ConteoRopa.aspx.cs" Inherits="IntranetINDUSAL.WebForms.ConteoRopa" %>
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
            width: 568px;
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
                            <asp:Label ID="lbFecha" runat="server" Text="FECHA:"></asp:Label></td>
                        <td class="descripcion">
                            <asp:TextBox ID="txFecha" runat="server"></asp:TextBox></td>
                        <td class="etiqueta">
                            <asp:Label ID="lbCliente" runat="server" Text="CLIENTE:"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txCodCliente" runat="server" AutoPostBack="True" 
                                ontextchanged="txCodCliente_TextChanged" Width="109px"></asp:TextBox></td>
                        <td class="descripcion">
                            <asp:Label ID="lbNomCliente" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="espacioIzda">
                            <asp:HiddenField ID="hfCodProducto" runat="server" />
                        </td>
                        <td class="etiqueta">
                            <asp:Label ID="lbProdSelec" runat="server" Text="PRODUCTO:"></asp:Label></td>
                        <td>
                            <asp:Label ID="lbDescProdSelec" runat="server"></asp:Label></td>                            
                        <td class="etiqueta">
                            <asp:Label ID="lbCantidad" runat="server" Text="CANTIDAD:"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txCantidad" runat="server" Enabled="False" Width="110px"></asp:TextBox>
                            &nbsp;
                            <asp:Label ID="lbQtyAlmacen" runat="server" Font-Bold="True" ForeColor="Gray"></asp:Label>
                        </td>
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
                                    <asp:CommandField ButtonType="Image" DeleteImageUrl="~/Imagenes/Eliminar.PNG" 
                                        ShowDeleteButton="True" />
                                    <asp:CommandField ButtonType="Image" CancelText="" DeleteText="" EditText="" 
                                        InsertText="" InsertVisible="False" NewText="" 
                                        SelectImageUrl="~/Imagenes/Editar.PNG" ShowCancelButton="False" 
                                        ShowSelectButton="True" UpdateText="" />
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
