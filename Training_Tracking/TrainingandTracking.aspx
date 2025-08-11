<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TrainingandTracking.aspx.cs" Inherits="FujiTecIntranetPortal.Training_Tracking.TrainingandTracking" %>

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
            <h3 class="text-center" style="font-family: Verdana">Sub-Contractor Training Tracking</h3>
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
                    <th class="auto-style2"><strong>ESI No.: <span class="hlt-txt"></span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtESINo" CssClass="myInput" runat="server" Width="175px" TabIndex="4"></asp:TextBox>
                    </td>
                    <th class="auto-style2"><strong>UAN No.: <span class="hlt-txt"></span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtUNINO" CssClass="myInput" runat="server" Width="175px" TabIndex="5"></asp:TextBox>
                    </td>
                    <th class="auto-style2"><strong>Category: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:DropDownList ID="ddcategory" CssClass="myInput" runat="server" Width="175px" TabIndex="6" AutoPostBack="true"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th class="auto-style2"><strong>Location: <span class="hlt-txt"></span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtLocation" CssClass="myInput" runat="server" Width="175px" TabIndex="7"></asp:TextBox>
                    </td>
                    <th class="auto-style2"><strong>State: <span class="hlt-txt"></span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:DropDownList ID="ddState" CssClass="myInput" runat="server" Width="175px" TabIndex="8" AutoPostBack="true"></asp:DropDownList>
                    </td>
                    <th class="auto-style2"><strong>Phone No.: <span class="hlt-txt"></span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtPhoneNo" CssClass="myInput" runat="server" Width="175px" TabIndex="9"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th class="auto-style2"><strong>Agreement Signed & in Live : <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:RadioButtonList ID="rbtnagreement" runat="server" RepeatDirection="Horizontal" TabIndex="10">
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="2">No</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <th class="auto-style2"><strong>Contractor Status : <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <%-- <asp:DropDownList ID="ddconStatus" CssClass="myInput" runat="server" Width="175px" TabIndex="11" AutoPostBack="true"></asp:DropDownList>--%>
                        <asp:RadioButtonList ID="rbtnconStatus" runat="server" RepeatDirection="Horizontal" TabIndex="11">
                            <asp:ListItem Value="1">Active</asp:ListItem>
                            <asp:ListItem Value="2">Discontinued</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <th class="auto-style2"><strong>Employee Status : <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:RadioButtonList ID="rbtnEmpstatus" runat="server" RepeatDirection="Horizontal" TabIndex="12">
                            <asp:ListItem Value="1">Active</asp:ListItem>
                            <asp:ListItem Value="2">Discontinued</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>

                </tr>
                <tr>
                     <th class="auto-style2"><strong>Department: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:DropDownList ID="ddDepartment" CssClass="myInput" runat="server" Width="175px" TabIndex="13" AutoPostBack="true">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem>Installation</asp:ListItem>
                            <asp:ListItem>Service</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <th class="auto-style2"><strong>Nature of Work: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:DropDownList ID="ddNatureofWork" CssClass="myInput" runat="server" Width="175px" TabIndex="14" AutoPostBack="true">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem>Store</asp:ListItem>
                            <asp:ListItem>Shifting</asp:ListItem>
                            <asp:ListItem>Barricades</asp:ListItem>
                            <asp:ListItem>Scaffolding</asp:ListItem>
                            <asp:ListItem>Electrical</asp:ListItem>
                            <asp:ListItem>Civil</asp:ListItem>
                            <asp:ListItem>Erection</asp:ListItem>
                            <asp:ListItem>Hoisting</asp:ListItem>
                            <asp:ListItem>SVC</asp:ListItem>
                            <asp:ListItem>Repair</asp:ListItem>

                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
        <div class="normal-table2">
            <h3 class="text-center" style="font-family: Verdana">Training Details</h3>
            <table>
                <tr>
                     <th class="auto-style2"><strong>Training Type: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:DropDownList ID="ddTrainingName" CssClass="myInput" runat="server" Width="175px" TabIndex="15" AutoPostBack="true"  OnSelectedIndexChanged="ddTrainingModule_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                    <th class="auto-style2"><strong>Training Module: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:DropDownList ID="ddTrainingModule" CssClass="myInput" runat="server" Width="175px" TabIndex="16" AutoPostBack="true"></asp:DropDownList>
                    </td>
                   

                </tr>
                <tr>
                    <th class="auto-style2"><strong>Planned Date <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtPlannedDate" CssClass="myInput" runat="server" Width="150px" TabIndex="17"></asp:TextBox><asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/assets/images/cal.jpg" OnClick="ImageButton1_Click" Style="height: 16px" />
                        <asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChanged" OnDayRender="Calendar1_DayRender"></asp:Calendar>
                    </td>
                    <th class="auto-style2"><strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Actual Date <span class="hlt-txt"></span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtActualDate" CssClass="myInput" runat="server" Width="150px" TabIndex="18"></asp:TextBox><asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/assets/images/cal.jpg" OnClick="ImageButton2_Click" Style="height: 16px" />
                        <asp:Calendar ID="Calendar2" runat="server" OnSelectionChanged="Calendar2_SelectionChanged" OnDayRender="Calendar2_DayRender"></asp:Calendar>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2" colspan="2" align="right" style="padding: 2px;">
                        <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="myInput" Width="80px"
                            TabIndex="19" OnClick="btnAdd_Click"></asp:Button>
                    </td>
                    <td class="auto-style2" style="padding: 2px;">
                        <asp:Button ID="btnTrainingClear" runat="server" Text="Clear" CssClass="myInput" Width="80px"
                            TabIndex="20" OnClick="btnTrainingClear_Click"></asp:Button>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:GridView ID="gv" runat="server" AutoGenerateSelectButton="false" AutoGenerateEditButton="true"  AutoGenerateDeleteButton="true" OnSelectedIndexChanged="GV_SelectedIndexChanged" AutoGenerateColumns="false"
                            OnPageIndexChanging="OnPageIndexChanging" AllowPaging="true" PageSize="10" GridLines="Both" HeaderStyle-BorderStyle="Solid" TabIndex="21"
                            HeaderStyle-BackColor="#9AD6ED" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-BorderStyle="Solid" BorderStyle="Solid" FooterStyle-BorderStyle="Solid"
                             OnRowDataBound="OnRowDataBound" OnRowEditing="OnRowEditing" OnRowCancelingEdit="OnRowCancelingEdit" OnRowUpdating="OnRowUpdating" OnRowDeleting="OnRowDeleting" 
                            HeaderStyle-Height="50px" Width="180%" HeaderStyle-ForeColor="#636363">
                            <Columns>
                                <asp:BoundField DataField="TrainingModule" HeaderText="Training Module" />
                                <asp:BoundField DataField="TrainingName" HeaderText="Training Name" />
                                <asp:BoundField DataField="PlannedDate" HeaderText="Planned Date" />
                                <asp:BoundField DataField="ActualDate" HeaderText="Actual Date" />
                               <%-- <asp:TemplateField HeaderText="Actual Date">
                                    <ItemTemplate>
                                        <%# Eval("ActualDate") %>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table>
                <tr>
                    <td class="auto-style2" colspan="3" align="center">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="myInput" Width="80px"
                            TabIndex="22" OnClick="btnSave_Click"></asp:Button>
                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="myInput" Width="80px"
                            TabIndex="23" OnClick="btnClear_Click"></asp:Button>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2" colspan="3" align="center">
                        <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label></td>
                </tr>
            </table>
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
