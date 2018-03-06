<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="INIKER_tecladoNumerico.ascx.cs" Inherits="IntranetINDUSAL.Controles_Personalizados.INIKER_tecladoNumerico" %>
   
    <style type="text/css">        
        .etiquetaTeclado
        {            
            font-family:Calibri;
            font-size: Large;
            font-weight:bold;            
        }
        .etiquetaIntro
        {
            height: 12mm;
            width: 60mm;
            font-family:Calibri;
            font-size: Large;
            font-weight:bold;                                                
        }
        .txIntroTeclado
        {
        	width: 60mm;        	
        	font-size: x-Large;
        	font-weight:bold;
        	text-align: right;
        }
       .botonTeclado
        {
        	height: 20mm;
        	width: 20mm;
        	font-family:Calibri;
        	font-size: x-large;
        }
        
        .botonTecladoPeq
        {
            margin: 0px;
            height: 80px;
            width: 100px;
            font-family: Calibri;
            font-size: medium;
            font-weight: bold;
        }
        .botonOK
        {
        	height: 20mm;
        	width: 41mm;
        	font-family:Calibri;
        	font-size: x-large;
        }
    </style>

    <script language="javascript" type="text/javascript">
        <!--
        function CapturarClickTeclado(caracter) {
            var ctl = document.getElementById("ctl00_ContentPlaceHolder1_INIKER_teclado_txValorTeclado");
            var hdn = document.getElementById("ctl00_ContentPlaceHolder1_INIKER_teclado_hdnValue");            
            var valor = ctl.value;
            
            if (caracter == '#') {
                valor = '';
            }
            else {
                if (caracter == '*') {
                    valor = valor.slice(0, valor.length - 1);
                }
                else {
                    valor += caracter;
                }
            }
            
            ctl.value = valor;
            hdn.value = valor;            
            return false;
        }
        //-->
    </script>    
    
    <table>
        <tr>                                    
            <td>
                <table style="width:100%;">
                    <tr>
                        <td>
                            <asp:Label ID="lbDatoIntro" runat="server" Text="VALOR:" 
                                onprerender="lbDatoIntro_PreRender" CssClass="etiquetaTeclado"></asp:Label>
                            <input id="hdnValue" type="hidden" runat="server" />                            
                        </td>
                    </tr>
                    <tr>
                        <td class="etiquetaIntro">
                            <asp:TextBox ID="txValorTeclado" runat="server" CssClass="txIntroTeclado" 
                                Enabled="False" onprerender="txValorTeclado_PreRender"></asp:TextBox>                            
                        </td>
                    </tr>
                </table>
                
                
            </td>
        </tr>
        <tr>
            <td style="padding:0;margin:0;">
                <asp:Button ID="btSiete" runat="server" Text="7" CssClass="botonTeclado" OnClientClick="return CapturarClickTeclado('7');"/>
                <asp:Button ID="btOcho" runat="server" Text="8" CssClass="botonTeclado" OnClientClick="return CapturarClickTeclado('8');"/>
                <asp:Button ID="btNueve" runat="server" Text="9" CssClass="botonTeclado" OnClientClick="return CapturarClickTeclado('9');"/>
            </td>
        </tr>
        <tr>
            <td style="padding:0;margin:0;">                                
                <asp:Button ID="btCuatro" runat="server" Text="4" CssClass="botonTeclado" OnClientClick="return CapturarClickTeclado('4');"/>
                <asp:Button ID="btCinco" runat="server" Text="5" CssClass="botonTeclado" OnClientClick="return CapturarClickTeclado('5');"/>
                <asp:Button ID="btSeis" runat="server" Text="6" CssClass="botonTeclado" OnClientClick="return CapturarClickTeclado('6');"/>
            </td>
        </tr>
        <tr>
            <td style="padding:0;margin:0;">                                
                <asp:Button ID="btUno" runat="server" Text="1" CssClass="botonTeclado" OnClientClick="return CapturarClickTeclado('1');"/>
                <asp:Button ID="btDos" runat="server" Text="2" CssClass="botonTeclado" OnClientClick="return CapturarClickTeclado('2');"/>
                <asp:Button ID="btTres" runat="server" Text="3" CssClass="botonTeclado" OnClientClick="return CapturarClickTeclado('3');"/>
            </td>
        </tr>
        <tr>
            <td style="padding:0;margin:0;">                               
                <asp:Button ID="btCero" runat="server" Text="0" CssClass="botonTeclado" OnClientClick="return CapturarClickTeclado('0');"/>                                
                <asp:Button ID="btSupr" runat="server" Text="SUP" CssClass="botonTeclado" OnClientClick="return CapturarClickTeclado('*');"/>                
                <asp:Button ID="btClr" runat="server" Text="CLR" CssClass="botonTeclado" OnClientClick="return CapturarClickTeclado('#');"/>
            </td>
        </tr>        
        <tr>
            <td style="padding:0;margin:0;">
            <asp:Button ID="btSepDec" runat="server" Text="." CssClass="botonTeclado" OnClientClick="return CapturarClickTeclado('.');"/>
                <asp:Button ID="btOK" runat="server" Text="OK" CssClass="botonOK" 
                    onclick="OK_Click"/>
            </td>
        </tr>        
    </table>
