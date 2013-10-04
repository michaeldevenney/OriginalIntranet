<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="apps_URLTagging_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="titleContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="headContent" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainContent" runat="Server">
    <div id="fuzz1">
        <div class="newLeadProj">
            <div class="heading">
                <asp:Label ID="lblHeading" runat="server" Text="Create New Tagged URL"></asp:Label>
                <img style="height: 20px; width: 20px;" alt="Close" class="close1" src="../../images/error.png" />
            </div>
            <asp:Table ID="Table4" runat="server" Width="100%">
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="Label1" runat="server" Text="Source (block, PDF on website, etc...)"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="txtSourceAdd" runat="server"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell></asp:TableCell>
                    <asp:TableCell>
                        <asp:Label ID="Label2" runat="server" Text="Target (prospectus, interiors sheets, etc...)"></asp:Label></asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="txtTargetAdd" runat="server" Width="250px"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="Label4" runat="server" Text="Campaign (Tradeshow, email, etc..)"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="txtCampaignAdd" runat="server"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell></asp:TableCell>
                    <asp:TableCell></asp:TableCell>
                    <asp:TableCell></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="Label7" runat="server" Text="Target URL"></asp:Label></asp:TableCell>
                    <asp:TableCell ColumnSpan="4">
                        <asp:TextBox ID="txtTargetURLAdd" runat="server" Width="250px" />
                        <asp:Button ID="cmdShorten" runat="server" Text="Shorten URL" />
                    </asp:TableCell></asp:TableRow><asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="Label3" runat="server" Text="Shortened URL"></asp:Label>
                    </asp:TableCell><asp:TableCell ColumnSpan="2">
                        <asp:TextBox ID="txtShortenedURLAdd" runat="server"></asp:TextBox>
                    </asp:TableCell><asp:TableCell></asp:TableCell><asp:TableCell></asp:TableCell></asp:TableRow><asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Center">
                    </asp:TableCell><asp:TableCell>
                        <asp:Button ID="cmdCreateURL" runat="server" Text="Save URL" OnClick="cmdCreateURL_Click" />
                    </asp:TableCell></asp:TableRow></asp:Table></div></div><div>
        <asp:Table ID="Table1" runat="server" Width="100%" CellPadding="1">
            <asp:TableRow>
                <asp:TableCell Width="30%" HorizontalAlign="Center" VerticalAlign="Top">
                    <div style="vertical-align: bottom; font-weight: bold; color: White; background-color: Gray;
                        height: 22px;">
                        URL List<asp:Image ID="imgAddRoom" runat="server" class="alert1" ImageUrl="~/images/Add.png"
                            ToolTip="Create a new tagged URL" Style="height: 20px; width: 20px; float: right;" />
                    </div>
                    <asp:ListBox ID="lstURLs" runat="server" Rows="20" Width="400px" OnSelectedIndexChanged="lstURLs_SelectedIndexChanged"
                        AutoPostBack="true" />
                    <br />
                    <br />
                    <asp:Button ID="cmdSave" runat="server" Text="Save" OnClick="cmdSave_Click" />&nbsp;
                    <asp:Button ID="cmdDelete" runat="server" Text="Delete Selected URL" OnClick="cmdDelete_Click" />
                </asp:TableCell><asp:TableCell Width="70%" HorizontalAlign="Left" VerticalAlign="Top">
                    <asp:Table ID="Table2" runat="server" Width="100%" CellPadding="1">
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Table ID="Table3" runat="server" CellPadding="1">
                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Right" Width="35%">Source</asp:TableCell>
                                        <asp:TableCell HorizontalAlign="Left">
                                            <asp:TextBox ID="txtSource" runat="server" /></asp:TableCell>
                                        <asp:TableCell HorizontalAlign="Right">Target</asp:TableCell>
                                        <asp:TableCell HorizontalAlign="Left">
                                            <asp:TextBox ID="txtTarget" runat="server" />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Right">Campaign</asp:TableCell>
                                        <asp:TableCell HorizontalAlign="Left">
                                            <asp:TextBox ID="txtCampaign" runat="server" /></asp:TableCell>
                                        <asp:TableCell HorizontalAlign="Right"></asp:TableCell>
                                        <asp:TableCell HorizontalAlign="Right">                                            
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Right">Target URL</asp:TableCell>
                                        <asp:TableCell HorizontalAlign="Left" ColumnSpan="3">
                                            <asp:TextBox ID="txtTargetURL" Width="400" runat="server" />
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Button ID="cmdShortenURL" runat="server" Text="Shorten" />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Right">Shortened URL</asp:TableCell>
                                        <asp:TableCell HorizontalAlign="Left" ColumnSpan="3">
                                            <asp:TextBox ID="txtShortenedURL" Width="200" runat="server" Enabled="false" />
                                        </asp:TableCell>
                                        <asp:TableCell HorizontalAlign="Right"></asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:TableCell></asp:TableRow></asp:Table></div></asp:Content>