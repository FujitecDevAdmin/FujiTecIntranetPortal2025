<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IntranetPortalNewLogin.aspx.cs" Inherits="FujiTecIntranetPortal.IntranetPortalNewLogin2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.2/css/all.min.css" />


    <%-- Layout Style--%>
    <style>
        html {
            scroll-behavior: smooth;
        }

        * {
            box-sizing: border-box;
        }

        /* texture-background8*/
        /*texture-background10*/
        body {
            margin: 0 auto;
            padding: 16px 16px 16px 16px;
            font-family: Arial, sans-serif;
            background-color: #f8e0b3;
            overflow-x: hidden;
            background-image: url('../assets/images/RoundedIcons/texture-background8.jpg');
            background-repeat: repeat;
            background-size: cover;
            background-position: center;
            animation: fadeInBackground 2s ease-in-out;
            animation: moveImage 60s linear infinite;
        }


        @keyframes fadeInBackground {
            0% {
                opacity: 0;
            }

            100% {
                opacity: 1;
            }
        }

        @keyframes moveImage {
            0% {
                background-position: 0 0;
            }

            100% {
                background-position: -2000px 0;
            }
        }


        .header {
            height: 50px;
            background-color: #fff;
            color: #fff;
            display: flex;
            align-items: center;
            justify-content: space-between;
            padding: 0 20px;
            position: fixed;
            top: 0;
            left: 0;
            right: 0;
            width: 100%;
            z-index: 1;
            box-shadow: 0px 2px 5px rgba(0,0,0,0.1);
        }

            .header img {
                height: 60px;
            }

            .header .logo {
                height: 42px;
                margin-right: 15px;
            }

        .section1 {
            margin-top: 50px; /*header height as top margin*/
            scroll-margin-top: 66px; /*header height as scroll-margin-top*/
            height: calc(100vh - 82px);
            max-height: calc(100vh - 82px);
            display: flex;
            flex-direction: column;
            justify-content: space-between;
            align-items: stretch;
            flex-wrap: wrap;
            /*background-color: yellow;*/
        }

            .section1 .column1 {
                width: 20%;
                display: flex;
                flex-direction: column;
                flex-basis: 100%;
                /*background-color: #f0f0f0;*/
            }

                .section1 .column1 .row1 {
                    padding: 0px 8px 8px 0px;
                    height: 50%;
                    display: flex;
                    align-items: center;
                    justify-content: center;
                    /*background-color: yellow;*/
                }

                .section1 .column1 .row2 {
                    padding: 8px 8px 0px 0px;
                    height: 50%;
                    display: flex;
                    align-items: center;
                    justify-content: center;
                    /*background-color: orange;*/
                }

            .section1 .column2 {
                padding: 0px 0px 0px 8px;
                width: 80%;
                display: flex;
                flex-direction: column;
                flex-basis: 100%;
                align-items: center;
                justify-content: center;
                /*background-color: #e0e0e0;*/
            }

        @media screen and (max-width: 767px) {
            .section1 .column1 {
                width: 100%;
                order: 2;
            }

            .section1 .column2 {
                width: 100%;
                order: 1;
            }
        }

        .section2 {
            margin-top: 50px; /*header height as top margin*/
            scroll-margin-top: 66px; /*header height as scroll-margin-top*/
            height: calc(100vh - 82px);
            max-height: calc(100vh - 82px);
            display: flex;
            flex-direction: column;
            justify-content: space-between;
            align-items: stretch;
            flex-wrap: wrap;
        }

            .section2 .row1 {
                height: 75%;
                display: flex;
                flex-direction: column;
                justify-content: space-between;
                align-items: stretch;
                flex-wrap: wrap;
                /*background-color: teal;*/
            }

                .section2 .row1 .column1 {
                    padding: 0px 8px 8px 0px;
                    width: 65%;
                    height: 100%;
                    display: flex;
                    /*background-color: red;*/
                }

                .section2 .row1 .column2 {
                    padding: 0px 0px 8px 8px;
                    width: 35%;
                    height: 100%;
                    display: flex;
                    /*background-color: lime;*/
                }

            .section2 .row2 {
                height: 25%;
                display: flex;
                /*background-color: orange;*/
            }

                .section2 .row2 .column1 {
                    padding: 8px 8px 8px 0px;
                    width: 100%;
                    height: 100%;
                    display: flex;
                    align-items: center;
                    justify-content: center;
                    /*background-color: greenyellow;*/
                }

        @media screen and (max-width: 767px) {
            .section1 .column1, .section2 .column1 {
                display: flex;
                flex-direction: row;
                flex-wrap: wrap;
            }

            .section2 .column1 {
                width: 100%;
                order: 1;
            }

            .section2 .column2 {
                width: 100%;
                order: 1;
            }
        }
    </style>

    <%--buddy_slideshow-container--%>
    <style>
        /* Slideshow container */
        .buddy_slideshow-container {
            position: relative;
            width: 100%;
            height: 100%;
        }

        /* Slides */
        .buddy_mySlides {
            display: none;
            width: 100%;
            height: 100%;
        }

        /* Card */
        .buddy_card {
            position: relative;
            width: 100%;
            height: 100%;
            overflow: hidden;
            box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2);
            transition: 0.3s;
            border-radius: 12px;
        }

        /* Card image */
        .buddy_card-image img {
            width: 100%;
            height: 100%;
            object-fit: cover;
            align-content: center;
        }

        /* Card content */
        .buddy_card-content {
            position: absolute;
            bottom: 0;
            left: 0;
            width: 100%;
            padding: 10px;
            background-color: rgba(0, 0, 0, 0.5);
            color: #fff;
        }

        /* Card title */
        .buddy_card-title {
            margin: 0;
            font-size: 1.2rem;
            font-weight: bold;
        }

        /* Card text */
        .buddy_card-text {
            margin: 0;
            font-size: 0.8rem;
        }

        /* Previous and next buttons */

        .buddy_prev, .buddy_next {
            position: absolute;
            top: 50%;
            width: auto;
            margin-top: -22px;
            padding: 16px;
            color: black;
            font-weight: bold;
            font-size: 16px;
            transition: 0.6s ease;
            border-radius: 0 3px 3px 0;
            user-select: none;
        }


        /* Next button */
        .buddy_next {
            right: 0;
            border-radius: 3px 0 0 3px;
        }

            /* On hover, add a black background color with a little bit see-through */

            .buddy_prev:hover, .buddy_next:hover {
                background-color: rgba(0, 0, 0, 0.2);
            }

        /* Add a background color to the active dot */
        .active {
            background-color: #717171;
        }
    </style>

    <%--quote_slideshow-container--%>
    <style>
        /* Quote container */
        .quote_slideshow-container {
            margin: 50px auto;
            padding: 10px;
            position: relative;
            width: 100%;
            height: 100%;
            box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2);
            transition: 0.3s;
        }

        /* Card */
        .quote_card {
            position: absolute;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            overflow: hidden;
            border-radius: 12px;
            background-color: #f0f0f0;
        }
            /* Card Hover */
            .quote_card:hover {
                transform: translateY(-0px);
            }

        /* Card Image */
        .quote_card-img {
            width: 100%;
            height: 100%;
            object-fit: fill;
            align-content: center;
        }
    </style>

    <%--event_slideshow-container--%>
    <style>
        /* Slideshow container */
        .event_slideshow-container {
            position: relative;
            width: 100%;
            height: 100%;
        }
        /* Slides */
        .event_mySlides {
            display: none;
        }

        /* Previous and next buttons */
        .event_prev,
        .event_next {
            position: absolute;
            top: 50%;
            width: auto;
            margin-top: -22px;
            padding: 16px;
            color: black;
            font-weight: bold;
            font-size: 24px;
            transition: 0.6s ease;
            border-radius: 0 3px 3px 0;
            user-select: none;
        }

        /* Next buttons */
        .event_next {
            right: 0;
            border-radius: 3px 0 0 3px;
        }

            .event_prev:hover,
            .event_next:hover {
                background-color: rgba(0, 0, 0, 0.2);
            }
        /* Card */
        .event_card {
            position: relative;
            max-width: 100%;
            max-height: calc(100vh - 82px);
            overflow: hidden;
            box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2);
            transition: 0.3s;
            border-radius: 12px;
            margin: 0 auto;
        }

            .event_card:hover {
                box-shadow: 0 8px 16px 0 rgba(0, 0, 0, 0.2);
            }

        /* Card Image */
        .event_card-image {
            max-width: 100%; /* changed to fill the container */
            height: 100%; /* changed to fill the container */
            object-fit: contain;
            margin-bottom: 1rem;
            box-shadow: 0 2px 2px rgba(0, 0, 0, 0.3);
        }

            .event_card-image:hover {
                transform: scale(1);
            }

            .event_card-image img {
                height: 100%;
                width: 100%;
                object-fit: contain;
            }

        .event_card-content {
            position: absolute;
            bottom: 0;
            width: 100%;
            background-color: rgba(0, 0, 0, 0.7); /* optional background color for better contrast */
        }

            .event_card-content p {
                margin: 0;
                color: #fff;
                font-weight: bold;
                font-size: 14px;
                padding: 8px;
                text-align: center;
            }


        .event_fade {
            -webkit-animation-name: event_fade;
            -webkit-animation-duration: 1.5s;
            animation-name: event_fade;
            animation-duration: 1.5s;
        }

        @-webkit-keyframes event_fade {
            from {
                opacity: 0.4;
            }

            to {
                opacity: 1;
            }
        }

        @keyframes event_fade {
            from {
                opacity: 0.4;
            }

            to {
                opacity: 1;
            }
    </style>

    <%--quicklink-container --%>
    <style>
        .quicklink-container {
            width: 100%;
            height: 100%;
            max-width: 100%;
            max-height: 100%;
            margin: 0 auto;
            display: flex;
            justify-content: center;
            align-items: center;
            /*background-color: aqua;*/
        }

            /* Animate the quicklink container on scroll */
            .quicklink-container.show {
                opacity: 1;
                transform: translateY(0);
            }

        .quicklink-card {
            display: flex;
            flex-direction: column;
            align-items: center;
            width: 100%;
            height: 100%;
            padding: 8px;
            margin: 0.8rem;
            background-color: #fff;
            border: 1px solid #ddd;
            border-radius: 5%;
            transition: all 0.2s ease-in-out;
        }

            .quicklink-card:hover {
                transform: translateY(-15px);
                box-shadow: 0 5px 10px rgba(0, 0, 0, 0.2);
            }

            .quicklink-card img {
                max-width: 100%;
                height: auto;
            }

            .quicklink-card h3 {
                margin: 0;
                font-size: 1.5rem;
                text-align: center;
            }

            .quicklink-card p {
                margin: 0;
                font-size: 1rem;
                text-align: center;
            }
    </style>

    <%--itawareness_slideshow-container--%>
    <style>
        /* IT Awareness container */
        .itawareness_slideshow-container {
            position: relative;
            width: 100%;
            height: 100%;
            margin: 5px,5px,5px,5px;
        }
        /* Slides */
        .itawareness_mySlides {
            display: none;
            width: auto;
            height: 100%;
        }

        /* Previous and next buttons */
        .itawareness_prev,
        .itawareness_next {
            position: absolute;
            top: 50%;
            width: auto;
            margin-top: -22px;
            padding: 16px;
            color: black;
            font-weight: bold;
            font-size: 20px;
            transition: 0.6s ease;
            border-radius: 0 3px 3px 0;
            user-select: none;
        }

        /* Next buttons */
        .itawareness_next {
            right: 0;
            border-radius: 3px 0 0 3px;
        }

            .itawareness_prev:hover,
            .itawareness_next:hover {
                background-color: rgba(0, 0, 0, 0.2);
            }
        /* Card */
        .itawareness_card {
            position: relative;
            width: 100%;
            height: 100%;
            max-width: 100%;
            max-height: 100%;
            overflow: hidden;
            box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2);
            transition: 0.3s;
            margin: 0 auto;
            border-radius: 12px;
        }

            .itawareness_card:hover {
                box-shadow: 0 8px 16px 0 rgba(0, 0, 0, 0.2);
            }

        /* Card Image */
        .itawareness_card-image {
            max-width: 100%; /* changed to fill the container */
            height: 100%; /* changed to fill the container */
            object-fit: fill;
            margin-bottom: 1rem;
            box-shadow: 0 2px 2px rgba(0, 0, 0, 0.3);
        }

            .itawareness_card-image:hover {
                transform: scale(1);
            }

            .itawareness_card-image img {
                height: 100%;
                width: 100%;
                object-fit: fill;
            }

        .itawareness_card-content {
            display: none;
            position: absolute;
            bottom: 0;
            width: 100%;
            background-color: rgba(0, 0, 0, 0.7); /* optional background color for better contrast */
        }

            .itawareness_card-content p {
                margin: 0;
                color: #fff;
                font-weight: bold;
                font-size: 14px;
                padding: 8px;
                text-align: center;
            }


        .itawareness_fade {
            -webkit-animation-name: itawareness_fade;
            -webkit-animation-duration: 1.5s;
            animation-name: itawareness_fade;
            animation-duration: 1.5s;
        }

        @-webkit-keyframes itawareness_fade {
            from {
                opacity: 0.4;
            }

            to {
                opacity: 1;
            }
        }

        @keyframes itawareness_fade {
            from {
                opacity: 0.4;
            }

            to {
                opacity: 1;
            }
    </style>

    <%--login-container--%>
    <style>
        .login-container {
            display: flex;
            flex-direction: column;
            justify-content: center;
            align-items: center;
            width: 100%;
            height: 100%;
            margin-bottom: 20px;
            /*       background-color: #fff;*/
            border-radius: 16px;
            border: 3px solid #fff;
        }

        .username-icon, .password-icon {
            width: 24px;
            height: 24px;
            background-size: cover;
            background-repeat: no-repeat;
            background-position: center;
        }

        .input-container {
            display: flex;
            align-items: center;
            width: 80%;
            margin-bottom: 16px;
            border-bottom: 1px solid #ccc;
            padding-bottom: 8px;
        }

            .input-container input {
                width: 100%;
                flex: 1;
                border: none;
                margin-left: 10px;
                font-size: 16px;
                padding: 12px;
                border-radius: 5px;
            }

        .button[type="submit"] {
            width: 80%;
            background-color: #007bff;
            color: #fff;
            border: none;
            border-radius: 5px;
            padding: 15px 20px;
            margin-bottom: 16px;
            font-size: 16px;
            cursor: pointer;
            transition: background-color 0.3s;
        }

            .button[type="submit"]:hover {
                background-color: #0069d9;
            }
    </style>

    <style>
        .scroll-button {
            display: block;
            position: fixed;
            bottom: 60px;
            right: -100px;
            z-index: 99;
            font-size: 15px;
            border: none;
            outline: none;
            background-color: red;
            color: white;
            cursor: pointer;
            padding: 25px;
            border-radius: 15px 0px 0px 15px;
            width: 125px;
            padding-left: 50px;
            padding-top: 12px;
            padding-bottom: 12px;
            text-decoration: none;
        }

            .scroll-button:hover {
                right: 0px;
            }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <body>
            <header class="header">
                <img src="../assets/images/fujitec_logo.png" alt="Company Logo" class="logo">
                <img src="../assets/images/1.png" class="logo" />
                <img src="../assets/images/BigRiseLogo.jpg" class="logo" />
            </header>

            <section id="section1" class="section1">
                <div class="column1">
                    <div class="row1">
                        <div class="buddy_slideshow-container">
                            <asp:Repeater ID="rptEmployeeSlideShow" runat="server">
                                <ItemTemplate>
                                    <div class="buddy_mySlides">
                                        <div class="buddy_card">
                                            <div class="buddy_card-image">
                                                <img src="<%# Eval("Emp_Photo") %>" alt="<%# Eval("Emp_Photo") %>">
                                            </div>
                                            <div class="buddy_card-content">
                                                <h5 class="buddy_card-title"><%# Eval("Emp_name") %></h5>
                                                <h6 class="buddy_card-text"><%# Eval("Department") %></h6>
                                                <h6 class="buddy_card-text"><%# Eval("Designation") %></h6>
                                                <h6 class="buddy_card-text"><%# Eval("location") %></h6>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <a class="buddy_prev" onclick="buddy_plusSlides(-1)">&#10094;</a>
                            <a class="buddy_next" onclick="buddy_plusSlides(1)">&#10095;</a>
                        </div>
                    </div>
                    <div class="row2">
                        <div class="quote_slideshow-container">
                            <div class="quote_mySlides">
                                <div class="quote_card">
                                    <img class="quote_card-img" src="/assets/images/QT12.jpg" alt="Quote">
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
                <div class="column2">
                    <div class="event_slideshow-container">
                        <asp:Repeater ID="rptEventsSlideShow" runat="server">
                            <ItemTemplate>
                                <div class="event_mySlides">
                                    <%--          <h4>News and Events</h4>--%>
                                    <div class="event_card">
                                        <div class="event_card-image">
                                            <img src="<%# Eval("FileNamepath") %>" alt="<%# Eval("ImageFileName") %>">
                                        </div>
                                        <div class="event_card-content">
                                            <p><%# Eval("ImageFileName") %></p>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <a class="event_prev" onclick="event_plusSlides(-1)">&#10094;</a>
                        <a class="event_next" onclick="event_plusSlides(1)">&#10095;</a>
                    </div>

                    <a href="#section2" id="gotoSec2" class="scroll-button">Goto Login</a>
                </div>
            </section>

            <section id="section2" class="section2">

                <div class="row1">
                    <div class="column1">

                        <div class="itawareness_slideshow-container">
                            <asp:Repeater ID="rptItAwarenessSlideShow" runat="server">
                                <ItemTemplate>
                                    <div class="itawareness_mySlides">
                                        <div class="itawareness_card">
                                            <div class="itawareness_card-image">
                                                <img src="<%# Eval("ItAwarenessImagePath") %>" alt="<%# Eval("ItAwarenessImageTitle") %>">
                                            </div>
                                            <div class="itawareness_card-content">
                                                <p><%# Eval("ItAwarenessImageTitle") %></p>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <a class="itawareness_prev" onclick="itawareness_plusSlides(-1)">&#10094;</a>
                            <a class="itawareness_next" onclick="itawareness_plusSlides(1)">&#10095;</a>
                        </div>

                    </div>

                    <div class="column2">

                        <div class="login-container">
                            <div class="user-icon">
                                <%--        <img src="../assets/images/RoundedIcons/Login_User2.png" class="logo" />--%>
                            </div>
                            <h1>Login to Confluence</h1>

                            <div class="input-container">
                                <div><i class="fa fa-user icon"></i></div>
                                <asp:TextBox runat="server" ID="txtUsername" placeholder="Enter your User ID" />
                            </div>
                            <div class="input-container">
                                <div><i class="fa fa-lock icon"></i></div>
                                <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" placeholder="Enter your Password" />
                            </div>
                            <asp:Button CssClass="button" runat="server" ID="btnLogin" Text="Login" Font-Bold="True" OnClick="btnLogin_Click" />
                            <div>
                                <asp:Label runat="server" ID="lblmsg" Text="" Font-Bold="True" BackColor="White" Font-Size="Small" />
                            </div>

                        </div>
                    </div>
                </div>

                <div class="row2">
                    <div class="column1">
                        <div class="quicklink-container">
                            <a title="Click to Open ERP" href="http://fujitecerp/" target="_blank" class="quicklink-card">
                                <img id="quicklink1" src="../assets/images/RoundedIcons/ERP.png" alt="ERP">
                            </a>

                            <a title="Click to Open Email" href="https://sites.google.com/a/jp.fujitec.com/googleapps-fujitec-global-site/?pli=1&authuser=0" target="_blank" class="quicklink-card">
                                <img id="quicklink2" src="../assets/images/RoundedIcons/Email.png" alt="Email">
                            </a>

                            <a title="Click to Open Outlook" href="https://www.office.com/" target="_blank" class="quicklink-card">
                                <img id="quicklink3" src="../assets/images/RoundedIcons/Outlook.png" alt="Outlook">
                            </a>

                            <a title="Click to Open DMS" href="https://app.cloudedi.io/#/Login/" target="_blank" class="quicklink-card">
                                <img id="quicklink4" src="../assets/images/RoundedIcons/DMS.png" alt="DMS">
                            </a>

                            <a title="Click to Open HR Mantra" href="https://hrmantra.com/HRMGlobal/" target="_blank" class="quicklink-card">
                                <img id="quicklink5" src="../assets/images/RoundedIcons/HRMantra.png" alt="HRMantra">
                            </a>

                            <a title="Click to FJP Intranet" href="http://home.intra.fujitec.com/" target="_blank" class="quicklink-card">
                                <img id="quicklink6" src="../assets/images/RoundedIcons/FJPN.png" alt="FJPN">
                            </a>

                            <a title="Click to Open ePlan Tool" href="https://e-plan.fujitecindia.com/user/login/" target="_blank" class="quicklink-card">
                                <img id="quicklink7"  src="../assets/images/RoundedIcons/ePlan.png" alt="ePlan">
                            </a>

                            <a title="Click to Open Vendor Portal" href=" https://scm.fujitecindia.com/buyer/users/login" target="_blank" class="quicklink-card">
                                <img id="quicklink8"  src="../assets/images/RoundedIcons/Vendor.png" alt="Vendor">
                            </a>                           

                            <a title="Click to Open IT Support" href="http://itsupport.fujitecindia.com:8080/" target="_blank" class="quicklink-card">
                                <img id="quicklink9"  src="../assets/images/RoundedIcons/ITSupport.png" alt="ITSupport">

                             <a title="Click to Open Meeting Room Booking" href=" http://10.26.1.12:8086/Login.aspx" target="_blank" class="quicklink-card">
                                <img id="quicklink10"  src="../assets/images/RoundedIcons/meetingroombooking.png" alt="MRB">
                            </a>
                            </a>
                        </div>
                    </div>
                </div>

                <a href="#section1" id="gotoSec1" class="scroll-button" style="display: none">Goto Home</a>
            </section>


        </body>


        <%--buddy_showSlides--%>
        <script>
            var buddy_slideIndex = 1;
            var buddy_slides = document.getElementsByClassName("buddy_mySlides");
            var buddy_timer = null;
            var buddy_isPlaying = true;

            buddy_showSlides(buddy_slideIndex);

            // Next/previous controls
            function buddy_plusSlides(n) {
                buddy_showSlides(buddy_slideIndex += n);
                buddy_resetTimer();
            }

            // Thumbnail image controls
            function buddy_currentSlide(n) {
                buddy_showSlides(buddy_slideIndex = n);
                buddy_resetTimer();
            }

            // Automatic slideshow timer
            function buddy_startTimer() {
                buddy_timer = setInterval(function () {
                    if (buddy_isPlaying) {
                        buddy_plusSlides(1);
                    }
                }, 5000); // Change slide every 5 seconds
            }

            function buddy_resetTimer() {
                if (buddy_timer) {
                    clearInterval(buddy_timer);
                }
                if (buddy_isPlaying) {
                    buddy_startTimer();
                }
            }

            function buddy_showSlides(n) {
                if (n > buddy_slides.length) {
                    buddy_slideIndex = 1;
                }
                if (n < 1) {
                    buddy_slideIndex = buddy_slides.length;
                }
                for (var i = 0; i < buddy_slides.length; i++) {
                    buddy_slides[i].style.display = "none";
                }
                buddy_slides[buddy_slideIndex - 1].style.display = "block";
            }

            buddy_startTimer(); // Start the slideshow timer
        </script>

        <%--event_showSlides--%>
        <script>
            var event_slideIndex = 1;
            var event_slides = document.getElementsByClassName("event_mySlides");
            var event_timer = null;
            var event_isPlaying = true;

            event_showSlides(event_slideIndex);

            // Next/previous controls
            function event_plusSlides(n) {
                event_showSlides(event_slideIndex += n);
                event_resetTimer();
            }

            // Thumbnail image controls
            function event_currentSlide(n) {
                event_showSlides(event_slideIndex = n);
                event_resetTimer();
            }

            // Automatic slideshow timer
            function event_startTimer() {
                event_timer = setInterval(function () {
                    if (event_isPlaying) {
                        event_plusSlides(1);
                    }
                }, 5000); // Change slide every 5 seconds
            }

            function event_resetTimer() {
                if (event_timer) {
                    clearInterval(event_timer);
                }
                if (event_isPlaying) {
                    event_startTimer();
                }
            }

            function event_showSlides(n) {
                if (n > event_slides.length) {
                    event_slideIndex = 1;
                }
                if (n < 1) {
                    event_slideIndex = event_slides.length;
                }
                for (var i = 0; i < event_slides.length; i++) {
                    event_slides[i].style.display = "none";
                }
                event_slides[event_slideIndex - 1].style.display = "block";
            }

            event_startTimer(); // Start the slideshow timer
        </script>

        <%--quicklink-container--%>
        <script>
            function animateQuicklinkContainer() {
                const quicklinkContainer = document.querySelector(".quicklink-container");
                const containerPosition = quicklinkContainer.getBoundingClientRect().top;
                const screenHeight = window.innerHeight;

                if (containerPosition < screenHeight) {
                    quicklinkContainer.classList.add("show");
                }
            }

            function handleSectionChange() {
                const sections = document.querySelectorAll("section");

                sections.forEach((section) => {
                    const sectionPosition = section.getBoundingClientRect().top;
                    const screenHeight = window.innerHeight;

                    if (sectionPosition < screenHeight) {
                        section.classList.add("in-view");
                    } else {
                        section.classList.remove("in-view");
                    }
                });


            }

            window.addEventListener("scroll", () => {
                animateQuicklinkContainer();
                handleSectionChange();
            });

        </script>


        <%--itawareness_showSlides--%>
        <script>
            var itawareness_slideIndex = 1;
            var itawareness_slides = document.getElementsByClassName("itawareness_mySlides");
            var itawareness_timer = null;
            var itawareness_isPlaying = true;

            itawareness_showSlides(itawareness_slideIndex);

            // Next/previous controls
            function itawareness_plusSlides(n) {
                itawareness_showSlides(itawareness_slideIndex += n);
                itawareness_resetTimer();
            }

            // Thumbnail image controls
            function itawareness_currentSlide(n) {
                itawareness_showSlides(itawareness_slideIndex = n);
                itawareness_resetTimer();
            }

            // Automatic slideshow timer
            function itawareness_startTimer() {
                itawareness_timer = setInterval(function () {
                    if (itawareness_isPlaying) {
                        itawareness_plusSlides(1);
                    }
                }, 5000); // Change slide every 3 seconds
            }

            function itawareness_resetTimer() {
                if (itawareness_timer) {
                    clearInterval(itawareness_timer);
                }
                if (itawareness_isPlaying) {
                    itawareness_startTimer();
                }
            }

            function itawareness_showSlides(n) {
                if (n > itawareness_slides.length) {
                    itawareness_slideIndex = 1;
                }
                if (n < 1) {
                    itawareness_slideIndex = itawareness_slides.length;
                }
                for (var i = 0; i < itawareness_slides.length; i++) {
                    itawareness_slides[i].style.display = "none";
                }
                itawareness_slides[itawareness_slideIndex - 1].style.display = "block";
            }

            itawareness_startTimer(); // Start the slideshow timer
        </script>


        <%--section navigate--%>
        <script>
            // Get the button
            let gotoSection1 = document.getElementById("gotoSec1");
            let gotoSection2 = document.getElementById("gotoSec2");

            // When the user scrolls down 20px from the top of the document, show the button
            window.onscroll = function () { scrollFunction() };

            function scrollFunction() {
                if (document.body.scrollTop > 65 || document.documentElement.scrollTop > 65) {
                    gotoSection1.style.display = "block";
                    gotoSection2.style.display = "none";
                } else {
                    gotoSection1.style.display = "none";
                    gotoSection2.style.display = "block";
                }
            }

        </script>

<%--disable drag and mouse click events for all img tags on an ASPX page --%>
        <script type="text/javascript">
		
				document.addEventListener('contextmenu', function(e) {
				e.preventDefault();
				});

            window.onload = function () {
                var images = document.getElementsByTagName("img");

                for (var i = 0; i < images.length; i++) {
                    var image = images[i];

                    // Disable drag event
                    image.ondragstart = function () { return false; };
					
					// Ignore images with ID containing "quicklink"
					if (image.id.includes("quicklink")) {
						continue;
					}
			

                    // Disable right-click context menu
                    image.oncontextmenu = function (e) {
                        e.preventDefault();
                    };

                    // Disable left-click event
                    image.onclick = function () { return false; };
                }
            };
        </script>
    </form>
</body>
</html>
