<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EnvioMailPass.aspx.cs" Inherits="GestSport.EnvioMailPass" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Envío mail password</title>    
    <link href="Estilos/EstilosGen.css" rel="stylesheet" type="text/css" />
    <style type="text/css"></style>
</head>
<body>    
    
    <div class="Reset">        
    <form id="form1" runat="server">
    <table id ="tablaTitulo">
    <tr>
        <td><h2>Envío mail reset password</h2></td>
        <td style="padding-left:60px"><asp:ImageButton ID="btnInicio" runat="server" ImageUrl="~/Img/InicioBtn.jpg" 
                CssClass="Imagen" onclick="btnInicio_Click" ToolTip="Volver Login"/></td>
    </tr>    
    </table>    
    <table id="tPrincipal" runat="server">
    <tr><td><asp:Label id="lblEmail" runat="server" Text="Dirección correo">  </asp:Label>
    <asp:TextBox ID="txtEmail" runat="server" ontextchanged="txtEmail_TextChanged"></asp:TextBox></td>
    <td>
    <asp:RegularExpressionValidator ID="RexpValidator" runat="server" 
         ControlToValidate="txtEmail"
         ErrorMessage="Introduzca un eMail válido" 
         ForeColor="Red"  Font-Bold="true"             
         ValidationExpression="^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$" 
         ValidationGroup="Enviar">
    </asp:RegularExpressionValidator>
    </td>
    </tr>
    </table>
    <p><asp:Button ID="btnEnviar" runat="server" Text="enviar" 
            onclick="btnEnviar_Click"/></p>
    <p><asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label></p>
    <p><asp:Label ID="lblEnvioCorreo" runat="server" CssClass="EnvioMail"></asp:Label></p>
    
    </form>    
    </div>    
</body>
</html>
