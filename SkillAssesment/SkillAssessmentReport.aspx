<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SkillAssessmentReport.aspx.cs" Inherits="FujiTecIntranetPortal.SkillAssesment.SkillAssessmentReport" %>

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

        .center {
            display: flex;
            justify-content: center;
            align-items: center;
        }

        .Middle {
            margin: auto;
            width: 80%;
            padding: 50px;
        }

        .auto-style2 {
            width: 17%;
            height: 30px;
        }

        .myInput:focus {
            border: solid 1px green !important;
            outline: double !important;
        }

        .middlepane {
            width: 85%;
            float: left;
            background-color: white;
            border-collapse: collapse;
        }

        .rightpane {
            width: 75%;
            position: relative;
            float: right;
            background-color: white;
            border-collapse: collapse;
        }

        .header-center {
            text-align: center;
        }

        .normal-table1 {
            font-size: 1px !important;
            border: 10px solid white !important;
            padding: 2px !important;
            margin-bottom: 5px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:Panel runat="server" SkinID="Default" ID="dft"></asp:Panel>
    <div class="page-container scroll-y">
        <div>
            <h3 class="text-center" style="font-family: Verdana">TNA</h3>
            <table class="normal_grid">
                <tr>
                    <th class="auto-style2"><strong>Skill Set: <span class="hlt-txt"></span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:DropDownList ID="ddSkillSet" CssClass="myInput" runat="server" Width="183px" TabIndex="1" AutoPostBack="true"></asp:DropDownList>
                    </td>
                    <th class="auto-style2"><strong>Employee ID: <span class="hlt-txt"></span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtEmployeeID" CssClass="myInput" runat="server" Width="183px" TabIndex="2" AutoPostBack="true"></asp:TextBox>
                    </td>

                    <th class="auto-style2"><strong>Department: <span class="hlt-txt"></span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:DropDownList ID="ddDepartment" CssClass="myInput" runat="server" Width="183px" TabIndex="3" AutoPostBack="true"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th class="auto-style2"><strong>Desgination: <span class="hlt-txt"></span></strong>
                    </th>
                    <td class="auto-style3">
                        <asp:DropDownList ID="ddDesignation" CssClass="myInput" runat="server" Width="183px" TabIndex="4" AutoPostBack="true"></asp:DropDownList>
                    </td>
                    <th class="auto-style2"><strong>Location: <span class="hlt-txt"></span></strong>
                    </th>
                    <td class="auto-style3">
                        <asp:DropDownList ID="ddLocation" CssClass="myInput" runat="server" Width="183px" TabIndex="5" AutoPostBack="true"></asp:DropDownList>
                    </td>                  
                </tr>
            </table>
        </div>
         <div>
            <table>
                <tr>
                    <td class="auto-style2" colspan="3" align="center">
                        <asp:Button ID="btnGenerate" runat="server" Text="Generate" CssClass="myInput" 
                            TabIndex="12" OnClick="btnGenerate_Click"></asp:Button>
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
       <%-- <div class="Middle">
            <asp:GridView ID="gv" runat="server" AutoGenerateSelectButton="false" TabIndex="17" AutoGenerateColumns="false" HorizontalAlign="Right"
                OnPageIndexChanging="OnPageIndexChanging" AllowPaging="true" PageSize="10" GridLines="Both" HeaderStyle-BorderStyle="Solid"
                HeaderStyle-BackColor="#9AD6ED" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-BorderStyle="Solid" BorderStyle="Solid" FooterStyle-BorderStyle="Solid"
                HeaderStyle-Height="50px" Width="90%" HeaderStyle-ForeColor="#636363">
                <Columns>
                    <asp:BoundField HeaderText="S.NO" DataField="SNO" />
                    <asp:BoundField HeaderText="Activities" DataField="SkillDesc" HeaderStyle-CssClass="header-center" />
                    <asp:BoundField HeaderText="Skill Level" DataField="SkillLevel" HeaderStyle-CssClass="header-center" ItemStyle-HorizontalAlign="Center" />
                </Columns>
            </asp:GridView>
        </div>--%>
       
    </div>

    <%--</asp:Panel>--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
