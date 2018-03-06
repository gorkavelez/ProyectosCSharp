<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EtiquetaPlegado.aspx.cs" Inherits="IntranetINDUSAL.Reports.EtiquetaPlegado" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="../Etiquetas.css" rel="Stylesheet" type="text/css" media="print"/>
    <link href="../Etiquetas.css" rel="Stylesheet" type="text/css" media="screen"/>    
    
    <script language="vbscript" type="text/vbcript"> 
        SUB Print()
         
            OLECMDID_PRINT = 6 
            OLECMDEXECOPT_DONTPROMPTUSER = 2 
            OLECMDEXECOPT_PROMPTUSER = 1 
            'ACA en caso de usar frames, 
            'enfocamos el frame a imprimir: 

            'window.parent.frames.main.document.body.focus() 
            window.document.body.focus() 

            'Llamamos al comando de Impresión Print 

            on error resume next 
            call IEWB.ExecWB (OLECMDID_PRINT, -1) 

            if err.number <> 0 then 
                alert "No se pudo imprimir" 
            end if 

        END SUB
    </script>
    
    <script language="javascript" type="text/javascript">
        function Imprimir() {
            window.print();            
            window.close();           
        }
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width: 110mm; border-style:solid; border-color:Black; border-width:thin;" cellspacing="0">
            <tr style="height:35mm;">
                <td style="width: 100%;">
                    <table style="width: 100%; height:100%" cellspacing="0">
                        <tr style=" height:20mm ;">
                            <td style="width: 55mm; border-right-style:solid; border-right-color:Black; border-right-width:thin; vertical-align:top;">
                                <asp:Label ID="lbClienteLinea1" runat="server" Text="HOTEL RESTAURANTE VILLA MARCILLA" CssClass="nombre"></asp:Label>                                
                            </td>
                            <td style="width: 55mm; vertical-align:top;">
                                <asp:Label ID="lbPedido" runat="server" Text="PV09-02-00116" CssClass="nombre"></asp:Label>
                            </td>                            
                        </tr>
                        <tr style=" height:15mm ;">
                            <td style="width: 55mm; border-right-style:solid; border-right-color:Black; border-right-width:thin; text-align:center;">
                                <asp:Label ID="lbCodClienteBarras" runat="server" Text="*000564*" CssClass="codigoBarras"></asp:Label>
                            </td>
                            <td style="width: 55mm; text-align:center;">
                                <asp:Label ID="lbNumPedidoBarras" runat="server" Text="*PV09-02-00116*" CssClass="codigoBarras"></asp:Label>
                            </td>                            
                        </tr>                        
                    </table>
                </td>                
            </tr>
            <tr style="height:35mm;">
                <td style="width: 100%; border-top-style:solid; border-top-color:Black; border-top-width:thin; vertical-align:top; text-align:center;">
                    <br />
                    <asp:Label ID="lbNomProducto" runat="server" Text="MANTEL BLANCO 140x140" CssClass="nombre"></asp:Label> 
                    <br />
                    <br />
                    <asp:Label ID="lbcodProductoBarras" runat="server" Text="*PMB00067*" CssClass="codigoBarras"></asp:Label> 
                </td>                
            </tr>
        </table>            
        <table>
            <tr>                
                <td>
                    <asp:Button ID="btImprimir" runat="server" Text="IMPRIMIR" CssClass="boton" OnClientClick="Imprimir()"/>
                </td>
            </tr>
        </table>
    </div>
    <object id="IEWB" width="0" height="0" classid="clsid:01398-997-0000-C000-000000000046"></object>
    <asp:HiddenField ID="hdUrlPrevia" runat="server" />
    </form>
</body>
</html>
