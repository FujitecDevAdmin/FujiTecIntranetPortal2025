<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SafetyTBT.aspx.cs" Inherits="FujiTecIntranetPortal.SafetyTBT" %>

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

        .center {
            display: flex;
            justify-content: center;
            align-items: center;
        }

        .auto-style2 {
            width: 17%;
            height: 30px;
            font-family: Verdana;
        }

        p {
            background-color: #CF2331;
        }

        .myInput:focus {
            border: solid 1px green !important;
            outline: double !important;
        }

        .auto-style1 {
            width: 107px;
        }

        .GridHeader {
            text-align: center !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="page-container scroll-y">
         <div id="divGridViewScroll" style="width: 100%; height: 600px; overflow: scroll" class="align-items-sm-center">
            <h2 class="text-center" style="font-family:Verdana"></h2>
            <table>
                <tr>
                    <td colspan="3">
                        <asp:Literal ID="ltEmbed" runat="server" /></td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:GridView ID="DGBooking"
                            runat="server" HeaderStyle-BackColor="#CCFFCC" HeaderStyle-Height="50px" HeaderStyle-CssClass="GridHeader" 
                            AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="0.5px" CellPadding="2">
                            <FooterStyle BackColor="#669999" ForeColor="#330099" />
                            <HeaderStyle BackColor="#008080" CssClass="GridHeader" Height="20px" Font-Bold="false" ForeColor="#FFFFCC"></HeaderStyle>
                            <PagerStyle ForeColor="#330099" HorizontalAlign="Center" BackColor="#FFFFCC" />
                            <RowStyle Height="10px" BackColor="White" ForeColor="#330099" />
                            <AlternatingRowStyle Height="10px" />
                            <Columns>
                                <asp:TemplateField HeaderText="File Name" HeaderStyle-ForeColor="White" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="Large"  HeaderStyle-Font-Names="Verdana"
                                    HeaderStyle-Width="200px" ItemStyle-Width="200px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFileName" runat="server" Font-Size="Small" Font-Bold="true" Font-Names="Verdana"
                                            Text='<%#Eval("FileName")%>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="200px"></HeaderStyle>
                                    <ItemStyle  Width="200px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="File Type" HeaderStyle-ForeColor="White" Visible="False"  HeaderStyle-Font-Names="Verdana">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFileType" runat="server" Font-Names="Verdana"
                                            Text='<%#Eval("File_Path")%>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="200px"></HeaderStyle>
                                    <ItemStyle Width="200px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="View File" HeaderStyle-ForeColor="White" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="Large" HeaderStyle-Font-Names="Verdana">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lblDownloadFile" CommandName="getid" runat="server" ForeColor="Black" Font-Size="small" Font-Bold="true" Font-Names="Verdana"
                                            Text='<%# Eval("View") %>' CommandArgument='<%# Eval("View") %>' OnClick="View_Click">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="200px"></HeaderStyle>
                                    <ItemStyle CssClass="GridHeader" Width="200px"></ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
   
        <%-- <div style="width: 100%; height: 850px">
            <iframe id="myframe" width="100%" height="100%" runat="server"></iframe>

        </div>--%>
        <%--<asp:Literal ID="Literal1" runat="server" />--%>
        <%--<div id="divGridViewScroll" style="width: 100%; height: 600px; overflow: scroll" class="align-items-sm-center">
            <h2 class="text-center" style="font-family: Verdana"></h2>
            <table class="normal_grid">
                <tr>
                    <td colspan="3">
                        <asp:Literal ID="ltEmbed" runat="server" /></td>
                </tr>
                <tr>
                    <td class="auto-style2">
                        <asp:Button runat="server" Text="Safety Policy" ID="btnSafetyPolicy" width="150px" OnClick="btnSafetyPolicy_Click" />
                    </td>
                    <td class="auto-style2">
                        <asp:Button runat="server" Text="ORG Chart" ID="btnORGChart" width="150px" OnClick="btnORGChart_Click" />
                    </td>
                    <td class="auto-style2">
                        <asp:Button runat="server" Text="Safety Induction" ID="btnSafetyInduction" width="150px" OnClick="btnSafetyInduction_Click" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">
                        <asp:Button runat="server" Text="Safety Training Photos" ID="btnSafetyTrainingPhotos" width="150px" OnClick="btnSafetyTrainingPhotos_Click" />
                    </td>
                    <td class="auto-style2">
                        <asp:Button runat="server" Text="Special Safety Articles" ID="btnSpecialSafetyArticles" width="150px" OnClick="btnORGChart_Click" />
                    </td>
                    <td class="auto-style2">
                        <asp:Button runat="server" Text="Safety process videos" ID="btnSafetyprocessvideos" width="150px" OnClick="btnSafetyprocessvideos_Click" />
                    </td>
                </tr>
            </table>
        </div>--%>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
