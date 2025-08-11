<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SubConRegistration.aspx.cs" Inherits="FujiTecIntranetPortal.Training_Tracking.SubConRegistration" %>

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
            left: 500px !important;
            margin-left: 200px;
            margin-right: 200px;
        }

        .myInput:focus {
            border: solid 1px green !important;
            outline: double !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

    <div class="page-container scroll-y">
        <div>
            <h3 class="text-center" style="font-family: Verdana">Sub-Contractor Registration Process</h3>
            <table class="normal-table">
                <tr>
                    <th class="auto-style2"><strong>SubConID: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtID" CssClass="myInput" runat="server" Width="183px" TabIndex="1" OnTextChanged="txtID_TextChanged" AutoPostBack="true"></asp:TextBox>
                    </td>
                    <th class="auto-style2"><strong>Branch: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:DropDownList ID="ddBranchID" CssClass="myInput" runat="server" Width="183px" TabIndex="2" AutoPostBack="true"></asp:DropDownList>
                    </td>
                    <th class="auto-style2"><strong>SC company Name: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtSCCompanyName" CssClass="myInput" runat="server" Width="183px" TabIndex="3"></asp:TextBox>
                    </td>
                </tr>
                <tr >
                    <td align="center" colspan="4">
                        <asp:TextBox ID="txtlvl1" runat="server" BackColor="Silver" Width="20px" Enabled="false" Height="15px"></asp:TextBox>
                        <asp:Label ID="lbl1" runat="server" Text="Area Admin" Font-Bold="true"></asp:Label>
                        <asp:TextBox ID="txtlvl2" runat="server" BackColor="Silver" Width="20px" Enabled="false" Height="15px"></asp:TextBox>
                        <asp:Label ID="lbl2" runat="server" Text="Branch Installation" Font-Bold="true"></asp:Label>
                        <asp:TextBox ID="txtlvl3" runat="server" BackColor="Silver" Width="20px" Enabled="false" Height="15px"></asp:TextBox>
                        <asp:Label ID="lbl3" runat="server" Text="Area Safety" Font-Bold="true"></asp:Label>                       
                        <asp:TextBox ID="txtlvl4" runat="server" BackColor="Silver" Width="20px" Enabled="false" Height="15px"></asp:TextBox>
                        <asp:Label ID="lbl4" runat="server" Text="Area Head" Font-Bold="true"></asp:Label>
                        <asp:TextBox ID="txtlvl5" runat="server" BackColor="Silver" Width="20px" Enabled="false" Height="15px"></asp:TextBox>
                        <asp:Label ID="lbl5" runat="server" Text="FOD CoOrdinator HO" Font-Bold="true"></asp:Label>
                        <asp:TextBox ID="txtlvl6" runat="server" BackColor="Silver" Width="20px" Enabled="false" Height="15px"></asp:TextBox>
                        <asp:Label ID="lbl6" runat="server" Text="HRD" Font-Bold="true"></asp:Label>
                        <asp:TextBox ID="txtlvl7" runat="server" BackColor="Silver" Width="20px" Enabled="false" Height="15px"></asp:TextBox>
                        <asp:Label ID="lbl7" runat="server" Text="FOD Head" Font-Bold="true"></asp:Label>
                        <asp:TextBox ID="txtlvl8" runat="server" BackColor="Silver" Width="20px" Enabled="false" Height="15px"></asp:TextBox>
                        <asp:Label ID="lbl8" runat="server" Text="Finance" Font-Bold="true"></asp:Label>
                    </td>
                    <th class="auto-style2"><strong>Vendor ID: <span class="hlt-txt"></span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtVendorID" CssClass="myInput" runat="server" Width="183px" TabIndex="3"></asp:TextBox>
                    </td>
                </tr>
              <tr><td align="center" colspan="6"><asp:Label ID="Label1" runat="server" Text="* Area Admin will create a registration checklist with details from 1 to 11 points.   * Area Safety will create PPF,subcon_EmpID and Induction, finally update the checklist with details from points 12 to 15." Font-Bold="true" ForeColor ="Red"></asp:Label></td>
                  <%--<td align="center" colspan="3"><asp:Label ID="Label2" runat="server" Text="* Area Safety will create PPF,EmpID and Induction, finally update the checklist with details from points 12 to 15." Font-Bold="true" ForeColor ="Red"></asp:Label></td>--%>
              </tr>
            </table>
        </div>
        <div class="normal-table2">
            <h3 class="text-center" style="font-family: Verdana">Description Check list</h3>
            <table>
                <tr>
                    <th class="auto-style1"><strong>S.no</strong></th>
                    <th class="auto-style2"><strong>Description Check list </strong></th>
                    <th class="auto-style2"><strong>Yes/No <span class="hlt-txt"></span></strong></th>
                    <th class="auto-style2"><strong>Remarks <span class="hlt-txt"></span></strong></th>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="lblSno1" runat="server" Text="1"></asp:Label>
                    </td>
                    <td class="auto-style2">
                        <asp:Label ID="lblCheckList1" runat="server"> Subcontractor Prequalication Form<span class="hlt-txt">*</span></></asp:Label>
                    </td>
                    <td class="auto-style2">
                        <asp:DropDownList ID="dd1" CssClass="myInput" runat="server" Width="183px" TabIndex="4" AutoPostBack="true">
                            <%--<asp:ListItem Enabled="true" Text="Select" Value="-1"></asp:ListItem>--%>
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style2">
                        <asp:TextBox ID="txt1" CssClass="myInput" runat="server" Width="183px" TabIndex="5"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="lblSno2" runat="server" Text="2"></asp:Label>
                    </td>
                    <td class="auto-style2">
                        <asp:Label ID="lblCheckList2" runat="server">Subcontractor  Registration Form<span class="hlt-txt">*</span></asp:Label>
                    </td>
                    <td class="auto-style2">
                        <asp:DropDownList ID="dd2" CssClass="myInput" runat="server" Width="183px" TabIndex="6" AutoPostBack="true">
                            <%--<asp:ListItem Enabled="true" Text="Select" Value="-1"></asp:ListItem>--%>
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style2">
                        <asp:TextBox ID="txt2" CssClass="myInput" runat="server" Width="183px" TabIndex="7"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="lblSno3" runat="server" Text="3"></asp:Label>
                    </td>
                    <td class="auto-style2">
                        <asp:Label ID="lblCheckList3" runat="server">Agreement of Subcontractor<span class="hlt-txt">*</span></asp:Label>
                    </td>
                    <td class="auto-style2">
                        <asp:DropDownList ID="dd3" CssClass="myInput" runat="server" Width="183px" TabIndex="8" AutoPostBack="true">
                            <%--<asp:ListItem Enabled="true" Text="Select" Value="-1"></asp:ListItem>--%>
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style2">
                        <asp:TextBox ID="txt3" CssClass="myInput" runat="server" Width="183px" TabIndex="9"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="lblSno4" runat="server" Text="4"></asp:Label>
                    </td>
                    <td class="auto-style2">
                        <asp:Label ID="lblCheckList4" runat="server">Pan Card Copy<span class="hlt-txt">*</span></asp:Label>
                    </td>
                    <td class="auto-style2">
                        <asp:DropDownList ID="dd4" CssClass="myInput" runat="server" Width="183px" TabIndex="10" AutoPostBack="true">
                            <%--<asp:ListItem Enabled="true" Text="Select" Value="-1"></asp:ListItem>--%>
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style2">
                        <asp:TextBox ID="txt4" CssClass="myInput" runat="server" Width="183px" TabIndex="11"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="lblSno5" runat="server" Text="5"></asp:Label>
                    </td>
                    <td class="auto-style2">
                        <asp:Label ID="lblCheckList5" runat="server"> Bank Details or  Cancelled Cheque [Passbook copy or cancelled cheque leaf] <span class="hlt-txt">*</span></asp:Label>
                    </td>
                    <td class="auto-style2">
                        <asp:DropDownList ID="dd5" CssClass="myInput" runat="server" Width="183px" TabIndex="12" AutoPostBack="true">
                            <%--<asp:ListItem Enabled="true" Text="Select" Value="-1"></asp:ListItem>--%>
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style2">
                        <asp:TextBox ID="txt5" CssClass="myInput" runat="server" Width="183px" TabIndex="13"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="lblSno6" runat="server" Text="6"></asp:Label>
                    </td>
                    <td class="auto-style2">
                        <asp:Label ID="lblCheckList6" runat="server">Self Declaration Or GST<span class="hlt-txt">*</span></asp:Label>
                    </td>
                    <td class="auto-style2">
                        <asp:DropDownList ID="dd6" CssClass="myInput" runat="server" Width="183px" TabIndex="14" AutoPostBack="true">
                            <%--<asp:ListItem Enabled="true" Text="Select" Value="-1"></asp:ListItem>--%>
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style2">
                        <asp:TextBox ID="txt6" CssClass="myInput" runat="server" Width="183px" TabIndex="15"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="lblSno7" runat="server" Text="7"></asp:Label>
                    </td>
                    <td class="auto-style2">
                        <asp:Label ID="lblCheckList7" runat="server">Bio Data<span class="hlt-txt">*</span></asp:Label>
                    </td>
                    <td class="auto-style2">
                        <asp:DropDownList ID="dd7" CssClass="myInput" runat="server" Width="183px" TabIndex="16" AutoPostBack="true">
                            <%--<asp:ListItem Enabled="true" Text="Select" Value="-1"></asp:ListItem>--%>
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style2">
                        <asp:TextBox ID="txt7" CssClass="myInput" runat="server" Width="183px" TabIndex="17"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="lblSno8" runat="server" Text="8"></asp:Label>
                    </td>
                    <td class="auto-style2">
                        <asp:Label ID="lblCheckList8" runat="server">Appointment Letter<span class="hlt-txt">*</span></asp:Label>
                    </td>
                    <td class="auto-style2">
                        <asp:DropDownList ID="dd8" CssClass="myInput" runat="server" Width="183px" TabIndex="18" AutoPostBack="true">
                            <%--<asp:ListItem Enabled="true" Text="Select" Value="-1"></asp:ListItem>--%>
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style2">
                        <asp:TextBox ID="txt8" CssClass="myInput" runat="server" Width="183px" TabIndex="19"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="lblSno9" runat="server" Text="9"></asp:Label>
                    </td>
                    <td class="auto-style2">
                        <asp:Label ID="lblCheckList9" runat="server">ESI Form<span class="hlt-txt">*</span></asp:Label>
                    </td>
                    <td class="auto-style2">
                        <asp:DropDownList ID="dd9" CssClass="myInput" runat="server" Width="183px" TabIndex="20" AutoPostBack="true">
                            <%--<asp:ListItem Enabled="true" Text="Select" Value="-1"></asp:ListItem>--%>
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style2">
                        <asp:TextBox ID="txt9" CssClass="myInput" runat="server" Width="183px" TabIndex="21"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="lblSno10" runat="server" Text="10"></asp:Label>
                    </td>
                    <td class="auto-style2">
                        <asp:Label ID="lblCheckList10" runat="server">PF Form<span class="hlt-txt">*</span></asp:Label>
                    </td>
                    <td class="auto-style2">
                        <asp:DropDownList ID="dd10" CssClass="myInput" runat="server" Width="183px" TabIndex="22" AutoPostBack="true">
                            <%--<asp:ListItem Enabled="true" Text="Select" Value="-1"></asp:ListItem>--%>
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style2">
                        <asp:TextBox ID="txt10" CssClass="myInput" runat="server" Width="183px" TabIndex="23"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="lblSno11" runat="server" Text="11"></asp:Label>
                    </td>
                    <td class="auto-style2">
                        <asp:Label ID="lblCheckList11" runat="server">Nagarik  Suraksha Form<span class="hlt-txt">*</span></asp:Label>
                    </td>
                    <td class="auto-style2">
                        <asp:DropDownList ID="dd11" CssClass="myInput" runat="server" Width="183px" TabIndex="24" AutoPostBack="true">
                            <%--<asp:ListItem Enabled="true" Text="Select" Value="-1"></asp:ListItem>--%>
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style2">
                        <asp:TextBox ID="txt11" CssClass="myInput" runat="server" Width="183px" TabIndex="25"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="lblSno12" runat="server" Text="12"></asp:Label>
                    </td>
                    <td class="auto-style2">
                        <asp:Label ID="lblCheckList12" runat="server">PPE Issued<span class="hlt-txt">*</span></asp:Label>
                    </td>
                    <td class="auto-style2">
                        <asp:DropDownList ID="dd12" CssClass="myInput" runat="server" Width="183px" TabIndex="26" AutoPostBack="true">
                            <asp:ListItem Enabled="true" Text="Select" Value="-1"></asp:ListItem>
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style2">
                        <asp:TextBox ID="txt12" CssClass="myInput" runat="server" Width="183px" TabIndex="27"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="lblSno13" runat="server" Text="13"></asp:Label>
                    </td>
                    <td class="auto-style2">
                        <asp:Label ID="lblCheckList13" runat="server"> Safety Induction Given<span class="hlt-txt">*</span></asp:Label>
                    </td>
                    <td class="auto-style2">
                        <asp:DropDownList ID="dd13" CssClass="myInput" runat="server" Width="183px" TabIndex="28" AutoPostBack="true">
                            <asp:ListItem Enabled="true" Text="Select" Value="-1"></asp:ListItem>
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style2">
                        <asp:TextBox ID="txt13" CssClass="myInput" runat="server" Width="183px" TabIndex="29"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="lblSno14" runat="server" Text="14"></asp:Label>
                    </td>
                    <td class="auto-style2">
                        <asp:Label ID="lblCheckList14" runat="server">ID Card Issued<span class="hlt-txt">*</span></asp:Label>
                    </td>
                    <td class="auto-style2">
                        <asp:DropDownList ID="dd14" CssClass="myInput" runat="server" Width="183px" TabIndex="30" AutoPostBack="true">
                            <asp:ListItem Enabled="true" Text="Select" Value="-1"></asp:ListItem>
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style2">
                        <asp:TextBox ID="txt14" CssClass="myInput" runat="server" Width="183px" TabIndex="31"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table>
                <tr>
                    <td class="auto-style2" colspan="3" align="center">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="myInput"
                            TabIndex="32" OnClick="btnSave_Click"></asp:Button>
                           <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="myInput"
                            TabIndex="32" OnClick="btnUpdate_Click"></asp:Button>
                        <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="myInput"
                            TabIndex="33" OnClick="btnApprove_Click"></asp:Button>
                        <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="myInput"
                            TabIndex="34" OnClick="btnReject_Click"></asp:Button>
                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="myInput"
                            TabIndex="35" OnClick="btnClear_Click"></asp:Button>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2" colspan="3" align="center">
                        <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label></td>
                </tr>
            </table>
        </div>
        <div><b style="color: white">abc</b></div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
