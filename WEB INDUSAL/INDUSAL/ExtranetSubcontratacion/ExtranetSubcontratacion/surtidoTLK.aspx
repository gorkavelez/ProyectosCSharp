<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="surtidoTLK.aspx.cs" Inherits="ExtranetSubcontratacion.surtidoTLK" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="ExtranetSubcontr.css" rel="stylesheet" type="text/css" />
    <script src="ScriptsSubcontratacion.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:Panel ID="Panel1" runat="server">     
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lbFamilia" runat="server" Text="FAMILIA:" CssClass="texto_titulo"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lbSubfamilias" runat="server" Text="SUBFAMILIA:" CssClass="texto_titulo"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadComboBox ID="ddlFamilias" runat="server" 
                            onselectedindexchanged="ddlFamilias_SelectedIndexChanged" 
                            AutoPostBack="True">
                        </telerik:RadComboBox>                                                   
                    </td>                    
                    <td>
                        <telerik:RadComboBox ID="ddlSubfamilias" runat="server" 
                            onselectedindexchanged="ddlSubfamilias_SelectedIndexChanged" 
                            AutoPostBack="True">
                        </telerik:RadComboBox>                       
                    </td>                                   
                    <td>
                        <asp:LinkButton ID="btAceptar" runat="server" onclick="btAceptar_Click" CssClass="texto_link">Aceptar</asp:LinkButton>        
                    </td>
                    <td></td>
                    <td>
                        <asp:LinkButton ID="btCancelar" runat="server" CssClass="texto_link" 
                            onclick="btCancelar_Click">Cancelar</asp:LinkButton>        
                    </td>
                </tr>
            </table>
        </asp:Panel>       
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Height="100%" Width="100%">        
        <telerik:RadGrid ID="gridSurtido" runat="server" GridLines="None" >                
            <MasterTableView>
                <RowIndicatorColumn>
                    <HeaderStyle Width="20px" />
                </RowIndicatorColumn>
                <ExpandCollapseColumn>
                    <HeaderStyle Width="20px" />
                </ExpandCollapseColumn>
            </MasterTableView>
        </telerik:RadGrid>
        </telerik:RadAjaxPanel>
    </div>
    </form>
</body>
</html>
