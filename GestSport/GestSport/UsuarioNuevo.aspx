<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UsuarioNuevo.aspx.cs" Inherits="GestSport.UsuarioNuevo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Registro usuario</title>
    <link href="Estilos/EstilosGen.css" rel="Stylesheet" type="text/css" />
    <style type="text/css"></style>
</head>
<body>    
    <div class="loginNuevoUsuario">   
    
        <form id="formUsuarioNuevo" runat="server">
        <table id ="tblTitulo">
            <tr><td><h1>Usuario Nuevo <asp:Label ID="lblTitulo" runat="server"></asp:Label></h1></td>
            <td><asp:ImageButton ID="btnInicio" runat="server" ImageUrl="~/Img/InicioBtn.jpg" 
                    CssClass="Imagen" ToolTip="Volver Inicio" onclick="btnInicio_Click" /></td>       
            </tr>
        </table>
    
        <table id="prinicpal" runat="server">
         <tr>
            <td><asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label></td>
            <td style="padding-left:40px"><asp:TextBox ID="txtEmail" runat="server" 
                 ontextchanged="txtEmail_TextChanged"></asp:TextBox></td>            
            <td>
            <asp:RegularExpressionValidator ID="RexpValidatorNewUser" runat="server" 
                ControlToValidate="txtEmail"
                ErrorMessage="Introduzca un eMail válido" 
                ForeColor="Red"  Font-Bold="true"             
                ValidationExpression="^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$" 
                ValidationGroup="Enviar">
            </asp:RegularExpressionValidator>            
            </td>
         </tr>
         
         <tr>
         <td><asp:Label ID="lblUsuario" runat="server" Text="Usuario"></asp:Label></td>
         <td style="padding-left:40px"><asp:TextBox ID="txtUsuario" runat="server"></asp:TextBox></td>
         </tr>
         
         <tr>
            <td ><asp:Label ID="lblNombre" runat="server" Text="Nombre"></asp:Label></td>
            <td style="padding-left:40px"><asp:TextBox ID="txtNombre" runat="server" 
                    ontextchanged="txtNombre_TextChanged"></asp:TextBox></td>            
         </tr>
            
         <tr>
            <td><asp:Label ID="lblApellido" runat="server" Text="Apellido"></asp:Label></td>
            <td style="padding-left:40px"><asp:TextBox ID="txtApellido" runat="server"></asp:TextBox></td>            
         </tr>
         
         <tr>
            <td><asp:Label ID="LblApellido2" runat="server" Text="Apellido2"></asp:Label></td>
            <td style="padding-left:40px"><asp:TextBox ID="txtApellido2" runat="server"></asp:TextBox></td>            
         </tr>
         
         <tr>
            <td><asp:Label ID="lblDirec" runat="server" Text="Dirección"></asp:Label></td>
            <td style="padding-left:40px"><asp:TextBox ID="txtDireccion" runat="server"></asp:TextBox></td>            
         </tr>
         
        </table>    
        <p></p>
        <h3>Password</h3>
        <table id="tblPassword" runat="server">        
        <tr>
        <td><asp:Label id="lblPassWord" runat="server" Text="Contraseña"></asp:Label></td>
        <td style="padding-left:40px"><asp:TextBox ID="txtPassWord" runat="server" TextMode="Password"
                ToolTip="Mínimo 8 caracteres y al menos 1 número" ></asp:TextBox></td>
        </tr>        
        <tr>
        <td><asp:Label id="lblConfirmPass" runat="server" Text="Confirmar Contraseña"></asp:Label></td>
        <td style="padding-left:40px"><asp:TextBox ID="txtConfirmPass" runat="server" TextMode="Password"></asp:TextBox></td>
        </tr>
        <tr><td style="padding-top:20px"><asp:Button ID="btnCrearUsuario" runat="server" Text="Aceptar" 
            onclick="btnCrearUsuario_Click" /></td></tr>
        </table>
        <p></p>
        <asp:RegularExpressionValidator ID="RegExpPassWord" runat="server" 
                ControlToValidate="txtPassWord"
                ErrorMessage="Contraseña incorrecta"                 
                ForeColor="Red"  Font-Bold="true"             
                ValidationExpression="(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,12})$" 
                ValidationGroup="Enviar">
            </asp:RegularExpressionValidator>   
        
        <p></p>
        
        <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
        
        </form>
    </div>    
</body>
</html>
