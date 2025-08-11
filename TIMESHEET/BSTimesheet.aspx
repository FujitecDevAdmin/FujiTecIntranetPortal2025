<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BSTimesheet.aspx.cs" Inherits="FujiTecIntranetPortal.TIMESHEET.BSTimesheet" %>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <style>
        html, body {
            height: 100%;
            margin: 0;
            padding: 0;
            overflow: auto;
        }

        .form-container {
            width: 100%;
            padding: 10px;
            background-color: #f8f9fa;
            border-radius: 6px;
            box-shadow: 0 0 6px rgba(0,0,0,0.08);
            overflow: auto;
        }

        .myInput, .myInputTall, .scrollable-grid input, .scrollable-grid select {
            padding: 3px;
            font-size: 12px;
            border: 1px solid #ccc;
            border-radius: 3px;
            width: 100%;
            box-sizing: border-box;
        }

        .myInputTall {
            height: 30px;
        }

        table.normal_grid th {
            text-align: left;
            font-size: 12px;
            padding: 4px;
        }

        table.normal_grid td {
            padding: 2px;
        }

        table.normal_grid {
            width: 100%;
            border-collapse: separate;
            border-spacing: 5px;
        }

        .btn-save, .btn-clear, .btn-blue {
            padding: 4px 10px;
            font-size: 12px;
            margin: 0 4px;
            border-radius: 3px;
            border: none;
            cursor: pointer;
        }

        .btn-save {
            background-color: #28a745;
            color: white;
        }

        .btn-clear {
            background-color: #dc3545;
            color: white;
        }

        .btn-blue {
            background-color: #007bff;
            color: white;
        }

        .scrollable-grid {
            max-height: 220px;
            overflow: auto;
            border: 1px solid #ccc;
            margin-top: 10px;
        }

        .scrollable-grid table {
            width: 100%;
            border-collapse: collapse;
            font-size: 12px;
        }

        .scrollable-grid th, .scrollable-grid td {
            padding: 4px;
            border: 1px solid #ddd;
        }

        h2 {
            font-size: 18px;
            margin-bottom: 15px;
        }

        .mandatory {
            color: red;
        }

    </style>

    <h2>Business Support Timesheet</h2>

    <div class="form-container">
        <table class="normal_grid">
            <tr>
                <th>Enquiry No: <span class="mandatory">*</span></th>
                <td><asp:TextBox ID="txtEnquiryNo" CssClass="myInput" runat="server" /></td>
                <th>Project No: <span class="mandatory">*</span></th>
                <td><asp:TextBox ID="txtProjectNo" CssClass="myInput" runat="server" /></td>
                <th>Project Name: <span class="mandatory">*</span></th>
                <td><asp:TextBox ID="txtProjectName" CssClass="myInput" runat="server" /></td>
                <th>Task: <span class="mandatory">*</span></th>
                <td><asp:DropDownList ID="ddTask" CssClass="myInput" runat="server" /></td>
            </tr>
            <tr>
                <th>Task Type: <span class="mandatory">*</span></th>
                <td><asp:DropDownList ID="ddTaskType" CssClass="myInput" runat="server" /></td>
                <th>Reference No: <span class="mandatory">*</span></th>
                <td><asp:TextBox ID="txtReferenceNo" CssClass="myInput" runat="server" /></td>
                <th>Checked By:</th>
                <td><asp:TextBox ID="txtCheckedBy" CssClass="myInput" runat="server" /></td>
                <th>Working Environment: <span class="mandatory">*</span></th>
                <td>
                    <asp:DropDownList ID="ddWorkingEnvironment" CssClass="myInput" runat="server">
                        <asp:ListItem Text="-- Select --" Value="" />
                        <asp:ListItem Text="Work from Home" Value="WFH" />
                        <asp:ListItem Text="Office" Value="Office" />
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>Planned Start Date:</th>
                <td><asp:TextBox ID="txtPlannedStart" CssClass="myInput" runat="server" /></td>
                <th>Planned End Date:</th>
                <td><asp:TextBox ID="txtPlannedEnd" CssClass="myInput" runat="server" /></td>
                <th>Planned Hours:</th>
                <td><asp:TextBox ID="txtPlannedHours" CssClass="myInput" runat="server" Text="0" /></td>
                <th>Status: <span class="mandatory">*</span></th>
                <td><asp:DropDownList ID="ddStatus" CssClass="myInput" runat="server" /></td>
            </tr>
            <tr>
                <th>Actual Start Date: <span class="mandatory">*</span></th>
                <td>
                    <asp:TextBox ID="txtActualStart" runat="server" TextMode="Date" CssClass="myInput"
                        AutoPostBack="true" OnTextChanged="txtActualStart_TextChanged"></asp:TextBox>
                </td>
                <th>Actual End Date: <span class="mandatory">*</span></th>
                <td>
                    <asp:TextBox ID="txtActualEnd" runat="server" TextMode="Date" CssClass="myInput"
                        AutoPostBack="true" OnTextChanged="txtActualEnd_TextChanged"></asp:TextBox>
                </td>
                <th>Actual Hours: <span class="mandatory">*</span></th>
                <td><asp:TextBox ID="txtActualHours" CssClass="myInput" runat="server" Text="0" /></td>
                <th>Remarks:</th>
                <td><asp:TextBox ID="txtRemarks" CssClass="myInputTall" runat="server" TextMode="MultiLine" Rows="3" /></td>
            </tr>
        </table>

        <div style="margin-top: 10px; text-align: center;">
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn-save" />
            <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" CssClass="btn-clear" />
            <asp:Button ID="btnPendingTask" runat="server" Text="Pending Task" OnClick="btnPendingTask_Click" CssClass="btn-blue" />
        </div>

        <div style="margin-top: 5px; text-align: center;">
            <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Font-Size="12px"></asp:Label>
        </div>

        <div class="scrollable-grid">
            <asp:GridView ID="gvPendingTasks" runat="server" AutoGenerateColumns="False" DataKeyNames="ScheduleID,TaskID,TaskTypeID,StatusID,PlannedStartDate,PlannedEndDate" CssClass="table table-bordered" OnSelectedIndexChanged="gvPendingTasks_SelectedIndexChanged">
                <Columns>
                    <asp:CommandField ShowSelectButton="True" SelectText="Select" />
                    <asp:BoundField DataField="ScheduleID" HeaderText="ID" />
                    <asp:BoundField DataField="EnquiryNo" HeaderText="Enquiry No" />
                    <asp:BoundField DataField="ProjectNo" HeaderText="Project No" />
                    <asp:BoundField DataField="Projectname" HeaderText="Project Name" />
                    <asp:BoundField DataField="TaskName" HeaderText="Task" />
                    <asp:BoundField DataField="TaskTypeName" HeaderText="Task Type" />
                    <asp:BoundField DataField="ReferenceNo" HeaderText="Reference No" />
                    <asp:BoundField DataField="PlannedStartDate" HeaderText="From Date" />
                    <asp:BoundField DataField="PlannedEndDate" HeaderText="To Date" />
                    <asp:BoundField DataField="PlannedHours" HeaderText="Planned Hours" />
                    <asp:BoundField DataField="Checkedby" HeaderText="Checkedby" />
                    <asp:BoundField DataField="StatusName" HeaderText="Status" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
