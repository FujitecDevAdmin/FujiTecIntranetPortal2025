<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BSTimesheetReport.aspx.cs" Inherits="FujiTecIntranetPortal.TIMESHEET.BSTimesheetReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server" />

<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <style>
        body, html {
            margin: 0;
            padding: 0;
            height: 100%;
            font-family: 'Segoe UI', sans-serif;
            font-size: 12px;
        }

        .page-scroll-container {
            padding: 10px;
            box-sizing: border-box;
            width: 100%;
            overflow-y: auto;
        }

        .filter-bar {
            display: flex;
            flex-wrap: wrap;
            gap: 10px;
            background: #f3f3f3;
            border: 1px solid #ccc;
            border-radius: 6px;
            padding: 10px;
            margin-bottom: 15px;
        }

        .filter-group {
            display: flex;
            flex-direction: column;
            min-width: 130px;
            flex: 1;
        }

        .filter-group label {
            font-weight: 500;
            margin-bottom: 3px;
            font-size: 11px;
        }

        .filter-group input[type="text"],
        .filter-group input[type="date"],
        .filter-group select {
            padding: 4px;
            font-size: 11px;
            border: 1px solid #ccc;
            border-radius: 3px;
        }

        .btn-row {
            display: flex;
            gap: 8px;
            margin-top: 18px;
            flex-wrap: wrap;
        }

        .btn-search {
            background-color: #28a745;
            color: white;
            border: none;
            padding: 6px 12px;
            font-size: 11px;
            font-weight: bold;
            border-radius: 4px;
            cursor: pointer;
        }
        .btn-clear {
            background-color: #dc3545;
            color: white;
            border: none;
            padding: 6px 12px;
            font-size: 11px;
            font-weight: bold;
            border-radius: 4px;
            cursor: pointer;
        }
        .btn-export {
            background-color: #007bff;
            color: white;
            border: none;
            padding: 6px 12px;
            font-size: 11px;
            font-weight: bold;
            border-radius: 4px;
            cursor: pointer;
        }

        .btn-search:hover {
            background-color: #333;
        }

        .scrollable-grid {
            overflow-x: auto;
            border: 1px solid #ccc;
            border-radius: 5px;
            box-shadow: 0 0 5px rgba(0,0,0,0.05);
        }

        .styled-grid {
            width: max-content;
            min-width: 100%;
            border-collapse: collapse;
        }

        .styled-grid th, .styled-grid td {
            padding: 6px 8px;
            border: 1px solid #ddd;
            font-size: 11px;
            white-space: nowrap;
            text-align: center;
        }

        .styled-grid th {
            background-color: #3e3e3e;
            color: white;
            position: sticky;
            top: 0;
            z-index: 2;
        }

        .styled-grid tr:nth-child(even) {
            background-color: #f7f7f7;
        }

        .styled-grid tr:hover {
            background-color: #e0f0ff;
        }
    </style>

    <div class="page-scroll-container">
        <div class="filter-bar">
            <div class="filter-group">
                <label for="txtUserId">User ID</label>
                <asp:TextBox ID="txtUserId" runat="server" />
            </div>
            <div class="filter-group">
                <label for="txtFromDate">From Date</label>
                <asp:TextBox ID="txtFromDate" runat="server" TextMode="Date" />
            </div>
            <div class="filter-group">
                <label for="txtToDate">To Date</label>
                <asp:TextBox ID="txtToDate" runat="server" TextMode="Date" />
            </div>
            <div class="filter-group">
                <label for="txtEnquiryNo">Enquiry No</label>
                <asp:TextBox ID="txtEnquiryNo" runat="server" />
            </div>
            <div class="filter-group">
                <label for="ddTask">Task</label>
                <asp:DropDownList ID="ddTask" runat="server" />
            </div>
            <div class="filter-group">
                <label for="ddTaskType">Task Type</label>
                <asp:DropDownList ID="ddTaskType" runat="server" />
            </div>
            <div class="filter-group">
                <label for="ddStatus">Status</label>
                <asp:DropDownList ID="ddStatus" runat="server" />
            </div>
            <div class="filter-group">
                <label for="txtAssignedBy">Assigned By</label>
                <asp:TextBox ID="txtAssignedBy" runat="server" />
            </div>
            <div class="btn-row">
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn-search" OnClick="btnSearch_Click" />
                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn-clear" OnClick="btnClear_Click" />
                <asp:Button ID="btnExport" runat="server" Text="Export" CssClass="btn-export" OnClick="btnExport_Click" />
            </div>
        </div>

        <div class="scrollable-grid">
            <asp:GridView ID="gvTimesheetReport" runat="server" AutoGenerateColumns="False"
                CssClass="styled-grid" Width="100%" EmptyDataText="No records found."
                AllowPaging="true" PageSize="10" OnPageIndexChanging="gvTimesheetReport_PageIndexChanging"
                HeaderStyle-HorizontalAlign="Center" RowStyle-HorizontalAlign="Center">
                <Columns>
                    <asp:BoundField DataField="UserId" HeaderText="User ID" />
                    <asp:BoundField DataField="EnquiryNo" HeaderText="Enquiry No" />
                    <asp:BoundField DataField="Project" HeaderText="Project No" />
                    <asp:BoundField DataField="Projectname" HeaderText="Project Name" />
                    <asp:BoundField DataField="ReferenceNo" HeaderText="Reference No" />
                    <asp:BoundField DataField="Task" HeaderText="Task" />
                    <asp:BoundField DataField="TaskType" HeaderText="Task Type" />
                    <asp:BoundField DataField="PlannedFromdate" HeaderText="Planned Start" DataFormatString="{0:dd-MM-yyyy}" HtmlEncode="false" />
                    <asp:BoundField DataField="PlannedTodate" HeaderText="Planned End" DataFormatString="{0:dd-MM-yyyy}" HtmlEncode="false" />
                    <asp:BoundField DataField="PlannedHours" HeaderText="Planned Hrs" />
                    <asp:BoundField DataField="ActualFromdate" HeaderText="Actual Start" DataFormatString="{0:dd-MM-yyyy}" HtmlEncode="false" />
                    <asp:BoundField DataField="ActualTodate" HeaderText="Actual End" DataFormatString="{0:dd-MM-yyyy}" HtmlEncode="false" />
                    <asp:BoundField DataField="ActualHours" HeaderText="Actual Hrs" />
                    <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                    <asp:BoundField DataField="Status" HeaderText="Status" />
                    <asp:BoundField DataField="AssignedBy" HeaderText="AssignedBy" />
                    <asp:BoundField DataField="Hybrid" HeaderText="Work Environment" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server" />
