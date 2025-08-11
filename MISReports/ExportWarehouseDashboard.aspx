<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ExportWarehouseDashboard.aspx.cs" Inherits="FujiTecIntranetPortal.MISReports.ExportWarehouseDashboard" %>
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
     <div class="normal-table1" style="width: 100%; margin: 0 auto; overflow: scroll">
            <%--<div>--%>
            <div id="divGridViewScroll" style="width: 180%; height: 380px;">
                <asp:GridView runat="server" ID="gv" HeaderStyle-BackColor="#9AD6ED" AllowPaging="true" PageSize="10" AutoGenerateColumns="false"
                    HeaderStyle-ForeColor="White" HeaderStyle-BorderStyle="Solid" AlternatingRowStyle-BackColor="WhiteSmoke" HeaderStyle-Font-Names="Verdana"
                    OnPageIndexChanging="OnPageIndexChanging" OnSelectedIndexChanged="GV_SelectedIndexChanged"
                    OnRowDataBound="gv_RowDataBound">
                    <AlternatingRowStyle BackColor="WhiteSmoke"></AlternatingRowStyle>
                    <RowStyle Width="50px" Height="30px" />
                    <HeaderStyle BorderStyle="Solid" Height="50px" Width="50px" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                     <PagerSettings  Mode="NextPreviousFirstLast" FirstPageText="First" PreviousPageText="Previous" NextPageText="Next" LastPageText="Last" />
                    <Columns>
                        <asp:BoundField DataField="OPPORTUNITYNAME" HeaderText="PROJECT NAME" ItemStyle-Width="140px" />
                        <asp:BoundField DataField="PROJECTNO" HeaderText="PROJECTNO" ItemStyle-Width="15px" />
                        <asp:BoundField DataField="BRANCH" HeaderText="BRANCH" ItemStyle-Width="80px" />
                        <asp:BoundField DataField="PROJECTNAME" HeaderText="PRODUCT NAME" ItemStyle-Width="30px" />
                        <asp:BoundField DataField="PRODUCTTYPE" HeaderText=" TYPE" ItemStyle-Width="35px" />

                        <asp:BoundField DataField="CAPACITY" HeaderText="CAPACITY" ItemStyle-Width="10px" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="SPEEDMPS" HeaderText="SPEED MPS" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="STOPS" HeaderText="STOPS" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="TARGETCOST" HeaderText="TARGET UNITS" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Right" />

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
                        <asp:BoundField DataField="TotalCostW5" HeaderText="Total Cost W5" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Right" />
                        <asp:BoundField DataField="TotalCost" HeaderText="Total Cost" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Right" />
                    </Columns>
                </asp:GridView>
                    
            </div>
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
