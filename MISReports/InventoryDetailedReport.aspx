<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InventoryDetailedReport.aspx.cs" Inherits="FujiTecIntranetPortal.MISReports.InventoryDetailedReport" %>

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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="page-container scroll-y">
        <div>
            <h2 class="text-center">Inventory Detailed Report</h2>
            <table class="normal_grid">
                <tr>
                    <th><strong>Warehouse: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <%-- <strong>Warehouse: <span class="hlt-txt">*</span></strong> --%>
                        <asp:DropDownList ID="ddWareHouse" runat="server" Width="183px" TabIndex="1"></asp:DropDownList>
                    </td>
                    <th><strong>Meeting Date: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtFromDate" CssClass="myInput" runat="server" Width="183px" TabIndex="2"></asp:TextBox><asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/assets/images/cal.jpg" OnClick="ImageButton1_Click" Style="height: 16px" />
                        <asp:Calendar ID="FromCal" runat="server" OnSelectionChanged="FromCalendar1_SelectionChanged" OnDayRender="FromCalendar1_DayRender"></asp:Calendar>
                    </td>
                    <th><strong>Meeting Date: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtTodate" CssClass="myInput" runat="server" Width="183px" TabIndex="3"></asp:TextBox><asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/assets/images/cal.jpg" OnClick="ImageButton2_Click" Style="height: 16px"/>
                        <asp:Calendar ID="Tocal" runat="server" OnSelectionChanged="ToCalendar1_SelectionChanged" OnDayRender="ToCalendar1_DayRender"></asp:Calendar>
                    </td>

                </tr>
                <tr>
                    <th><strong>Part Type Name: <span class="hlt-txt"></span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:DropDownList ID="ddPartTypeName" runat="server" Width="183px" TabIndex="4" OnSelectedIndexChanged="ddPartTypeName_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </td>
                    <th><strong>Part Number/Part Name: <span class="hlt-txt"></span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:DropDownList ID="ddPartNumber" runat="server" Width="183px" TabIndex="5"></asp:DropDownList>
                    </td>
                    <th><strong>Movement Status: <span class="hlt-txt"></span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:DropDownList ID="ddMovementStatus" runat="server" Width="183px" TabIndex="6">
                            <asp:ListItem Text="Select Movement Status" Value="-1"></asp:ListItem>
                            <asp:ListItem Text="A - No Movement in this year" Value="A"></asp:ListItem>
                            <asp:ListItem Text="B - Opening stock+No Purchase-100% Consumed" Value="B"></asp:ListItem>
                            <asp:ListItem Text="C - No Open stock + Purchase - Consumption 100%" Value="C"></asp:ListItem>
                            <asp:ListItem Text="D - Opening stock+Purchase-100% Consumed" Value="D"></asp:ListItem>
                            <asp:ListItem Text="E - Opening stock+Purchase but No Consumption" Value="E"></asp:ListItem>
                            <asp:ListItem Text="F - Opening stock+No Purchase-No Consumption" Value="F"></asp:ListItem>
                            <asp:ListItem Text="G - Opening stock+No Purchase-Minimum Consumption" Value="G"></asp:ListItem>
                            <asp:ListItem Text="H - No Ope Stock+Pruchase - No Consumption" Value="H"></asp:ListItem>
                            <asp:ListItem Text="I - Consumed Less than Op-Stock but Purchased" Value="I"></asp:ListItem>
                            <asp:ListItem Text="J - Normal Moving" Value="J"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table>
                <tr>
                    <td class="auto-style2" colspan="3" align="center">

                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="myInput"
                            TabIndex="7" OnClick="btnSearch_Click"></asp:Button>
                        <%-- <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="myInput"
                            TabIndex="12" OnClick="btnUpdate_Click"></asp:Button>--%>
                        <asp:Button ID="btnDwnldExl" runat="server" Text="Download To Excel" OnClick="btnDwnldExl_Click" />
                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="myInput"
                            TabIndex="8" OnClick="btnClear_Click"></asp:Button>
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
        <%-- <div>
            <table>
                <tr>
                    <asp:GridView ID="DGV" runat="server" AutoGenerateSelectButton="True"  OnPageIndexChanging="OnPageIndexChanging" PageSize="10" HeaderStyle-BackColor="#ffa500" HeaderStyle-ForeColor="White" BorderColor="Black" GridLines="Both" BorderStyle="Solid" HeaderStyle-BorderStyle="Solid" PagerSettings-Mode="NextPreviousFirstLast" ShowFooter="false" TabIndex="16">
                        <PagerSettings Mode="NextPreviousFirstLast" PageButtonCount="4" PreviousPageText="Previous" NextPageText="Next" FirstPageText="First" LastPageText="Last" />
                    </asp:GridView>
                </tr>
            </table>
        </div>--%>
        <div id="divGridViewScroll" style="width: 100%; height: 400px; overflow: scroll">
            <asp:GridView ID="DGV" runat="server"  OnPageIndexChanging="OnPageIndexChanging" HeaderStyle-BackColor="#9AD6ED" AllowPaging="true" PageSize="10"
                HeaderStyle-ForeColor="White" HeaderStyle-BorderStyle="Solid" OnRowDataBound="DGV_RowDataBound" AlternatingRowStyle-BackColor="WhiteSmoke"
                BorderStyle="Solid">
                <AlternatingRowStyle BackColor="WhiteSmoke"></AlternatingRowStyle>
                <RowStyle BorderStyle="Solid" Width="30px" Height="30px" />


                <HeaderStyle BorderStyle="Solid" Height="50px" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                <%--<Columns><asp:DynamicField ItemStyle-BorderStyle="Solid"/></Columns>--%>
                <%--<Columns CssClass="colHeader-RightAlign"></Columns>--%>
            </asp:GridView>

        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>

