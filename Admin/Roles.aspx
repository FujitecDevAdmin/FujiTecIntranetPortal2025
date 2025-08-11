<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Roles.aspx.cs" Inherits="FujiTecIntranetPortal.Admin.Roles" %>

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

        .auto-style2 {
            width: 17%;
            height: 30px;
        }

        .colHeader-RightAlign {
            text-align: right !important;
        }

        .myInput:focus {
            border: solid 1px green !important;
            outline: double !important;
        }

        .auto-style3 {
            width: 184px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="page-container scroll-y">
        <div>
            <h4 class="text-center" style="font-family: Verdana">User Access Rights</h4>
            <table width="100%">
                <tr align="center">
                    <%-- <th class="auto-style3"><strong> <span class="hlt-txt"></span></strong>
                    </th>
                    <td class="auto-style2" colspan="3" align="center">
                        <asp:DropDownList ID="DropDownList2" runat="server" Visible="false"></asp:DropDownList>
                    </td>--%>
                    <%-- <th class="auto-style3" ><strong>User: <span class="hlt-txt">*</span></strong>
                    </th>--%>
                    <td class="auto-style2" colspan="3" align="center">
                        <strong>User: <span class="hlt-txt">* </span></strong>
                        <asp:DropDownList ID="ddUser" runat="server" Width="150px" OnSelectedIndexChanged="ddUser_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>
                   <strong> Module: <span class="hlt-txt"></span></strong>
                        <asp:DropDownList ID="DropDownList1" runat="server" Width="150px" Visible="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </td>
                </tr>

            </table>
        </div>
        <div>
            <table>
                <tr>
                    <td class="auto-style2" colspan="3" align="center">
                        <asp:Button ID="BtnSave" runat="server" Text="Save" CssClass="myInput" Width="100px"
                            TabIndex="12" OnClick="btnSave_Click"></asp:Button>
                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="myInput" Width="100px"
                            TabIndex="13" OnClick="btnClear_Click"></asp:Button>
                        <asp:Button ID="btnCheckAll" runat="server" Text="Select All" OnClick="btnCheckAll_Click" Width="100px" />
                        <asp:Button ID="btnUnCheckAll" runat="server" Text="Unselect  All" OnClick="btnUnCheckAll_Click" Width="100px" />
                    </td>
                    <%--<td class="auto-style2">
                        <asp:Button ID="btnCheckAll" runat="server" Text="Select All" OnClick="btnCheckAll_Click" Width="100px" /></td>
                    <td class="auto-style2">
                        <asp:Button ID="btnUnCheckAll" runat="server" Text="Unselect  All" OnClick="btnUnCheckAll_Click" Width="100px" /></td>--%>
                </tr>
                <tr>
                    <td class="auto-style2" colspan="3" align="center">
                        <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label></td>
                </tr>
            </table>
        </div>
        <div style="width: 100px !important; float: left !important;"></div>
        <div>
            <div id="divGridViewScroll" style="width: 90%; height: 400px; overflow: scroll; float: right;">
                <asp:GridView ID="DGV" runat="server" OnPageIndexChanging="OnPageIndexChanging" HeaderStyle-BackColor="#9AD6ED" AllowPaging="true" PageSize="100"
                    HeaderStyle-ForeColor="White" HeaderStyle-BorderStyle="Solid" OnRowDataBound="DGV_RowDataBound" AlternatingRowStyle-BackColor="WhiteSmoke"
                    AutoGenerateColumns="False" BorderStyle="Solid">
                    <AlternatingRowStyle BackColor="WhiteSmoke"></AlternatingRowStyle>
                    <RowStyle BorderStyle="Solid" Width="30px" Height="30px" />


                    <HeaderStyle BorderStyle="Solid" Height="50px" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    <%--<Columns><asp:DynamicField ItemStyle-BorderStyle="Solid"/></Columns>--%>
                    <%--<Columns CssClass="colHeader-RightAlign"></Columns>--%>

                    <Columns>
                        <%--<asp:CommandField ShowSelectButton="True" SelectText="Approve" />--%>
                        <%--<asp:CommandField SelectText="View" ShowSelectButton="true" ItemStyle-ForeColor="Black" ItemStyle-Font-Bold="true" />--%>
                        <asp:TemplateField HeaderText="SNO" ItemStyle-Width="1%" ItemStyle-HorizontalAlign="Center">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtSNO" runat="server" Text='<%# Bind("SNO") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblSNO" runat="server" Text='<%# Bind("SNO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MODULE" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtMODULE" runat="server" Text='<%# Bind("MODULE") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblMODULE" runat="server" Text='<%# Bind("MODULE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SCREEN ID" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" Visible="false">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtSCREENID" runat="server" Text='<%# Bind("SCREENID") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblSCREENID" runat="server" Text='<%# Bind("SCREENID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SCREEN NAME" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDISPLAYNAME" runat="server" Text='<%# Bind("DISPLAYNAME") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblDISPLAYNAME" runat="server" Text='<%# Bind("DISPLAYNAME") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%-- <asp:TemplateField HeaderText="Status">
                      
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("ACCESS") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Select Data" ItemStyle-Width="1%" ItemStyle-HorizontalAlign="Center">
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

                </asp:GridView>

            </div>
        </div>
        <%--<div>
            <asp:TreeView ID="TreeView1" runat="server" ImageSet="BulletedList2" ShowExpandCollapse="False">
                <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="0px" NodeSpacing="0px" VerticalPadding="0px" />
                <ParentNodeStyle Font-Bold="False" />
                <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px" VerticalPadding="0px" />
                <Nodes>
                    <asp:TreeNode Text="Home" NavigateUrl="~/Home.aspx" Target="_blank" />
                    <asp:TreeNode Text="Employee" NavigateUrl="~/Employee.aspx" Target="_blank">
                        <asp:TreeNode Text="Upload Resume" NavigateUrl="~/Upload_Resume.aspx" Target="_blank" />
                        <asp:TreeNode Text="Edit Resume" NavigateUrl="~/Edit_Resume.aspx" Target="_blank" />
                        <asp:TreeNode Text="View Resume" NavigateUrl="~/View_Resume.aspx" Target="_blank" />
                    </asp:TreeNode>
                    <asp:TreeNode Text="Employer" NavigateUrl="~/Employer.aspx" Target="_blank">
                        <asp:TreeNode Text="Upload Job" NavigateUrl="~/Upload_Job.aspx" Target="_blank" />
                        <asp:TreeNode Text="Edit Job" NavigateUrl="~/Edit_Job.aspx" Target="_blank" />
                        <asp:TreeNode Text="View Job" NavigateUrl="~/View_Job.aspx" Target="_blank" />
                    </asp:TreeNode>
                    <asp:TreeNode Text="Admin" NavigateUrl="~/Admin.aspx" Target="_blank">
                        <asp:TreeNode Text="Add User" NavigateUrl="~/Add_User.aspx" Target="_blank" />
                        <asp:TreeNode Text="Edit User" NavigateUrl="~/Edit_Use.aspx" Target="_blank" />
                        <asp:TreeNode Text="View User" NavigateUrl="~/View_User.aspx" Target="_blank" />
                    </asp:TreeNode>
                </Nodes>
                <RootNodeStyle BackColor="#009900" />
            </asp:TreeView>
        </div>--%>
        <%-- <div>
            <table>
                <tr>
                    <asp:GridView ID="DGV" runat="server" AutoGenerateSelectButton="True"  OnPageIndexChanging="OnPageIndexChanging" PageSize="10" HeaderStyle-BackColor="#ffa500" HeaderStyle-ForeColor="White" BorderColor="Black" GridLines="Both" BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" PagerSettings-Mode="NextPreviousFirstLast" ShowFooter="false" TabIndex="16">
                        <PagerSettings Mode="NextPreviousFirstLast" PageButtonCount="4" PreviousPageText="Previous" NextPageText="Next" FirstPageText="First" LastPageText="Last" />
                    </asp:GridView>
                    <asp:GridView   ID="DGV" runat="server"></asp:GridView>
                </tr>
            </table>
        </div>--%>
        <%-- <div id="divGridViewScroll" style="width: 100%; height: 400px; overflow: scroll">
            <asp:GridView ID="DGV" runat="server" AllowPaging="false" OnPageIndexChanging="OnPageIndexChanging" HeaderStyle-BackColor="#9AD6ED" AutoGenerateSelectButton="false"
                HeaderStyle-ForeColor="White" HeaderStyle-BorderStyle="Solid" OnRowDataBound="DGV_RowDataBound" AlternatingRowStyle-BackColor="WhiteSmoke" OnSelectedIndexChanged="GV_SelectedIndexChanged"
                BorderStyle="Solid">
                <AlternatingRowStyle BackColor="WhiteSmoke"></AlternatingRowStyle>
                <RowStyle BorderStyle="Solid" Width="30px" Height="30px" />


                <HeaderStyle BorderStyle="Solid" Height="50px" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
            </asp:GridView>

        </div>--%>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
