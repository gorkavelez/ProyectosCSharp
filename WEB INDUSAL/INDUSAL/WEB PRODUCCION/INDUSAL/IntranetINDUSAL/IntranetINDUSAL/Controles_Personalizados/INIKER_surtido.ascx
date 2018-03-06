<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="INIKER_surtido.ascx.cs" Inherits="IntranetINDUSAL.Controles_Personalizados.INIKER_surtido" %>


<asp:Panel ID="panelSeleccion" runat="server">
    <table>
        <tr>
            <td class="etiqueta">                
                <asp:Button ID="btFamilia" runat="server" Text="FAMILIA" CssClass="boton" OnClick="btFamilia_Click" />
            </td>
            <td>                
                <asp:Label ID="lbFamilia" runat="server" Text="" CssClass="codigo"></asp:Label>
            </td>            
        </tr>
        <tr>
            <td class="etiqueta">
                <asp:Button ID="btSubfamilia" runat="server" Text="SUBFAMILIA" CssClass="boton" OnClick="btSubfamilia_Click" />                
            </td>
            <td>
                <asp:Label ID="lbSubfamilia" runat="server" Text="" CssClass="codigo"></asp:Label>
            </td>               
        </tr>        
    </table>
    <br />
    <asp:Label ID="lbTitSeleccion" runat="server" Text="" Style="font-size:medium; font-weight:bold; color:Red;"></asp:Label>    
</asp:Panel>
<br />
<asp:Panel ID="panelSurtido" runat="server">

</asp:Panel>
