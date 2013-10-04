<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
    CodeFile="default.aspx.cs" Inherits="apps_LeadToProject_default" %>

<asp:Content ID="Content3" ContentPlaceHolderID="mainContent" runat="Server">
    <asp:Table ID="Table1" runat="server" Width="100%">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" Style="color: Black; font-size: large;">
                Prospects
            </asp:TableCell>
            <asp:TableCell></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell Width="50%" VerticalAlign="Top">
                <asp:ListBox ID="lstProspects" Width="100%" runat="server" Rows="25" AutoPostBack="true"
                    OnSelectedIndexChanged="lstProspects_Changed" />
            </asp:TableCell>
            <asp:TableCell Width="50%" HorizontalAlign="Center" VerticalAlign="Middle">
                <asp:Label ID="Label1" runat="server" Text="Project Number (editable)" /><br />
                <asp:TextBox ID="txtProjectNumber" runat="server" /><br />
                <asp:HyperLink ID="hlShowProjects" Style="cursor: pointer; color: Blue; text-decoration: underline;"
                    runat="server">Show Active Projects</asp:HyperLink>
                <br />
                <br />
                <table width="100%">
                    <tr>
                        <td>
                            Project Manager
                        </td>
                        <td>
                            Engineer
                        </td>
                        <td>
                            Eng Consultant
                        </td>
                        <td>
                            Physicist
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="ddlPM" runat="server" DataSource=' <%# DAL.User.GetUsersByTitle("Project Manager") %>'
                                DataTextField="Name" DataValueField="ID" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlEng" runat="server" DataSource=' <%# DAL.User.GetUsersByTitle("Draftsman") %>'
                                DataTextField="Name" DataValueField="ID" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlEngConsultant" runat="server" DataSource=' <%# DAL.Lookup.GetLookupList("EngineeringConsultants") %>'
                                DataTextField="LookupValue" DataValueField="LookupValue" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPhysicist" runat="server" DataSource=' <%# DAL.User.GetUsersByTitle("Physicist") %>'
                                DataTextField="Name" DataValueField="ID" />
                        </td>
                    </tr>
                </table>
                <br />
                <hr />
                <asp:CheckBox ID="chkFolders" runat="server" Text="Move Prospect Folders" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:CheckBox ID="chkNotification" runat="server" AutoPostBack="true" Text="Send Notification" OnCheckedChanged="chkNotify_Changed" />
                <br />
                <hr />                
                <asp:Label ID="lblJobEstimates" runat="server" Text="Select Accepted Estimate" /><br />
                <asp:ListBox ID="lstJobEstimates" runat="server" Rows="7" Width="100" />
                <hr />
                <br />                
                <asp:Panel ID="Panel1" runat="server" Visible="false" HorizontalAlign="Left" style="padding-left:30px;" >                
                    <asp:CheckBoxList ID="chkUsers" runat="server" RepeatColumns="4" RepeatDirection="Horizontal"
                    DataSource=' <%# DAL.User.GetUsersByStatus(DAL.User.UserStatus.Active) %>' DataTextField="Name" />                
                </asp:Panel>
                <hr />
                <br />
                <asp:Button ID="cmdGo" runat="server" Text="GO" OnClick="cmdGo_Click" />
                <asp:Label ID="lblStatus" runat="server" color="Red" Text="" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <%--AJAX--%>
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" />
    <ajaxToolkit:ModalPopupExtender ID="MPENew" runat="server" TargetControlID="hlShowProjects"
        PopupControlID="pnlNewConfirm" BackgroundCssClass="modalBackground" DropShadow="true"
        CancelControlID="cmdOKNew" PopupDragHandleControlID="pnlMessageBox">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlNewConfirm" runat="server" CssClass="ModalWindow" HorizontalAlign="Center">
        <asp:Repeater ID="rptProjects" runat="server">
            <HeaderTemplate>
                <table class="ModalWindowTable" cellpadding="2" width="100%">
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td align="left">
                        <%# DataBinder.Eval(Container.DataItem, "DisplayName") %>
                    </td>
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr bgcolor="silver">
                    <td align="left">
                        <%# DataBinder.Eval(Container.DataItem, "DisplayName") %>
                    </td>
                </tr>
            </AlternatingItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
        <br />
        <br />
        <asp:Button ID="cmdOKNew" runat="server" Text="Close" />
    </asp:Panel>
</asp:Content>
