<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WarehouseDashboard.aspx.cs" Inherits="FujiTecIntranetPortal.MISReports.WarehouseDashboard" %>

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
    <div class="page-container scroll-y" style="font-family: Verdana">
        <div>
            <h3 class="text-center" style="font-family: Verdana">Warehouse Dashboard Report</h3>
            <table class="normal_grid" style="width:75%;">
                <tr>

                    <td class="auto-style2" align="left">
                        <strong>Domestic/Export: <span class="hlt-txt">*</span></strong>
                        <asp:RadioButtonList ID="rbtnType" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table"  AutoPostBack="true"  Font-Bold="True"  Font-Overline="True" CssClass="mr-0" OnSelectedIndexChanged="rbtnType_SelectedIndexChanged">
                            <asp:ListItem Text="Domestic" Value="DM" Selected="True"/>
                            <asp:ListItem Text="Export" Value="EXP" />
                        </asp:RadioButtonList>
                    </td>


                    <td class="auto-style2" colspan="2" align="center">
                        <asp:Label ID="lblUpload" runat="server"><strong>Upload Schedule Plan: <span class="hlt-txt">*</span></strong></asp:Label>
                        <asp:FileUpload ID="FileUpload1" runat="server" accept="application/vnd.ms-excel,application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" />
                        <asp:Button ID="btnUpload" Text="Upload" OnClick="Upload" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table>
                <tr>
                    <td class="auto-style2" colspan="3" align="center">
                        <asp:Button ID="btnDwnldExl" runat="server" Text="Load Report" OnClick="btnDwnldExl_Click"  />
                        <asp:Button ID="btnGenerate" Text="Generate Template" OnClick="GenerateTemplate_OnClick" runat="server" />                        
                        <asp:Button ID="btnClear" Text="Clear" OnClick="Clear_OnClick" runat="server" />
                        <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
                    </td>
                    <%--  <td class="auto-style2" >
                        <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
                    </td>--%>
                </tr>
                <%--  <tr>
                    <td class="auto-style2" colspan="3" align="center">
                        <asp:Label ID="asdf" runat="server" Text="dAY1"></asp:Label>
                        <asp:TextBox ID="txtTotalDay1" runat="server" Text="1000.00"></asp:TextBox>
                    </td>
                   <td class="auto-style2" colspan="3" align="center">
                        <asp:Label ID="Label1" runat="server" Text="dAY1"></asp:Label>
                        <asp:TextBox ID="TextBox1" runat="server" Text="1000.00"></asp:TextBox>
                    </td>
                </tr>--%>
            </table>
        </div>

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
                <%-- <asp:GridView ID="gv" runat="server" OnPageIndexChanging="OnPageIndexChanging" HeaderStyle-BackColor="#165c7a" AllowPaging="true" PageSize="20"
                HeaderStyle-ForeColor="White" HeaderStyle-BorderStyle="Solid" AlternatingRowStyle-BackColor="WhiteSmoke" HeaderStyle-Font-Names="Verdana"
                AutoGenerateSelectButton="false" OnSelectedIndexChanged="GV_SelectedIndexChanged" AutoGenerateColumns="false"
                BorderStyle="Solid" OnRowDataBound="gv_RowDataBound">
                <alternatingrowstyle backcolor="WhiteSmoke"></alternatingrowstyle>
                <rowstyle borderstyle="Solid" width="30px" height="30px" />


                <headerstyle borderstyle="Solid" height="50px" horizontalalign="Center" verticalalign="Middle"></headerstyle>

                <columns>
                    <asp:TemplateField HeaderText="Region" ItemStyle-Width="50px">
                        <edititemtemplate>
                            <asp:TextBox ID="txtRegion" runat="server" Text='<%# Bind("OrganizationName") %>'></asp:TextBox>
                        </edititemtemplate>
                        <itemtemplate>
                            <asp:Label ID="lblRegion" runat="server" Text='<%# Bind("OrganizationName") %>'></asp:Label>
                        </itemtemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="3+9 Forecast Units" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                        <edititemtemplate>
                            <asp:TextBox ID="txtForecastUnits" runat="server" Text='<%# Bind("ForecastUnits") %>'></asp:TextBox>
                        </edititemtemplate>
                        <itemtemplate>
                            <asp:Label ID="lblForecastUnits" runat="server" Text='<%# Bind("ForecastUnits") %>'></asp:Label>
                        </itemtemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="3+9 Forecast Revenue" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                        <edititemtemplate>
                            <asp:TextBox ID="txtForecastRevenue" runat="server" Text='<%# Bind("ForecastRevenue") %>'></asp:TextBox>
                        </edititemtemplate>
                        <itemtemplate>
                            <asp:Label ID="lblForecastRevenue" runat="server" Text='<%# Bind("ForecastRevenue") %>'></asp:Label>
                        </itemtemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Till Date Units" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                        <edititemtemplate>
                            <asp:TextBox ID="txtTillDateUnits" runat="server" Text='<%# Bind("TillDateUnits") %>'></asp:TextBox>
                        </edititemtemplate>
                        <itemtemplate>
                            <asp:Label ID="lblTillDateUnits" runat="server" Text='<%# Bind("TillDateUnits") %>'></asp:Label>
                        </itemtemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Till Date Revenue" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                        <edititemtemplate>
                            <asp:TextBox ID="txtTillDateRevenue" runat="server" Text='<%# Bind("TillDateRevenue") %>'></asp:TextBox>
                        </edititemtemplate>
                        <itemtemplate>
                            <asp:Label ID="lblTillDateRevenue" runat="server" Text='<%# Bind("TillDateRevenue") %>'></asp:Label>
                        </itemtemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Variance Units" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                        <edititemtemplate>
                            <asp:TextBox ID="txtVarianceUnits" runat="server" Text='<%# Bind("VarianceUnits") %>'></asp:TextBox>
                        </edititemtemplate>
                        <itemtemplate>
                            <asp:Label ID="lblVarianceUnits" runat="server" Text='<%# Bind("VarianceUnits") %>'></asp:Label>
                        </itemtemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Variance Revenue" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                        <edititemtemplate>
                            <asp:TextBox ID="txtVarianceRevenue" runat="server" Text='<%# Bind("VarianceRevenue") %>'></asp:TextBox>
                        </edititemtemplate>
                        <itemtemplate>
                            <asp:Label ID="lblVarianceRevenue" runat="server" Text='<%# Bind("VarianceRevenue") %>'></asp:Label>
                        </itemtemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Attainment %" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                        <edititemtemplate>
                            <asp:TextBox ID="txtAttainment" runat="server" Text='<%# Bind("Attainment") %>'></asp:TextBox>
                        </edititemtemplate>
                        <itemtemplate>
                            <asp:Label ID="lblAttainment" runat="server" Text='<%# Bind("Attainment") %>'></asp:Label>
                        </itemtemplate>
                    </asp:TemplateField>

                </columns>

            </asp:GridView>--%>
                <%-- <div>
                    <strong style="color: white">asdf</strong>
                </div>--%>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
