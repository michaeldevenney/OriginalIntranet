<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="apps_estimating_Default" %>

<%@ Register TagPrefix="ajax" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContent" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" />
     <hr />
    &nbsp;&nbsp;Reports (click to run): &nbsp;&nbsp;
    <asp:HyperLink ID="hlEstPriority" NavigateUrl="http://veritas15/reports?/Estimating/Estimate Queue&rs:Command=Render&rs:Format=PDF"
        runat="server">Estimate Queue</asp:HyperLink>&nbsp;&nbsp;|
    <hr />
    <div style="height: 1200px;">
        <asp:Table ID="Table1" runat="server" Width="100%" CellPadding="1">
            <asp:TableRow>
                <asp:TableCell Width="30%" HorizontalAlign="Center" VerticalAlign="Top">
                    <div style="vertical-align: bottom; font-weight: bold; color: White; background-color: Gray;
                        height: 22px;">
                        Estimate List
                        <asp:DropDownList ID="ddlEstFilter" AutoPostBack="true" OnSelectedIndexChanged="ddlEstFilter_SelectedIndexChanged"
                            runat="server">
                            <asp:ListItem Selected="True">Active</asp:ListItem>
                            <asp:ListItem>All</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <asp:ListBox ID="lstEstimates" runat="server" Rows="20" Width="400px" OnSelectedIndexChanged="lstEstimates_SelectedIndexChanged"
                        AutoPostBack="true" />
                    <br />
                    <br />
                    <asp:Button ID="cmdDelete" runat="server" Text="Delete Estimate" OnClick="cmdDelete_Click" /><br />
                    <asp:Button ID="cmdNew" runat="server" Text="New Estimate for this Job" OnClick="cmdNew_Click" /><br />
                </asp:TableCell>
                <asp:TableCell Width="70%" HorizontalAlign="Left" VerticalAlign="Top">
                    <asp:Table ID="Table2" runat="server" Width="100%" CellPadding="1">
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Table ID="Table3" runat="server" Width="100%" CellPadding="1">
                                    <asp:TableRow Height="30">
                                        <asp:TableCell ColumnSpan="4" HorizontalAlign="Left" VerticalAlign="Top">
                                            <asp:Label Font-Size="Large" ForeColor="Black" ID="lblProspectHeading" runat="server"
                                                Text="" />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Right">Estimate Recipient</asp:TableCell>
                                        <asp:TableCell HorizontalAlign="Left">
                                            <asp:TextBox ID="txtClientContact" runat="server" Width="300" /></asp:TableCell>
                                        <asp:TableCell HorizontalAlign="Right">Est Due Date</asp:TableCell><asp:TableCell
                                            HorizontalAlign="Left">
                                            <asp:TextBox ID="txtBidDate" runat="server" />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Right">Recipient Email</asp:TableCell><asp:TableCell
                                            HorizontalAlign="Left">
                                            <asp:TextBox ID="txtContactEmail" runat="server" Width="200px" />
                                            <ajaxToolkit:CalendarExtender runat="server" TargetControlID="txtBidDate" Format="d" />
                                        </asp:TableCell>
                                        <asp:TableCell HorizontalAlign="Right" VerticalAlign="Top">Estimator</asp:TableCell>
                                        <asp:TableCell HorizontalAlign="Left">
                                            <asp:DropDownList ID="ddlEstimator" runat="server" />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Right" VerticalAlign="Top">Building Name and Address</asp:TableCell>
                                        <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
                                            <asp:TextBox ID="txtHospitalClinic" Width="100%" Rows="2" Wrap="false" TextMode="MultiLine"
                                                runat="server" />
                                        </asp:TableCell>
                                        <asp:TableCell HorizontalAlign="Right"></asp:TableCell>
                                        <asp:TableCell HorizontalAlign="Right"></asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Left" ColumnSpan="4" VerticalAlign="Top">
                                            Description<br />
                                            <asp:TextBox ID="txtEstDescription" runat="server" Rows="4" TextMode="MultiLine"
                                                Width="100%" />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell ColumnSpan="4">
                                            <asp:Table ID="fuzzyone" runat="server" Width="100%">
                                                <asp:TableRow HorizontalAlign="Center">
                                                    <asp:TableCell>Bunker Subtotal</asp:TableCell>
                                                    <asp:TableCell>Door Subtotal</asp:TableCell>
                                                    <asp:TableCell>Interior Subtotal</asp:TableCell>
                                                    <asp:TableCell>Estimate Total</asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow HorizontalAlign="Center">
                                                    <asp:TableCell>
                                                        <asp:TextBox ID="txtBunkerTotal" OnTextChanged="UpdateEstimateTotal" AutoPostBack="True"
                                                            runat="server" />
                                                    </asp:TableCell>
                                                    <asp:TableCell>
                                                        <asp:TextBox ID="txtDoorsTotal" OnTextChanged="UpdateEstimateTotal" AutoPostBack="True"
                                                            runat="server" />
                                                    </asp:TableCell>
                                                    <asp:TableCell>
                                                        <asp:TextBox ID="txtInteriorsTotal" OnTextChanged="UpdateEstimateTotal" AutoPostBack="True"
                                                            runat="server" />
                                                    </asp:TableCell>
                                                    <asp:TableCell>
                                                        <asp:TextBox ID="txtEstimatetotal" runat="server" />
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell ColumnSpan="4">
                                                        <hr />
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                            </asp:Table>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <asp:Table ID="Table11" runat="server" Width="100%" CellPadding="1">
                        <asp:TableRow Font-Bold="true" HorizontalAlign="Center">
                            <asp:TableCell></asp:TableCell>
                            <asp:TableCell></asp:TableCell>
                            <asp:TableCell BackColor="Gray" ForeColor="White">Doors</asp:TableCell>
                            <asp:TableCell BackColor="Gray" ForeColor="White">Vaults</asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Right" Width="10%" VerticalAlign="Top">
                                            Interiors
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Left" Width="10%" VerticalAlign="Top">
                                <asp:TextBox ID="txtInteriors" runat="server" Width="50" />
                            </asp:TableCell>
                            <asp:TableCell RowSpan="3" Width="40%" HorizontalAlign="Center" VerticalAlign="Top">
                                <asp:Table ID="Table4" runat="server">
                                    <asp:TableRow>
                                        <asp:TableCell>Qty</asp:TableCell>
                                        <asp:TableCell></asp:TableCell>
                                        <asp:TableCell></asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            <asp:TextBox ID="txtDoorQty" runat="server" Width="25" />
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:DropDownList ID="ddlSize" runat="server">
                                                <asp:ListItem Selected="True">...</asp:ListItem>
                                                <asp:ListItem>20"</asp:ListItem>
                                                <asp:ListItem>15"</asp:ListItem>
                                                <asp:ListItem>10"</asp:ListItem>
                                                <asp:ListItem>5"</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlStyle" runat="server">
                                                <asp:ListItem Selected="True">...</asp:ListItem>
                                                <asp:ListItem>Swing</asp:ListItem>
                                                <asp:ListItem>Bi-Parting</asp:ListItem>
                                                <asp:ListItem>Single Leaf</asp:ListItem>
                                            </asp:DropDownList>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Button ID="cmdAddDoor" runat="server" Text="Add" OnClick="cmdAddDoor_Click" />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell ColumnSpan="3">
                                            <asp:ListBox ID="lstDoors" runat="server" />
                                            <br />
                                            <asp:Button ID="cmdDelDoor" runat="server" Text="Delete Selected" OnClick="cmdDelDoor_Click" />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </asp:TableCell>
                            <asp:TableCell RowSpan="3" Width="40%" HorizontalAlign="Center" VerticalAlign="Top">
                                <asp:Table ID="Table7" runat="server">
                                    <asp:TableRow>
                                        <asp:TableCell>Qty</asp:TableCell>
                                        <asp:TableCell>Room info</asp:TableCell>
                                        <asp:TableCell></asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell>
                                            <asp:TextBox ID="txtVaultQty" runat="server" Width="25" />
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:DropDownList ID="ddlEntryType" runat="server">
                                                <asp:ListItem Selected="True">...</asp:ListItem>
                                                <asp:ListItem>Direct</asp:ListItem>
                                                <asp:ListItem>Maze</asp:ListItem>
                                                <asp:ListItem>Mini-Maze</asp:ListItem>
                                                <asp:ListItem>Doorless</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlRoomType" runat="server">
                                                <asp:ListItem Selected="True">...</asp:ListItem>
                                                <asp:ListItem>Linac</asp:ListItem>
                                                <asp:ListItem>Cyberknife</asp:ListItem>
                                                <asp:ListItem>Tomotherapy</asp:ListItem>
                                                <asp:ListItem>HDR</asp:ListItem>
                                                <asp:ListItem>Other</asp:ListItem>
                                            </asp:DropDownList>
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Button ID="cmdAddRoom" runat="server" Text="Add" OnClick="cmdAddRoom_Click" />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell ColumnSpan="3">
                                            <asp:ListBox ID="lstVaults" runat="server" />
                                            <br />
                                            <asp:Button ID="cmdDelVault" runat="server" Text="Delete Selected" OnClick="cmdDelVault_Click" />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Right" VerticalAlign="Top">Supplemental</asp:TableCell>
                            <asp:TableCell HorizontalAlign="Left" VerticalAlign="Top">
                                <asp:TextBox ID="txtSupplementalQty" runat="server" Width="50" /><br />
                                <asp:Label ID="Label1" runat="server" Font-Size="Smaller" Text="Approx Block Count" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell></asp:TableCell>
                            <asp:TableCell></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Right" ColumnSpan="4">
                    <asp:Button ID="cmdSave" runat="server" Text="Save Estimate" OnClick="cmdSave_Click" />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Center" VerticalAlign="Middle" ColumnSpan="4"> 
                                            <div style="font-weight:bold; color:White; background-color:Gray; height:18px;">Estimate Dates</div>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Font-Bold="true" VerticalAlign="Middle" HorizontalAlign="Left" ColumnSpan="4">
                    Current Status:
                    <asp:Label ID="lblCurrentStatus" ForeColor="Red" runat="server" Text="" />
                </asp:TableCell></asp:TableRow></asp:Table><asp:Table ID="Table10" runat="server" Width="100%" CellPadding="1">
            <asp:TableRow>
                <asp:TableCell RowSpan="2" VerticalAlign="Top" HorizontalAlign="Center" BackColor="Gray"
                    ForeColor="White">
                    Estimate Timeline<br />
                    <asp:ListBox ID="lstTimeline" runat="server" Font-Size="Smaller" Rows="19" AutoPostBack="true"
                        OnSelectedIndexChanged="lstEstimateTimeline_SelectedIndexChanged" />
                    <br />
                    <asp:Button ID="cmdDelTimelineStep" runat="server" Text="Delete Selected" OnClick="cmdDelTimelineStep_Click" />
                </asp:TableCell><asp:TableCell ColumnSpan="3" HorizontalAlign="Left">
                    <asp:Table ID="Table9" runat="server" CellPadding="1" Width="100%">
                        <asp:TableRow>
                            <asp:TableCell BackColor="Gray" ForeColor="White" ColumnSpan="2">Details of Selected Step</asp:TableCell></asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>Step:</asp:TableCell><asp:TableCell>
                                <asp:Label ID="lblStep" runat="server" /></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>Responsible:</asp:TableCell><asp:TableCell>
                                <asp:DropDownList ID="ddlResponsibleEdit" runat="server" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>Date:</asp:TableCell><asp:TableCell>
                                <asp:TextBox ID="txtDateTimeStampEdit" runat="server" />
                                <ajaxToolkit:CalendarExtender ID="ceDateTimeEdit" runat="server" TargetControlID="txtDateTimeStampEdit"
                                    Format="d" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2">
                                Additional Information:<br />
                                <asp:TextBox ID="txtAdditionalInfoEdit" TextMode="MultiLine" runat="server" Width="400px" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <asp:Button ID="cmdEditStep" runat="server" Text="Edit Step" OnClick="cmdEditStep_Click" />
                </asp:TableCell></asp:TableRow><asp:TableRow>
                <asp:TableCell ColumnSpan="3" HorizontalAlign="Left">
                    <asp:Table ID="Table8" runat="server" CellPadding="1" Width="100%">
                        <asp:TableRow>
                            <asp:TableCell BackColor="Gray" ForeColor="White" ColumnSpan="2">Add Next Step</asp:TableCell></asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>Step:</asp:TableCell><asp:TableCell>
                                <asp:DropDownList ID="ddlWorkflowStep" runat="server" OnSelectedIndexChanged="ddlWorkflowStep_Change"
                                    AutoPostBack="true" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>Responsible:</asp:TableCell><asp:TableCell>
                                <asp:DropDownList ID="ddlResponsible" runat="server" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2">
                                Additional Information:<br />
                                <asp:TextBox ID="txtAdditionalInformation" TextMode="MultiLine" runat="server" Width="400px" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <asp:Button ID="cmdAddStep" runat="server" Text="Add Step" OnClick="cmdAddStep_Click" />
                </asp:TableCell></asp:TableRow></asp:Table><asp:Table ID="Table5" runat="server" CellPadding="1" Width="100%">
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Center">
                            <div style="font-weight: bold; color: White; background-color: Gray; height: 18px;">
                                Detailed Room Information</div>
                </asp:TableCell></asp:TableRow><asp:TableRow HorizontalAlign="Center">
                <asp:TableCell ColumnSpan="2"> Design basis ("client supplied" or "verbally agreed" or "vendor recommended room dimensions") </asp:TableCell></asp:TableRow><asp:TableRow HorizontalAlign="Center">
                <asp:TableCell Width="80%">
                    <asp:TextBox ID="txtDesignBasis" TextMode="MultiLine" runat="server" Width="600px" />
                </asp:TableCell></asp:TableRow><asp:TableRow HorizontalAlign="Center">
                <asp:TableCell ColumnSpan="2">
                    <br />
                    Physics Basis&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlPhysicsBasis" AutoPostBack="true" runat="server" DataSource=' <%# GetPhysicsBasis() %>'
                        DataTextField="Display" DataValueField="Id" OnSelectedIndexChanged="ddlPhysicsBasis_SelectedIndexChanged" />
                    Labor Type&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlLaborType" runat="server">
                        <asp:ListItem Value="Select..." Text="Select..." Selected="True">Select...</asp:ListItem>
                        <asp:ListItem Value="Non-Union" Text="Non-Union" />
                        <asp:ListItem Value="Union" Text="Union" />
                    </asp:DropDownList>
                </asp:TableCell></asp:TableRow><asp:TableRow HorizontalAlign="Center">
                <asp:TableCell ColumnSpan="2">
                    <asp:TextBox ID="txtPhysicsBasis" TextMode="MultiLine" Rows="11" runat="server" Width="600px" />
                </asp:TableCell></asp:TableRow><asp:TableRow HorizontalAlign="Center">
                <asp:TableCell  ColumnSpan="2">
                    <br />
                    Bunker Heading
                    <br />
                    <asp:TextBox ID="txtBunkerHeader" runat="server" Width="600px" />
                </asp:TableCell></asp:TableRow><asp:TableRow HorizontalAlign="Center">
                <asp:TableCell>
                    Bunker Scope
                    <br />
                    <asp:TextBox ID="txtBunkerScope" TextMode="MultiLine" runat="server" Width="600px" />
                </asp:TableCell></asp:TableRow><asp:TableRow HorizontalAlign="Center">
                <asp:TableCell>
                    Bunker Clarifications
                    <br />
                    <asp:TextBox ID="txtBunkerClarifications" TextMode="MultiLine" runat="server" Width="600px" />
                </asp:TableCell></asp:TableRow><asp:TableRow HorizontalAlign="Center">
                <asp:TableCell HorizontalAlign="Center">
                    <br />
                    Interior Scope
                    <br />
                    <asp:TextBox ID="txtInteriorScope" TextMode="MultiLine" runat="server" Width="600px" />
                </asp:TableCell></asp:TableRow><asp:TableRow HorizontalAlign="Center">
                <asp:TableCell HorizontalAlign="Center">
                    <br />
                    Door Scope
                    <br />
                    <asp:TextBox ID="txtDoorScope" TextMode="MultiLine" runat="server" Width="600px" />
                </asp:TableCell></asp:TableRow></asp:Table><asp:Table ID="Table6" runat="server" CellPadding="1" Width="100%">
            <asp:TableRow HorizontalAlign="Center">
                <asp:TableCell HorizontalAlign="Center" ColumnSpan="4">
                            <div style="font-weight: bold; color: White; background-color: Gray; height: 18px;">
                                Filesystem Information</div>
                </asp:TableCell></asp:TableRow><asp:TableRow HorizontalAlign="Center">
                <asp:TableCell HorizontalAlign="Left" ColumnSpan="4">
                    Estimates Directory&nbsp;&nbsp;&nbsp;
                    <asp:HyperLink ID="hlExecDir" Target="_blank" NavigateUrl="" runat="server">Open</asp:HyperLink>
                    <br />
                    <asp:TextBox ID="txtEstimatesDirectory" runat="server" Width="99%" />
                </asp:TableCell></asp:TableRow><asp:TableRow HorizontalAlign="Center">
                <asp:TableCell HorizontalAlign="Left" ColumnSpan="4">
                    Prospect Directory&nbsp;&nbsp;&nbsp;
                    <asp:HyperLink ID="hlProspectDir" Target="_blank" NavigateUrl="" runat="server">Open</asp:HyperLink>
                    <br />
                    <asp:TextBox ID="txtProspectDirectory" runat="server" Width="99%" />
                    <br />
                    <br />
                    <div style="text-align: center;">
                    </div>
                </asp:TableCell></asp:TableRow></asp:Table></div><!--AJAX--><ajaxToolkit:DropShadowExtender ID="dse" runat="server" TargetControlID="cmdSave"
        Opacity=".8" Rounded="true" TrackPosition="true" />
    <ajaxToolkit:AlwaysVisibleControlExtender ID="AlwaysVisibleControlExtender1" runat="server"
        TargetControlID="cmdSave" HorizontalOffset="5" HorizontalSide="Right" UseAnimation="true"
        VerticalOffset="5" VerticalSide="Middle" />
    <%-- <!-- CONFIRMATION DIALOGS-->
       <ajaxToolkit:ModalPopupExtender ID="MPENew" runat="server" TargetControlID="cmdNew"
            PopupControlID="pnlNewConfirm" BackgroundCssClass="modalBackground" DropShadow="true"
            CancelControlID="cmdMsgBoxOK" PopupDragHandleControlID="pnlMessageBox">            
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlNewConfirm" runat="server" CssClass="ModalWindow" HorizontalAlign="Center">
            <asp:Label ID="lblPopupMessage" runat="server" Text="Estimate has been created." /><br /><br />
            <asp:Button ID="cmdOKNew" runat="server" Text="OK" OnClick="cmdNewCopy_Click" />
        </asp:Panel>

         <ajaxToolkit:ModalPopupExtender ID="MPENewCopy" runat="server" TargetControlID="cmdNewCopy"
            PopupControlID="pnlNewCopyConfirm" BackgroundCssClass="modalBackground" DropShadow="true"
            OkControlID="cmdOKCopy" CancelControlID="cmdMsgBoxOK" PopupDragHandleControlID="pnlMessageBox">            
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlNewCopyConfirm" runat="server" CssClass="ModalWindow" HorizontalAlign="Center">
            <asp:Label ID="Label1" runat="server" Text="Estimate has been created." /><br /><br />
            <asp:Button ID="cmdOKCopy" runat="server" Text="OK" />
        </asp:Panel>

         <ajaxToolkit:ModalPopupExtender ID="MPEDelete" runat="server" TargetControlID="cmdDelete"
            PopupControlID="pnlDeleteConfirm" BackgroundCssClass="modalBackground" DropShadow="true"
            OkControlID="cmdOKDelete" CancelControlID="cmdMsgBoxOK" PopupDragHandleControlID="pnlMessageBox">            
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlDeleteConfirm" runat="server" CssClass="ModalWindow" HorizontalAlign="Center">
            <asp:Label ID="Label2" runat="server" Text="Estimate has been deleted." /><br /><br />
            <asp:Button ID="cmdOKDelete" runat="server" Text="OK" />
        </asp:Panel>--%>
</asp:Content>
