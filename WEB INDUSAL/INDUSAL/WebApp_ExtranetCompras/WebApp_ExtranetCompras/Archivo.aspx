<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Archivo.aspx.cs" Inherits="WebApp_ExtranetCompras._Archivo" %>

<%@ Register assembly="Telerik.Web.UI, Version=2009.1.527.35, Culture=neutral, PublicKeyToken=121fae78165ba3d4" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="ExtranetCompras.css" rel="Stylesheet" type="text/css" />
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 828px;
        }
        .style2
        {
            width: 124px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div> 
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table>
            <tr>
                <td class="style1">
                    <table style="width:100%;">
                        <tr>
                            <td class="style2"><asp:Label ID="lbTitVersiones" runat="server" Text="VERSIONES:"                        
                                    CssClass="label"></asp:Label>
                                
                            </td>
                            <td>
                                <telerik:RadComboBox ID="rcbVersiones" runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="rcbVersiones_SelectedIndexChanged">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2"><asp:Label ID="lbTitPortes" runat="server" Text="PORTES:"                        
                                    CssClass="label"></asp:Label></td>
                            <td>
                                <asp:Label ID="lbPortes" runat="server" CssClass="dato"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2"><asp:Label ID="lbTitTipoRappel" runat="server" Text="TIPO DE RAPPEL:" 
                                    CssClass="label"></asp:Label></td>
                            <td>
                                <asp:Label ID="lbTipoRappel" runat="server" CssClass="dato"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2"><asp:Label ID="lbTitPorcenRappel" runat="server" Text="% RAPPEL:" 
                                    CssClass="label"></asp:Label></td>
                            <td>
                                <asp:Label ID="lbPorcenRappel" runat="server" CssClass="dato"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2"><asp:Label ID="lbTitFPago" runat="server" Text="FORMA DE PAGO:" 
                                    CssClass="label"></asp:Label></td>
                            <td>
                                <asp:Label ID="lbFPago" runat="server" CssClass="dato"></asp:Label>                                
                            </td>
                        </tr>
                        <tr>
                            <td class="style2"><asp:Label ID="lbTitComentario" runat="server" Text="COMENTARIO:" 
                                    CssClass="label"></asp:Label></td>
                            <td>
                                <asp:Label ID="lbComentario" runat="server" CssClass="dato"></asp:Label>
                            </td>
                        </tr>            
                    </table>
                </td>
                <td style="vertical-align:bottom;">
                    <telerik:RadToolBar ID="RadToolBar1" runat="server" 
                        onbuttonclick="RadToolBar1_ButtonClick">
                        <Items>
                            <telerik:RadToolBarButton runat="server" Text="Restaurar ...">
                            </telerik:RadToolBarButton>
                            <telerik:RadToolBarButton runat="server" 
                                Text="Volver">
                            </telerik:RadToolBarButton>
                        </Items>
                    </telerik:RadToolBar>
                </td>
            </tr>
        </table>
        <br />
        <telerik:RadGrid ID="rg_QuoteLines" runat="server" GridLines="None">
        </telerik:RadGrid>
    
    </div>
    </form>
</body>
</html>
