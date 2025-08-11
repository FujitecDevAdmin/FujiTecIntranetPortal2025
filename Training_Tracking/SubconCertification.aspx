<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SubconCertification.aspx.cs" Inherits="FujiTecIntranetPortal.Training_Tracking.SubconCertification" %>

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

        .space {
            width: 10%;
            height: 30px;
        }

        .normal-table1 {
            font-size: 1px !important;
            border: 10px solid White !important;
            padding: 2px !important;
            margin-bottom: 10px !important;
        }

        .normal-table2 {
            font-size: 1px !important;
            border: 2px solid Black !important;
            padding: 2px !important;
            margin-bottom: 10px !important;
        }

        .normal-table3 {
            font-size: 1px !important;
            border: 2px solid White !important;
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
            <h3 class="text-center" style="font-family: Verdana">Sub-Con Certification</h3>
            <table class="normal_grid">
                <tr>
                    <th class="auto-style2"><strong>SC Company Name : <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:DropDownList ID="ddMainSubConName" CssClass="myInput" runat="server" Width="175px" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddMainSubConName_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                    <th class="auto-style2"><strong>Employee Name: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:DropDownList ID="ddEmployeeName" CssClass="myInput" runat="server" Width="175px" TabIndex="2" AutoPostBack="true" OnSelectedIndexChanged="ddEmployeeName_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                    <th class="auto-style2"><strong>ID Card No.: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtIDCardNo" CssClass="myInput" runat="server" Width="175px" TabIndex="3"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th class="auto-style2"><strong>Trained By : <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtTrainedBy" CssClass="myInput" runat="server" Width="175px" TabIndex="4"></asp:TextBox>
                        <%-- <asp:DropDownList ID="ddTrainedBy" CssClass="myInput" runat="server" Width="175px" TabIndex="1" AutoPostBack="true" ></asp:DropDownList>--%>
                    </td>
                    <th class="auto-style2"><strong>Certified By: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtCertifiedBy" CssClass="myInput" runat="server" Width="175px" TabIndex="5"></asp:TextBox>
                        <%--<asp:DropDownList ID="ddCertifiedBy" CssClass="myInput" runat="server" Width="175px" TabIndex="2" AutoPostBack="true" ></asp:DropDownList>--%>
                    </td>
                    <th class="auto-style2"><strong>Certification: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:DropDownList ID="ddCertification" CssClass="myInput" runat="server" Width="175px" TabIndex="6" AutoPostBack="true" OnSelectedIndexChanged="ddCertification_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th class="auto-style2"><strong>Training: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:DropDownList ID="ddTraining" CssClass="myInput" runat="server" Width="175px" TabIndex="7" AutoPostBack="true" Enabled="false"></asp:DropDownList>
                    </td>
                    <th class="auto-style2"><strong>Training date : <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtTrainingdate" CssClass="myInput" runat="server" Width="175px" TabIndex="8" Enabled="false"></asp:TextBox>
                        <%-- <asp:DropDownList ID="ddTrainedBy" CssClass="myInput" runat="server" Width="175px" TabIndex="1" AutoPostBack="true" ></asp:DropDownList>--%>
                    </td>
                    <th class="auto-style2"><strong>Certification status: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:DropDownList ID="ddCertstatus" CssClass="myInput" runat="server" Width="175px" TabIndex="9">
                            <%--<asp:ListItem Text="UnderObservation" Value="UnderObservation"></asp:ListItem>--%>
                            <asp:ListItem Text="Certified" Value="Certified"></asp:ListItem>
                            <asp:ListItem Text="Terminated" Value="Terminated"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table>
                <tr>
                    <td class="auto-style2" colspan="3" align="center">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="myInput" Width="80px"
                            TabIndex="10" OnClick="btnSave_Click"></asp:Button>
                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="myInput" Width="80px"
                            TabIndex="11" OnClick="btnClear_Click"></asp:Button>
                        <asp:Button ID="btnMail" runat="server" Text="Mail" CssClass="myInput" Width="80px"
                            TabIndex="12" OnClick="btnMail_Click"></asp:Button>
                        <asp:Button ID="btnPreview" runat="server" Text="Preview" CssClass="myInput" Width="80px" Visible="false"
                            TabIndex="13" OnClick="btnPreview_Click"></asp:Button>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2" colspan="3" align="center">
                        <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label></td>
                </tr>
            </table>
        </div>
        <div class="normal-table1">
            <asp:GridView ID="gv" runat="server" AutoGenerateSelectButton="True" OnSelectedIndexChanged="GV_SelectedIndexChanged" TabIndex="14"
                OnPageIndexChanging="OnPageIndexChanging" AllowPaging="true" PageSize="10" GridLines="Both" HeaderStyle-BorderStyle="Solid"
                HeaderStyle-BackColor="#9AD6ED" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-BorderStyle="Solid" BorderStyle="Solid" FooterStyle-BorderStyle="Solid"
                HeaderStyle-Height="50px" Width="100%" HeaderStyle-ForeColor="#636363">
            </asp:GridView>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
