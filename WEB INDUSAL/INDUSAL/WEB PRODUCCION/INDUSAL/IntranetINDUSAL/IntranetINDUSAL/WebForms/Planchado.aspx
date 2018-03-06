<%@ Page Title="" Language="C#" MasterPageFile="~/MasterForms/MenuIntranetVertical.Master" AutoEventWireup="true" CodeBehind="Planchado.aspx.cs" Inherits="IntranetINDUSAL.WebForms.Planchado" %>
<%@ Register src="../Controles_Personalizados/INIKER_ButtonDG.ascx" tagname="INIKER_ButtonDG" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">        
        .etiqueta
        {
            height: 40px;
            width: 90px;
            font-family:Calibri;
            font-size: medium;
            font-weight:bold;                                    
        }        
        .descripcion
        {
        	font-family:Calibri;
        	height: 40px;
            width: 250px;            
        }  
        .numSerie
        {
        	font-family:Calibri;        	
            font-size: small;
            font-weight:bold;
        	height: 40px;
            width: 330px;            
        }  
        .datoProducto
        {
        	height: 40px;
        	width: 68px;   
        	font-family:Calibri;
            font-size:Small;
            font-weight:bold;      	
        }
        .total
        {
        	height: 100%;
        	width: 100%;
        }
        .boton
        {
            height: 40px;
        	width: 166px;
        }
        .style4
        {
            font-family: Calibri;
            height: 40px;
            width: 290px;
        }
        .textoEtiqueta
        {
        	font-family:Calibri;
            font-size: medium;
            font-weight:bold; 
        }          
        .textoBotonSurtido
        {
        	height:40px;
        	width:180px;
        	font-family:Calibri;
            font-size:small;                                                
        }        
        .panelBotones
        {
        	width:1020px;
        	vertical-align:top;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>                        
            <td class="etiqueta">
                <asp:Label ID="lbOperario" runat="server" Text="OPERARIO:"></asp:Label></td>
            <td class="descripcion">
                <asp:TextBox ID="txCodOperario" runat="server" AutoPostBack="True" 
                    ontextchanged="txCodCliente_TextChanged" Width="109px"></asp:TextBox><br />
            <asp:Label ID="lbNomOperario" runat="server" Font-Size="Small"></asp:Label>
            </td> 
            <td class="etiqueta">
                <asp:Label ID="lbCliente" runat="server" Text="CLIENTE:"></asp:Label></td>
            <td class="descripcion">
                <asp:TextBox ID="txCodCliente" runat="server" AutoPostBack="True" 
                    ontextchanged="txCodCliente_TextChanged" Width="109px"></asp:TextBox><br />
                <asp:Label ID="lbNomCliente" runat="server" Font-Size="Small"></asp:Label>
            </td>                                                         
            <td class="etiqueta">
                <asp:Label ID="lbCalandra" runat="server" Text="MAQUINA:"></asp:Label></td>
            <td class="descripcion">
                <asp:DropDownList ID="ddlMaquinas" runat="server" DataSourceID="odsCalandras" 
                    DataTextField="Name" DataValueField="No" AutoPostBack="True" 
                    onselectedindexchanged="ddlMaquinas_SelectedIndexChanged" Width="107px">
                </asp:DropDownList><br />
                <asp:Label ID="lbCalandraSel" runat="server" Font-Size="Small"></asp:Label>
            </td>                        
        </tr>
    </table>
    
    <table>
        <tr>                        
            <td class="etiqueta">
                <asp:Label ID="lbProdSelec" runat="server" Text="PRODUCTO:"></asp:Label>                            
            </td>
            <td class="descripcion">
                <asp:Label ID="lbDescProdSelec" runat="server" Font-Size="Medium"></asp:Label><br />
                <asp:Label ID="lbQtyAlmacen" runat="server" Font-Size="Small"></asp:Label>
            </td>                        
            <td class="datoProducto">
                <asp:Label ID="lbPaq" runat="server" Text="PAQUETES"></asp:Label><br />
                <asp:TextBox ID="txPaq" runat="server" Enabled="False" 
                            AutoPostBack="True" ontextchanged="txPaq_TextChanged" Width="50px"></asp:TextBox>
            </td>
            <td class="datoProducto">
                <asp:Label ID="lbEtiPaq" runat="server" Text="ETQ. PAQ."></asp:Label><br />
                <asp:TextBox ID="txEtiqPaq" runat="server" Enabled="False" Width="50px"></asp:TextBox>
            </td>
            <td class="datoProducto">
                <asp:Label ID="lbUds" runat="server" Text="UNIDADES"></asp:Label><br />
                <asp:TextBox ID="txUnidades" runat="server" Enabled="False" 
                            AutoPostBack="True" 
                    ontextchanged="txUnidades_TextChanged" Width="50px"></asp:TextBox>
            </td>
            <td class="datoProducto">
                <asp:Label ID="lbEtiUds" runat="server" Text="ETQ. UDS."></asp:Label><br />
                <asp:TextBox ID="txEtiqUds" runat="server" Enabled="False" Width="50px"></asp:TextBox>
            </td>
            <td class="datoProducto">
                <asp:Label ID="lbCantPaq" runat="server" Text="UDS. PAQ."></asp:Label><br />
                <asp:TextBox ID="txUdsPaq" runat="server" ReadOnly="True" Width="50px"></asp:TextBox>                            
            </td>
            <td class="numSerie">
                <asp:Label ID="lbNSerie" runat="server" Text="NUM. SERIE"></asp:Label><br />
                <asp:TextBox ID="TxNSerie" runat="server" Enabled="False" Width="300px" ></asp:TextBox>
            </td> 
        </tr>
    </table>
    
    <table>
        <tr>
            <td class="etiqueta">&nbsp;</td>
            <td class="descripcion">&nbsp;</td>
            <td class="boton">
                <asp:Button ID="btAddCount" runat="server" onclick="btAddCount_Click" 
                    Text="Añadir línea" CssClass="total" />
            </td>    
            <td class="boton">
                <asp:Button ID="btCancel" runat="server" onclick="btCancel_Click" 
                    Text="Cancelar línea" CssClass="total" />
            </td>
            <td class="boton">
                <asp:Button ID="btRegistrar" runat="server" Text="Registrar" CssClass="total" 
                    onclick="btRegistrar_Click" Visible="False" />
            </td>
            <td class="boton">
                <asp:Button ID="btCancelar" runat="server" Text="Cancelar registro" CssClass="total" 
                    Visible="False" onclick="btCancelar_Click" />
            </td>
        </tr>
        <tr><td style="height:15px;"></td></tr>
    </table>
         
    <asp:GridView ID="gridConteo" runat="server" CellPadding="4" 
        ForeColor="#333333" GridLines="None" Height="100%" Width="1020px"
        onrowdeleting="gridConteo_RowDeleting" 
        onselectedindexchanged="gridConteo_SelectedIndexChanged" 
        ondatabound="gridConteo_DataBound">
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
    
    <asp:Panel ID="pnlClteNormal" runat="server">
        <uc1:INIKER_ButtonDG ID="INIKER_gridSurtido" runat="server" Width="1020" 
        EnableViewState="True" HeaderBackColor="#0080C0" />
    </asp:Panel>    
         
    <asp:Panel ID="pnlClteSAL" runat="server">
        <asp:Panel ID="pnlTitFamilias" runat="server">
            <table>
                <tr>
                    <td class="etiqueta"><asp:Label ID="lbFamilia" runat="server" Text="FAMILIA:" CssClass="textoEtiqueta"></asp:Label></td>
                    <td><asp:Label ID="lbFamSel" runat="server" Text="" CssClass="textoEtiqueta" ForeColor="Blue"></asp:Label></td>
                </tr>
            </table>            
        </asp:Panel>        
        <asp:Panel ID="pnlFamilias" runat="server" Width="1020">                    
        </asp:Panel>        
        
        <asp:Panel ID="pnlTitSubfam" runat="server">
            <table>
                <tr>
                    <td class="etiqueta"><asp:Label ID="lbSubfam" runat="server" Text="SUBFAMILIA:" CssClass="textoEtiqueta"></asp:Label></td>
                    <td><asp:Label ID="lbSubfamSel" runat="server" Text="" CssClass="textoEtiqueta" ForeColor="Blue"></asp:Label></td>
                </tr>
            </table>            
        </asp:Panel>        
        <asp:Panel ID="pnlSubfamilias" runat="server" Width="1020">
        </asp:Panel> 
        
        <asp:Panel ID="pnlTitProductos" runat="server">
            <table>
                <tr>
                    <td class="etiqueta"><asp:Label ID="lbSurtido" runat="server" Text="SURTIDO:" CssClass="textoEtiqueta"></asp:Label></td>                    
                </tr>
            </table>                        
        </asp:Panel>        
        <asp:Panel ID="pnlProductos" runat="server" Width="1020">
        </asp:Panel>       
    </asp:Panel>
    
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
    
</asp:Content>
