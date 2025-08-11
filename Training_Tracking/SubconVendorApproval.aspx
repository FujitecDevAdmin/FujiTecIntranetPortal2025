<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SubconVendorApproval.aspx.cs" Inherits="FujiTecIntranetPortal.Training_Tracking.SubconVendorApproval" %>

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
    <div class="normal-table2">
        <h3 class="text-center" style="font-family: Verdana">Sub-Contract Vendor Approval</h3>
        <table>

            <tr>
                <td>
                    <asp:GridView ID="gv" runat="server" AutoGenerateSelectButton="true" OnSelectedIndexChanged="GV_SelectedIndexChanged"
                        OnPageIndexChanging="OnPageIndexChanging" AllowPaging="true" PageSize="10" GridLines="Both" HeaderStyle-BorderStyle="Solid" TabIndex="21"
                        OnRowCommand="gv_OnRowCommand" HeaderStyle-BackColor="#9AD6ED" AlternatingRowStyle-BackColor="WhiteSmoke" 
                        RowStyle-BorderStyle="Solid" BorderStyle="Solid" FooterStyle-BorderStyle="Solid"
                        HeaderStyle-Height="30px" Width="90%" HeaderStyle-ForeColor="#636363">
                       <%-- <Columns>
                            <asp:BoundField DataField="ID" HeaderText="ID" />
                            <asp:BoundField DataField="Branchname" HeaderText="Branch Name" />
                            <asp:BoundField DataField="Companyname" HeaderText="Company Name" />
                            <asp:BoundField DataField="ApprovalStatus" HeaderText="Approval Status" />                          
                        </Columns>--%>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
