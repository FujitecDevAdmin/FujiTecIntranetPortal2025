<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="FujiTecIntranetPortal.ChangePassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Update Credential</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>
                        Change Password: <asp:TextBox runat="server" ID="txtPassword"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Confirm Password: <asp:TextBox runat="server" ID="txtConfirmPassword"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
