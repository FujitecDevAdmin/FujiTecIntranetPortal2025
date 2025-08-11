<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Testform.aspx.cs" Inherits="FujiTecIntranetPortal.Testform" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" id="login-page">
<head id="Head1" runat="server">
    <title>National Film Archive of India (NFAI) - Asset Tracking</title>
    <link rel="shortcut icon" href="~/assets/images/favicon.ico" />
	<meta name="viewport" content="width=device-width, initial-scale=1.0" />
	<link rel="stylesheet" href="~/assets/css/bootstrap.min.css" type="text/css" media="screen" />
	<link rel="stylesheet" href="~/assets/css/line-awesome.min.css" type="text/css" media="screen" />
	<link rel="stylesheet" href="~/assets/css/style.css" type="text/css" media="screen" />
</head>
<body class="login-page">
    <form id="form1" runat="server">
   
    <div class="login-container">
        <div class="login-title text-center">
            <h2>Film Collection Assessment Application</h2>
        </div>
        <div class="login-row">
            <asp:Label ID="Label1" CssClass="login-label" runat="server" Text="Username"></asp:Label>
            <asp:TextBox ID="rtxtUsername" runat="server">
            </asp:TextBox>
        </div>
        <div class="login-row">
            <asp:Label ID="Label2" CssClass="login-label" runat="server" Text="Password"></asp:Label>
            <asp:TextBox ID="rtxtPassword" TextMode="Password" runat="server">
            </asp:TextBox>
        </div>
        <div class="login-btn text-center">
            <asp:Button ID="rbtnLogin" runat="server"  CssClass="btn-icons">               
            </asp:Button>
        </div>
    </div>
    
    </form>
</body>
</html>
