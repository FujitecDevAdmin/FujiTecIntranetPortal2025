<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserCreation.aspx.cs" Inherits="FujiTecIntranetPortal.Admin.UserCreation" %>

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
            font-family: Verdana;
        }

        .normal-table1 {
            font-size: 1px !important;
            border: 10px solid white !important;
            padding: 2px !important;
            margin-bottom: 10px !important;
            font-family: Verdana !important;
        }

        .myInput:focus {
            border: solid 1px green !important;
            outline: double !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

    <div class="page-container scroll-y" style="font-family: Verdana">
        <div>
            <h3 class="text-center" style="font-family: Verdana">&nbsp;User Detail Management</h3>
            <table class="normal_grid">
                <tr>
                    <th class="auto-style2" style="font-family: Verdana"><strong>User ID: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtUserID" CssClass="myInput" runat="server" Width="183px" TabIndex="1" Font-Names="Verdana" OnTextChanged="txtUserID_TextChanged" AutoPostBack="true"></asp:TextBox>
                    </td>
                    <th class="auto-style2"><strong>User Name: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtUserName" CssClass="myInput" runat="server" Width="183px" TabIndex="2"></asp:TextBox>
                    </td>
                    <th class="auto-style2"><strong>Branch: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:DropDownList ID="ddBranch" CssClass="myInput" runat="server" Width="183px" TabIndex="3" AutoPostBack="true" ></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th class="auto-style2"><strong>Email Id: <span class="hlt-txt"></span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtEmailId" CssClass="myInput" runat="server" Width="183px" TabIndex="4"></asp:TextBox>
                    </td>
                    <th class="auto-style2"><strong>Mobile No: <span class="hlt-txt"></span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtMobileNo" CssClass="myInput" runat="server" MaxLength="10" Width="183px" TabIndex="5"></asp:TextBox>
                    </td>
                    <th class="auto-style2"><strong>Status: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:DropDownList ID="ddstatus" CssClass="myInput" runat="server" Width="183px" TabIndex="6" AutoPostBack="true">
                            <%--  <asp:ListItem Text="--Select Status--  " Value="-1"></asp:ListItem>--%>
                            <asp:ListItem Text="ACTIVE" Value="MSC0001"></asp:ListItem>
                            <asp:ListItem Text="INACTIVE" Value="MSC0002"></asp:ListItem>
                        </asp:DropDownList>
                    </td>

                </tr>
                <tr>
                    <th class="auto-style2"><strong>Password: <span class="hlt-txt"></span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtPassword" TextMode="Password" CssClass="myInput" runat="server" Width="183px" TabIndex="7"></asp:TextBox>
                    </td>
                    <th class="auto-style2"><strong>Confirm Password: <span class="hlt-txt"></span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtConfirmPassword" TextMode="Password" CssClass="myInput" runat="server" Width="183px" TabIndex="8"></asp:TextBox>
                        <%-- <asp:DropDownList ID="ddDepartment" CssClass="myInput" runat="server" Width="183px" TabIndex="4" AutoPostBack="true"></asp:DropDownList>--%>
                    </td>

                </tr>
            </table>
        </div>
        <div>
            <table>
                <tr>
                    <td class="auto-style2" colspan="3" align="center">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="myInput"
                            TabIndex="9" OnClick="btnSave_Click"></asp:Button>
                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="myInput"
                            TabIndex="10" OnClick="btnClear_Click"></asp:Button>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2" colspan="3" align="center">
                        <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
                    </td>
                </tr>

            </table>
        </div>
        <div class="normal-table1">
            <%--<div id="divGridViewScroll" style="width: 100%; height: 300px; overflow: scroll">--%>
            <%-- <asp:GridView ID="gv" runat="server" AutoGenerateSelectButton="True" OnSelectedIndexChanged="GV_SelectedIndexChanged" Font-Names="Verdana"
                    OnPageIndexChanging="OnPageIndexChanging" AllowPaging="true" PageSize="10" GridLines="Both" HeaderStyle-BorderStyle="Solid"
                    HeaderStyle-BackColor="#9AD6ED" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-BorderStyle="Solid" BorderStyle="Solid" FooterStyle-BorderStyle="Solid"
                    HeaderStyle-Height="50px"  HeaderStyle-ForeColor="#636363" HeaderStyle-Font-Names="Verdana">
                </asp:GridView>--%>
            <%-- <asp:GridView ID="GridView1" runat="server" AutoGenerateSelectButton="True" OnSelectedIndexChanged="GV_SelectedIndexChanged"
                OnPageIndexChanging="OnPageIndexChanging" AllowPaging="true" PageSize="10" GridLines="Both" HeaderStyle-BorderStyle="Solid"
                HeaderStyle-BackColor="#9AD6ED" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-BorderStyle="Solid" BorderStyle="Solid" FooterStyle-BorderStyle="Solid"
                HeaderStyle-Height="50px" Width="100%" HeaderStyle-ForeColor="#636363">
            </asp:GridView>--%>
            <asp:GridView ID="gv" runat="server" OnPageIndexChanging="OnPageIndexChanging" HeaderStyle-BackColor="#9AD6ED" AllowPaging="true" PageSize="10"
                HeaderStyle-ForeColor="White" HeaderStyle-BorderStyle="Solid" AlternatingRowStyle-BackColor="WhiteSmoke" HeaderStyle-Font-Names="Verdana"
                AutoGenerateSelectButton="false" OnSelectedIndexChanged="GV_SelectedIndexChanged" AutoGenerateColumns="false"
                BorderStyle="Solid">
                <AlternatingRowStyle BackColor="WhiteSmoke"></AlternatingRowStyle>
                <RowStyle BorderStyle="Solid" Width="30px" Height="30px" />


                <HeaderStyle BorderStyle="Solid" Height="50px" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                <%--<Columns><asp:DynamicField ItemStyle-BorderStyle="Solid"/></Columns>--%>
                <%--<Columns CssClass="colHeader-RightAlign"></Columns>--%>

                <Columns>
                    <%--<asp:CommandField ShowSelectButton="True" SelectText="Approve" />--%>
                    <%--<asp:CommandField SelectText="View" ShowSelectButton="true" ItemStyle-ForeColor="Black" ItemStyle-Font-Bold="true" />--%>
                    <asp:TemplateField HeaderText="SNO" ItemStyle-HorizontalAlign="Center">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtSNO" runat="server" Text='<%# Bind("SNO") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblSNO" runat="server" Text='<%# Bind("SNO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="USER ID" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtUSERID" runat="server" Text='<%# Bind("USERID") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblUSERID" runat="server" Text='<%# Bind("USERID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="USER NAME" ItemStyle-HorizontalAlign="Center">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtUSERNAME" runat="server" Text='<%# Bind("USERNAME") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblUSERNAME" runat="server" Text='<%# Bind("USERNAME") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="BRANCH" ItemStyle-HorizontalAlign="Center">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtBRANCHDESC" runat="server" Text='<%# Bind("BRANCHDESC") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblBRANCHDESC" runat="server" Text='<%# Bind("BRANCHDESC") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="EMAIL ID" ItemStyle-HorizontalAlign="Center">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEMAILID" runat="server" Text='<%# Bind("EMAILID") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblEMAILID" runat="server" Text='<%# Bind("EMAILID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MOBILE NO" ItemStyle-HorizontalAlign="Center">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtMOBILENO" runat="server" Text='<%# Bind("MOBILENO") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblMOBILENO" runat="server" Text='<%# Bind("MOBILENO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="STATUS" ItemStyle-HorizontalAlign="Center">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtSTATUS" runat="server" Text='<%# Bind("STATUSDESC") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblSTATUS" runat="server" Text='<%# Bind("STATUSDESC") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>

            </asp:GridView>
            <%--</div>--%>
            <div>
                <strong style="color: white">asdf</strong>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
    <div class="footer">
        <p style="text-align: left; color: white">
            <strong>Developed by Information Technology.    
    <span style="float: right; color: white">© 2022 Fujitec India Pvt,Ltd.
    </span>
            </strong>
        </p>
    </div>
</asp:Content>

