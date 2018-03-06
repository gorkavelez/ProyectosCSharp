<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PruebaBascula.aspx.cs" Inherits="IntranetINDUSAL.Automatizacion.PruebaBascula" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="../IntranetINDUSAL.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%;">
            <tr>
                <td class="etiqueta">
                    <asp:Button ID="btPeso" runat="server" Text="PESO" CssClass="boton" 
                        onclick="btPeso_Click" />
                </td>
                <td class="descripcion">
                    <asp:TextBox ID="txPeso" runat="server" CssClass="codigo"></asp:TextBox>
                </td>                
            </tr>            
        </table>
        <asp:Label ID="lbError" runat="server" Text="" CssClass="codigo" ForeColor="Red"></asp:Label>
    </div>
    </form>
</body>
</html>
