<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SubConEmployeeMaster.aspx.cs" Inherits="FujiTecIntranetPortal.Training_Tracking.SubConEmployeeMaster" %>

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

        .normal-table1 {
            font-size: 1px !important;
            border: 10px solid white !important;
            padding: 2px !important;
            margin-bottom: 10px !important;
        }

        .myInput:focus {
            border: solid 1px green !important;
            outline: double !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

    <div class="page-container scroll-y">
        <div>
            <h3 class="text-center" style="font-family: Verdana">Sub-Contract Employee Master</h3>
            <table class="normal_grid">
                <tr>
                    <th class="auto-style2"><strong>Employee ID: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtEmployeeID" CssClass="myInput" runat="server" Width="183px" TabIndex="1"></asp:TextBox>
                    </td>
                    <th class="auto-style2"><strong>Employee Name: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtEmployeeName" CssClass="myInput" runat="server" Width="183px" TabIndex="2"></asp:TextBox>
                    </td>
                    <th class="auto-style2"><strong>Employee Category: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:DropDownList ID="ddEmployeeCategory" CssClass="myInput" runat="server" Width="183px" TabIndex="3" AutoPostBack="true"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th class="auto-style2"><strong>ESI No.: <span class="hlt-txt"></span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtESINo" CssClass="myInput" runat="server" Width="183px" TabIndex="4"></asp:TextBox>
                        <%-- <asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="Integer"
                            ControlToValidate="txtESINo" ErrorMessage="Value must be a whole number" />--%>
                    </td>
                    <th class="auto-style2"><strong>UAN No.: <span class="hlt-txt"></span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtUNINO" CssClass="myInput" runat="server" Width="183px" TabIndex="5"></asp:TextBox>
                    </td>
                    <th class="auto-style2"><strong>Phone No.: <span class="hlt-txt"></span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtPhoneNo" CssClass="myInput" runat="server" Width="183px" TabIndex="6"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th class="auto-style2"><strong>Vendor Code: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:DropDownList ID="ddVendorNo" CssClass="myInput" runat="server" Width="183px" TabIndex="7" AutoPostBack="true" OnSelectedIndexChanged="ddVendorNo_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                    <th class="auto-style2"><strong>Location: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtLocation" CssClass="myInput" runat="server" Width="183px" TabIndex="8"></asp:TextBox>
                    </td>
                    <th class="auto-style2"><strong>State : <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:DropDownList ID="DDSTATE" CssClass="myInput" runat="server" Width="183px" TabIndex="9" AutoPostBack="true"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th class="auto-style2"><strong>Sub-Con Company: <span class="hlt-txt"></span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtsubconCompany" CssClass="myInput" runat="server" Width="183px" TabIndex="10"></asp:TextBox>
                    </td>
                    <th class="auto-style2"><strong>Blood Group: <span class="hlt-txt"></span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtBloodGroup" CssClass="myInput" runat="server" Width="183px" TabIndex="10"></asp:TextBox>
                    </td>
                    <th class="auto-style2"><strong>Select Photo: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:FileUpload ID="FileUpload1" runat="server" TabIndex="11" />
                        <asp:Button ID="Button1" runat="server" Text="Upload" OnClick="btnUpload_Click" Visible="false" />
                    </td>

                </tr>
                <tr>
                    <th class="auto-style2"><strong>Status : <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:DropDownList ID="ddStatus" CssClass="myInput" runat="server" Width="183px" TabIndex="12" AutoPostBack="true"></asp:DropDownList>
                    </td>
                    <th class="auto-style2"><strong>Valid upto : <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtvalidDate" runat="server" Width="180px" TextMode="Date" AutoPostBack="true" ></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table>
                <tr>
                    <td class="auto-style2" colspan="3" align="center">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="myInput"
                            TabIndex="12" OnClick="btnSave_Click"></asp:Button>
                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="myInput"
                            TabIndex="13" OnClick="btnClear_Click"></asp:Button>
                        <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="myInput"
                            TabIndex="14" OnClick="btnPrint_Click"></asp:Button>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2" colspan="3" align="center">
                        <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label></td>
                </tr>

            </table>
        </div>
        <div class="normal-table1">

            <h5 style="font-family: Verdana">Search:
            </h5>
            <asp:DropDownList ID="ddselect" CssClass="myInput" runat="server" Width="150px" TabIndex="14" Font-Size="Small" AutoPostBack="true">
                <asp:ListItem>--Select--</asp:ListItem>
                <asp:ListItem>Vendor</asp:ListItem>
                <asp:ListItem>Employee</asp:ListItem>
                <asp:ListItem>Location</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="txtSearch" runat="server" Width="175px" CssClass="myInput auto-style2" Font-Size="Small" Style="margin-left: 5px" TabIndex="15" />
            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="myInput" Font-Size="Small" Width="80px" Height="30px" OnClick="btnSearch_Click" Style="margin-left: 5px" TabIndex="16" />
            <asp:Button ID="btnDownload" runat="server" Text="Download" CssClass="myInput" Font-Size="Small" Width="80px" Height="30px" OnClick="btnDownload_Click" Style="margin-left: 5px" />
            <hr />
            <asp:GridView ID="gv" runat="server" AutoGenerateSelectButton="True" OnSelectedIndexChanged="GV_SelectedIndexChanged" TabIndex="17"
                OnPageIndexChanging="OnPageIndexChanging" AllowPaging="true" PageSize="10" GridLines="Both" HeaderStyle-BorderStyle="Solid"
                HeaderStyle-BackColor="#9AD6ED" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-BorderStyle="Solid" BorderStyle="Solid" FooterStyle-BorderStyle="Solid"
                HeaderStyle-Height="50px" Width="100%" HeaderStyle-ForeColor="#636363">
            </asp:GridView>
        </div>
        <div><b style="color: white">abc</b></div>
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
