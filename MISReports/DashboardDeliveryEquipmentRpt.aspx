<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DashboardDeliveryEquipmentRpt.aspx.cs" Inherits="FujiTecIntranetPortal.MISReports.DashboardDeliveryEquipment" %>

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
            <h3 class="text-center" style="font-family: Verdana">Dashboard Report</h3>
            <table class="normal_grid">
                <tr>
                   <%-- <th class="auto-style2" style="font-family: Verdana"><strong>Dashboard Type: <span class="hlt-txt">*</span></strong>
                    </th>--%>
                    <td><strong>Dashboard Type: <span class="hlt-txt">*</span></strong>
                        <asp:DropDownList ID="DDType" CssClass="myInput" runat="server" Width="183px" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="DDType_SelectedIndexChanged">
                            <asp:ListItem Text="--Select--" Value="EQPINST"></asp:ListItem>
                            <asp:ListItem Text="Equipment" Value="EQP"></asp:ListItem>
                            <asp:ListItem Text="Installation" Value="INST"></asp:ListItem>
                        </asp:DropDownList></td>
                  <%--  <th class="auto-style2" style="font-family: Verdana"><strong>Calendar Period: <span class="hlt-txt">*</span></strong>
                    </th>--%>
                    <td>
                        <strong>Calendar Period: <span class="hlt-txt">*</span></strong>
                        <asp:DropDownList ID="ddcalendarPeriod" CssClass="myInput" runat="server" Width="183px" TabIndex="2" AutoPostBack="true" OnSelectedIndexChanged="ddcalendarPeriod_SelectedIndexChanged" Enabled="false">
                        </asp:DropDownList></td>
                   <%-- <th class="auto-style2" style="font-family: Verdana"><strong>Upload Schedule Plan: <span class="hlt-txt">*</span></strong>
                    </th>--%>
                    <td>
                        <%-- <asp:DropDownList ID="DropDownList1" CssClass="myInput" runat="server" Width="183px" TabIndex="2" AutoPostBack="true">
                    </asp:DropDownList>--%>
                        <strong>Upload Schedule Plan: <span class="hlt-txt">*</span></strong>
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

                        <asp:Button ID="btnGenerate" Text="Generate Template" OnClick="GenerateTemplate_OnClick" runat="server" />
                        <asp:Button ID="btnDwnldExl" runat="server" Text="Download Excel" OnClick="btnDwnldExl_Click" Visible="false"/>
                        <asp:Button ID="btnClear" Text="Clear" OnClick="Clear_OnClick" runat="server" />
                        
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2" colspan="3" align="center">
                        <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </div>

        <div class="normal-table1" style="width: 900px; margin: 0 auto;">

            <asp:GridView ID="gv" runat="server" OnPageIndexChanging="OnPageIndexChanging" HeaderStyle-BackColor="#165C7A" AllowPaging="true" PageSize="20"
                HeaderStyle-ForeColor="White" HeaderStyle-BorderStyle="Solid" AlternatingRowStyle-BackColor="WhiteSmoke" HeaderStyle-Font-Names="Verdana"
                AutoGenerateSelectButton="false" OnSelectedIndexChanged="GV_SelectedIndexChanged" AutoGenerateColumns="false"
                 OnRowDataBound="gv_RowDataBound">
                <AlternatingRowStyle BackColor="WhiteSmoke"></AlternatingRowStyle>
                <RowStyle Width="30px" Height="30px" />


                <HeaderStyle BorderStyle="Solid" Height="50px" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>

                <Columns>
                    <asp:TemplateField HeaderText="Region" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Left">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtRegion" runat="server" Text='<%# Bind("OrganizationName") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblRegion" runat="server" Text='<%# Bind("OrganizationName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="3+9 Forecast Units" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtForecastUnits" runat="server" Text='<%# Bind("ForecastUnits") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblForecastUnits" runat="server" Text='<%# Bind("ForecastUnits") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="3+9 Forecast Revenue" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtForecastRevenue" runat="server" Text='<%# Bind("ForecastRevenue") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblForecastRevenue" runat="server" Text='<%# Bind("ForecastRevenue") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Till Date Units" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtTillDateUnits" runat="server" Text='<%# Bind("TillDateUnits") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblTillDateUnits" runat="server" Text='<%# Bind("TillDateUnits") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Till Date Revenue" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtTillDateRevenue" runat="server" Text='<%# Bind("TillDateRevenue") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblTillDateRevenue" runat="server" Text='<%# Bind("TillDateRevenue") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Variance Units" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtVarianceUnits" runat="server" Text='<%# Bind("VarianceUnits") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblVarianceUnits" runat="server" Text='<%# Bind("VarianceUnits") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Variance Revenue" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtVarianceRevenue" runat="server" Text='<%# Bind("VarianceRevenue") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblVarianceRevenue" runat="server" Text='<%# Bind("VarianceRevenue") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Attainment %" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtAttainment" runat="server" Text='<%# Bind("Attainment") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblAttainment" runat="server" Text='<%# Bind("Attainment") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>

            </asp:GridView>
           <%-- <div>
                <strong style="color: white">asdf</strong>
            </div>--%>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
