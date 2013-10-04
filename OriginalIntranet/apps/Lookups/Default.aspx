<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="it_Lookups_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titleContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContent" Runat="Server">
    <div>
        <asp:Table ID="Table1" runat="server" Width="100%">
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Right" Width="33%">
                    <asp:ListBox Rows="15" ID="lstLookupLists" AutoPostBack="true" OnSelectedIndexChanged="lstLookupLists_Change" runat="server" />
                </asp:TableCell>
                <asp:TableCell Width="33%">
                    <asp:ListBox Rows="15" ID="lstLookupListItems" AutoPostBack="true" OnSelectedIndexChanged="lstLookupItems_Change" runat="server" />
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Left" Width="34%">                    
                    <asp:ListBox Rows="15" ID="lstLookupListItems2" AutoPostBack="true" OnSelectedIndexChanged="lstLookupItems2_Change" runat="server" />
                </asp:TableCell>               
            </asp:TableRow>
        </asp:Table>
    </div>
</asp:Content>

