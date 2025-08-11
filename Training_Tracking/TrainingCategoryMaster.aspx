<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TrainingCategoryMaster.aspx.cs" Inherits="FujiTecIntranetPortal.Training_Tracking.TrainingCategoryMaster" %>

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
        }

        .normal-table1 {
            font-size: 1px !important;
            border: 10px solid white !important;
            padding: 2px !important;
            margin-bottom: 10px !important;
        }

        .myInput:focus {
            border: solid 1px green !important;
            outline: double !important;
        }

        .auto-style3 {
            font-weight: normal;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <div class="page-container scroll-y">
        <div>
            <h3 class="text-center" style="font-family: Verdana">Training Category Master</h3>
            <table class="normal_grid">
                <tr>
                    <th class="auto-style2"><strong>Training No: </strong>&nbsp;</th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtTrainingNo" CssClass="myInput" runat="server" Width="183px" TabIndex="1" Enabled="false"></asp:TextBox>
                    </td>
                    <th class="auto-style2"><strong>Select Training Type: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:DropDownList ID="ddlSelectTraining" CssClass="myInput" runat="server" Width="183px" TabIndex="2" AutoPostBack="true" OnSelectedIndexChanged="ddlSelectTraining_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                    <th class="auto-style2"><strong>Module Name: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtName" CssClass="myInput" runat="server" Width="183px" TabIndex="3"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <%--   <th class="auto-style2"><strong>Training Category: </strong>
                    </th>
                    <td class="auto-style2">
                        <asp:DropDownList ID="ddlTrainingCategory" CssClass="myInput" runat="server" Width="183px" TabIndex="3" AutoPostBack="true"></asp:DropDownList>
                    </td>--%>
                    <th class="auto-style2"><strong>Status : </strong>
                    </th>
                    <td class="auto-style2">
                        <asp:DropDownList ID="ddStatus" CssClass="myInput" runat="server" Width="183px" TabIndex="4" AutoPostBack="true"></asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table>
                <tr>
                    <td class="auto-style2" colspan="3" align="center">

                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="myInput"
                            TabIndex="5" OnClick="btnSave_Click"></asp:Button>
                        <%-- <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="myInput"
                            TabIndex="12" OnClick="btnUpdate_Click"></asp:Button>--%>
                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="myInput"
                            TabIndex="6" OnClick="btnClear_Click"></asp:Button>
                        <%--<asp:Button ID="btnback" runat="server" Text="Go To Dashboardpage" CssClass="myInput"
                            TabIndex="14" OnClick="btnback_Click"></asp:Button>--%>
                    </td>

                </tr>
                <tr>
                    <td class="auto-style2" colspan="3" align="center">
                        <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label></td>
                </tr>

            </table>
        </div>
        <div class="normal-table1">
            <asp:GridView ID="gv" runat="server" AutoGenerateSelectButton="True" OnSelectedIndexChanged="GV_SelectedIndexChanged"
                OnPageIndexChanging="OnPageIndexChanging" AllowPaging="true" PageSize="10" GridLines="Both" HeaderStyle-BorderStyle="Solid"
                HeaderStyle-BackColor="#9AD6ED" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-BorderStyle="Solid" BorderStyle="Solid" FooterStyle-BorderStyle="Solid"
                HeaderStyle-Height="50px" Width="100%" HeaderStyle-ForeColor="#636363">
            </asp:GridView>
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

