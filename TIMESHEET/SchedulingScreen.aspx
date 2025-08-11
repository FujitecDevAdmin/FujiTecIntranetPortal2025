<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SchedulingScreen.aspx.cs" Inherits="FujiTecIntranetPortal.TIMESHEET.SchedulingScreen" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <%--<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>--%>
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
            width: 6px;
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


    <%--</asp:ScriptManager>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

    <div class="page-container scroll-y" >
        <div>
            <h3 class="text-center" style="font-family: Verdana">Project Schedule</h3>
            <table class="normal_grid">
                <tr>
                    <th class="auto-style3"><strong>Work Assigned To: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                         <asp:TextBox ID="txtAssignedUser" runat="server" Width="60px" TabIndex="1" AutoPostBack="true" OnTextChanged="txtAssignedFilter_TextChanged"></asp:TextBox>
                        <asp:DropDownList ID="ddAssignedUser" CssClass="myInput" runat="server" Width="110px" TabIndex="2" AutoPostBack="true" OnSelectedIndexChanged="ddAssignedUser_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                    <th class="auto-style3"><strong>Task Type: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style3">
                        <asp:DropDownList ID="ddTaskType" CssClass="myInput" runat="server" Width="170px" TabIndex="3">
                        </asp:DropDownList>
                    </td>
                    <th class="auto-style3"><strong>Task: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:DropDownList ID="ddTask" CssClass="myInput" runat="server" Width="170px" TabIndex="4" AutoPostBack="true"></asp:DropDownList>
                    </td>    
                    
                     <th class="auto-style3"><strong>Assign/Reassign: <span class="hlt-txt"></span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:DropDownList ID="ddReassign" CssClass="myInput" runat="server" Width="170px" TabIndex="5" AutoPostBack="true">
                            <asp:ListItem Text="Assign" Value="A"></asp:ListItem>
                            <asp:ListItem Text="Reassign" Value="R"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th class="auto-style3"><strong>Project : <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtProjectFilter" runat="server" Width="60px" TabIndex="6" AutoPostBack="true" OnTextChanged="txtProjectFilter_TextChanged"></asp:TextBox>
                        <asp:DropDownList ID="ddProject" CssClass="myInput" runat="server" Width="110px" TabIndex="7" AutoPostBack="true" OnSelectedIndexChanged="ddProj_SelectedIndexChanged"></asp:DropDownList>
                       
                          
                    </td>
                     <th class="auto-style3">
                         <%--<strong>Project list: <span class="hlt-txt">*</span></strong>--%>
                         <div><asp:Button ID="Button1" runat="server"  OnClick="Button1_Click" Text="Add" Width="35px" TabIndex="8" />
                            <asp:Button ID="Button2" runat="server"  Text="Del" Width="35px" OnClick="DeleteItem_Click" TabIndex="9" /></div>
                    </th>
                    <td> 
                          <asp:ListBox ID="lstProj" runat="server" Width="170px" TabIndex="10" SelectionMode="Multiple"></asp:ListBox> 
                    </td>                   

                     <th class="auto-style3"><strong>Area: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:DropDownList ID="ddArea" CssClass="myInput" runat="server" Width="170px" TabIndex="11" AutoPostBack="true" OnSelectedIndexChanged="ddArea_SelectedIndexChanged"></asp:DropDownList>
                        <asp:TextBox ID="txtArea" CssClass="myInput" runat="server" Width="170px" TabIndex="12"></asp:TextBox>
                    </td>
                     <th class="auto-style3"><strong>Remarks: <span class="hlt-txt"></span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtRemarks" CssClass="myInput" runat="server" Width="170px" TabIndex="13"></asp:TextBox>
                    </td>                    
                </tr>
                <tr>
                     <th class="auto-style3"><strong>Product: <span class="hlt-txt"></span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtProduct" CssClass="myInput" runat="server" Width="170px" TabIndex="14"></asp:TextBox>
                        <%--<asp:DropDownList ID="ddCertifiedBy" CssClass="myInput" runat="server" Width="170px" TabIndex="2" AutoPostBack="true" ></asp:DropDownList>--%>
                    </td>
                    <th class="auto-style3"><strong>Capacity (Kg): <span class="hlt-txt"></span></strong>
                    </th>
                    <td class="auto-style2">
                        <%--<asp:DropDownList ID="ddCapacity" CssClass="myInput" runat="server" Width="170px" TabIndex="6" AutoPostBack="true" OnSelectedIndexChanged="ddCertification_SelectedIndexChanged"></asp:DropDownList>--%>
                        <asp:TextBox ID="txtCapacity" CssClass="myInput" runat="server" Width="170px" TabIndex="15"></asp:TextBox>
                    </td>
                   
                     <th class="auto-style3"><strong>Speed: <span class="hlt-txt"></span></strong>
                    </th>
                    <td class="auto-style2">
                        <%--<asp:DropDownList ID="ddCapacity" CssClass="myInput" runat="server" Width="170px" TabIndex="6" AutoPostBack="true" OnSelectedIndexChanged="ddCertification_SelectedIndexChanged"></asp:DropDownList>--%>
                        <asp:TextBox ID="txtSpeed" CssClass="myInput" runat="server" Width="170px" TabIndex="16"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                     <th class="auto-style3"><strong>Start Date: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtFromDate" CssClass="myInput" runat="server" Width="170px" TabIndex="17"></asp:TextBox><asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/assets/images/cal.jpg" OnClick="ImageButton1_Click" Style="height: 16px" />
                        <asp:Calendar ID="FromCal" runat="server" OnSelectionChanged="FromCalendar1_SelectionChanged" OnDayRender="FromCalendar1_DayRender"></asp:Calendar>
                    </td>
                    <th class="auto-style3"><strong>End Date: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtTodate" CssClass="myInput" runat="server" Width="170px" TabIndex="18" AutoPostBack="true" OnTextChanged="txtTodate_TextChanged"></asp:TextBox><asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/assets/images/cal.jpg" OnClick="ImageButton2_Click" Style="height: 16px" />
                        <asp:Calendar ID="Tocal" runat="server" OnSelectionChanged="ToCalendar1_SelectionChanged" OnDayRender="ToCalendar1_DayRender"></asp:Calendar>
                    </td>
                    <th class="auto-style3"><strong>Planned Hours: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtPlannedHours" CssClass="myInput" runat="server" Width="170px" TabIndex="19" Text="0" TextMode="Number" placeholder="0"></asp:TextBox>
                        <%--<asp:DropDownList ID="ddCertifiedBy" CssClass="myInput" runat="server" Width="170px" TabIndex="2" AutoPostBack="true" ></asp:DropDownList>--%>
                    </td>

                     <th class="auto-style3"><strong>Status: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:DropDownList ID="ddStatus" CssClass="myInput" runat="server" Width="170px" TabIndex="20" AutoPostBack="true"></asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table>
                <tr>
                    <td class="auto-style2" colspan="3" align="center">
                        <asp:Button ID="btnSave" runat="server" Text="Add/Save" CssClass="myInput" Width="80px"
                            TabIndex="21" OnClick="btnSave_Click"></asp:Button>
                        <asp:Button ID="btnClear" runat="server" Text="New" CssClass="myInput" Width="80px"
                            TabIndex="22" OnClick="btnClear_Click"></asp:Button>
                        <%-- <asp:Button ID="btnMail" runat="server" Text="Mail" CssClass="myInput" Width="80px"
                            TabIndex="12" OnClick="btnMail_Click"></asp:Button>
                        <asp:Button ID="btnPreview" runat="server" Text="Preview" CssClass="myInput" Width="80px" Visible="false"
                            TabIndex="13" OnClick="btnPreview_Click"></asp:Button>--%>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2" colspan="3" align="center">
                        <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label></td>
                </tr>
            </table>
        </div>
        <div class="normal-table1">
            <asp:GridView ID="gv" runat="server" OnSelectedIndexChanged="GV_SelectedIndexChanged" TabIndex="23" AutoGenerateSelectButton="true"
                OnPageIndexChanging="OnPageIndexChanging" AllowPaging="true" PageSize="10" GridLines="Both" HeaderStyle-BorderStyle="Solid"
                HeaderStyle-BackColor="#9AD6ED" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-BorderStyle="Solid" BorderStyle="Solid" FooterStyle-BorderStyle="Solid"
                HeaderStyle-Height="50px" Width="100%" HeaderStyle-ForeColor="#636363">
            </asp:GridView>
        </div>
    </div>
    <%--  <script type="text/javascript">  
        $(function () {
            $('[id*=lstEmployee]').multiselect({
                includeSelectAllOption: true
            });
        });
    </script>--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
