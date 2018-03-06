<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pruebaTeclado.aspx.cs" Inherits="IntranetINDUSAL.Pruebas.pruebaTeclado" %>
<%@ Register src="../Controles_Personalizados/INIKER_tecladoNumerico.ascx" tagname="INIKER_tecladoNumerico" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <uc1:INIKER_tecladoNumerico ID="INIKER_tecladoNumerico1" runat="server" 
            Dato="1450" TituloDato="CANTIDAD:" />
    
        <asp:TextBox ID="TextBox1" runat="server" AutoPostBack="True" 
            ontextchanged="TextBox1_TextChanged"></asp:TextBox>
    
    </div>
    </form>
</body>
</html>
