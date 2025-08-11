<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SkillAssessmentPopup.aspx.cs" Inherits="FujiTecIntranetPortal.SkillAssesment.SkillAssessmentPopup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="gv" runat="server" AutoGenerateColumns="true"
                AllowPaging="false" OnRowDataBound="OnRowDataBound"
                HeaderStyle-BackColor="#9AD6ED" AlternatingRowStyle-BackColor="WhiteSmoke" RowStyle-BorderStyle="Solid" BorderStyle="Solid" FooterStyle-BorderStyle="Solid"
                HeaderStyle-Height="20px" Width="90%" HeaderStyle-ForeColor="#636363" HeaderStyle-Font-Size ="Medium" Font-Size ="Small">
            </asp:GridView>
        </div>
    </form>
</body>
</html>
