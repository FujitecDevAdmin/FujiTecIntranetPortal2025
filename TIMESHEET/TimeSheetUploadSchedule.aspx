<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TimeSheetUploadSchedule.aspx.cs" Inherits="FujiTecIntranetPortal.TIMESHEET.TimeSheetUploadSchedule" %>
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
            width: 15%;
            height: 30px;
        }
         .auto-style3 {
            width: 6px;
            height: 30px;
        }

        .space {
            width: 10%;
            height: 30px;
        }

        .normal-table1 {
            font-size: 1px !important;
            border: 10px solid White !important;
            padding: 2px !important;
            margin-bottom: 10px !important;
        }

        .normal-table2 {
            font-size: 1px !important;
            border: 2px solid Black !important;
            padding: 2px !important;
            margin-bottom: 10px !important;
        }

        .normal-table3 {
            font-size: 1px !important;
            border: 2px solid White !important;
            padding: 2px !important;
            margin-bottom: 10px !important;
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
            <h3 class="text-center" style="font-family: Verdana">Timesheet Schedule Upload</h3>
            <br /> 
            <table class="normal_grid">
                <tr>
                 
                    <td class="auto-style2" colspan="3" align="center">
                        <%-- <asp:DropDownList ID="DropDownList1" CssClass="myInput" runat="server" Width="183px" TabIndex="2" AutoPostBack="true">
                    </asp:DropDownList>--%>
                        <strong>Upload Schedule Plan: <span class="hlt-txt">*</span></strong>
                        <asp:FileUpload ID="FileUpload1" runat="server" accept="application/vnd.ms-excel,application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" />
                        <asp:Button ID="btnUpload" Text="Upload" OnClick="Upload" runat="server" /><br />
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table>
                <tr>
                    <td class="auto-style2" colspan="3" align="center">
                        <br />
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

      
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
