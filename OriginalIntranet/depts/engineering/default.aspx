<%@ Page Title="Engineering Dept Page" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="engineering_default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titleContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContent" Runat="Server">
 <%--LINKS--%>
    <div class="links">
        <div class="linkhead">Links</div>  
        <asp:HyperLink ID="hlHelpdesk" runat="server" NavigateUrl="http://veritas15/ReportManager/Pages/Report.aspx?ItemPath=%2fEngineering%2fEstimate+Queue+-+Engineering+Dept+Planning">Estimate Report</asp:HyperLink>
    </div>
</asp:Content>

