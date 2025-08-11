<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HybridModel.aspx.cs" Inherits="FujiTecIntranetPortal.TIMESHEET.HybridModel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <style>
        .auto-style2 {
            width: 17%;
            height: 30px;
        }

        .normal-table1 {
            font-size: 1px !important;
            border: 10px solid white !important;
            padding: 2px !important;
            margin-bottom: 10px !important;
        }
    </style>
    <script type="text/javascript">
        function showMessage(message) {
            alert(message);
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="page-container scroll-y">
        <div>
            <h2 class="text-center" style="font-family: Verdana">Workspace Reservation</h2>
            <br />
            <table>
                <tr>
                    <th class="auto-style2"><strong>Employee ID:</strong></th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtEmployee" CssClass="myInput" runat="server" Width="180px" TabIndex="1" Enabled="false"></asp:TextBox>
                    </td>

                    <th class="auto-style2"><strong>Department ID:</strong></th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtDepartment" CssClass="myInput" runat="server" Width="180px" TabIndex="2" Enabled="false"></asp:TextBox>
                    </td>

                    <th class="auto-style2"><strong>Booking Date:</strong></th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtBookingDate" runat="server" Width="180px" TextMode="Date" AutoPostBack="true" OnTextChanged="txtBookingDate_TextChanged"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th class="auto-style2"><strong>Total Seats:</strong></th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtTotalseats" runat="server" Width="180px" Enabled="false"></asp:TextBox>
                    </td>
                    <th class="auto-style2"><strong>Available Seats:</strong></th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtAvailableSeats" runat="server" Width="180px" Enabled="false"></asp:TextBox>
                    </td>

                    <th class="auto-style2"><strong>Transport Required:</strong></th>
                    <td class="auto-style2">
                        <asp:CheckBox ID="chkTransport" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th class="auto-style2"><strong>Food Required:</strong></th>
                    <td class="auto-style2">
                        <asp:CheckBox ID="chkFood" runat="server" />
                    </td>

                    <td class="auto-style2" colspan="3" align="center">
                        <asp:Label ID="lblmsg" runat="server" Text="" Visible="false"></asp:Label></td>
                </tr>
                <tr>
                    <td class="auto-style2" colspan="6" align="center">
                        <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSubmit_Click" width ="80px"/>
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-warning btn-sm" OnClick="btnUpdate_Click" width ="80px"/>

                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-danger" OnClick="btnClear_Click" width ="80px" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2" colspan="6" align="center">
                        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label></td>
                </tr>
            </table>
        </div>
        <div>
            <br />
            <asp:GridView ID="gvBookings" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered" GridLines="Both" OnSelectedIndexChanged="gvBookings_SelectedIndexChanged">
                <Columns>
                    <asp:CommandField ShowSelectButton="True" />
                    <asp:BoundField DataField="BookingID" HeaderText="Booking ID" />
                    <asp:BoundField DataField="EmpName" HeaderText="Employee" />
                    <asp:BoundField DataField="DepartmentName" HeaderText="Department"  />
                    <asp:BoundField DataField="FoodRequired" HeaderText="Food" />
                    <asp:BoundField DataField="TransportRequire" HeaderText="Transport" />
                    <asp:BoundField DataField="BookingDate" HeaderText="Booking Date" DataFormatString="{0:dd-MM-yyyy}" />
                    

                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
