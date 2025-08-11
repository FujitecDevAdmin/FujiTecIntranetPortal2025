<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SalesInduction.aspx.cs" Inherits="FujiTecIntranetPortal.Induction.SalesInduction" %>

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
    <div align="right">
        <table>
            <tr align="right">
                <td><strong><b>Welcome:
                       <asp:Label ID="lblUserName" runat="server" ForeColor="#FF3300" Font-Bold="True" Font-Size="Larger" Font-Italic="True"></asp:Label>
                    <asp:Label runat="server" Text=" ------- " ForeColor="White"></asp:Label>
                </b></strong></td>
            </tr>
        </table>
    </div>
    <div class="page-container scroll-y">
        <%-- <div style="width: 100%; height: 850px">
            <iframe id="myframe" width="100%" height="100%" runat="server"></iframe>

        </div>--%>
         <%--<asp:Literal ID="Literal1" runat="server" />--%>
        <div id="divGridViewScroll" style="width: 100%; height: 600px; overflow: scroll">
            <h2 class="text-center">Sales Induction</h2>
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

                            <HeaderStyle BackColor="#008080" CssClass="GridHeader" Height="30px" Font-Bold="True" ForeColor="#FFFFCC"></HeaderStyle>

                            <PagerStyle ForeColor="#330099" HorizontalAlign="Center" BackColor="#FFFFCC" />
                            <RowStyle Height="10px" BackColor="White" ForeColor="#330099" />
                            <AlternatingRowStyle Height="10px" />
                            <Columns>
                                <asp:TemplateField HeaderText="File Name" HeaderStyle-ForeColor="White" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="Large"
                                    HeaderStyle-Width="200px" ItemStyle-Width="200px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFileName" runat="server" Font-Size="Small" Font-Bold="true"
                                            Text='<%#Eval("FileName")%>'></asp:Label>

                                    </ItemTemplate>

                                    <HeaderStyle Width="200px"></HeaderStyle>

                                    <ItemStyle  Width="200px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="File Type" HeaderStyle-ForeColor="White" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFileType" runat="server"
                                            Text='<%#Eval("File_Path")%>'></asp:Label>

                                    </ItemTemplate>

                                    <HeaderStyle HorizontalAlign="Center" Width="200px"></HeaderStyle>

                                    <ItemStyle Width="200px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="View File" HeaderStyle-ForeColor="White" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="Large">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lblDownloadFile" CommandName="getid" runat="server" ForeColor="Black" Font-Size="small" Font-Bold="true"
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
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
    <div class="footer">
        <p style="text-align: left; color: white">
            <strong>Developed by Information Technology.    
    <span style="float: right; color: white">© 2022 Fujitec India Pvt,Ltd.
    </span>
            </strong>
        </p>
    </div>
</asp:Content>
