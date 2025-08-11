<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SkillMappingMaster.aspx.cs" Inherits="FujiTecIntranetPortal.Master.CategoryMaster" %>

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
            <h3 class="text-center" style="font-family: Verdana">Skill Master</h3>
            <table class="normal_grid">
                <tr>
                    <th class="auto-style2"><strong>Skill ID: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtSkillID" CssClass="myInput" runat="server" Width="183px" TabIndex="1" Enabled="false"></asp:TextBox>
                    </td>
                    <th class="auto-style2"><strong>Skill Assesment Name: <span class="hlt-txt">*</span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtSkillName" CssClass="myInput" runat="server" Width="200px" TabIndex="2" TextMode="MultiLine" Height="53px" OnTextChanged="txtSkillName_TextChanged" Style="margin-left: 0"></asp:TextBox>
                    </td>
                    <th class="auto-style2"><strong>Skill Type: <span class="hlt-txt"></span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:DropDownList ID="ddSkillType" CssClass="myInput" runat="server" Width="183px" TabIndex="3" AutoPostBack="true"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th class="auto-style2"><strong>Status: <span class="hlt-txt"></span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:DropDownList ID="ddStatus" CssClass="myInput" runat="server" Width="183px" TabIndex="4" AutoPostBack="true"></asp:DropDownList>
                    </td>
                </tr>
                <%-- <tr>
                    <th class="auto-style2"><strong>Desgination: <span class="hlt-txt"></span></strong>
                    </th>
                  
                    <td class="auto-style3">
                        <asp:CheckBoxList runat="server" ID="cbl_Ds1" AutoPostBack="True" CssClass="myInput" CellPadding="25" CellSpacing="25" RepeatColumns="1" RepeatDirection="Vertical" RepeatLayout="Flow" TextAlign="Right" Height="16px" TabIndex="6" Font-Bold="true">
                            <asp:ListItem Text="Installation Technician" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Project Cordinator" Value="4"></asp:ListItem>
                            <asp:ListItem Text="IPCC Technician" Value="5"></asp:ListItem>                            
                            <asp:ListItem Text="Adjuster Technician" Value="6"></asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                    <td>
                        <asp:CheckBoxList runat="server" ID="cbl_Ds2" AutoPostBack="True" CssClass="myInput" CellPadding="25" CellSpacing="25" RepeatColumns="1" RepeatDirection="Vertical" RepeatLayout="Flow" TextAlign="Right" Height="16px" TabIndex="6" Font-Bold="true">
                             
                            <asp:ListItem Text="Installation Incharge" Value="7"></asp:ListItem>
                            <asp:ListItem Text="Installation Engineer" Value="8"></asp:ListItem>
                            <asp:ListItem Text="Service Technicians" Value="9"></asp:ListItem>
                            <asp:ListItem Text="Repair Technicians" Value="10"></asp:ListItem>

                        </asp:CheckBoxList></td>
                    <td>
                        <asp:CheckBoxList runat="server" ID="cbl_Ds3" AutoPostBack="True" CssClass="myInput"  CellPadding="25" CellSpacing="25" RepeatColumns="1" RepeatDirection="Vertical" RepeatLayout="Flow" TextAlign="Right" Height="16px" TabIndex="6" Font-Bold="true">
                           
                            
                            <asp:ListItem Text="Service Incharge" Value="11"></asp:ListItem>
                            <asp:ListItem Text="Service Engineer" Value="12"></asp:ListItem>

                        </asp:CheckBoxList></td>
                     
                </tr>--%>
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
            <asp:GridView ID="gv" runat="server" AutoGenerateSelectButton="True" OnSelectedIndexChanged="GV_SelectedIndexChanged" TabIndex="17" AutoGenerateColumns="false"
                OnPageIndexChanging="OnPageIndexChanging" AllowPaging="true" PageSize="10" GridLines="Both" HeaderStyle-BorderStyle="Solid"
                HeaderStyle-BackColor="#9AD6ED" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-BorderStyle="Solid" BorderStyle="Solid" FooterStyle-BorderStyle="Solid"
                HeaderStyle-Height="50px" Width="50%" HeaderStyle-ForeColor="#636363">
                <Columns>
                    <%-- <asp:BoundField HeaderText="S.No" DataField="SNO" />--%>
                    <asp:BoundField HeaderText="Skill ID" DataField="ID" />
                    <asp:BoundField HeaderText="Skill Name" DataField="Skill" />
                    <asp:BoundField HeaderText="Skill Type" DataField="SkillType"  />
                    <asp:BoundField HeaderText="Skill Type" DataField="SkillTypeDesc" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <%--</asp:Panel>--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterContent" runat="server">
</asp:Content>
