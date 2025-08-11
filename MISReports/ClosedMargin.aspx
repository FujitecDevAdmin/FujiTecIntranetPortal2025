<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ClosedMargin.aspx.cs" Inherits="FujiTecIntranetPortal.MISReports.ClosedMargin" %>

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
            <h3 class="text-center" style="font-family: Verdana">Closed Margin Upload</h3><br><br>
            <table class="normal_grid">
                <tr>                  
                    <td class="auto-style2" colspan="3" align="center">
                        <strong>Upload Schedule Plan: <span class="hlt-txt">*</span></strong>
                        <asp:FileUpload ID="FileUpload1" runat="server" accept="application/vnd.ms-excel,application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" />
                        <asp:Button ID="btnUpload" Text="Upload" OnClick="btnUpload_Onclick" runat="server" /><br><br>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table>
                <tr>
                    <td class="auto-style2" colspan="3" align="center">
                        <asp:Button ID="btnGenerateTemplate" Text="Excel Template" OnClick="GenerateTemplate_OnClick" runat="server" />
                        <asp:Button ID="BtnGenerate" runat="server" Text="Generate Actual" OnClick="btnActual_Click" Visible="True"/>
                        <asp:Button ID="btnDwnldExl" runat="server" Text="Download" OnClick="btnDwnldExl_Click" Visible="True"/>
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

        <%--<div class="normal-table1" style="width: 900px; margin: 0 auto;">

           
        </div>--%>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
