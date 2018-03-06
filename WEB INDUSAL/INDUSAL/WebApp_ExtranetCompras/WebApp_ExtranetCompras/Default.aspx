<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApp_ExtranetCompras._Default" %>

<%@ Register assembly="Telerik.Web.UI, Version=2009.1.527.35, Culture=neutral, PublicKeyToken=121fae78165ba3d4" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="ExtranetCompras.css" rel="Stylesheet" type="text/css" />
    <script src="Scripts.js" type="text/javascript"></script>
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 761px;
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
                            <td><asp:Label ID="lbTitPortes" runat="server" Text="PORTES:"                        
                                    CssClass="label"></asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlPortes" runat="server" 
                                    onselectedindexchanged="ddlPortes_SelectedIndexChanged">
                                </asp:DropDownList>                                
                            </td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lbTitTipoRappel" runat="server" Text="TIPO DE RAPPEL:" 
                                    CssClass="label"></asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlTipoRappel" runat="server" 
                                    onselectedindexchanged="ddlTipoRappel_SelectedIndexChanged">
                                </asp:DropDownList>                                
                            </td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lbTitPorcenRappel" runat="server" Text="% RAPPEL:" 
                                    CssClass="label"></asp:Label></td>
                            <td>
                                <telerik:RadNumericTextBox ID="rntxPorcenRappel" runat="server" 
                                    Culture="Spanish (Spain)" Type="Percent" Width="125px" 
                                    ontextchanged="rntxPorcenRappel_TextChanged">
                                </telerik:RadNumericTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lbTitFPago" runat="server" Text="FORMA DE PAGO:" 
                                    CssClass="label"></asp:Label></td>
                            <td>
                                <asp:DropDownList ID="ddlFPago" runat="server" 
                                    onselectedindexchanged="ddlFPago_SelectedIndexChanged">
                                </asp:DropDownList>                                
                            </td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lbTitComentario" runat="server" Text="COMENTARIO:" 
                                    CssClass="label"></asp:Label></td>
                            <td>
                                <telerik:RadTextBox ID="rtxComentario" runat="server" MaxLength="250" 
                                    ontextchanged="rtxComentario_TextChanged" Width="100%" AutoPostBack="True" 
                                    Rows="1">
                                </telerik:RadTextBox>
                            </td>
                        </tr>            
                    </table>
                </td>
                <td style="vertical-align:bottom;">
                    <telerik:RadToolBar ID="RadToolBar1" runat="server" 
                        onbuttonclick="RadToolBar1_ButtonClick">
                        <Items>
                            <telerik:RadToolBarButton runat="server" 
                                Text="Guardar">
                            </telerik:RadToolBarButton>
                            <telerik:RadToolBarButton runat="server" 
                                Text="Archivar">
                            </telerik:RadToolBarButton>
                            <telerik:RadToolBarButton runat="server" 
                                Text="Deshacer">
                            </telerik:RadToolBarButton>
                            <telerik:RadToolBarButton runat="server" Text="Versiones">
                            </telerik:RadToolBarButton>
                        </Items>
                    </telerik:RadToolBar>
                </td>
            </tr>
        </table>        
        <br />   
        <telerik:RadGrid ID="rg_QuoteLines" runat="server" GridLines="None">
        </telerik:RadGrid>
        <asp:HiddenField ID="hfFocusID" runat="server" />
    </div>
    </form>
</body>
</html>
