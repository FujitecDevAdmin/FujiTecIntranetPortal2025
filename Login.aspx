<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="FujiTecIntranetPortal.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Fujitec Intranet Portal</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" href="~/assets/css/bootstrap.min.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="~/assets/css/line-awesome.min.css" type="text/css" media="screen" />

    <link rel="stylesheet" href="~/assets/css/style.css" type="text/css" media="screen" />
    <script type="text/javascript">
        function swapImages() {
            var $active = $('#myGallery .active');
            var $next = ($('#myGallery .active').next().length > 0) ? $('#myGallery .active').next() : $('#myGallery img:first');
            $active.fadeOut(function () {
                $active.removeClass('active');
                $next.fadeIn().addClass('active');
            });
        }
    </script>
    <style type="text/css">
        #myGallery {
            position: relative;
            width: 400px; /* Set your image width */
            height: 300px; /* Set your image height */
        }

            #myGallery img {
                display: none;
                position: absolute;
                top: 0;
                left: 0;
            }

                #myGallery img.active {
                    display: block;
                }

        .center {
            display: flex;
            justify-content: center;
            align-items: center;
        }

        .auto-style1 {
            margin-left: 20px;
        }

        .auto-style2 {
            margin-left: 8px;
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
            background-color: #CF2331;
        }

        p {
            background-color: orangered;
        }

        .auto-style3 {
            display: flex;
            justify-content: center;
            align-items: center;
            height: 35px;
        }

        .normal-table {
            font-size: 14px !important;
            border: 20px solid #EDF2EB !important;
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
            padding: 10px !important;
            margin-bottom: 20px !important;
        }

        .gridcss {
            padding: 2px !important;
            color: black;
            font-size: larger
        }

        .gridcss1 {
            color: black;
            font-size: larger
        }

        .auto-style4 {
            width: 194px;
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:HiddenField ID="hdtimer" runat="server" />
        <header>
            <div class="container">
                <div class="row">
                    <div class="col-md-2 govt-logo">
                        <img src="../assets/images/FujitecLogo.jpg" height="36" width="130" />
                    </div>
                    <div class="col-md-8 text-center nfai-text-logo">
                        <h2>Fujitec Intranet Portal</h2>
                    </div>
                    <div class="col-md-2 nfai-logo text-right">
                        <img src="../assets/images/BigRiseLogo.jpg" height="35" width="120" />
                    </div>
                </div>
            </div>
        </header>
        <hr />
        <div class="clr" style="width: 88%;">

            <div class="center">
                <%--<div class="login_container border_radius">--%>
                <div>
                    <table class="normal-table" style="background-image: url('/assets/images/lg.png'); background-size: cover; background-repeat: no-repeat; float: none; width: 100%; position: relative; height: 100%;">
                        <tr>
                            <td class="auto-style3"><strong><span class="hlt-txt"></span></strong>
                        </tr>
                        <tr>
                            <td class="auto-style3"><strong>User ID:  <span class="hlt-txt"></span></strong>
                                <asp:TextBox runat="server" ID="txtUsername" placeholder="Enter User ID" CssClass="auto-style1"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="auto-style3"><strong>Password:<span class="hlt-txt"></span></strong>
                                <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" placeholder="Enter Password" CssClass="auto-style2"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="auto-style3">
                                <asp:Button runat="server" ID="btnLogin" Text="Login" Width="75px" Font-Bold="True" OnClick="btnLogin_Click" /></td>
                        </tr>
                        <tr>
                            <td class="center" colspan="4">
                                <asp:Label runat="server" ID="lblmsg1" Text="" Font-Bold="True" BackColor="White" /></td>
                        </tr>
                    </table>
                </div>

            </div>
            <div>
                <table>
                    <tr>
                        <td class="center" colspan="4">
                            <asp:Label runat="server" ID="lblmsg" Text="" Font-Bold="True" />

                        </td>
                    </tr>
                </table>
            </div>
            <%-- <div class="center" >
                <table class="normal-table1" style="width: 60%;">
                    <tr>
                        <td style="margin: 753px 49px 753px 49px; border-collapse: separate; border-spacing: 2px;">
                            <asp:ImageButton ID="Imgbtnoffice365" runat="server" Height="50px" ImageUrl="~/Content/image/office365.png" Width="115px" OnClick="Imgbtnoffice365_Click" />
                        </td>
                        <td style="padding: inherit; margin: 753px 49px 753px 49px">
                            <asp:ImageButton ID="ImgbtnGoogleWorkspace" runat="server" Height="35px" ImageUrl="content/image/Workspace1.png" Width="150px" OnClick="ImgbtnGoogleWorkspace_Click" />
                        </td>
                        <td style="padding: inherit; margin: 753px 49px 753px 49px">
                            <asp:ImageButton ID="ImgbtnERP" runat="server" Height="40px" ImageUrl="content/image/ERP.png" Width="120px" OnClick="ImgbtnERP_Click" />
                        </td>
                        <td style="padding: inherit; margin: 753px 49px 753px 49px">
                            <asp:ImageButton ID="ImgbtnHRM" runat="server" ImageUrl="~/Content/image/HRMANTRA_V.JPG" ImageAlign="Left" OnClick="ImgbtnHRM_Click" />
                        </td>
                        <td></td>

                    </tr>
                    <tr>
                        <td style="padding: inherit; margin: 753px 49px 753px 49px">
                            <asp:ImageButton ID="ImgbtnCloud" runat="server" Height="60px" ImageUrl="content/image/Dms.png" Width="80px" OnClick="ImgbtnCloud_Click" />
                        </td>
                        <td style="padding: inherit; margin: 753px 49px 753px 49px">
                            <asp:ImageButton ID="ImgbtnITSupport" runat="server" Height="40px" ImageUrl="content/image/Itsupport.png" Width="138px" OnClick="ImgbtnITSupport_Click" />
                        </td>
                        <td style="padding: inherit; margin: 753px 49px 753px 49px">
                            <asp:ImageButton ID="ImgbtnVendora" runat="server" Height="40px" Width="120px" ImageUrl="~/Content/image/Vendora2.png" ImageAlign="Middle" OnClick="ImgbtnVendora_Click" />
                        </td>
                        <td style="padding: inherit; margin: 753px 49px 753px 49px">
                            <asp:ImageButton ID="ImgbtnFJP" runat="server" Height="50px" ImageUrl="~/Content/image/Intranet_J.png" Width="100px" ImageAlign="Middle" OnClick="ImgbtnFJP_Click" />
                        </td>
                    </tr>
                </table>
            </div>--%>
            <div>
                <table>
                    <tr>
                        <asp:Label runat="server" ID="Label1" Text="aSdfsa" Font-Bold="True" ForeColor="#EDF2EB" />
                    </tr>
                </table>
            </div>
            <%--<marquee scrollamount="10" behavior="alternate" onmouseover="stop()" onmouseout="start()">--%>
            <div class="center">
                <table class="normal-table2" style="width: 100%;">

                    <tr>
                        <td colspan="3">
                            <h4 class="text-center">New Joiners</h4>
                        </td>

                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="Label2" Text="a" Font-Bold="True" ForeColor="White" /></td>
                        <td class="auto-style4">
                            <asp:Image ID="img_Empphoto" runat="server" Height="200px" Width="174px" />
                        </td>
                        <td>
                            <asp:GridView ID="gvconverted" runat="server" OnRowDataBound="gvconverted_RowDataBound" GridLines="None">
                                <RowStyle Font-Names="Calibri" Font-Size="9" />
                            </asp:GridView>
                        </td>
                        <td>
                            <asp:TextBox ID="lstWelcomeMsg" runat="server" TextMode="MultiLine" Height="220px" Width="320px" Font-Bold="true" Font-Names="Calibri" Font-Size="11" BorderColor="#EDF2EB" Visible="true" BackColor="#EDF2EB" Enabled="false" ReadOnly="True"></asp:TextBox>

                        </td>
                    </tr>
                    <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick"></asp:Timer>
                </table>
            </div>
            <%--</marquee>--%>
            <hr />
        </div>
        <div class="footer">
            <p style="text-align: left; color: white">
                <strong>Developed by Information Technology.
    eveloped by Information Technology.
    <span style="float: right; color: white">© 2022 Fujitec India Pvt,Ltd.
    </span>
                </strong>
            </p>
        </div>
    </form>
</body>
</html>
