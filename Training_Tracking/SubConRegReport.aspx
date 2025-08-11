<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SubConRegReport.aspx.cs" Inherits="FujiTecIntranetPortal.Training_Tracking.SubConRegReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="page-container scroll-y">
        <div>
            <h2 class="text-center" style="font-family: Verdana">Sub Con Registration Report</h2>
            <table >
                <tr class="auto-style2" align="right">
                    <br />
                    <th class="auto-style3"><strong>Sub Con Registration ID: <span class="hlt-txt"></span></strong>
                    </th>
                    <td  colspan ="3" align="left">
                        <asp:TextBox ID="txtSubconRegID" runat="server" Width="160px" ></asp:TextBox>
                       <%-- <asp:DropDownList ID="ddProject" runat="server" Width="110px"></asp:DropDownList>--%>
                    </td>                                     
                    
                </tr>
            </table>
        </div>
        <div>
            <table>
                <tr><br />
                    <td class="auto-style2" colspan="3" align="center">

                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="myInput"
                            TabIndex="12" OnClick="btnSearch_Click"></asp:Button>                       
                       
                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="myInput"
                            TabIndex="13" OnClick="btnClear_Click"></asp:Button>
                      
                    </td>

                </tr>
                <tr>
                    <td class="auto-style2" colspan="3" align="center">
                        <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label></td>
                </tr>
            </table>
        </div>

        <div id="divGridViewScroll" style="width: 100%; height: 400px; overflow: scroll">
            <asp:GridView ID="DGV" runat="server" AllowPaging="false" OnPageIndexChanging="OnPageIndexChanging" HeaderStyle-BackColor="#9AD6ED" AutoGenerateSelectButton="false"
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
