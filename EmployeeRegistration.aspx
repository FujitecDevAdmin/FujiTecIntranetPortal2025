<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmployeeRegistration.aspx.cs" Inherits="FujiTecIntranetPortal.EmployeeRegistration" %>

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
            <h3 class="text-center" style="font-family: Verdana">New Joiners Welcome Message</h3>
            <table class="normal_grid">
                <tr>
                    <th class="auto-style2" style="font-family: Verdana"><strong>Employee ID: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtEmployeeID" CssClass="myInput" runat="server" Width="183px" TabIndex="1" Font-Names="Verdana"></asp:TextBox>
                    </td>
                    <th class="auto-style2"><strong>Employee Name: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtEmployeeName" CssClass="myInput" runat="server" Width="183px" TabIndex="2"></asp:TextBox>
                    </td>
                    <th class="auto-style2"><strong>Designation: <span class="hlt-txt"></span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtDesignation" CssClass="myInput" runat="server" Width="183px" TabIndex="3"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <th class="auto-style2"><strong>Department: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:DropDownList ID="ddDepartment" CssClass="myInput" runat="server" Width="183px" TabIndex="4" AutoPostBack="true"></asp:DropDownList>
                    </td>
                    <th class="auto-style2"><strong>Reporting To: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtReportingTo" CssClass="myInput" runat="server" Width="183px" TabIndex="5"></asp:TextBox>
                    </td>
                    <th class="auto-style2"><strong>Qualification To: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtQualification" CssClass="myInput" runat="server" Width="183px" TabIndex="6"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th class="auto-style2"><strong>Email Id: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtEmailId" CssClass="myInput" runat="server" Width="183px" TabIndex="7"></asp:TextBox>
                    </td>
                    <th class="auto-style2"><strong>Mobile No: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtMobileNo" CssClass="myInput" runat="server" MaxLength="10" Width="183px" TabIndex="8"></asp:TextBox>
                    </td>
                    <th class="auto-style2"><strong>Location: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtLocation" CssClass="myInput" runat="server" Width="183px" TabIndex="9"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <th class="auto-style2"><strong>Experience: </strong>&nbsp;</th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtExperience" CssClass="myInput" runat="server" Width="183px" TabIndex="10"></asp:TextBox>
                    </td>
                    <th class="auto-style2"><strong>Select Photo: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:FileUpload ID="FileUpload1" runat="server" TabIndex="11" /><asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" Visible="false" />
                    </td>
                    <th class="auto-style2"><strong>Welcome Message: </strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtWelcomeMsg" CssClass="myInput" runat="server" Width="183px" TabIndex="12" TextMode="MultiLine"></asp:TextBox>
                    </td>

                    <%--<th class="auto-style2"><strong>Select Photo: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:Image ID="Img" runat="server" />
                    </td>   --%>
                </tr>
            </table>
        </div>
        <div>
            <table>
                <tr>
                    <td class="auto-style2" colspan="3" align="center">

                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="myInput"
                            TabIndex="12" OnClick="btnSave_Click"></asp:Button>
                        <%-- <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="myInput"
                            TabIndex="12" OnClick="btnUpdate_Click"></asp:Button>--%>
                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="myInput"
                            TabIndex="13" OnClick="btnClear_Click"></asp:Button>
                        <%--<asp:Button ID="btnback" runat="server" Text="Go To Dashboardpage" CssClass="myInput"
                            TabIndex="14" OnClick="btnback_Click"></asp:Button>--%>
                    </td>

                </tr>
                <tr>
                    <td class="auto-style2" colspan="3" align="center">
                        <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label></td>
                </tr>

            </table>
        </div>
        <div class="normal-table1">
            <div id="divGridViewScroll" style="width: 100%; height: 300px; overflow: scroll">
                <asp:GridView ID="gv" runat="server" AutoGenerateSelectButton="True" OnSelectedIndexChanged="GV_SelectedIndexChanged" Font-Names="Verdana"
                    OnPageIndexChanging="OnPageIndexChanging" AllowPaging="true" PageSize="10" GridLines="Both" HeaderStyle-BorderStyle="Solid"
                    HeaderStyle-BackColor="#9AD6ED" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-BorderStyle="Solid" BorderStyle="Solid" FooterStyle-BorderStyle="Solid"
                    HeaderStyle-Height="50px" Width="200%" HeaderStyle-ForeColor="#636363" HeaderStyle-Font-Names="Verdana">
                </asp:GridView>
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
