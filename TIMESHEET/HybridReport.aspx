<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HybridReport.aspx.cs" Inherits="FujiTecIntranetPortal.TIMESHEET.HybridReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <style>
        .auto-style2 {
            width: 17%;
            height: 30px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="page-container scroll-y">
        <h2 class="text-center" style="font-family: Verdana">Workspace Report</h2>
        <br />
        <table>
            <tr>
                <th class="auto-style2"><strong>From Date:</strong></th>
                <td class="auto-style2">
                    <asp:TextBox ID="txtFromDate" runat="server" Width="180px" TextMode="Date"></asp:TextBox>
                </td>

                <th class="auto-style2"><strong>To Date:</strong></th>
                <td class="auto-style2">
                    <asp:TextBox ID="txtToDate" runat="server" Width="180px" TextMode="Date"></asp:TextBox>
                </td>

                <th class="auto-style2"><strong>Department:</strong></th>
                <td class="auto-style2">
                    <asp:DropDownList ID="ddlDepartment" runat="server" Width="180px" AppendDataBoundItems="true">
                        <asp:ListItem Text="--Select--" Value="" />
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style2" colspan="6" align="center">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" Width="80px" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-danger" OnClick="btnClear_Click" Width="80px" />
                    <asp:Button ID="btnExport" runat="server" Text="Export to Excel" CssClass="btn btn-success" OnClick="btnExport_Click" />

                </td>
            </tr>
        </table>

        <br />
        <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered" GridLines="Both">
            <Columns>
                <asp:BoundField DataField="DepartmentName" HeaderText="Department" />
                <asp:BoundField DataField="BookingDate" HeaderText="Booking Date" DataFormatString="{0:dd-MM-yyyy}" />
                <asp:BoundField DataField="EmployeesBooked" HeaderText="Employees Booked" />
                <asp:BoundField DataField="FoodRequiredCount" HeaderText="Food Count" />
                <asp:BoundField DataField="TransportRequiredCount" HeaderText="Transport Count" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>

