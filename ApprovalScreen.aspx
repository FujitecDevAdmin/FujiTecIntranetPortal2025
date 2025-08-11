<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApprovalScreen.aspx.cs" Inherits="FujiTecIntranetPortal.ApprovalScreen" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <style type="text/css">
        .footer {
            position: fixed;
            left: 0;
            bottom: 0;
            width: 100%;
            background-color: white;
            color: white;
            text-align: center;
            height: 2.5%;
        }

        p {
            background-color: #CF2331;
        }

        .center {
            display: flex;
            justify-content: center;
            align-items: center;
        }

        .auto-style2 {
            width: 17%;
            height: 30px;
            font-family: Verdana;
        }

        .myInput:focus {
            border: solid 1px green !important;
            outline: double !important;
        }

        .middlepane {
            width: 85%;
            float: left;
            background-color: white;
            border-collapse: collapse;
        }

        .rightpane {
            width: 75%;
            position: relative;
            float: right;
            background-color: white;
            border-collapse: collapse;
        }

        .normal-table1 {
            font-size: 1px !important;
            border: 10px solid white !important;
            padding: 2px !important;
            margin-bottom: 5px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="page-container scroll-y">

        <%--<asp:GridView ID="gv" runat="server" AutoGenerateSelectButton="True" OnSelectedIndexChanged="GV_SelectedIndexChanged" OnRowDataBound="GV_OnRowDataBound" CellPadding="4" ForeColor="#333333" GridLines="None">
               
                <AlternatingRowStyle BackColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>--%>
        <div class="rightpane">
            <div class="container">
                <table>
                    <tr>
                        <td class="auto-style2">
                            <asp:Button ID="btnCheckAll" runat="server" Text="Select All" OnClick="btnCheckAll_Click" Width="100px" /></td>
                        <td class="auto-style2">
                            <asp:Button ID="btnUnCheckAll" runat="server" Text="Unselect  All" OnClick="btnUnCheckAll_Click" Width="100px" /></td>
                        <td class="auto-style2">
                            <asp:Button ID="btnApprove" runat="server" Text="Approve" OnClick="btnApprove_Click" Width="100px" /></td>
                        <td class="auto-style2">
                            <asp:Button ID="btnReject" runat="server" Text="Reject" OnClick="btnReject_Click" Width="100px" /></td>
                        <td class="auto-style2">
                            <asp:Button ID="btnClear" runat="server" Text="Clear" Width="100px" Visible="true" OnClick="btnClear_Click" /></td>
                        <td class="auto-style2">
                            <asp:Button ID="Button2" runat="server" Text="Reject" Width="100px" Visible="false" /></td>
                        <td class="auto-style2">
                            <asp:Button ID="Button3" runat="server" Text="Reject" Width="100px" Visible="false" /></td>
                    </tr>
                    <%--<tr>
                        <td class="auto-style2" colspan="1" align="center">
                            <asp:Label ID="Label4" runat="server" Text="Remarks:" Width="300px" Font-Size="Large" Visible="true"></asp:Label></td>
                        <td class="auto-style2">
                            <asp:TextBox ID="txtEventName" CssClass="myInput" runat="server" Width="232px" TabIndex="1" Height="60px" TextMode="MultiLine" Style="margin-left: 0" Visible="true"></asp:TextBox></td>
                    </tr>--%>
                    <tr>
                        <td class="auto-style2" colspan="3" align="center">
                            <asp:Label ID="lblmsg" runat="server" Text="" Width="300px" Font-Size="Larger"></asp:Label></td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="container">
            <div id="divGridViewScroll" style="width: 100%; height: 300px; overflow: scroll">
                <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" OnRowCommand="gv_OnRowCommand" Font-Names="Verdana"
                    BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" OnSelectedIndexChanged="GV_SelectedIndexChanged"
                    CellPadding="4" ForeColor="Black" GridLines="Vertical" HeaderStyle-Font-Names="Verdana">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <%--<asp:CommandField ShowSelectButton="True" SelectText="Approve" />--%>
                        <asp:CommandField SelectText="View" ShowSelectButton="true" ItemStyle-ForeColor="Black" ItemStyle-Font-Bold="true" />
                        <asp:TemplateField HeaderText="Subject" ItemStyle-Width="30%">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("NEWSANDEVENTS") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("NEWSANDEVENTS") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("EventName") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("EventName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <%-- <EditItemTemplate>  
                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("ApprovalStatus") %>'></asp:TextBox>  
                    </EditItemTemplate>  --%>
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("ApprovalStatus") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Select Data">
                            <%-- <EditItemTemplate>
                            <asp:CheckBox ID="Chckselect" runat="server" />
                        </EditItemTemplate>--%>
                            <ItemTemplate>
                                <asp:CheckBox ID="Chckselect" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="Approve">
                        <ItemTemplate>
                            <asp:Button ID="btnApprove" runat="server" CausesValidation="false" CommandName="Approve"
                                Text="Approve" CommandArgument='<%# Eval("Approve") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    </Columns>
                    <%--<asp:CommandField ShowSelectButton="True" SelectText="Approve" />--%>
                    <RowStyle HorizontalAlign="Center"></RowStyle>
                    <FooterStyle BackColor="#CCCC99" />
                    <HeaderStyle HorizontalAlign="Center" BackColor="#66e0ff" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                    <RowStyle BackColor="#F7F7DE" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#FBFBF2" />
                    <SortedAscendingHeaderStyle BackColor="#848384" />
                    <SortedDescendingCellStyle BackColor="#EAEAD3" />
                    <SortedDescendingHeaderStyle BackColor="#575357" />
                </asp:GridView>
            </div>
        </div>
        <div>
            <table class="normal-table1">
                <tr>
                    <td class="auto-style2" align="right">
                        <th class="auto-style2"><strong style="font-size: large;font-family:Verdana">Remarks: <span class="hlt-txt">*</span></strong>
                            <asp:TextBox ID="txtRemarks" CssClass="myInput" runat="server" Width="232px" TabIndex="1" Height="60px" TextMode="MultiLine" Style="margin-left: 0" Visible="true"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
