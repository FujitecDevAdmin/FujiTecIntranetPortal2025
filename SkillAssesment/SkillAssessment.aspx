<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SkillAssessment.aspx.cs" Inherits="FujiTecIntranetPortal.SkillAssesment.SkillAssessment" %>

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

        .Middle {
            margin: auto;
            width: 80%;
            padding: 50px;
        }

        .auto-style2 {
            width: 17%;
            height: 30px;
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

        .header-center {
            text-align: center;
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
    <asp:Panel runat="server" SkinID="Default" ID="dft"></asp:Panel>
    <div class="page-container scroll-y">
        <div>
            <h3 class="text-center" style="font-family: Verdana">Skill Assessment & Competency</h3>
            <table class="normal_grid">
                <tr>
                    <th class="auto-style2"><strong>Employee ID: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtEmployeeID" CssClass="myInput" runat="server" Width="183px" TabIndex="1" OnTextChanged="txtEmployeeID_TextChanged" AutoPostBack="true"></asp:TextBox>
                    </td>
                    <th class="auto-style2"><strong>Employee Name: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtEmployeeName" CssClass="myInput" runat="server" Width="183px" Enabled="false" TabIndex="2" Style="margin-left: 0"></asp:TextBox>
                    </td>
                    <th class="auto-style2"><strong>Department: <span class="hlt-txt"></span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtDepartment" CssClass="myInput" runat="server" Width="183px" TabIndex="3" Enabled="false" Style="margin-left: 0"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th class="auto-style2"><strong>Desgination: <span class="hlt-txt"></span></strong>
                    </th>
                    <td class="auto-style3">
                        <asp:TextBox ID="txtDesignationID" CssClass="myInput" runat="server" Width="183px" TabIndex="3" Enabled="false" Style="margin-left: 0"></asp:TextBox>
                    </td>
                    <th class="auto-style2"><strong>Remarks: <span class="hlt-txt"></span></strong>
                    </th>
                    <td class="auto-style3">
                        <asp:TextBox ID="txtRemark" CssClass="myInput" runat="server" Width="183px" TabIndex="3" Height="53px" Style="margin-left: 0" Enabled="true"></asp:TextBox>
                    </td>
                    <th class="auto-style2"><strong>Date: <span class="hlt-txt"></span></strong>
                    </th>
                    <td class="auto-style3">
                        <asp:TextBox ID="txtDate" CssClass="myInput" runat="server" Width="183px" TabIndex="3" Height="53px" Style="margin-left: 0" Enabled="false"></asp:TextBox>
                    </td>

                    <td class="auto-style3">
                        <asp:Label ID="lblDepartmentid" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="lblDesignationid" runat="server" Text="" Visible="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                     <th class="auto-style2"><strong>Skill Legends: <span class="hlt-txt"></span></strong>
                    </th>
                    <td>
                        <ul style="color: red; list-style-type: square;">
                            <li>1 - Do not know </li>
                            <li>2 - Knows Partially </li>
                            <li>3 - Do Independently </li>
                            <li>4 - Teach/Train others </li>
                        </ul>
                    </td>
                     <th class="auto-style2"><strong>Competency Legends: <span class="hlt-txt"></span></strong>
                    </th>
                    <td>
                        <ul style="color: red; list-style-type: square;">
                            <li>1 - Needs improvement </li>
                            <li>2 - Satisfactory </li>
                            <li>3 - Good </li>
                            <li>4 - Excellent </li>
                        </ul>
                    </td>
                </tr>
            </table>
        </div>

        <div class="Middle">
            <h5 class="text-center" style="font-family: Verdana">Skill Assessment Detail:</h5>
            <asp:GridView ID="gv" runat="server" AutoGenerateSelectButton="false" TabIndex="17" AutoGenerateColumns="false" HorizontalAlign="Right"
                OnPageIndexChanging="OnPageIndexChanging" AllowPaging="true" PageSize="100" GridLines="Both" HeaderStyle-BorderStyle="Solid" OnRowDataBound="OnRowDataBound"
                HeaderStyle-BackColor="#9AD6ED" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-BorderStyle="Solid" BorderStyle="Solid" FooterStyle-BorderStyle="Solid"
                HeaderStyle-Height="50px" Width="90%" HeaderStyle-ForeColor="#636363" Font-Bold="true">
                <Columns>
                    <asp:BoundField HeaderText="S.NO" DataField="SNO" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <asp:BoundField HeaderText="Type" DataField="SkillType" HeaderStyle-CssClass="header-center" visible="false" > <ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                        <asp:BoundField HeaderText="Type Name" DataField="SkillTypeDesc" HeaderStyle-CssClass="header-center" ><ItemStyle HorizontalAlign="Center" /></asp:BoundField>
                    <asp:BoundField HeaderText="Activities" DataField="SkillDesc" HeaderStyle-CssClass="header-center" />
                    <asp:TemplateField HeaderText="Skill Level">
                        <ItemTemplate>
                            <%-- <asp:Label ID="Skilllevel" runat="server" Text='<%# Eval("Skilllevel") %>' Visible="false" />--%>
                            <asp:DropDownList ID="ddlSkilllevel" runat="server" Width="70px">
                                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                <asp:ListItem Text="4" Value="4"></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div>
            <table>
                <tr>
                    <td class="auto-style2" colspan="3" align="center">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="myInput"
                            TabIndex="12" OnClick="btnSave_Click"></asp:Button>
                        <asp:Button ID="btnPreviewPopup" Text="Preview" runat="server" OnClick="PreviewPopUp" TabIndex="13"/>
                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="myInput"
                            TabIndex="14" OnClick="btnClear_Click"></asp:Button>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2" colspan="3" align="center">
                        <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td class="auto-style2" colspan="3" align="center">
                        <asp:Label ID="Label1" runat="server" Text=""></asp:Label></td>
                </tr>
            </table>
        </div>
    </div>

    <%--</asp:Panel>--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
