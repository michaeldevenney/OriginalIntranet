<%@ Page Title="Project Details" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="apps_projects_Default" %>

<%--<%@ Register TagPrefix="ver" TagName="projectFilter" Src="~/_controls/projectFilter.ascx" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="titleContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headContent" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContent" runat="Server">
    <%--New Lead/Project Entry Form--%>
    <div id="fuzz1">
        <div class="newLeadProj">
            <div class="heading">
                <asp:Label ID="lblHeading" runat="server" Text="Enter New Lead/Project"></asp:Label>
                <img style="height: 20px; width: 20px;" alt="Close" class="close1" src="../../images/error.png" />
            </div>
            <asp:Table ID="Table2" runat="server" Width="100%">
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="Label7" runat="server" Text="This is a new "></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:DropDownList ID="ddlProjectLead" runat="server">
                            <asp:ListItem Text="Lead" Value="Lead" />
                            <asp:ListItem Text="Project" Value="Project" />
                        </asp:DropDownList>
                    </asp:TableCell>
                    <asp:TableCell></asp:TableCell>
                    <asp:TableCell>Region</asp:TableCell>
                    <asp:TableCell>
                        <asp:DropDownList ID="ddlRegionNew" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="Label1" runat="server" Text="Project/Lead Number"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="txtAddProjNumber" runat="server"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell></asp:TableCell>
                    <asp:TableCell>
                        <asp:Label ID="Label2" runat="server" Text="Name"></asp:Label></asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="txtAddProjName" runat="server" Width="250px"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="Label3" runat="server" Text="Activity"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:DropDownList ID="ddlAddProjActivity" runat="server" DataSource=' <%# DAL.Lookup.GetLookupList("Project Activity") %>'
                            DataTextField="LookupValue" DataValueField="LookupValue" />
                    </asp:TableCell>
                    <asp:TableCell></asp:TableCell>
                    <asp:TableCell>
                        <asp:Label ID="Label4" runat="server" Text="Location"></asp:Label></asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="txtAddProjLocation" runat="server"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="Label6" runat="server" Text="Assigned PM"></asp:Label></asp:TableCell>
                    <asp:TableCell>
                        <asp:DropDownList ID="ddlAddProjMgr" runat="server" DataSource=' <%# DAL.User.GetUsersByTitle("Project Manager") %>'
                            DataTextField="Name" DataValueField="ID" />
                    </asp:TableCell>
                    <asp:TableCell></asp:TableCell>
                    <asp:TableCell></asp:TableCell>
                    <asp:TableCell></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="5" HorizontalAlign="Center">
                        <asp:Button ID="cmdAddProjLead" runat="server" Text="Create Lead/Project" OnClick="cmdAddProjLead_Click" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </div>
    </div>
    <%--Dropdown Filter--%>
    <div style="position: relative;">
        <%--<ver:projectFilter runat="server" />--%>
        <asp:Table ID="Table3" runat="server">
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="left">
                    <asp:Label ID="lblProjectLead" runat="server" Text="Select Project:" />
                    <asp:DropDownList ID="ddlProject" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProject_SelectedIndexChanged" />
                </asp:TableCell><asp:TableCell HorizontalAlign="Left">
                    <asp:RadioButtonList RepeatDirection="Horizontal" ID="rdoLeadProject" runat="server"
                        AutoPostBack="true" OnSelectedIndexChanged="rdoLeadProject_SelectedIndexChanged">
                        <asp:ListItem Selected="true" Text="Show Projects" Value="Projects" />
                        <asp:ListItem Text="Show Leads" Value="Leads" />
                    </asp:RadioButtonList>
                    <asp:RadioButtonList RepeatDirection="Horizontal" ID="rdoActiveAll" runat="server"
                        AutoPostBack="true" OnSelectedIndexChanged="rdoActiveAll_SelectedIndexChanged">
                        <asp:ListItem Selected="true" Text="Just Active" Value="Active" />
                        <asp:ListItem Text="Show All" Value="All" />
                    </asp:RadioButtonList>
                </asp:TableCell></asp:TableRow>
        </asp:Table>
    </div>
    <hr />
    &nbsp;&nbsp;Reports (click to run):&nbsp;&nbsp;
    <asp:HyperLink ID="hlActiveReports" NavigateUrl="http://veritas15/reports?/Project Management/Active Projects&rs:Command=Render&rs:Format=PDF"
        runat="server">Active Projects</asp:HyperLink>&nbsp;&nbsp;|&nbsp;&nbsp;
    <asp:HyperLink ID="hlAllReports" NavigateUrl="http://veritas15/reports?/Project Management/All Projects&rs:Command=Render&rs:Format=PDF"
        runat="server">All Projects</asp:HyperLink><hr />
    <asp:Table ID="Table1" runat="server" Width="100%">
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="Label5" runat="server" Text="Salesperson"></asp:Label>
            </asp:TableCell><asp:TableCell>
                <asp:DropDownList ID="ddlSalesPerson" runat="server" DataSource=' <%# DAL.User.GetUsersByTitle("Sales") %>'
                    DataTextField="Name" DataValueField="ID" />
            </asp:TableCell><asp:TableCell></asp:TableCell><asp:TableCell>
                <asp:Label ID="lblName" runat="server" Text="Project Name"></asp:Label></asp:TableCell><asp:TableCell>
                    <asp:TextBox ID="txtProjectName" runat="server" Width="450px"></asp:TextBox>
                </asp:TableCell></asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>PM</asp:TableCell><asp:TableCell>
                <asp:DropDownList ID="ddlAssignedPM" runat="server" DataSource=' <%# DAL.User.GetUsersByTitle("Project Manager") %>'
                    DataTextField="Name" DataValueField="ID" />
            </asp:TableCell><asp:TableCell></asp:TableCell><asp:TableCell>
                <asp:Label ID="lblNumber" runat="server" Text="Project Number"></asp:Label>
            </asp:TableCell><asp:TableCell>
                <asp:TextBox ID="txtProjectNumber" runat="server" Width="100px"></asp:TextBox>
            </asp:TableCell></asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>Design/Draftsman</asp:TableCell><asp:TableCell>
                <asp:DropDownList ID="ddlDraftsman" runat="server" DataSource=' <%# DAL.User.GetUsersByTitle("Draftsman") %>'
                    DataTextField="Name" DataValueField="ID" />
            </asp:TableCell><asp:TableCell></asp:TableCell><asp:TableCell>Hospital / Clinic Name</asp:TableCell><asp:TableCell>
                <asp:TextBox ID="txtHospitalClinic" runat="server" Width="200px" /></asp:TableCell></asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>Engineering Consultant</asp:TableCell><asp:TableCell>
                <asp:DropDownList ID="ddlEngineeringConsultant" runat="server" DataSource=' <%# DAL.Lookup.GetLookupList("EngineeringConsultants") %>'
                    DataTextField="LookupValue" DataValueField="LookupValue" />
            </asp:TableCell><asp:TableCell></asp:TableCell><asp:TableCell>
                <asp:Label ID="lblActivity" runat="server" Text="Project Status"></asp:Label></asp:TableCell><asp:TableCell>
                    <asp:DropDownList ID="ddlProjectActivity" runat="server" DataSource=' <%# DAL.Lookup.GetLookupList("Project Activity") %>'
                        DataTextField="LookupValue" DataValueField="LookupValue" />
                </asp:TableCell></asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>Physicist</asp:TableCell><asp:TableCell>
                <asp:DropDownList ID="ddlPhysicist" runat="server" DataSource=' <%# DAL.User.GetUsersByTitle("Physicist") %>'
                    DataTextField="Name" DataValueField="ID" />
            </asp:TableCell><asp:TableCell></asp:TableCell><asp:TableCell>Region</asp:TableCell><asp:TableCell>
                <asp:DropDownList ID="ddlProspectRegion" runat="server" />
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="5"></asp:TableCell></asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="5" HorizontalAlign="Center">
                <asp:Button ID="cmdSave" runat="server" Text="Update Project" OnClick="cmdSaveLeadProj_Click" />
                <asp:Button ID="cmdDelete" runat="server" Text="Delete Project" OnClick="cmdDeleteLeadProj_Click" />
            </asp:TableCell></asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="5">
                <asp:Label ID="lblRecordStatus" runat="server" Style="font-size: x-small;" Text="" />
            </asp:TableCell></asp:TableRow>
    </asp:Table>
</asp:Content>
