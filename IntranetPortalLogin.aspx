<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IntranetPortalLogin.aspx.cs" Inherits="FujiTecIntranetPortal.IntranetPortalLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="stylesheet" href="~/assets/css/bootstrap.min.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="~/assets/css/line-awesome.min.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="~/assets/css/style.css" type="text/css" media="screen" />
    <%--<script src="jquery.js">
    </script>
    <script>
        function swapImages() {
            var $active = $('#myGallery .active');
            var $next = ($('#myGallery .active').next().length > 0) ? $('#myGallery .active').next() : $('#myGallery img:first');
            $active.fadeOut(function () {
                $active.removeClass('active');
                $next.fadeIn().addClass('active');
            });
        }

        $(document).ready(function () {
            // Run our swapImages() function every 5secs
            setInterval('swapImages()', 5000);
        }
    </script>--%>
    <style type="text/css">
        /*#myGallery {
            position: relative;
            width: 400px;*/ /* Set your image width */
        /*height: 300px;*/ /* Set your image height */
        /*}

            #myGallery img {
                display: none;
                position: absolute;
                top: 0;
                left: 0;
            }

                #myGallery img.active {
                    display: block;
                }*/

        .center {
            display: flex;
            justify-content: center;
            align-items: center;
        }

        .midd {
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
            background-color: white;
            color: white;
        }

        .pad1 {
            padding-top: 20px;
            padding-right: 60px;
            /*padding-bottom: 10px;*/
            padding-left: 85px;
        }

        .pad {
            /*display: ruby-base;*/
            justify-content: center;
            /*align-items: center;*/
            padding-top: 20px;
            padding-right: 20px;
            padding-bottom: 10px;
            padding-left: 500px !important;
        }

        p {
            background-color: #CF2331;
        }

        .hd {
            font-family: 'Brush Script MT', cursive !important;
            color: red;
            text-decoration: underline;
        }

        .auto-style3 {
            display: flex;
            justify-content: center;
            align-items: center;
            height: 35px;
        }

        .normal-table {
            font-size: 14px !important;
            border: 10px solid White !important;
            padding: 5px !important;
            margin-bottom: 10px !important;
        }

        .normal-tablebtm {
            font-size: 10px !important;
            /*border: 1px solid Black !important;*/
            padding: 5px !important;
            bottom: 5px !important;
            margin-bottom: 5px !important;
            /*width: 1000px !important;*/
            width: 100% !important;
            left: 550px !important;
            text-align: center;
        }

        .bottompane {
            border-collapse: collapse;
            background-color: white;
            position: fixed;
            left: 0;
            bottom: 25px;
            width: 100%;
            background-color: white;
            color: white;
            text-align: center;
            height: 10%;
        }

        .normal-table10 {
            font-size: 14px !important;
            border: 10px solid black !important;
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
            padding: 1px;
            !important;
            color: black;
            font-size: 12px;
            width: 100px;
            font-family: Verdana;
            font-weight: bold;
            /*font-size: large*/
            height: 40px;
        }

        .gridcss1 {
            color: black;
            font-family: Verdana;
            font-size: 12px;
            /*font-family: Italic;*/
            font-weight: bold;
            width: 250px;
            height: 40px;
            /* font-family:'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif*/
        }

        .clr {
            background-color: #EDF2EB;
            margin: 2px;
            border-left: 200px solid white;
        }

        /* .Outlayer {
            background-color: #EDF2EB;
            margin: 2px;
            border-left: 200px solid Black;
        }*/

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

        body, h1, h2, h3, h4, h5, h6 {
            font-family: verdana;
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

        /*  .middlepane {
            width: 25%;
            height: 100%;
            float: left;
            background-color: white;
            border-collapse: collapse;
        }*/

        .geek1 {
            font-size: 36px;
            font-weight: bold;
            color: white;
            text-align: center;
        }

        .rightpane {
            width: 30%;
            height: 100%;
            position: relative;
            float: right;
            background-color: white;
            border-collapse: collapse;
        }

        .toppane {
            width: 100%;
            height: 5px;
            border-collapse: collapse;
            background-color: lightgray;
        }

        .leftpane1 {
            right: 150px !important;
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
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:HiddenField ID="hdtimer" runat="server" />
        <header>
            <div class="container">
                <div class="row">
                    <div class="col-md-2 ">
                        <img src="../assets/images/FujitecLogo.jpg" height="36" width="130" />
                    </div>
                    <div class="col-md-8 text-center ">
                        <%--<h2 style="font-family:Verdana">CONFLUENCE</h2>--%>
                        <img src="../assets/images/1.png" height="40" width="250" />
                    </div>
                    <div class="col-md-2  text-right">
                        <img src="../assets/images/BigRiseLogo.jpg" height="35" width="120" />
                    </div>
                </div>
            </div>
        </header>
        <div class="container">
            <div class="toppane"></div>
            <div class="leftpane">
                <%-- <table class="normal-table midd" style="padding: 1px !important">
                    <tr>
                        <td>
                            <h4 class="center">News & Events:</h4>
                        </td>
                    </tr>
                    <tr>
                       <td>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="conditional">
                                <ContentTemplate>
                                    <asp:Image ID="Image3" runat="server" Style="width: 90%; height: 300px" ImageAlign="Right" />
                                    <asp:Timer ID="Timer2" runat="server" OnTick="Timer2_Tick" Interval="5000"></asp:Timer>
                                     <asp:Image ID="img_Empphoto" runat="server" Height="160px" Width="160px" ImageAlign="NotSet" />
                                 <asp:GridView ID="gvconverted" runat="server" OnRowDataBound="gvconverted_RowDataBound" GridLines="None">
                                            <RowStyle Font-Names="Calibri" Font-Size="9" />
                                        </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>--%>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="conditional">
                    <ContentTemplate>
                        <div>
                            <table>
                                <tr>
                                    <td style="width: 250px;">
                                        <h4 style="margin-top: 6px;">🅾🆄🆁 🅽🅴🆆 🅱🆄🅳🅳🅸🅴🆂</h4>
                                    </td>
                                    <td>
                                        <%--<h4 class="center" style="font-family:Verdana">News & Events:</h4>--%>
                                        <h4 style="margin-top: 12.5px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 🅽🅴🆆🆂 🅰🅽🅳 🅴🆅🅴🅽🆃🆂:</h4>
                                    </td>
                                </tr>
                                <tr>

                                    <td style="width: 100%; background-image: url('/assets/images/lg.png'); background-size: 750px; background-repeat: no-repeat; float: right; position: relative; height: 100%; margin-bottom: 10px !important; border: 1px solid black !important; border-radius: 2px 2px 2px 2px; background-color: #3366ff; margin-top: 10px">
                                        <%--<h4>🅾🆄🆁 🅽🅴🆆 🅱🆄🅳🅳🅸🅴🆂</h4>--%>
                                        <asp:Image ID="img_Empphoto" runat="server" Height="200px" Width="100%" ImageAlign="Right" />
                                        <asp:GridView ID="gvconverted" runat="server" OnRowDataBound="gvconverted_RowDataBound" GridLines="Vertical">
                                            <RowStyle Font-Names="Calibri" Font-Size="9" />
                                        </asp:GridView>
                                    </td>
                                    <td class="normal-table" style="margin-top: 10px;">
                                        <%-- <td class="normal-table" style="width: 100%; background-image: url('/assets/images/lg.png'); background-size: 750px; background-repeat: no-repeat; float: right; position: relative; height: 100%; margin-bottom: 10px !important; border: 1px solid black !important; border-radius: 5px 5px 5px 5px; background-color: #3366ff; margin-top: 10px">--%>
                                        <asp:Image ID="Image3" runat="server" Style="width: 595px; height: 360px" ImageAlign="Right" />
                                        <span>
                                            <asp:Label ID="lblImageEvent" runat="server" Text="" Font-Names="Verdana" ForeColor="Black"
                                                Font-Size="12px" Style="text-align: center;"
                                                Width="100%"></asp:Label></span>
                                        <asp:Timer ID="Timer2" runat="server" OnTick="Timer2_Tick"></asp:Timer>
                                    </td>
                                </tr>
                                <%--<tr>
                                    <td colspan="2">
                                        <h3 class="center">Our New assets</h3>
                                    </td>
                                </tr>--%>
                            </table>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <%-- <h3>News:</h3>
                <div class="cent" style="width: 50%; align-content: center">

                    <marquee direction="Up" height="100" width="100%" scrollamount="3" style="border: solid">
                        <div>
                            <strong>Many-many congratulations, you are among the very few people who made it to the firm. The entire team is looking forward to meeting you.
                            </strong>
                        </div>
                    </marquee>

                </div>--%>
            </div>
            <%--<div class="middlepane">
                <h3 class="text-center login">Portal </h3>
                <div style="display: flex; justify-content: space-around">
                    <div></div>

                    <div>
                        <table class="normal-table midd pad" style="background-image: url('/assets/images/lg.png'); background-size: cover; background-repeat: no-repeat; float: none; width: 100%; position: relative; height: 100%;">

                            <tr>
                                <td class="auto-style3"><strong><span class="hlt-txt"></span></strong></td>
                            </tr>
                            <tr>
                                <td class="auto-style3"><strong>User ID:  <span class="hlt-txt"></span></strong>
                                    <asp:TextBox runat="server" ID="txtUsername" placeholder="Enter User ID" CssClass="auto-style1" Width="200px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="auto-style3"><strong>Password:<span class="hlt-txt"></span></strong>
                                    <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" placeholder="Enter Password" CssClass="auto-style2" Width="200px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="auto-style3">
                                    <asp:Button runat="server" ID="btnLogin" Text="Login" Width="75px" Font-Bold="True" OnClick="btnLogin_Click" /></td>
                            </tr>
                            <tr>
                                <td class="center" colspan="4">
                                    <asp:Label runat="server" ID="Label1" Text="" Font-Bold="True" BackColor="White" Visible="false" /></td>
                            </tr>


                        </table>
                        <table>
                            <tr>
                                <td class="center" colspan="4">
                                    <asp:Label runat="server" ID="lblmsg" Text="" Font-Bold="True" BackColor="White" Font-Size="Small" /></td>
                            </tr>

                        </table>
                    </div>
                    <div></div>

                </div>--%>
            <%--<div>
                    <h3 class="center">Our New assets</h3>
                    <table class="normal-table2" style="width: 100%;">

                        <tr>
                            <td class="midd">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="conditional">
                                    <ContentTemplate>
                                        <asp:Image ID="img_Empphoto" runat="server" Height="160px" Width="160px" ImageAlign="NotSet" />
                                         </td>
                                            <td>
                                        <asp:GridView ID="gvconverted" runat="server" OnRowDataBound="gvconverted_RowDataBound" GridLines="None">
                                            <RowStyle Font-Names="Calibri" Font-Size="9" />
                                        </asp:GridView>
                                        <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick"></asp:Timer>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>

                    </table>
                </div>--%>
            <%-- <div id="myGallery" style="width: 100%;">
                    <table>
                        <tr>
                            <td style="width: 100%;">
                            <img src="~/assets/images/_RAJ9014.jpg" runat="server" class="active" style="width: 400px; height: 200px" /> 
                                </td>
                        </tr>
                    </table>
                </div>
        </div>--%>
            <div class="rightpane">
                <h3 style="color: white">LOGIN SCREEN</h3>

                <div class="center">
                    <table class="normal-table" style="background-image: url('/assets/images/lg.png'); background-size: cover; background-repeat: no-repeat; float: none; width: 50%; position: relative; height: 100%; margin-top: 12.5px;">

                        <tr>
                            <td class="auto-style3"><strong><span class="hlt-txt"></span></strong></td>
                        </tr>
                        <tr>
                            <td class="auto-style3"><strong>User ID:  <span class="hlt-txt"></span></strong>
                                <asp:TextBox runat="server" ID="txtUsername" placeholder="Enter User ID" CssClass="auto-style1" Width="170px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="auto-style3"><strong>Password:<span class="hlt-txt"></span></strong>
                                <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" placeholder="Enter Password" CssClass="auto-style2" Width="170px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="auto-style3">
                                <asp:Button runat="server" ID="btnLogin" Text="Login" Width="75px" Font-Bold="True" OnClick="btnLogin_Click" /></td>
                        </tr>
                        <tr>
                            <td class="center" colspan="4">
                                <asp:Label runat="server" ID="Label1" Text="" Font-Bold="True" BackColor="White" Visible="false" /></td>
                        </tr>
                    </table>

                </div>

                <%-- <div class="auto-style5">
                    <table>
                        <tr>
                            <td class="auto-style3"><strong><span class="hlt-txt"></span></strong></td>
                        </tr>
                        <tr>
                            <td class="auto-style4">
                                <asp:Image ID="Image2" runat="server" Height="120px" Width="110px" ImageUrl="~/assets/images/Tecky_L1.png" />
                            </td>
                        </tr>
                    </table>
                </div>--%>
                <table>
                    <tr>
                        <td class="center" colspan="4">
                            <asp:Label runat="server" ID="lblmsg" Text="" Font-Bold="True" BackColor="White" Font-Size="Small" /></td>
                    </tr>

                </table>
                 <table class="center" margin-left: 120px">
                        
                        <tr>
                            <td >
                                <asp:Image ID="Image2" runat="server"  Height="180px" Width="265px" ImageUrl="~/assets/images/Quote56.jpg" />
                            </td>
                        </tr>
                    </table>
                 <table class="center" margin-left: 140px">
                        
                        <tr>
                            <td >
                               <marquee > 
                                <strong>Form-16 is available in our Confluence portal - Home Screen. Please login and click the link to download.
                                </strong>  
                                 </marquee>
                            </td>
                        </tr>
                    </table>
            </div>
            <div class="bottom">
            <%--<div>--%>
                <%-- <table class="normal-table1 bottompane" style="width: 90%; margin-left: 60px">
                    <tr>
                        <td style="margin-left: 1px; align-content: flex-start; width: 90%;" colspan="8">
                            <h4 class="leftpane1">🅲🅾🅽🅽🅴🅲🆃🅸🅾🅽🆂</h4>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="Label4" Text="asdf" ForeColor="White"></asp:Label></td>
                        <td>
                            <asp:Label runat="server" ID="Label5" Text="asdf" ForeColor="White"></asp:Label></td>
                        <td>
                            <asp:Label runat="server" ID="Label6" Text="asdf" ForeColor="White"></asp:Label></td>

                    </tr>
                </table>--%>
                <table class="normal-table" style="width: 31%; margin-left: 120px">
                    <%-- <tr>
                        <td style="margin-left: 1px; align-content: flex-start; width: 90%;" colspan="8">
                            <h4 class="leftpane1">🅲🅾🅽🅽🅴🅲🆃🅸🅾🅽🆂</h4>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblbtn1" Text="asdf" ForeColor="White"></asp:Label></td>
                        <td>
                            <asp:Label runat="server" ID="Label2" Text="asdf" ForeColor="White"></asp:Label></td>
                        <td>
                            <asp:Label runat="server" ID="Label3" Text="asdf" ForeColor="White"></asp:Label></td>

                    </tr>--%>
                    <%--<tr >
                   
                         <th class="right" colspan="8" ><h4 >🅲🅾🅽🅽🅴🅲🆃🅸🅾🅽🆂  </h4></th>            
                     </tr>--%>
                    <tr>

                        <td>
                            <asp:ImageButton ID="ImgbtnERP" runat="server" Height="35px" ImageUrl="~/assets/images/erp_v.png" Width="65px" OnClick="ImgbtnERP_Click" ImageAlign="Left" />
                        </td>
                        <td>
                            <asp:Label runat="server" ID="Label4" Text="asdf" ForeColor="White"></asp:Label></td>
                        <td>
                            <asp:ImageButton ID="ImgbtnGoogleWorkspace" runat="server" Height="50px" ImageUrl="~/assets/images/GM.png" Width="50px" OnClick="ImgbtnGoogleWorkspace_Click" ImageAlign="Left" />
                        </td>
                         <td>
                            <asp:Label runat="server" ID="lblbtn1" Text="asdf" ForeColor="White"></asp:Label></td>
                        <td>
                            <asp:ImageButton ID="Imgbtnoffice365" runat="server" Height="50px" ImageUrl="~/assets/images/OFF1.png" Width="50px" OnClick="Imgbtnoffice365_Click" ImageAlign="Left" />
                        </td>
                         <td>
                            <asp:Label runat="server" ID="Label3" Text="asdf" ForeColor="White"></asp:Label></td>
                        <%-- </tr>
                    <tr>
                        <td class="auto-style3"><strong><span class="hlt-txt"></span></strong></td>
                    </tr>
                    <tr>--%>
                        <td>
                            <asp:ImageButton ID="ImgbtnCloud" runat="server" Height="60px" ImageUrl="content/image/Dms.png" Width="70px" OnClick="ImgbtnCloud_Click" ImageAlign="Left" />
                        </td>
                        <td>
                            <asp:Label runat="server" ID="Label5" Text="asdf" ForeColor="White"></asp:Label></td>
                        <%-- </tr>
                    <tr>--%>
                        <td>
                            <asp:ImageButton ID="ImgbtnHRM" runat="server" ImageUrl="~/Content/image/HRMANTRA_V.JPG" ImageAlign="Left" OnClick="ImgbtnHRM_Click" Height="25px" Width="80px" />
                        </td>
                        <td>
                            <asp:Label runat="server" ID="Label6" Text="asdf" ForeColor="White"></asp:Label></td>
                        <td>
                            <asp:ImageButton ID="ImgbtnITSupport" runat="server" Height="45px" ImageUrl="~/assets/images/ITSupport.png" Width="45px" OnClick="ImgbtnITSupport_Click" ImageAlign="Left" />
                        </td>
                        <%-- </tr>
                    <tr>
                        <td class="auto-style3"><strong><span class="hlt-txt"></span></strong></td>
                    </tr>
                    <tr>--%>
                         <td>
                            <asp:Label runat="server" ID="Label2" Text="asdf" ForeColor="White"></asp:Label></td>
                        <td>
                            <asp:ImageButton ID="ImgbtnFJP" runat="server" Height="30px" ImageUrl="~/assets/images/FJP.png" Width="50px" OnClick="ImgbtnFJP_Click" ImageAlign="Left" />
                        </td>
                         <td>
                            <asp:Label runat="server" ID="lblbtn3" Text="asdf" ForeColor="White"></asp:Label></td>
                        <td>
                            <asp:ImageButton ID="ImgbtnVendora" runat="server" Height="30px" Width="100px" ImageUrl="~/assets/images/Vend.png" ImageAlign="Left" OnClick="ImgbtnVendora_Click" />
                        </td>
                        <td>
                            <asp:Label runat="server" ID="Label7" Text="asdf" ForeColor="White"></asp:Label></td>
                        <%--<td>
                            <asp:ImageButton ID="ImageButton1" runat="server" Height="30px" Width="100px" ImageUrl="~/assets/images/Quiz2.jpg" ImageAlign="Left" PostBackUrl="http://www.google.com"  />
                        </td>--%>
                    </tr>
                </table>
            </div>
        </div>
       <%-- <marquee width="100%" height="50">This text will scroll from right to left</marquee>--%>
        <%-- <marquee > <div>
             <strong>Form-16 is available in our Confluence portal - Home Screen. Please login and click the link to download.
               </strong>  
        </div></marquee>--%>
        <div class="footer">
            <p style="text-align: left; color: white">
                <strong>Developed by Information Technology.    
    <span style="float: right; color: white">© 2022 Fujitec India Pvt,Ltd.
    </span>
                </strong>
            </p>
        </div>
    </form>
</body>
</html>
