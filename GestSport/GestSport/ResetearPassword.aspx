<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetearPassword.aspx.cs" Inherits="GestSport.ResetearPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Reset password</title>
     <link href="Estilos/EstilosGen.css" rel="stylesheet" type="text/css" />
    <style type="text/css"></style>
</head>
<body>
    
    <section class="container">
    <div class="Reset">
    
        <h1>Reset pasword</h1>
      <form id="form2" runat="server">              
        <table id="tablaLogin" runat="server">
        <tr><td><asp:Label ID="LblUsuario" runat="server" Text="Usuario"></asp:Label></td>
        <td><asp:TextBox ID="txtUsuario" runat="server" placeholder="Usuario" ></asp:TextBox></td></tr>        
        
        <tr><td><asp:Label ID="lblPasswordNew" runat="server" Text="Password Nuevo"></asp:Label></td>
        <td><asp:TextBox ID="txtPasswordNew" runat="server" TextMode="Password" 
                placeholder="Password" ontextchanged="txtPasswordNew_TextChanged"></asp:TextBox></td></tr>                
        <tr><td><asp:Label ID="lblConfirm" runat="server" Text="Confirmación"></asp:Label></td>                
        <td><asp:TextBox ID="txtPassConfirm" runat="server" TextMode="Password" 
        placeholder="Password"></asp:TextBox></td></tr>                
        <tr><td><p><asp:Button ID="btnActualizarPass" runat="server" Text="Actualizar" onclick="btnLogin_Click"/></p></td></tr>              
        </table>    
        <p><asp:Label ID="lblEstado" runat="server" Text="" CssClass="Error"></asp:Label></p>
        
        <asp:RegularExpressionValidator ID="RGpassword" runat="server" 
             ControlToValidate="txtPasswordNew"
             ErrorMessage="La clave debe ser como minimo de 8 digitos y al menos un número y letra mayúscula" 
             ForeColor="Red"  Font-Bold="true"             
             ValidationExpression="(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$" 
             ValidationGroup="Enviar">
        </asp:RegularExpressionValidator>
      </form>
    </div>    
  </section>
    
    
    
</body>
</html>
