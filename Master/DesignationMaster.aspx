<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DesignationMaster.aspx.cs" Inherits="FujiTecIntranetPortal.Master.DesignationMaster" %>

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
    <%--<asp:Panel runat="server" DefaultButton="btn1" ID="dftbtn">--%>
    <asp:Panel runat="server" SkinID="Default" ID="dft"></asp:Panel>
    <div class="page-container scroll-y">
        <div>
            <h3 class="text-center" style="font-family: Verdana">Designation Master</h3>
            <table class="normal_grid">
                <tr>
                    <th class="auto-style2"><strong>Designation ID: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtDesignationID" CssClass="myInput" runat="server" Width="183px" TabIndex="1" Enabled="false"></asp:TextBox>
                    </td>
                    <th class="auto-style2"><strong>Designation Name: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtDesignationName" CssClass="myInput" runat="server" Width="183px" TabIndex="2"></asp:TextBox>
                    </td>
                  <%--  <th class="auto-style2"><strong>Department Id: <span class="hlt-txt"></span></strong>
                    </th>--%>
                    <td class="auto-style2">
                        <asp:DropDownList ID="ddDepartment" CssClass="myInput" runat="server" Width="183px" TabIndex="3" AutoPostBack="true" Visible="false"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th class="auto-style2"><strong>Status: <span class="hlt-txt"></span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:DropDownList ID="ddStatus" CssClass="myInput" runat="server" Width="183px" TabIndex="4" AutoPostBack="true"></asp:DropDownList>
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
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2" colspan="3" align="center">
                        <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label></td>
                </tr>

            </table>
        </div>
        <div class="normal-table1">

            <%-- <h5 style="font-family: Verdana">Search:
            </h5>
            <asp:DropDownList ID="ddselect" CssClass="myInput" runat="server" Width="150px" TabIndex="8" Font-Size="Small" AutoPostBack="true" Enabled="false">
               <asp:ListItem>--Select--</asp:ListItem>
                <asp:ListItem>Vendor</asp:ListItem>
                <asp:ListItem>Location</asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="txtSearch" runat="server" Width="175px" CssClass="myInput auto-style2" Font-Size="Small" style="margin-left: 5px"  />
            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="myInput" Font-Size="Small" Width="80px" Height="30px" OnClick="btnSearch_Click" style="margin-left: 5px" />
            <hr />--%>

            <asp:GridView ID="gv" runat="server" AutoGenerateSelectButton="True" OnSelectedIndexChanged="GV_SelectedIndexChanged" TabIndex="14"
                OnPageIndexChanging="OnPageIndexChanging" AllowPaging="true" PageSize="10" GridLines="Both" HeaderStyle-BorderStyle="Solid"
                HeaderStyle-BackColor="#9AD6ED" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-BorderStyle="Solid" BorderStyle="Solid" FooterStyle-BorderStyle="Solid"
                HeaderStyle-Height="50px" Width="100%" HeaderStyle-ForeColor="#636363">
            </asp:GridView>
        </div>
    </div>
    <%--</asp:Panel>--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
