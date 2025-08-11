<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewandEventPopup.aspx.cs" Inherits="FujiTecIntranetPortal.NewandEventPopup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Request for Approval</title>
    <link rel="stylesheet" href="~/assets/css/bootstrap.min.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="~/assets/css/line-awesome.min.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="~/assets/css/style.css" type="text/css" media="screen" />
    <style type="text/css">
        .center {
            display: flex;
            justify-content: center;
            align-items: center;
        }

        .midd {
            justify-content: center;
            align-items: center;
        }

        .auto-style2 {
            margin-left: 8px;
            font-family:Verdana;
        }

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



        .login {
            background-color: white;
            color: white;
        }

        .pad {
            padding-top: 50px;
            padding-right: 30px;
            padding-bottom: 50px;
            padding-left: 80px;
        }

        p {
            background-color: #CF2331;
        }

        .hd {
            font-family: 'Brush Script MT', cursive !important;
            color: red;
            text-decoration: underline;
        }

        .normal-table {
            font-size: 14px !important;
            border: 10px solid White !important;
            padding: 5px !important;
            margin-bottom: 10px !important;
        }

        .normallg {
            font-size: 14px !important;
            padding: 10px !important;
            margin-bottom: 20px !important;
        }

        .normal-table2 {
            font-size: 14px !important;
            padding: 1px !important;
            margin-bottom: 10px !important;
            border: 2px solid lightgray !important;
            border-radius: 5px 5px 5px 5px;
            /* display: flex;*/
        }

        .gridcss {
            padding: 2px;
            !important;
            color: black;
            font-size: 15px;
            width: 150px;
            /*font-size: large*/
        }

        .gridcss1 {
            color: darkblue;
            font-size: 13px;
            font-family: Italic;
            font-weight: bold;
            width: 150px;
        }

        .clr {
            background-color: #EDF2EB;
            margin: 2px;
            border-left: 200px solid white;
        }



        .txtbox {
            border-top-left-radius: 20px;
            border-top-right-radius: 20px;
            border-bottom-left-radius: 20px;
            border-bottom-right-radius: 20px;
            margin-left: 20px;
        }


        body, html {
            width: 100%;
            height: 100%;
            margin: 0;
        }

        .container {
            width: 100%;
            height: 100%;
        }

        .leftpane {
            width: 70%;
            height: 100%;
            float: left;
            background-color: white;
            border-collapse: collapse;
        }


        .geek1 {
            font-size: 36px;
            font-weight: bold;
            color: white;
            text-align: center;
        }

        .rightpane {
            width: 75%;
            position: relative;
            float: right;
            background-color: white;
            border-collapse: collapse;
        }

        .toppane {
            width: 100%;
            height: 50px;
            border-collapse: collapse;
            background-color: white;
        }

        .splitleftpane {
            width: 10px;
            height: 100px;
            float: left;
            background-color: white;
            border-collapse: collapse;
        }

        .splitrightpane {
            width: 10%;
            height: 200px;
            position: relative;
            float: right;
            background-color: white;
            border-collapse: collapse;
        }

        .bottom-left {
            position: absolute;
            bottom: 8px;
            left: 16px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="toppane"></div>
        <div class="center">
            <h3 style="font-family: Verdana">Request for News and Events Approval</h3>

        </div>
        <div>
            <table class="center">
                <tr>
                    <th class="auto-style2"><strong><span class="hlt-txt"></span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtID" CssClass="myInput" runat="server" Width="232px" TabIndex="1" Height="60px" TextMode="MultiLine" Style="margin-left: 0" Visible="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th class="auto-style2"><strong>Event Name  :   <span class="hlt-txt"></span></strong>
                    </th>
                    <td class="auto-style2">
                        <asp:TextBox ID="txtEventName" CssClass="myInput" runat="server" Width="232px" TabIndex="1" Height="60px" TextMode="MultiLine" Style="margin-left: 0" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th class="auto-style2"><strong>Event Photo  :   <span class="hlt-txt"></span></strong>
                    </th>
                    <td>
                        <asp:Image ID="img_Empphoto" runat="server" Height="350px" Width="550" ImageAlign="Right" />
                    </td>
                </tr>
                <tr>
                    <th class="auto-style2"><strong>Remarks: <span class="hlt-txt"></span></strong></th>
                        <td>
                            <asp:TextBox ID="txtRemarks" CssClass="myInput" runat="server" Width="232px" TabIndex="1" Height="50px" TextMode="MultiLine" Style="margin-left: 0" Visible="true"></asp:TextBox></td>
                </tr>
            </table>
        </div>
        <div></div>
        <div class="container " style="left: 100px; top: 35px; height: 0px">

            <table class="center normal-table">
                <tr>
                    <td class="auto-style2" colspan="3">
                        <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="myInput"
                            TabIndex="12" OnClick="btnApprove_Click"></asp:Button>

                        <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="myInput"
                            TabIndex="13" OnClick="btnReject_Click"></asp:Button>

                        <asp:Button ID="btnback" runat="server" Text="Go back" CssClass="myInput"
                            TabIndex="14" OnClick="btnback_Click"></asp:Button>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2" colspan="3" align="center">
                        <asp:Label ID="lblmsg" runat="server" Text="" Font-Size="Larger"></asp:Label></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
