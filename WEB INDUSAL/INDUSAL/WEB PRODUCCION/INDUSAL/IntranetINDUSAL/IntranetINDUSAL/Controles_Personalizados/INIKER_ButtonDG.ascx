<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="INIKER_ButtonDG.ascx.cs" Inherits="IntranetINDUSAL.INIKER_ButtonDG" %>
    
    <style type="text/css">
        .botonGrid
        {
        	height: 40px;
            width: 100%;            
        }
    </style>
    
    <asp:GridView ID="grid" runat="server" CellPadding="4" ForeColor="#333333" 
        GridLines="None" Height="100%" Width="100%">
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#999999" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    </asp:GridView>

    