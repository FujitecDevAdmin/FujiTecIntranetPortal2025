<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ServiceEmailPowerBISetup.aspx.cs" Inherits="FujiTecIntranetPortal.ServiceEmailPowerBISetup" %>

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
    <div class="page-container scroll-y">
        <div>
            <h3 class="text-center" style="font-family: Verdana">Email Service</h3>
            <table class="normal_grid">
                <tr>
                    <th class="auto-style2"><strong>Project Code: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtProjectCode" CssClass="myInput" runat="server" Width="183px" TabIndex="1"></asp:TextBox>
                    </td>
                    <th class="auto-style2"><strong>Project Description: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtProjectDescription" CssClass="myInput" runat="server" Width="183px" TabIndex="2"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <th class="auto-style2"><strong>To Email: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtToEmail" CssClass="myInput" runat="server" Width="183px" TabIndex="2" TextMode="MultiLine" Height="200px"></asp:TextBox>
                    </td>
                    <th class="auto-style2"><strong>CC Email: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtCCEmail" CssClass="myInput" runat="server" Width="183px" TabIndex="2" TextMode="MultiLine" Height="200px"></asp:TextBox>
                    </td>
                    <th class="auto-style2"><strong>BCC Email: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtBCCEmail" CssClass="myInput" runat="server" Width="183px" TabIndex="2" TextMode="MultiLine" Height="200px" Text="sankar.v@in.fujitec.com,vinothkumar.s@in.fujitec.com,biuser1@fujitec.co.in"></asp:TextBox>
                    </td>
                    <%--<th class="auto-style2"><strong> ToEmails: <span class="hlt-txt"></span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:DropDownList ID="ddStatus" CssClass="myInput" runat="server" Width="183px" TabIndex="3" AutoPostBack="true"></asp:DropDownList>
                    </td>--%>
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
                <asp:ListItem Value="0">--Select--</asp:ListItem>
                <asp:ListItem Value="1">Project code</asp:ListItem>
                <asp:ListItem Value="2">Project Description</asp:ListItem>
                <asp:ListItem Value="3">ToEmaild</asp:ListItem>
                <asp:ListItem Value="4">CCEmaild</asp:ListItem>
                <asp:ListItem Value="5">BCCEmaild</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="txtSearch" runat="server" Width="175px" CssClass="myInput auto-style2" Font-Size="Small" Style="margin-left: 5px" TabIndex="15" />
            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="myInput" Font-Size="Small" Width="80px" Height="30px" OnClick="btnSearch_Click" Style="margin-left: 5px" TabIndex="16" />
            <hr />
            <%--  <asp:GridView ID="gv" runat="server" AutoGenerateSelectButton="True" OnSelectedIndexChanged="GV_SelectedIndexChanged" TabIndex="17"
                OnPageIndexChanging="OnPageIndexChanging" AllowPaging="true" PageSize="10" GridLines="Both" HeaderStyle-BorderStyle="Solid"
                HeaderStyle-BackColor="#9AD6ED" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-BorderStyle="Solid" BorderStyle="Solid" FooterStyle-BorderStyle="Solid"
                HeaderStyle-Height="50px" Width="100%" HeaderStyle-ForeColor="#636363" >
            </asp:GridView>--%>
            <div style="overflow-x: auto; width: 100%;">
                <asp:GridView ID="gv" runat="server" AutoGenerateSelectButton="True" OnSelectedIndexChanged="GV_SelectedIndexChanged"
                    OnPageIndexChanging="OnPageIndexChanging" AllowPaging="true" PageSize="10" GridLines="Both" AutoGenerateColumns ="false"
                    HeaderStyle-BorderStyle="Solid" HeaderStyle-BackColor="#9AD6ED" AlternatingRowStyle-BackColor="WhiteSmoke"
                    RowStyle-BorderStyle="Solid" BorderStyle="Solid" FooterStyle-BorderStyle="Solid"
                    HeaderStyle-Height="50px" Width="100%" HeaderStyle-ForeColor="#636363">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="ID" HeaderStyle-Width="15px" ItemStyle-Width="15px" />
                        <asp:BoundField DataField="ProjectCode" HeaderText="Project Code" HeaderStyle-Width="150px" ItemStyle-Width="150px" />
                        <asp:BoundField DataField="ProjectDescription" HeaderText="Project Description" HeaderStyle-Width="10px" ItemStyle-Width="10px" />
                        <asp:BoundField DataField="ToEmails" HeaderText="To Emails" HeaderStyle-Width="10px" ItemStyle-Width="10px" />
                        <asp:BoundField DataField="CcEmails" HeaderText="Cc Emails" HeaderStyle-Width="10px" ItemStyle-Width="10px" />
                        <asp:BoundField DataField="BccEmails" HeaderText="Bcc Emails" HeaderStyle-Width="10px" ItemStyle-Width="10px" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
