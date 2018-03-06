<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Surtido.aspx.cs" Inherits="ExtranetSubcontratacion.Surtido" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Telerik.Web.UI, Version=2009.1.527.35, Culture=neutral, PublicKeyToken=121fae78165ba3d4"
    Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
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
        <asp:Panel ID="panelSeleccionSurtido" runat="server">            
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
                        <asp:DropDownList ID="ddlFamilias" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="ddlFamilias_SelectedIndexChanged">
                        </asp:DropDownList>                                                    
                    </td>                    
                    <td>
                        <asp:DropDownList ID="ddlSubfamilias" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="ddlSubfamilias_SelectedIndexChanged">
                        </asp:DropDownList>                       
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
        <asp:GridView ID="gridSurtido" runat="server" BackColor="White" 
            BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
            GridLines="Vertical">
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="#DCDCDC" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
