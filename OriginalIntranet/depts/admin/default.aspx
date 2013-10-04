<%@ Page Title="Admin Dept Page" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="admin_default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titleContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContent" Runat="Server">
 <%--LINKS--%>
    <div class="links">
        <div class="linkhead">Links</div>  
        <asp:HyperLink ID="hlHelpdesk" runat="server" NavigateUrl="https://member.aetna.com/appConfig/login/login.fcc">Health Benefits Portal</asp:HyperLink>
    </div>
</asp:Content>

