<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="GestSport.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Login</title>
    <link href="Estilos/EstilosGen.css" rel="stylesheet" type="text/css" />
    <style type="text/css"></style>
</head>
<body>    
    <section class="container">
    <div class="login">
        <h1>Login GestSport</h1>
      <form id="form1" runat="server">        
        <table id="tablaLogin" runat="server">
        <tr><td><asp:Label ID="LblUsuario" runat="server" Text="Usuario"></asp:Label></td>
        <td><asp:TextBox ID="txtUsuario" runat="server" placeholder="Usuario"></asp:TextBox></td></tr>        
        <tr><td><asp:Label ID="LblPassword" runat="server" Text="Password"></asp:Label></td>
        <td><asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Password"></asp:TextBox></td></tr>
        <tr><td><asp:Button ID="btnLogin" runat="server" Text="Login" onclick="btnLogin_Click"/></td></tr>
        </table>    
        <asp:Label ID="lblEstado" runat="server" Text="" CssClass="Error"></asp:Label>   
        
      </form>
    </div>
    <div class="login-help">    
      <p>
      <a href="UsuarioNuevo.aspx" >Registrarse</a>
      nbsp+nbsp+nbsp+nbsp
      <a href="EnvioMailPass.aspx">Olvidó su contraseña</a>.</p>
    </div>
  </section>
  
</body>
</html>
