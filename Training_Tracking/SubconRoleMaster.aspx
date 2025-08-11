<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SubconRoleMaster.aspx.cs" Inherits="FujiTecIntranetPortal.Training_Tracking.SubconRoleMaster" %>
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
         .auto-style1 {
            width: 5%;
            height: 30px;
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

        .normal-table2 {
            font-size: 1px !important;
            border: 2px solid Black !important;
            padding: 2px !important;
            margin-bottom: 10px !important;
            left:500px !important;
            margin-left:200px;
            margin-right:200px;
        }

        .myInput:focus {
            border: solid 1px green !important;
            outline: double !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
      <asp:Panel runat="server" SkinID="Default" ID ="dft"></asp:Panel>
    <div class="page-container scroll-y">
        <div>
            <h3 class="text-center" style="font-family: Verdana">SubContractor Role Master</h3>
            <table class="normal_grid">
                <tr>
                    <th class="auto-style2"><strong>Role ID: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtRoleID" CssClass="myInput" runat="server" Width="183px" TabIndex="1" Enabled="false"></asp:TextBox>
                    </td>
                    <th class="auto-style2"><strong>Branch Name: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                      <%--  <asp:TextBox ID="txtDepartmentName" CssClass="myInput" runat="server" Width="183px" TabIndex="2"></asp:TextBox>--%>
                         <asp:DropDownList ID="ddBranchID" CssClass="myInput" runat="server" Width="183px" TabIndex="2" AutoPostBack="true"></asp:DropDownList>
                    </td>
                    <th class="auto-style2"><strong> ApproverID: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtApproverID" CssClass="myInput" runat="server" Width="183px" TabIndex="3"></asp:TextBox>
                    </td>
                </tr>        
                 <tr>
                    <th class="auto-style2"><strong>Approver Role: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                      <%--  <asp:TextBox ID="txtDepartmentName" CssClass="myInput" runat="server" Width="183px" TabIndex="2"></asp:TextBox>--%>
                         <asp:DropDownList ID="ddApproverRole" CssClass="myInput" runat="server" Width="183px" TabIndex="4" AutoPostBack="true"></asp:DropDownList>
                    </td>
                    <th class="auto-style2"><strong> Status: <span class="hlt-txt"></span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:DropDownList ID="ddStatus" CssClass="myInput" runat="server" Width="183px" TabIndex="5" AutoPostBack="true"></asp:DropDownList>
                    </td>
                </tr>           
            </table>
        </div>
        <div>
            <table>
                <tr>
                    <td class="auto-style2" colspan="3" align="center">

                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="myInput"
                            TabIndex="12" OnClick="btnSave_Click"></asp:Button>
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
        <div class="normal-table1">

          

            <asp:GridView ID="gv" runat="server" AutoGenerateSelectButton="True" OnSelectedIndexChanged="GV_SelectedIndexChanged" TabIndex="14"
                OnPageIndexChanging="OnPageIndexChanging" AllowPaging="true" PageSize="10" GridLines="Both" HeaderStyle-BorderStyle="Solid"
                HeaderStyle-BackColor="#9AD6ED" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-BorderStyle="Solid" BorderStyle="Solid" FooterStyle-BorderStyle="Solid"
                HeaderStyle-Height="50px" Width="100%" HeaderStyle-ForeColor="#636363">
            </asp:GridView>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
