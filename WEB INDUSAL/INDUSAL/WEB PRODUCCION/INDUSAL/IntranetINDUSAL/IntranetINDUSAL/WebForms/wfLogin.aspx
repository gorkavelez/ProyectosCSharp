<%@ Page Language="C#" AutoEventWireup="true" CodeFile="wfLogin.aspx.cs" Inherits="wfLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 159px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    <asp:Panel ID="Panel1" runat="server" BackColor="#F7F7DE" BorderColor="#CCCC99" 
        BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="10pt" 
        Height="130px" Width="306px">
        <table style="width:100%; height: 97px;">
            <tr>
                <td align="center" 
                    style="color: White; background-color: #6B696B; font-weight: bold;">
                    Iniciar sesión</td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td class="style1">
                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="txUserName">Nombre de usuario:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txUserName" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="txPassword">Contraseña:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txPassword" runat="server" TextMode="Password" Width="125px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="lbErrorLogin" runat="server" Font-Bold="False" 
                        Font-Names="Calibri" Font-Size="10pt" ForeColor="Red" 
                        Text="El usuario y password no son correctos" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Button ID="btLogin" runat="server" onclick="btLogin_Click" 
                        Text="Inicio de sesión" Width="131px" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    </form>
</body>
</html>
