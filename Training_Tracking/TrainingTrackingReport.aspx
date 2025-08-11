<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TrainingTrackingReport.aspx.cs" Inherits="FujiTecIntranetPortal.Training_Tracking.TrainingTrackingReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="page-container scroll-y">
        <div>
            <h2 class="text-center" style="font-family: Verdana">Sub Con Cerification Report</h2>
            <table>               
                 <tr>      
                     <th class="auto-style3"><strong>Type: <span class="hlt-txt"></span></strong>
                    </th>
                     <td align="left">
                      <asp:DropDownList ID="ddType" runat="server" Width="180px" AutoPostBack="true" OnSelectedIndexChanged="ddType_SelectedIndexChanged">
                          <asp:ListItem Value="MSC0006" Text="Training"></asp:ListItem>
                          <asp:ListItem Value="MSC0014" Text="Certification"></asp:ListItem>
                      </asp:DropDownList> <br /> <br />
                    </td>
                    <th class="auto-style3"><strong>From Date: <span class="hlt-txt"></span></strong>
                    </th>
                    <td class="auto-style3">
                        <asp:TextBox ID="txtFromDate" CssClass="myInput" runat="server" Width="180px" TabIndex="1"></asp:TextBox><asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/assets/images/cal.jpg" OnClick="ImageButton1_Click" Style="height: 16px" />
                        <asp:Calendar ID="FromCal" runat="server" OnSelectionChanged="FromCalendar1_SelectionChanged" OnDayRender="FromCalendar1_DayRender"></asp:Calendar><br /> <br />
                    </td>
                      <th class="auto-style3"><strong>To Date: <span class="hlt-txt"></span></strong>
                    </th>
                    <td class="auto-style3">
                        <asp:TextBox ID="txtTodate" CssClass="myInput" runat="server" Width="180px" TabIndex="1"></asp:TextBox><asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/assets/images/cal.jpg" OnClick="ImageButton2_Click" Style="height: 16px" />
                        <asp:Calendar ID="Tocal" runat="server" OnSelectionChanged="ToCalendar1_SelectionChanged" OnDayRender="ToCalendar1_DayRender"></asp:Calendar><br /> <br />
                    </td>
                </tr>
                 <tr >                   
                    <th class="auto-style3"><strong>Vendor: <span class="hlt-txt"></span></strong>
                    </th>
                    <td align="left">
                        <asp:TextBox ID="txtVendorID" CssClass="myInput" runat="server" Width="180px" TabIndex="1" AutoPostBack="true" OnTextChanged="txtVendorID_TextChanged"></asp:TextBox>
                        <br />
                         <asp:DropDownList ID="ddVendor" runat="server" Width="180px"></asp:DropDownList>
                    </td>
                    <th class="auto-style3"><strong>Employee: <span class="hlt-txt"></span></strong>
                    </th>
                    <td align="left">
                        <asp:TextBox ID="txtEmployee" CssClass="myInput" runat="server" Width="180px" TabIndex="1" AutoPostBack="true" OnTextChanged="txtEmployee_TextChanged"></asp:TextBox>
                        <br /> <asp:DropDownList ID="ddEmployee" runat="server" Width="180px"></asp:DropDownList>
                    </td>
                    <th class="auto-style3"><strong>Training Module: <span class="hlt-txt"></span></strong>
                    </th>
                    <td align="left">
                        <asp:TextBox ID="txtTM" CssClass="myInput" runat="server" Width="180px" TabIndex="1" AutoPostBack="true" OnTextChanged="txtTM_TextChanged"></asp:TextBox>
                        <br /> <asp:DropDownList ID="ddTrainingModule" runat="server" Width="180px"></asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table>
                <tr><br/>
                    <td class="auto-style2" colspan="3" align="center">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="myInput" Height="30px" Width="80px" 
                            TabIndex="12" OnClick="btnSearch_Click" style="margin-left: 5px" ></asp:Button>  
                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="myInput" Height="30px" Width="80px" 
                            TabIndex="13" OnClick="btnClear_Click" style="margin-left: 5px" ></asp:Button>   
                         <asp:Button ID="btnDownload" runat="server" Text="Download" CssClass="myInput" Font-Size="Small" Height="30px" Width="80px" 
                              OnClick="btnDownload_Click" style="margin-left: 5px" />
           
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2" colspan="3" align="center">
                        <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label></td>
                </tr>
            </table>
        </div>

        <div id="divGridViewScroll" style="width: 100%; height: 400px; overflow: scroll">
            <asp:GridView ID="DGV" runat="server" AllowPaging="true" OnPageIndexChanging="OnPageIndexChanging" HeaderStyle-BackColor="#9AD6ED" AutoGenerateSelectButton="false"
                HeaderStyle-ForeColor="White" HeaderStyle-BorderStyle="Solid" AlternatingRowStyle-BackColor="WhiteSmoke" 
                BorderStyle="Solid">
                <AlternatingRowStyle BackColor="WhiteSmoke"></AlternatingRowStyle>
                <RowStyle BorderStyle="Solid" Width="30px" Height="30px" />
                <HeaderStyle BorderStyle="Solid" Height="50px" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>