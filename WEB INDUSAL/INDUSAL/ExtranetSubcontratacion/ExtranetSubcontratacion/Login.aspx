<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ExtranetSubcontratacion.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="ExtranetSubcontr.css" rel="stylesheet" type="text/css" />
    <script src="ScriptsSubcontratacion.js" type="text/javascript"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="panelLogin" runat="server" CssClass="panel_login">
            <asp:Panel ID="panelTituloLogin" runat="server" CssClass="title_panel_login" >
                <asp:Label ID="lbTitulo" runat="server" Text="INICIO DE SESION"></asp:Label>
            </asp:Panel>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lbUsuario" runat="server" Text="Usuario login:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txUsuario" runat="server" ontextchanged="txLogin_TextChanged"></asp:TextBox>
                    </td>                    
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbPassword" runat="server" Text="Contraseña:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txPassword" runat="server" ontextchanged="txLogin_TextChanged" 
                            TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <asp:Label ID="lbErrorLogin" runat="server" Text="" ForeColor="Red" Font-Bold="true" Visible="false"></asp:Label>
            <asp:Panel ID="panelPieLogin" runat="server" CssClass="footer_panel_Login">
                <asp:Button ID="btLogin" runat="server" Text="INICIAR SESION" 
                onclick="btLogin_Click" Font-Bold="True" />
            </asp:Panel>
            
        </asp:Panel>
        <br />
    </div>
    </form>
</body>
</html>
