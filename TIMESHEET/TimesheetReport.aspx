<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TimesheetReport.aspx.cs" Inherits="FujiTecIntranetPortal.TIMESHEET.TimesheetReport" %>

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
            width: 15%;
            height: 30px;
        }

        .auto-style3 {
            width: 100px;
            height: 30px;
        }
       /* .auto-style2 {
            width: 17%;
            height: 30px;
        }*/

        .colHeader-RightAlign {
            text-align: right !important;
        }

        .myInput:focus {
            border: solid 1px green !important;
            outline: double !important;
        }

        .rightpane {
            width: 70%;
            position: relative;
            float: right;
            background-color: white;
            border-collapse: collapse;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="page-container scroll-y" >
        <div>
            <h2 class="text-center" style="font-family: Verdana">Timesheet Report</h2>
            <table class="normal_grid">
                <tr>
                    <th class="auto-style3"><strong>From Date: <span class="hlt-txt"></span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtFromDate" CssClass="myInput" runat="server" Width="170px" TabIndex="1"></asp:TextBox><asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/assets/images/cal.jpg" OnClick="ImageButton1_Click" Style="height: 16px" />
                        <asp:Calendar ID="FromCal" runat="server" OnSelectionChanged="FromCalendar1_SelectionChanged" OnDayRender="FromCalendar1_DayRender"></asp:Calendar>
                    </td>
                     <th class="auto-style3"><strong>User ID: <span class="hlt-txt"></span></strong>
                    </th>
                    <td class="auto-style2" align="center">
                         <asp:TextBox ID="txtAssignedUser" runat="server" Width="60px" AutoPostBack="true" OnTextChanged="txtAssignedFilter_TextChanged"></asp:TextBox>
                        <asp:DropDownList ID="dduser" runat="server" Width="110px"></asp:DropDownList>
                    </td>
                     <th class="auto-style3"><strong>Project: <span class="hlt-txt"></span></strong>
                    </th>
                    <td class="auto-style2" align="center">
                         <asp:TextBox ID="txtProjectFilter" runat="server" Width="60px" AutoPostBack="true" OnTextChanged="txtProjectFilter_TextChanged"></asp:TextBox>
                        <asp:DropDownList ID="ddProject" runat="server" Width="110px"></asp:DropDownList>
                    </td>     
                                 
                       <th class="auto-style3"><strong>Area: <span class="hlt-txt"></span></strong>
                    </th>
                    <td class="auto-style2" align="center">
                        <asp:TextBox ID="txtArea" CssClass="myInput" runat="server" Width="170px" TabIndex="1"></asp:TextBox>
                    </td>

                </tr>
                 <tr>
                     <th class="auto-style3"><strong>To Date: <span class="hlt-txt"></span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtTodate" CssClass="myInput" runat="server" Width="170px" TabIndex="1"></asp:TextBox><asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/assets/images/cal.jpg" OnClick="ImageButton2_Click" Style="height: 16px" />
                        <asp:Calendar ID="Tocal" runat="server" OnSelectionChanged="ToCalendar1_SelectionChanged" OnDayRender="ToCalendar1_DayRender"></asp:Calendar>
                    </td>
                       <th class="auto-style3"><strong>Task Type: <span class="hlt-txt"></span></strong>
                    </th>
                    <td class="auto-style2" align="center">
                        <asp:DropDownList ID="ddTaskType" runat="server" Width="170px"></asp:DropDownList>
                    </td>
                      <th class="auto-style3"><strong>Task: <span class="hlt-txt"></span></strong>
                    </th>
                    <td class="auto-style2" align="center">
                        <asp:DropDownList ID="ddTask" runat="server" Width="170px"></asp:DropDownList>
                    </td>
                                 
                  
                    <th class="auto-style3"><strong>Status: <span class="hlt-txt"></span></strong>
                    </th>
                    <td class="auto-style2" align="center">
                        <asp:DropDownList ID="ddStatus" runat="server" Width="170px"></asp:DropDownList>
                    </td>                 
                </tr>
            </table>
        </div>
        <div>
            <table>
                <tr>
                    <td class="auto-style2" colspan="3" align="center">

                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="myInput"
                            TabIndex="12" OnClick="btnSearch_Click"></asp:Button>
                        <%-- <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="myInput"
                            TabIndex="12" OnClick="btnUpdate_Click"></asp:Button>--%>
                        <asp:Button ID="btnDwnldExl" runat="server" Text="Download Excel" OnClick="btnDwnldExl_Click" />
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

        <div id="divGridViewScroll" style="width: 100%; height: 400px; overflow: scroll">
            <asp:GridView ID="DGV" runat="server" AllowPaging="false" OnPageIndexChanging="OnPageIndexChanging" HeaderStyle-BackColor="#9AD6ED" AutoGenerateSelectButton="false"
                HeaderStyle-ForeColor="White" HeaderStyle-BorderStyle="Solid"  AlternatingRowStyle-BackColor="WhiteSmoke" OnSelectedIndexChanged="GV_SelectedIndexChanged"
                BorderStyle="Solid">
                <AlternatingRowStyle BackColor="WhiteSmoke"></AlternatingRowStyle>
                <RowStyle BorderStyle="Solid" Width="30px" Height="30px" />
                <HeaderStyle BorderStyle="Solid" Height="50px" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
