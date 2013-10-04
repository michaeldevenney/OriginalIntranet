<%@ Page Title="IT Dept Page" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="it_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titleContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContent" Runat="Server">
    <%--LINKS--%>
    <div class=test">
        
    </div>
    <div class="links">
        <div class="linkhead">Links</div>
        <asp:HyperLink ID="hlHelpdesk" runat="server" NavigateUrl="http://VERITAS15:9675/portal">Helpdesk</asp:HyperLink><br />
        <asp:HyperLink ID="hlReports" runat="server" NavigateUrl="http://veritas15/ReportManager/Pages/Folder.aspx">Report Server</asp:HyperLink><br />       
    </div>
</asp:Content>

