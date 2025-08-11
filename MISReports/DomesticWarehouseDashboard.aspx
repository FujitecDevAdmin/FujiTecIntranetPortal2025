<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DomesticWarehouseDashboard.aspx.cs" Inherits="FujiTecIntranetPortal.MISReports.DomesticWarehouseDashboard" %>

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
            font-family: Verdana;
        }

        .normal-table1 {
            font-size: 1px !important;
            border: 10px solid white !important;
            padding: 2px !important;
            margin-bottom: 10px !important;
            font-family: Verdana !important;
        }

        .myInput:focus {
            border: solid 1px green !important;
            outline: double !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <table class="normal_grid">
        <%--<tr>
            <td>
                &nbsp;<asp:Label ID="lblD1"  runat="server" Font-Bold="true"></asp:Label>
                &nbsp;<asp:TextBox ID="txtD1"  Width ="85px" runat="server" style="text-align:right"  Text="0" Font-Bold="true"></asp:TextBox>
            </td>
            <td> 
                <asp:Label ID="lblD2"  runat="server" Font-Bold="true"></asp:Label>
                <asp:TextBox ID="txtD2"  Width ="85px" runat="server" style="text-align:right"  Text="0" Font-Bold="true"></asp:TextBox>
            </td>
                
            <td> <asp:Label ID="lblD3"  runat="server" Font-Bold="true"></asp:Label>
                <asp:TextBox ID="txtD3"  Width ="85px" runat="server" style="text-align:right"  Text="0" Font-Bold="true"></asp:TextBox>
               </td>
            <td> <asp:Label ID="lblD4"  runat="server" Font-Bold="true"></asp:Label>
                <asp:TextBox ID="txtD4"  Width ="85px" runat="server" style="text-align:right"  Text="0" Font-Bold="true"></asp:TextBox>
               </td>
            <td> <asp:Label ID="lblD5"  runat="server" Font-Bold="true"></asp:Label>
                <asp:TextBox ID="txtD5"  Width ="85px" runat="server" style="text-align:right"  Text="0" Font-Bold="true"></asp:TextBox>
               </td>
            <td> <asp:Label ID="lblD6"  runat="server" Font-Bold="true"></asp:Label>
                <asp:TextBox ID="txtD6"  Width ="85px" runat="server" style="text-align:right"  Text="0" Font-Bold="true"></asp:TextBox>
               </td>
            <td>               
                <asp:Label ID="lblD7"  runat="server" Font-Bold="true"></asp:Label>
                <asp:TextBox ID="txtD7"  Width ="85px" runat="server" style="text-align:right"  Text="0" Font-Bold="true"></asp:TextBox>
            </td>
           
        </tr>--%>
        <tr>
            <td>&nbsp;<asp:Label ID="lblTotalCostW1" Text="Total Units & Cost W1:" runat="server" Font-Bold="true"></asp:Label>

            </td>
            <td>
                <asp:Label ID="lblTotalCostW2" Text="Total Units & Cost W2:" runat="server" Font-Bold="true"></asp:Label>

            </td>
            <td>
                <asp:Label ID="lblTotalCostTillW2" Text="Total Units & Cost Till W2:" runat="server" Font-Bold="true"></asp:Label>

            </td>
            <td>
                <asp:Label ID="lblTotalCostW3" Text="Total Units & Cost W3:" runat="server" Font-Bold="true"></asp:Label>

            </td>
            <td>
                <asp:Label ID="lblTotalCostTillW3" Text="Total Units & Cost Till W3:" runat="server" Font-Bold="true"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblTotalCostW4" Text="Total Units & Cost W4:" runat="server" Font-Bold="true"></asp:Label></td>
            <td>
                <asp:Label ID="lblTotalCostTillW4" Text="Total Units & Cost Till W4:" runat="server" Font-Bold="true"></asp:Label></td>
            <td>
                <asp:Label ID="lblTotalCostW5" Text="Total Units & Cost W5:" runat="server" Font-Bold="true"></asp:Label></td>
            <td>
                <asp:Label ID="lblTotal" Text="Total Units & Cost:" runat="server" Font-Bold="true"></asp:Label></td>
        </tr>
        <tr>
            <td>&nbsp;<asp:TextBox ID="txtTotalUnitsW1" runat="server" Width="25px" Style="text-align: right" TextMode="Number" Text="0" Font-Bold="true"></asp:TextBox>&nbsp;<asp:TextBox ID="txtTotalCostW1" runat="server" Width="85px" Style="text-align: right" Text="0" Font-Bold="true" AutoPostBack="true"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtTotalUnitsW2" runat="server" Width="25px" Style="text-align: right" TextMode="Number" Text="0" Font-Bold="true"></asp:TextBox>&nbsp;<asp:TextBox ID="txtTotalCostW2" Width="85px" runat="server" Style="text-align: right" Text="0" Font-Bold="true"></asp:TextBox>

            </td>
            <td>
                <asp:TextBox ID="txtTotalUnitsTillW2" runat="server" Width="25px" Style="text-align: right" TextMode="Number" Text="0" Font-Bold="true"></asp:TextBox>&nbsp;<asp:TextBox ID="txtTotalCostTillW2" Width="85px" runat="server" Style="text-align: right" Text="0" Font-Bold="true"></asp:TextBox></td>
            <td>
                <asp:TextBox ID="txtTotalUnitsW3" runat="server" Width="25px" Style="text-align: right" TextMode="Number" Text="0" Font-Bold="true"></asp:TextBox>&nbsp;<asp:TextBox ID="txtTotalCostW3" Width="85px" runat="server" Style="text-align: right" Text="0" Font-Bold="true"></asp:TextBox></td>
            <td>
                <asp:TextBox ID="txtTotalUnitsTillW3" runat="server" Width="25px" Style="text-align: right" TextMode="Number" Text="0" Font-Bold="true"></asp:TextBox>&nbsp;<asp:TextBox ID="txtTotalCostTillW3" Width="85px" runat="server" Style="text-align: right" Text="0" Font-Bold="true"></asp:TextBox></td>
            <td>
                <asp:TextBox ID="txtTotalUnitsW4" runat="server" Width="25px" Style="text-align: right" TextMode="Number" Text="0" Font-Bold="true"></asp:TextBox>&nbsp;<asp:TextBox ID="txtTotalCostW4" Width="85px" runat="server" Style="text-align: right" Text="0" Font-Bold="true"></asp:TextBox></td>
            <td>
                <asp:TextBox ID="txtTotalUnitsTillW4" runat="server" Width="25px" Style="text-align: right" TextMode="Number" Text="0" Font-Bold="true"></asp:TextBox>&nbsp;<asp:TextBox ID="txtTotalCostTillW4" Width="85px" runat="server" Style="text-align: right" Text="0" Font-Bold="true"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtTotalUnitsW5" runat="server" Width="30px" Style="text-align: right" Font-Bold="true" Text="0"></asp:TextBox>&nbsp;<asp:TextBox ID="txtTotalCostW5" Width="85px" runat="server" Style="text-align: right" Text="0" Font-Bold="true"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtTotalUnits" runat="server" Width="30px" Style="text-align: right" Font-Bold="true" Text="0"></asp:TextBox>&nbsp;<asp:TextBox ID="txtTotal" Width="85px" runat="server" Style="text-align: right" Text="0" Font-Bold="true"></asp:TextBox>
            </td>
        </tr>
    </table>

    <div class="normal-table1" style="width: 100%; margin: 0 auto; overflow: scroll">
        <%--<div>--%>
        <div id="divGridViewScroll" style="width: 160%; height: 400px;">
            <asp:GridView runat="server" ID="gv" HeaderStyle-BackColor="#9AD6ED" AllowPaging="true" PageSize="15" AutoGenerateColumns="false"
                HeaderStyle-ForeColor="White" HeaderStyle-BorderStyle="Solid" HeaderStyle-Font-Names="Verdana"
                OnPageIndexChanging="OnPageIndexChanging" OnSelectedIndexChanged="GV_SelectedIndexChanged"
                OnRowDataBound="gv_RowDataBound">
                <AlternatingRowStyle BackColor="WhiteSmoke"></AlternatingRowStyle>
                <RowStyle Height="20px" />
                <HeaderStyle BorderStyle="Solid" Height="50px" Width="50px" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First" PreviousPageText="Previous" NextPageText="Next" LastPageText="Last" />
                <Columns>
                    <asp:BoundField DataField="OPPORTUNITYNAME" HeaderText="PROJECT NAME" ItemStyle-Width="140px" />
                    <asp:BoundField DataField="PROJECTNO" HeaderText="PROJECTNO" ItemStyle-Width="5px" />
                    <asp:BoundField DataField="BRANCH" HeaderText="BRANCH" ItemStyle-Width="60px" />
                    <%--<asp:BoundField DataField="PROJECTNAME" HeaderText="PRODUCT NAME" ItemStyle-Width="30px" />--%>
                    <asp:BoundField DataField="PRODUCTTYPE" HeaderText="PRODUCT TYPE" ItemStyle-Width="35px" />

                    <asp:BoundField DataField="CAPACITY" HeaderText="CAPACITY" ItemStyle-Width="10px" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="SPEEDMPS" HeaderText="SPEED MPS" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="STOPS" HeaderText="STOPS" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="TARGETCOST" HeaderText="TARGET UNITS" ItemStyle-Width="10px" ItemStyle-HorizontalAlign="Right" />

                    <asp:BoundField DataField="D01" HeaderText="D01" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="D02" HeaderText="D02" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="D03" HeaderText="D03" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="D04" HeaderText="D04" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Right" />

                    <asp:BoundField DataField="D05" HeaderText="D05" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="D06" HeaderText="D06" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="D07" HeaderText="D07" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="TotalCostW1" HeaderText="Total Cost W1" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Right" />

                    <asp:BoundField DataField="D08" HeaderText="D08" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="D09" HeaderText="D09" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="D10" HeaderText="D10" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="D11" HeaderText="D11" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="D12" HeaderText="D12" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Right" />

                    <asp:BoundField DataField="D13" HeaderText="D13" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="D14" HeaderText="D14" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="TotalCostW2" HeaderText="Total Cost W2" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="TotalCostTillW2" HeaderText="Total Cost Till W2" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Right" />

                    <asp:BoundField DataField="D15" HeaderText="D15" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="D16" HeaderText="D16" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Right" />

                    <asp:BoundField DataField="D17" HeaderText="D17" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="D18" HeaderText="D18" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="D19" HeaderText="D19" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="D20" HeaderText="D20" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Right" />

                    <asp:BoundField DataField="D21" HeaderText="D21" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="TotalCostW3" HeaderText="Total Cost W3" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="TotalCostTillW3" HeaderText="Total Cost Till W3" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Right" />

                    <asp:BoundField DataField="D22" HeaderText="D22" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Right" />

                    <asp:BoundField DataField="D23" HeaderText="D23" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="D24" HeaderText="D24" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="D25" HeaderText="D25" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="D26" HeaderText="D26" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Right" />

                    <asp:BoundField DataField="D27" HeaderText="D27" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="D28" HeaderText="D28" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="TotalCostW4" HeaderText="Total Cost W4" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="TotalCostTillW4" HeaderText="Total Cost Till W4" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Right" />

                    <asp:BoundField DataField="D29" HeaderText="D29" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="D30" HeaderText="D30" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="D31" HeaderText="D31" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="TotalCostW5" HeaderText="Total Cost W5" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="TotalCost" HeaderText="Total Cost" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Right" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <div>
        <table>
            <tr>
                <td colspan="3" align="center">
                    <asp:Button ID="btnback" Text="Back" Width="80px" OnClick="btnBack_OnClick" runat="server" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
