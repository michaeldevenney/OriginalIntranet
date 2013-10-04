<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="priority.aspx.cs" Inherits="apps_estimating_priority" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titleContent" runat="Server">
    Sales Queue and Estimate Priority
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headContent" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContent" runat="Server">
    <div id="fuzz1">
        <div class="newLeadProj">
            <div class="heading">
                <asp:Label ID="lblHeading" runat="server" Text="Enter New Estimate"></asp:Label>
                <img style="height: 20px; width: 20px;" alt="Close" class="close1" src="../../images/error.png" />
            </div>
            <asp:Table ID="Table4" runat="server" Width="100%">
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="Label3" runat="server" Text="Lead Number"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="txtAddProjNumber" runat="server"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell></asp:TableCell>
                    <asp:TableCell>
                        <asp:Label ID="Label4" runat="server" Text="Lead Name"></asp:Label></asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="txtAddProjName" runat="server" Width="250px"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="5"></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="Label5" runat="server" Text="Lead Region"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:DropDownList ID="ddlProspectRegion" runat="server" />
                    </asp:TableCell>
                    <asp:TableCell></asp:TableCell>
                    <asp:TableCell></asp:TableCell>
                    <asp:TableCell></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="lblmufwacker" runat="server" Text="Assigned PM"></asp:Label></asp:TableCell>
                    <asp:TableCell>
                        <asp:DropDownList ID="ddlAddProjMgr" runat="server" DataSource=' <%# DAL.User.GetUsersByTitle("Project Manager") %>'
                            DataTextField="Name" DataValueField="ID" />
                    </asp:TableCell>
                    <asp:TableCell></asp:TableCell>
                    <asp:TableCell>
                        <asp:Label ID="Label6" runat="server" Text="Assigned Salesperson" /></asp:TableCell><asp:TableCell>
                            <asp:DropDownList ID="ddlSalesperson" runat="server" DataSource=' <%# DAL.User.GetUsersByTitle("Sales") %>'
                                DataTextField="Name" DataValueField="Id" />
                        </asp:TableCell></asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="5" Height="20px"></asp:TableCell></asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="5" HorizontalAlign="Center" VerticalAlign="Bottom">
                        <asp:CheckBox ID="chkCreateExec" Checked="true" runat="server" Text="Create Exec Folder" />&nbsp;&nbsp;&nbsp;
                        <asp:CheckBox ID="chkCreateProspect" Checked="true" runat="server" Text="Create Prospect Folder" />&nbsp;&nbsp;&nbsp;
                        <asp:CheckBox ID="chkCreateOutlook" Checked="true" runat="server" Text="Create Outlook Folder" />
                    </asp:TableCell></asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="5" HorizontalAlign="Center">
                        <br />
                        <asp:Button ID="cmdAddProjLead" runat="server" Text="Create Lead/Project" OnClick="cmdAddProjLead_Click" />
                    </asp:TableCell></asp:TableRow>
            </asp:Table>
        </div>
    </div>
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <hr />
    &nbsp;&nbsp;Reports (click to run): &nbsp;&nbsp;
    <asp:HyperLink ID="hlEstPriority" NavigateUrl="http://veritas15/reports?/Estimating/Estimate Queue&rs:Command=Render&rs:Format=PDF"
        runat="server">Estimate Queue</asp:HyperLink>&nbsp;&nbsp;|&nbsp;&nbsp;
    <asp:HyperLink ID="HyperLink1" NavigateUrl="http://veritas15/reports?/Estimating/Sales Queue&rs:Command=Render&rs:Format=PDF"
        runat="server">Sales Queue</asp:HyperLink><hr />
    <div style="padding: 5px 5px 5px 5px;">
        <asp:Table ID="Table1" runat="server" HorizontalAlign="Center" CellPadding="5" Width="100%">
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Center" Style="font-size: large; font-weight: bold;
                    color: Black;">Sales Queue</asp:TableCell>
                <asp:TableCell Width="200px"></asp:TableCell>
                <asp:TableCell HorizontalAlign="Center" Style="font-size: large; font-weight: bold;
                    color: Black;">Estimate Queue</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell VerticalAlign="Top">
                    <asp:Label ID="Label7" runat="server" Text="Queue is sorted alphabetically" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Image ID="imgAddRoom" runat="server" class="alert1" ImageUrl="~/images/Add.png"
                        ToolTip="Add a new lead to Sales Queue" Style="height: 20px; width: 20px; vertical-align: middle;" /><br />
                    <asp:ListBox ID="lstSalesQueue" runat="server" Width="100%" Rows="20" AutoPostBack="true"
                        OnSelectedIndexChanged="lstSalesQueue_SelectedIndexChanged" />
                    <br />
                    <asp:Button ID="cmdDelSalesItem" runat="server" Text="Delete Selected" OnClick="cmdDelSalesItem_Click" />
                </asp:TableCell><asp:TableCell VerticalAlign="Top" HorizontalAlign="Left">
                    <asp:Table ID="farrashi" runat="server" Width="100%">
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2" ForeColor="Red">
                                Select Y or N and enter a due date before proceeding
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell></asp:TableCell>
                            <asp:TableCell>&nbsp;&nbsp;&nbsp;Y&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;N</asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="Label2" runat="server" Text="Drawings: " /></asp:TableCell>
                            <asp:TableCell>
                                <asp:RadioButtonList ID="rdoDrawings" AutoPostBack="true" OnSelectedIndexChanged="rdoBttnLists_Click"
                                    runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="" Value="0"></asp:ListItem>
                                </asp:RadioButtonList>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="Label8" runat="server" Text="Prospectus: " /></asp:TableCell>
                            <asp:TableCell>
                                <asp:RadioButtonList ID="rdoProspectus" AutoPostBack="true" OnSelectedIndexChanged="rdoBttnLists_Click"
                                    runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="" Value="0"></asp:ListItem>
                                </asp:RadioButtonList>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="Label9" runat="server" Text="Tx Parameters: " /></asp:TableCell>
                            <asp:TableCell>
                                <asp:RadioButtonList ID="rdoTxParams" AutoPostBack="true" OnSelectedIndexChanged="rdoBttnLists_Click"
                                    runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="" Value="0"></asp:ListItem>
                                </asp:RadioButtonList>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <br />
                    Estimate Due Date<br />
                    <asp:TextBox ID="txtEstDueDate" runat="server" OnTextChanged="rdoBttnLists_Click"
                        AutoPostBack="true" />
                    <ajaxToolkit:CalendarExtender ID="CE1" runat="server" TargetControlID="txtEstDueDate"
                        Format="d" />
                    <br />
                    <br />
                    <asp:Button ID="cmdSubmitEst" OnClick="cmdSubmitEst_Clicked" runat="server" Text="Submit to Estimating" />
                </asp:TableCell><asp:TableCell VerticalAlign="Top">
                    <asp:Label ID="Label1" runat="server" Text="Drag and drop items to reorder the list" /><br />
                    <ajaxToolkit:ReorderList ID="lstEstimates" runat="server" DragHandleAlignment="Left"
                        ItemInsertLocation="Beginning" DataKeyField="ID" SortOrderField="Priority" AllowReorder="true"
                        OnItemReorder="lstEstimates_Reorder" BorderColor="Black" BorderStyle="solid"
                        BorderWidth="1px" Width="100%" BackColor="White">
                        <ItemTemplate>
                            <div style="height: 20px;">
                                <%# Eval("DisplayText") %>
                            </div>
                        </ItemTemplate>
                    </ajaxToolkit:ReorderList>
                </asp:TableCell></asp:TableRow>
        </asp:Table>
    </div>
</asp:Content>
